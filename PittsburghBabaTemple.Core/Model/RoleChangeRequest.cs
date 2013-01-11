using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Model
{
    public class RoleChangeRequest : ModelEntity<RoleChangeRequest>
    {
        public RoleChangeRequest()
        {
            ResponsibilityAnswers = new List<ResponsibilityAnswer>();
        }

        public virtual User User { get; set; }
        public virtual string Status { get; set; }
        public virtual DateTime RequestedAt { get; set; }
        public virtual int? ReviewerId { get; set; }
        public virtual DateTime? ReviewedAt { get; set; }
        public virtual string ReviewerComments { get; set; }

        public virtual IList<ResponsibilityAnswer> ResponsibilityAnswers { get; set; }

        public virtual void AddResponsiblityAnswer(ResponsibilityAnswer responsibilityAnswer)
        {
            ResponsibilityAnswers.Add(responsibilityAnswer);
            responsibilityAnswer.RoleChangeRequest = this;
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            var errors = new Dictionary<string, string>();
            if (this.ResponsibilityAnswers.Count == 0) { errors.Add("ResponsibilityAnswers", "Please select an answer"); }
            return errors;
        }

        public virtual IEnumerable<Role> Roles
        {
            get { return ResponsibilityAnswers.Select(answer => answer.ResponsibilityQuestion.Role); }
            set { }
        }
    }
}
