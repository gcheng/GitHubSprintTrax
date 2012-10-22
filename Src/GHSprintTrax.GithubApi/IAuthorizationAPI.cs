using System.Collections.Generic;

namespace GHSprintTrax.GithubApi
{
    public interface IAuthorizationAPI
    {
        Authorization CreateAuthorization(string note = null, string noteUri = null, IEnumerable<string> scopes = null);
        Authorization GetAuthorization(int authId);
        IEnumerable<Authorization> GetAuthorizations();
        void DeleteAuthorization(int id);
        void DeleteAuthorization(Authorization authorization);
    }
}