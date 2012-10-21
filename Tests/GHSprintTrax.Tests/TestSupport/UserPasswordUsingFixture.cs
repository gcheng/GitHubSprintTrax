using System;
using Xunit.Extensions;

namespace GHSprintTrax.Tests.TestSupport
{
    public class UserPasswordUsingFixture : TestClass
    {
        private const string UserNameVariable = "GHSPRINTTRAX_USER";
        private const string PasswordVariable = "GHSPRINTTRAX_PASSWORD";

        protected string Username
        {
            get { return Environment.GetEnvironmentVariable(UserNameVariable); }
        }

        protected string Password
        {
            get { return Environment.GetEnvironmentVariable(PasswordVariable); }
        }
    }
}