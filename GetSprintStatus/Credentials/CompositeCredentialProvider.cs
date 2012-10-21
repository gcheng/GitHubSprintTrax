using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GetSprintStatus.Credentials
{
    /// <summary>
    /// Class that runs a list of credential providers until
    /// it finds one that returns credentials
    /// </summary>
    class CompositeCredentialProvider : ICredentialProvider
    {
        private readonly List<ICredentialProvider> providers = new List<ICredentialProvider>();

        public CompositeCredentialProvider Add(ICredentialProvider provider)
        {
            providers.Add(provider);
            return this;
        }

        public Credentials GetCredentials()
        {
            return providers.Select(provider => provider.GetCredentials()).FirstOrDefault(credential => credential != null);
        }
    }
}
