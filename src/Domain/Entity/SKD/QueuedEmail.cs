
using System;

namespace DCMS.Domain.SKD
{

    public partial class QueuedEmail : AuditableEntity<int>
    {

        public int PriorityId { get; set; }

        public string From { get; set; }

         public string FromName { get; set; }
        public string To { get; set; }

        public string ToName { get; set; }

        public string CC { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime CreatedOnUtc { get; set; }

        public int SentTries { get; set; }

        public DateTime? SentOnUtc { get; set; }

        public int EmailAccountId { get; set; }


        public virtual EmailAccount EmailAccount { get; set; }

        
        public QueuedEmailPriority Priority
        {
            get => (QueuedEmailPriority)PriorityId;
            set => PriorityId = (int)value;
        }
    }
}
