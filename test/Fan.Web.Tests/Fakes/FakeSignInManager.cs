﻿using Fan.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System.Threading.Tasks;

namespace Fan.Web.Tests.Fakes
{
    public class FakeSignInManager : SignInManager<User>
    {
        public FakeSignInManager(IHttpContextAccessor contextAccessor)
            : base(new FakeUserManager(),
                  contextAccessor,
                  new Mock<IUserClaimsPrincipalFactory<User>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<ILogger<SignInManager<User>>>().Object,
                  new Mock<IAuthenticationSchemeProvider>().Object)
        {
        }

        public override Task SignInAsync(User user, bool isPersistent, string authenticationMethod = null)
        {
            return Task.FromResult(0);
        }

        public override Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return Task.FromResult(SignInResult.Success);
        }

        public override Task SignOutAsync()
        {
            return Task.FromResult(0);
        }

        public override Task<bool> CanSignInAsync(User user)
        {
            return Task.FromResult(true);
        }
    }
}
