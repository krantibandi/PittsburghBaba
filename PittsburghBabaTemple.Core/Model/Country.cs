using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Model
{
    public class Country : ModelEntity<Country>
    {
        public virtual string Name { get; set; }
        public virtual IList<User> Users { get; set; }
        public virtual IList<State> States { get; set; }

        public Country()
        {
            this.Users = new List<User>();
            this.States = new List<State>();
        }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            throw new NotImplementedException();
        }
    }
}
