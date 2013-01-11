using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Interfaces
{
    public interface IEntity
    {
        int Id { get; set; }
        bool IsValid();
        IDictionary<string, string> BrokenRules();
    }
}
