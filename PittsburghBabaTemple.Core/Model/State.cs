using System;
using System.Collections.Generic;

namespace PittsburghBabaTemple.Core.Model
{
    public class State : ModelEntity<State>
    {
        public virtual string Name { get; set; }
        public virtual Country Country { get; set; }
        public virtual IList<AccountRequest> AccountRequests { get; set; }
        public virtual IList<User> Users { get; set; }

        public State()
        {
            this.AccountRequests = new List<AccountRequest>();
            this.Users = new List<User>();
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            throw new NotImplementedException();
        }
    }
}
