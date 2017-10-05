﻿using Fan.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Fan.Web.UrlRewrite
{
    public class HttpWwwRewriteMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _settings;
        private ILogger<HttpWwwRewriteMiddleware> _logger;

        public HttpWwwRewriteMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = loggerFactory.CreateLogger<HttpWwwRewriteMiddleware>();
        }

        public Task Invoke(HttpContext context, IHttpWwwRewriter helper)
        {
            var settings = context.RequestServices.GetService<IOptionsSnapshot<AppSettings>>().Value;

            _logger.LogDebug("AppSettings:PreferredDomain {@PreferredDomain}", settings.PreferredDomain);
            _logger.LogDebug("AppSettings:UseHttps {@UseHttps}", settings.UseHttps);

            if (helper.ShouldRewrite(settings, context.Request.GetDisplayUrl(), out string url))
            {
                _logger.LogInformation("RewriteUrl: {@RewriteUrl}", url);

                context.Response.Headers[HeaderNames.Location] = url;
                context.Response.StatusCode = 301;
                context.Response.Redirect(url);
                return Task.CompletedTask;
            }

            return _next(context);
        }
    }
}
