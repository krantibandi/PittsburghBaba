namespace PittsburghBabaTemple.Core.Enums
{
    public enum ChangePasswordReturnTypes
    {
        Success,
        InvalidFormat,
        TempPasswordExpired,
        WrongOldPassword,
        NewPasswordNotUnique,
        Error
    }
}
