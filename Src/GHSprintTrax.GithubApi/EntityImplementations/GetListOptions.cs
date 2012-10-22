using System.Collections.Specialized;

namespace GHSprintTrax.GithubApi.EntityImplementations
{
    /// <summary>
    /// base class used for setting options to be passed
    /// as query parameters.
    /// </summary>
    public class GetListOptions
    {
        protected readonly NameValueCollection Parameters = new NameValueCollection();

        public virtual  NameValueCollection GetParameters()
        {
            return Parameters;
        }
    }
}
