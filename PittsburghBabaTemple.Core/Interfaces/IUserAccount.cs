using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PittsburghBabaTemple.Core.Model;

namespace PittsburghBabaTemple.Core.Interfaces
{
    public interface IUserAccount
    {
        string UserName { get; set; }
        string FirstName { get; set; }
        string MiddleInitial { get; set; }
        string LastName { get; set; }
        string JobTitle { get; set; }
        string WorkLocation { get; set; }
        string City { get; set; }
        string ZipCode { get; set; }
        string PhoneNumber { get; set; }
        string Email { get; set; }
        string Organization { get; set; }
        string Country { get; set; }
        State State { get; set; }
    }
}
