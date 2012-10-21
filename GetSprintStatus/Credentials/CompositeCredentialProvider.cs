using System.Collections.Generic;
using System.Linq;

namespace GetSprintStatus.Credentials
{
    /// <summary>
    /// Class that runs a list of credential providers until
    /// it finds one that returns credentials
    /// </summary>
    internal class CompositeCredentialProvider : ICredentialProvider
    {
        private readonly List<ICredentialProvider> providers = new List<ICredentialProvider>();

        #region ICredentialProvider Members

        public Credentials GetCredentials()
        {
            return
                providers.Select(provider => provider.GetCredentials()).FirstOrDefault(credential => credential != null);
        }

        #endregion

        public CompositeCredentialProvider Add(ICredentialProvider provider)
        {
            providers.Add(provider);
            return this;
        }
    }
}