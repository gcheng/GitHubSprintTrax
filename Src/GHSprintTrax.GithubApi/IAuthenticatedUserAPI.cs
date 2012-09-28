using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHSprintTrax.GithubApi
{
    public interface IAuthenticatedUserAPI
    {
        User GetInfo();
    }
}
