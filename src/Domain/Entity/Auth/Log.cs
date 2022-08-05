using System;

namespace DCMS.Domain.Auth
{

    public partial class Log : AuditableEntity<int>
    {

        public int LogLevelId { get; set; }

        public string ShortMessage { get; set; }

        public string FullMessage { get; set; }

        public string IpAddress { get; set; }

        public int? UserId { get; set; }

        public string PageUrl { get; set; }


        public string ReferrerUrl { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public virtual User User { get; set; }
    }
}
