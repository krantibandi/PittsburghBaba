using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
namespace PittsburghBabaTemple.Core.Data
{
    public interface IFetchMode
    {
        ICriteria Fetch(ICriteria t);
    }
}
