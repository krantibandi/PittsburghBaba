namespace PittsburghBabaTemple.Core.Authentication
{
    public class AccountIncorrectPasswordResponse : IAuthenticationResponse
    {
        #region IAuthenticationResponse Members

        public string Message
        {
            get { return ""; }
        }

        #endregion
    }
}
