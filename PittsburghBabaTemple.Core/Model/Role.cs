using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Model
{
    public class Role : ModelEntity<Role>
    {
        public virtual string Name { get; set; }
        public virtual int ApplicationId { get; set; } //TODO: might be obsolete - chris
        public virtual Guid GUID { get; set; } //TODO: might be obsolote - chris
        public virtual IList<ResponsibilityQuestion> ResponsibilityQuestions { get; set; }

        public Role()
        {
           this.ResponsibilityQuestions = new List<ResponsibilityQuestion>();
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            throw new NotImplementedException();
        }
    }
}
