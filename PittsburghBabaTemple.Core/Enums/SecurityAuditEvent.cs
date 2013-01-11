using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PittsburghBabaTemple.Core.Enums
{
    public enum SecurityAuditEvent
    {
        ValidRequest,
        NonexistentEmail,
        NonexistentUser,
        SuspendedUser,
        InvalidUsername,
        InvalidPassword,
        LoggedIn,
        LockedUserTriedToLogIn,
        LockUserAccount,
        LoginAfterTimeout,
        DeletedUserTriedToLogIn,
        TempPasswordExpiredUserTriedToLogIn,
        AccountNotDeactivated,
        ManageUserAccount = 13
    }
}
