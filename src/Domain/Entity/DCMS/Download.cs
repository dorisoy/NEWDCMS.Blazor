using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DCMS.Domain.Main
{

    public partial class Download : AuditableEntity<int>
    {

        public Guid DownloadGuid { get; set; }

        [Column(TypeName = "BIT(1)")]
        public bool UseDownloadUrl { get; set; }


        public string DownloadUrl { get; set; }


        public byte[] DownloadBinary { get; set; }


        public string ContentType { get; set; }

        public string Filename { get; set; }

        public string Extension { get; set; }

        [Column(TypeName = "BIT(1)")]
        public bool IsNew { get; set; }
    }
}
