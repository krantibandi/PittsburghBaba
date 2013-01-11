using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Authentication
{
    public interface IAuthenticationResponse {
        string Message { get; }
    }
}
