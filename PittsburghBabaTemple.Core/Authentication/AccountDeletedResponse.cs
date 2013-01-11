namespace PittsburghBabaTemple.Core.Authentication
{
    public class AccountDeletedResponse : IAuthenticationResponse
    {
        #region IAuthenticationResponse Members

        public string Message
        {
            get { return ""; }
        }

        #endregion
    }
}
