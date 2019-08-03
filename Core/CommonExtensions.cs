using System;
using Microsoft.AspNetCore.Http;

namespace Evoflare.API.Core
{
    public static class CommonExtensions
    {
        public static Uri GetBaseUri(this HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port.GetValueOrDefault(80)
            };
            return uriBuilder.Uri;
        }
    }
}