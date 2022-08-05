
using System;

namespace DCMS.Domain.Auth
{
	public partial class ActivityLog : AuditableEntity<int>
	{
		public int ActivityLogTypeId { get; set; }

		public int UserId { get; set; }

		public string Comment { get; set; }

		public DateTime CreatedOnUtc { get; set; }

		public virtual ActivityLogType ActivityLogType { get; set; }

		public virtual User User { get; set; }
	}
}
