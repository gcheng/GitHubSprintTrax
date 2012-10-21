using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetSprintStatus.Credentials
{
    /// <summary>
    /// Interface for objects that can be used to get user credentials
    /// </summary>
    interface ICredentialProvider
    {
        Credentials GetCredentials();
    }
}
