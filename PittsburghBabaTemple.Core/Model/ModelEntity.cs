using System.Collections.Generic;
using PittsburghBabaTemple.Core.Data;
using PittsburghBabaTemple.Core.Interfaces;

namespace PittsburghBabaTemple.Core.Model
{
    public abstract class ModelEntity<T> : ActiveRecordBase<T>, IEntity where T : class, IEntity, new()
    {
        public virtual int Id { get; set; }

        public virtual bool IsValid() {
            return (this.CollectBrokenRules().Count == 0); 
        }

        public virtual IDictionary<string, string> BrokenRules()
        {
            return this.CollectBrokenRules();
        }

        protected abstract IDictionary<string, string> CollectBrokenRules();
    }
}
