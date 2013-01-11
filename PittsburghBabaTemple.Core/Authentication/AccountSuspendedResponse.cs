namespace PittsburghBabaTemple.Core.Authentication
{
    public class AccountSuspendedResponse : IAuthenticationResponse
    {
        #region IAuthenticationResponse Members

        public string Message
        {
            get { return ""; }
        }

        #endregion
    }
}
