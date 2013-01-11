using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PittsburghBabaTemple.Core.Enums;

namespace PittsburghBabaTemple.Core.Model
{

    public class SecurityAuditLog : ModelEntity<SecurityAuditLog>
    {
        public virtual string EventFunction { get; set; }
        public virtual string EventType { get; set; }
        public virtual string EventData { get; set; }
        public virtual int? EventUserId { get; set; }
        public virtual string IPAddress { get; set; }
        public virtual DateTime EventDateTime { get; set; }

        protected override IDictionary<string, string> CollectBrokenRules()
        {
            throw new NotImplementedException();
        }

        public virtual IList<Tuple<int?, string, DateTime?>> GetLatestLogins()
        {
            return
                Find(x => x.EventType == SecurityAuditEvent.LoggedIn.ToString()).GroupBy(h => new { h.EventUserId, h.EventType })
                               .Select(x => new Tuple<int?, string, DateTime?>(
                                   x.Key.EventUserId,
                                   x.Key.EventType,
                                   EventDateTime = x.Max(p => p.EventDateTime)
                               )).ToList();
        }

        public virtual int GetDaysSinceLastLogin(DateTime logEventDate)
        {
            var dtTodayMidnight = DateTime.UtcNow.Date;  //Today has a default time of midnight
            var dtEventDateMidnight = logEventDate;
            var interval = dtTodayMidnight - dtEventDateMidnight;
            return interval.Days;
        }
    }
}
