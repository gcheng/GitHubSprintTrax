using GHSprintTrax.GithubApi.EntityImplementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GHSprintTrax.GithubApi
{
    public class GetMilestonesOptions : GetListOptions
    {
        public MilestoneState State
        {
            set
            {
                switch (value)
                {
                    case MilestoneState.None:
                        Parameters.Remove("state");
                        break;

                    case MilestoneState.Open:
                        Parameters["state"] = "open";
                        break;

                    case MilestoneState.Closed:
                        Parameters["state"] = "closed";
                        break;
                }
            }
        }

        public SortBy SortBy
        {
            set
            {
                switch (value)
                {
                    case SortBy.None:
                        Parameters.Remove("sort");
                        break;
                    case SortBy.Date:
                        Parameters["sort"] = "date";
                        break;

                    case SortBy.Completeness:
                        Parameters["sort"] = "completeness";
                        break;
                }
            }
        }

        public SortDirection Direction
        {
            set
            {
                switch (value)
                {
                    case SortDirection.None:
                        Parameters.Remove("direction");
                        break;
                    case SortDirection.Asc:
                        Parameters["direction"] = "asc";
                        break;
                    case SortDirection.Desc:
                        Parameters["direction"] = "desc";
                        break;
                }
            }
        }
    }

    public enum MilestoneState
    {
        None = 0,
        Open,
        Closed
    }

    public enum SortBy
    {
        None = 0,
        Date = 1,
        Completeness = 2
    }

    public enum SortDirection
    {
        None = 0,
        Asc = 1,
        Desc = 2
    }
}
