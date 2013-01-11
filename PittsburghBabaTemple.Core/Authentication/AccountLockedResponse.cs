namespace PittsburghBabaTemple.Core.Authentication
{
    public class AccountLockedResponse : IAuthenticationResponse
    {
        #region IAuthenticationResponse Members

        public string Message
        {
            get { return ""; }
        }

        #endregion
    }
}
