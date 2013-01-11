using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Model
{
    public class ErrorLog : ModelEntity<ErrorLog>
    {
        public virtual string Controller { get; set; }
        public virtual string Action { get; set; }
        public virtual string ExceptionType { get; set; }
        public virtual string ExceptionMessage { get; set; }
        public virtual string ExceptionSource { get; set; }
        public virtual string ExceptionTargetSite { get; set; }
        public virtual string StackTrace { get; set; }
        public virtual DateTime ErrorTime { get; set; }

        public ErrorLog()
        {
            this.ErrorTime = DateTime.Now;
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            throw new NotImplementedException();
        }
    }
}
