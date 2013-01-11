using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Model
{
    public class ResponsibilityQuestion : ModelEntity<ResponsibilityQuestion>
    {
        public virtual string QuestionText { get; set; }
        public virtual int DisplaySequence { get; set; }
        public virtual bool IsSelected { get; set; }
        public virtual Role Role { get; set; }
        public virtual IList<AccountRequest> AccountRequests { get; set; }
        public virtual IList<ResponsibilityAnswer> ResponsibilityAnswers { get; set; }

        public ResponsibilityQuestion()
        {
            this.AccountRequests = new List<AccountRequest>();
            this.ResponsibilityAnswers = new List<ResponsibilityAnswer>();
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            throw new NotImplementedException();
        }
    }
}
