using System;
using System.Collections.Generic;
using PittsburghBabaTemple.Core.Extensions;

namespace PittsburghBabaTemple.Core.Model
{
    public class Password : ModelEntity<Password>
    {
        public virtual User User { get; set; }
        public virtual string PasswordHash { get; set; }
        public virtual bool IsTemporary { get; set; }
        public virtual DateTime CreatedDateTime { get; set; }

        public virtual string ResetPassword()
        {
            var tempPassword = CreateTemporaryPassword(8);
            this.PasswordHash = tempPassword.ToMD5().ToHexString();
            this.IsTemporary = true;
            this.CreatedDateTime = DateTime.Now;
            return tempPassword;
        }

        public virtual bool IsExpired()
        {
            return CreatedDateTime.AddDays(90) < DateTime.Now;
        }

        //public virtual Core.Enums.ChangePasswordReturnTypes ChangePassword(string oldPassword, string newPassword)
        //{
        //    if (this.PasswordHash == oldPassword.ToMD5().ToHexString())
        //    {
        //        this.PasswordHash = newPassword.ToMD5().ToHexString();
        //        this.CreatedDateTime = DateTime.Now;
        //        this.Save();
        //        return ChangePasswordReturnTypes.Success;
        //    }
        //    else
        //        return ChangePasswordReturnTypes.WrongOldPassword;
        //}

        protected internal virtual string CreateTemporaryPassword(int length)
        {
            var random = new Random();
            const string alphanums = "0123456789!@#$_*-ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var characters = new char[length];
            for (int i = 0; i < length; i++) { characters[i] = alphanums[random.Next(0, alphanums.Length)]; }
            return new string(characters);
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            throw new NotImplementedException();
        }
    }
}
