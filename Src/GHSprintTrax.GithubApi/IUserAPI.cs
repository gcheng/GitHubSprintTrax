using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHSprintTrax.GithubApi
{
    /// <summary>
    /// Methods for retrieving and manipulating user information.
    /// </summary>
    public interface IUserAPI
    {
        User GetAuthenticatedUser();

    }
}
