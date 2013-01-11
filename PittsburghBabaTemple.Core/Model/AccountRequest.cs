using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PittsburghBabaTemple.Core.Interfaces;

namespace PittsburghBabaTemple.Core.Model
{
    public class AccountRequest : ModelEntity<AccountRequest>, IUserAccount
    {
        public virtual string UserName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleInitial { get; set; }
        public virtual string LastName { get; set; }
        public virtual string JobTitle { get; set; }
        public virtual string WorkLocation { get; set; }
        public virtual string Organization { get; set; }
        public virtual string City { get; set; }
        public virtual string ZipCode { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Email { get; set; }
        public virtual string Country { get; set; }
        public virtual State State { get; set; }
        public virtual User User { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime? RequestedAt { get; set; }
        public virtual DateTime? ReviewedAt { get; set; }
        public virtual string ApproverCommments { get; set; }
        public virtual IList<ResponsibilityAnswer> ResponsibilityAnswers { get; set; }

        public AccountRequest()
        {
            this.ResponsibilityAnswers = new List<ResponsibilityAnswer>();
        }

        public virtual string FullName()
        {
            return String.Format("{0}{1} {2}", LastName + ", ", FirstName, (!String.IsNullOrEmpty(MiddleInitial) && MiddleInitial.Trim() != "-") ? String.Format(" {0}.", MiddleInitial) : "");
        }

        public virtual void AddResponsiblityAnswer(ResponsibilityAnswer responsibilityAnswer)
        {
            this.ResponsibilityAnswers.Add(responsibilityAnswer);
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            var errors = new Dictionary<string, string>();
            if (String.IsNullOrEmpty(this.FirstName)) { errors.Add("FirstName", "First Name is required"); }
            if (String.IsNullOrEmpty(this.MiddleInitial)) { errors.Add("MiddleInitial", "Middle Initial is required"); }
            if (String.IsNullOrEmpty(this.LastName)) { errors.Add("LastName", "Last Name is required"); }
            if (String.IsNullOrEmpty(this.JobTitle)) { errors.Add("JobTitle", "Job Title is required"); }
            if (String.IsNullOrEmpty(this.WorkLocation)) { errors.Add("WorkLocation", "Work Location is required"); }
            if (String.IsNullOrEmpty(this.Organization)) { errors.Add("Organization", "Organization is required"); }
            if (String.IsNullOrEmpty(this.City)) { errors.Add("City", "City is required"); }
            if (String.IsNullOrEmpty(this.Email)) { errors.Add("Email", "Email is required"); }
            else if (!Regex.IsMatch(this.Email, @"^[A-Za-z0-9](([_\.\-']?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$")) { errors.Add("EmailFormat", "Email is not in the correct format"); }
            if (!String.IsNullOrEmpty(this.PhoneNumber))
            {
                if (!Regex.IsMatch(this.PhoneNumber, @"^[2-9]\d{2}-\d{3}-\d{4}$")) { errors.Add("PhoneNumber", "Phone number is not in the correct format"); }
            }
            if (this.ResponsibilityAnswers.Count == 0) { errors.Add("ResponsibilityAnswers", "Please select an answer"); }
            return errors;
        }
    }
}
