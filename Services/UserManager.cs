using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http.Extensions;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Evoflare.API.Auth.Identity;
using Evoflare.API.Auth.Models;
using Evoflare.API.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Web;

namespace Evoflare.API.Services
{
    public class UserManager : UserManager<ApplicationUser>, IUserManager
    {
        private readonly IEmailSender emailSender;
        private readonly IHttpContextAccessor httpContextAccessor;

        public UserManager(IUserStore<ApplicationUser> store,
                           IOptions<IdentityOptions> optionsAccessor,
                           IPasswordHasher<ApplicationUser> passwordHasher,
                           IEnumerable<IUserValidator<ApplicationUser>> userValidators,
                           IEnumerable<IPasswordValidator<ApplicationUser>> passwordValidators,
                           ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors,
                           IServiceProvider services,
                           ILogger<UserManager<ApplicationUser>> logger,
                           IEmailSender emailSender,
                           IHttpContextAccessor httpContextAccessor
                           ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
            this.emailSender = emailSender;
            this.httpContextAccessor = httpContextAccessor;
        }

        public ClaimsPrincipal User => httpContextAccessor.HttpContext.User;

        public async Task<string> SendEmailConfirmationMessage(ApplicationUser user)
        {
            var code = await this.GenerateEmailConfirmationTokenAsync(user);
            var url = httpContextAccessor.HttpContext.Request.GetBaseUri();
            var callbackUrl = $"{url}api/account/confirmemail?id={user.Id}&code={HttpUtility.UrlEncode(code)}";
            var messageHtml = $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.";
            await emailSender.SendEmailAsync(user.Email, "Confirm your email", messageHtml);
            return callbackUrl;
        }

    }
}