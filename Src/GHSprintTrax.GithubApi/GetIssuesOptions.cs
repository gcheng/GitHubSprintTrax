using GHSprintTrax.GithubApi.EntityImplementations;

namespace GHSprintTrax.GithubApi
{
    public class GetIssuesOptions : GetListOptions
    {
        public Milestone Milestone
        {
            set { Parameters["milestone"] = value.Number.ToString(); }
        }

        public IssueState State
        {
            set
            {
                switch(value)
                {
                    case IssueState.None:
                        Parameters.Remove("state");
                        break;

                    case IssueState.Open:
                        Parameters["state"] = "open";
                        break;

                    case IssueState.Closed:
                        Parameters["state"] = "closed";
                        break;
                }
            }
        }
    }

    public enum IssueState
    {
        None = 0, 
        Open,
        Closed
    }
}
