using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PittsburghBabaTemple.Core.Authentication;
using PittsburghBabaTemple.Core.Enums;
using PittsburghBabaTemple.Core.Extensions;
using PittsburghBabaTemple.Core.Interfaces;

namespace PittsburghBabaTemple.Core.Model
{
    public class User : ModelEntity<User>, IUserAccount
    {
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleInitial { get; set; }
        public virtual string LastName { get; set; }
        public virtual string JobTitle { get; set; }
        public virtual string WorkLocation { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string Organization { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string Country { get; set; }
        public virtual State State { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool IsLocked { get; set; }
        public virtual bool IsDeleted { get; set; }
        public virtual IList<Password> Passwords { get; set; }
        public virtual IList<Role> Roles { get; set; }

        public User()
        {
            this.Passwords = new List<Password>();
            this.Roles = new List<Role>();
        }

        public virtual void AddRole(Role role)
        {
            //role.Users.Add(this);
            this.Roles.Add(role);
        }

        public virtual Password CurrentPassword()
        {
            return Passwords.OrderBy(t => t.Id).FirstOrDefault();
        }

        public virtual void RemoveRole(Role role)
        {
            //role.Users.Remove(this);
            Roles.Remove(role);
        }

        public virtual void AddPassword(Password password)
        {
            password.User = this;
            this.Passwords.Add(password);
        }

        public virtual string FullName()
        {
            return FullName(true);
        }

        public virtual void CreateFrom(AccountRequest accountRequest)
        {
            this.UserName = this.GenerateUserName(accountRequest.FirstName, accountRequest.MiddleInitial, accountRequest.LastName);
            this.Passwords.Add(new Password() { User = this });
            this.FirstName = accountRequest.FirstName;
            this.MiddleInitial = accountRequest.MiddleInitial;
            this.LastName = accountRequest.LastName;
            this.JobTitle = accountRequest.JobTitle;
            this.WorkLocation = accountRequest.WorkLocation;
            this.Organization = accountRequest.Organization;
            this.Country = accountRequest.Country;
            this.State = accountRequest.State;
            this.City = accountRequest.City;
            this.ZipCode = accountRequest.ZipCode;
            this.PhoneNumber = accountRequest.PhoneNumber;
            this.Email = accountRequest.Email;
            this.IsActive = true;
            this.IsLocked = false;
            this.IsLocked = false;
            var roles = accountRequest.ResponsibilityAnswers.Select(x => x.ResponsibilityQuestion.Role).ToList();
            foreach (var role in roles) AddRole(role);
        }

        public virtual void UpdateRolesFrom(RoleChangeRequest request)
        {
            var rolesToRemove = new List<Role>();
            foreach (var role in Roles)  if (!request.Roles.Any(t => t.Id == role.Id)) rolesToRemove.Add(role);
            foreach (var role in request.Roles) if (!Roles.Any(t => t.Id == role.Id)) AddRole(role);
            foreach (var role in rolesToRemove) RemoveRole(role);
        }

        public virtual string FullName(bool lastNameFirst)
        {
            if (lastNameFirst)
                return String.Format("{0}{1} {2}", LastName + ", ", FirstName, HasMiddleInitial ? String.Format(" {0}.", MiddleInitial) : "");

            return String.Format("{0}{1} {2}", FirstName, (HasMiddleInitial ? String.Format(" {0}.", MiddleInitial) : ""), LastName);
        }

        private bool HasMiddleInitial
        {
            get
            {
                return !String.IsNullOrEmpty(MiddleInitial) && MiddleInitial.Trim() != "-";
            }
        }

        public virtual IAuthenticationResponse IsAuthenticated(string password)
        {
            var currentPassword = this.Passwords.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
            if (this.IsLocked) { return new AccountLockedResponse(); }
            if (!this.IsActive) { return new AccountSuspendedResponse(); }
            if (this.IsDeleted) { return new AccountDeletedResponse(); }
            if (currentPassword == null && password.Length == 0) { return new SuccessResponse(); }
            //if (currentPassword == null) { return new MissingPasswordResponse(); } TODO:  handle missing password?  currentPassword could be null on next line
            if ((currentPassword.PasswordHash != password.ToMD5().ToHexString())) { return new AccountIncorrectPasswordResponse(); }
            return new SuccessResponse();
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            var errors = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(this.FirstName)) { errors.Add("FirstName", "First Name is required"); }
            if (String.IsNullOrEmpty(this.MiddleInitial)) { errors.Add("MiddleInitial", "Middle Initial is required"); }
            if (String.IsNullOrEmpty(this.LastName)) { errors.Add("LastName", "First Name is required"); }
            if (String.IsNullOrEmpty(this.JobTitle)) { errors.Add("JobTitle", "First Name is required"); }
            if (String.IsNullOrEmpty(this.WorkLocation)) { errors.Add("WorkLocation", "First Name is required"); }
            if (String.IsNullOrEmpty(this.City)) { errors.Add("City", "City is required"); }
            if (String.IsNullOrEmpty(this.Email)) { errors.Add("Email", "Email is required"); }
            else if (!Regex.IsMatch(this.Email, @"^[A-Za-z0-9](([_\.\-']?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$")) { errors.Add("EmailFormat", "Email is not in the correct format"); }
            if (!String.IsNullOrEmpty(this.PhoneNumber))
            {
                if (!Regex.IsMatch(this.PhoneNumber, @"^[2-9]\d{2}-\d{3}-\d{4}$")) { errors.Add("PhoneNumber", "Phone number is not in the correct format"); }
            }
            return errors;
        }

        public virtual int GetIncorrectNumberOfPasswords(User user)
        {
            var lastLoginDate = (from securityAuditLog2 in SecurityAuditLog.FindAll()
                                 where
                                     securityAuditLog2.EventUserId == user.Id &&
                                     securityAuditLog2.EventType == SecurityAuditEvent.LoggedIn.ToString()
                                 orderby
                                     securityAuditLog2.EventDateTime
                                     descending
                                 select securityAuditLog2.EventDateTime).FirstOrDefault();

            if (lastLoginDate >= DateTime.Parse("1/1/1900 12:00:00"))
            {
                return (from securityAuditLog in SecurityAuditLog.FindAll()
                        where
                            securityAuditLog.EventUserId == user.Id &&
                            securityAuditLog.EventType == SecurityAuditEvent.InvalidPassword.ToString() &&
                            securityAuditLog.EventDateTime >= lastLoginDate
                        select securityAuditLog).Count();
            }
            return (from securityAuditLog in SecurityAuditLog.FindAll()
                    where
                        securityAuditLog.EventUserId == user.Id &&
                        securityAuditLog.EventType == SecurityAuditEvent.InvalidPassword.ToString()
                    select securityAuditLog).Count();
        }

        private string GenerateUserName(string firstName, string middleInitial, string lastName) 
        {
            var userName = String.Format("{0}{1}", firstName.ToLower()[0], lastName.ToLower());
            var userName1 = String.Format("{0}{1}{2}", firstName.ToLower()[0], middleInitial.ToLower(), lastName.ToLower());
            var userName2 = String.Format("{0}{1}", firstName.ToLower(), lastName.ToLower());
            var userName3 = String.Format("{0}{1}{2}", firstName.ToLower(), middleInitial.ToLower(), lastName.ToLower());
            if (User.FindOne(x => x.UserName == userName).Id == 0) { return userName; }
            if (User.FindOne(x => x.UserName == userName1).Id == 0) { return userName1; } 
            if (User.FindOne(x => x.UserName == userName2).Id == 0) { return userName2; } 
            if (User.FindOne(x => x.UserName == userName3).Id == 0) { return userName3; } 
            var user = User.Find(x => x.UserName.Contains(userName)).OrderByDescending(x => x.UserName).FirstOrDefault();
            int lastNumberUsed;
            int.TryParse(user.UserName.Reverse().ElementAt(0).ToString(), out lastNumberUsed);
            return String.Format("{0}{1}", userName, lastNumberUsed + 1);
        }

        public virtual string ResetPassword()
        {
            var password = new Password { User = this, CreatedDateTime = DateTime.Now };
            Passwords.Add(password);
            var tempPassword = password.ResetPassword();
            return tempPassword;
        }

        public virtual ChangePasswordReturnTypes ChangePassword(string oldPassword, string newPassword)
        {
            Password password = this.Passwords.OrderByDescending(x => x.CreatedDateTime).FirstOrDefault();
            if (password != null)
            {
                if (password.PasswordHash != oldPassword.ToMD5().ToHexString())
                    return ChangePasswordReturnTypes.WrongOldPassword;
                if (password.IsTemporary && password.CreatedDateTime < DateTime.Now.AddDays(-3))
                    return ChangePasswordReturnTypes.TempPasswordExpired;
                if (this.Passwords.OrderByDescending(x => x.CreatedDateTime).Take(8).Any(lastPassword => lastPassword.PasswordHash == newPassword.ToMD5().ToHexString()))
                    return ChangePasswordReturnTypes.NewPasswordNotUnique;
                var addPassword = new Password
                {
                    IsTemporary = false,
                    CreatedDateTime = DateTime.Now,
                    PasswordHash = newPassword.ToMD5().ToHexString(),
                    User = this
                };
                this.Passwords.Add(addPassword);
                this.Save();
                return ChangePasswordReturnTypes.Success;
            }
            return ChangePasswordReturnTypes.Error;
        }
    }
}