namespace GetSprintStatus.Credentials
{
    /// <summary>
    /// Interface for objects that can be used to get user credentials
    /// </summary>
    internal interface ICredentialProvider
    {
        Credentials GetCredentials();
    }
}