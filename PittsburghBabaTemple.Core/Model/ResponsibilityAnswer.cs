using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Model
{
    public class ResponsibilityAnswer : ModelEntity<ResponsibilityAnswer>
    {
        public virtual AccountRequest AccountRequest { get; set; }
        public virtual RoleChangeRequest RoleChangeRequest { get; set; }
        public virtual ResponsibilityQuestion ResponsibilityQuestion { get; set; }
        public virtual bool NeedsResponsibility { get; set; }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            throw new NotImplementedException();
        }
    }
}
