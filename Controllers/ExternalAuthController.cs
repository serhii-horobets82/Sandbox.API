﻿using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Evoflare.API.Auth;
using Evoflare.API.Auth.Models;
using Evoflare.API.Configuration;
using Evoflare.API.Helpers;
using Evoflare.API.Models;
using Evoflare.API.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ExternalAuthController : Controller
    {
        private static readonly HttpClient Client = new HttpClient();
        private readonly EvoflareDbContext appDbContext;
        private readonly FacebookAuthSettings fbAuthSettings;
        private readonly GithubAuthSettings githubAuthSettings;
        private readonly IJwtFactory jwtFactory;
        private readonly JwtIssuerOptions jwtOptions;
        public readonly UserManager<ApplicationUser> userManager;

        public ExternalAuthController(
            IOptions<FacebookAuthSettings> fbAuthSettingsAccessor,
            IOptions<GithubAuthSettings> githubAuthSettingsAccessor,
            UserManager<ApplicationUser> userManager,
            EvoflareDbContext appDbContext,
            IJwtFactory jwtFactory,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            fbAuthSettings = fbAuthSettingsAccessor.Value;
            githubAuthSettings = githubAuthSettingsAccessor.Value;
            this.userManager = userManager;
            this.appDbContext = appDbContext;
            this.jwtFactory = jwtFactory;
            this.jwtOptions = jwtOptions.Value;
        }

        // POST api/externalauth/facebook
        [HttpPost]
        public async Task<IActionResult> Facebook([FromBody] OAuthViewModel model)
        {
            var appAccessTokenResponse = await Client.GetStringAsync(
                $"https://graph.facebook.com/v2.4/oauth/access_token?client_id={fbAuthSettings.AppId}&client_secret={fbAuthSettings.AppSecret}&code={model.Code}&redirect_uri={model.RedirectUri}");
            var appAccessToken = JsonConvert.DeserializeObject<OAuthAccessToken>(appAccessTokenResponse);

            var userInfoResponse = await Client.GetStringAsync(
                $"https://graph.facebook.com/v2.8/me?fields=id,email,first_name,last_name,name,gender,locale,birthday,picture&access_token={appAccessToken.AccessToken}");
            var userInfo = JsonConvert.DeserializeObject<FacebookUserData>(userInfoResponse);

            var user = await userManager.FindByEmailAsync(userInfo.Email);

            if (user == null)
            {
                var appUser = new ApplicationUser
                {
                    FirstName = userInfo.FirstName,
                    LastName = userInfo.LastName,
                    FacebookId = userInfo.Id,
                    Email = userInfo.Email,
                    UserName = userInfo.Email
                };

                var result = await userManager.CreateAsync(appUser,
                    Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                if (!result.Succeeded)
                    return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

                await appDbContext.Profile.AddAsync(new UserProfile
                {
                    IdentityId = appUser.Id,
                    PictureUrl = userInfo.Picture.Data.Url,
                    Location = "",
                    Locale = userInfo.Locale
                });
                await appDbContext.SaveChangesAsync();
            }

            user = await userManager.FindByNameAsync(userInfo.Email);

            if (user != null)
            {
                // get all user roles
                var userRoles = await userManager.GetRolesAsync(user);
                var userClaims = await userManager.GetClaimsAsync(user);
                var authToken = await jwtFactory.GenerateAuthToken(user, userRoles, userClaims);
                return new OkObjectResult(authToken);
            }
            return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
        }

        [HttpPost]
        public async Task<IActionResult> Github([FromBody] OAuthViewModel model)
        {
            Client.DefaultRequestHeaders.Add("Accept", "application/json");
            var appAccessTokenResponse = await Client.GetStringAsync(
                $"https://github.com/login/oauth/access_token?client_id={githubAuthSettings.AppId}&client_secret={githubAuthSettings.AppSecret}&code={model.Code}&redirect_uri={model.RedirectUri}");
            var appAccessToken = JsonConvert.DeserializeObject<OAuthAccessToken>(appAccessTokenResponse);

            try
            {
                Client.DefaultRequestHeaders.Add("User-Agent", "github");
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("token", appAccessToken.AccessToken);

                var userInfoResponse = await Client.GetStringAsync($"https://api.github.com/user?access_token={appAccessToken.AccessToken}");
                var userInfo = JsonConvert.DeserializeObject<GithubUserData>(userInfoResponse);
                var user = await userManager.FindByEmailAsync(userInfo.Email);

                if (user == null)
                {
                    var appUser = new ApplicationUser
                    {
                        LastName = userInfo.Name,
                        Email = userInfo.Email,
                        UserName = userInfo.Email
                    };

                    var result = await userManager.CreateAsync(appUser,
                        Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 8));

                    if (!result.Succeeded)
                        return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

                    await appDbContext.Profile.AddAsync(new UserProfile
                    {
                        IdentityId = appUser.Id,
                        PictureUrl = userInfo.AvatarUrl,
                        Location = userInfo.Location,
                    });
                    await appDbContext.SaveChangesAsync();
                }

                user = await userManager.FindByNameAsync(userInfo.Email);

                if (user != null)
                {
                    // get all user roles
                    var userRoles = await userManager.GetRolesAsync(user);
                    var userClaims = await userManager.GetClaimsAsync(user);
                    var authToken = await jwtFactory.GenerateAuthToken(user, userRoles, userClaims);
                    return new OkObjectResult(authToken);
                }
                return BadRequest(Errors.AddErrorToModelState("login_failure", "Failed to create local user account.", ModelState));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}