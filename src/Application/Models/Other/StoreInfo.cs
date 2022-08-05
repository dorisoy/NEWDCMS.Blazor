using DCMS.Shared.Models.Map;

namespace DCMS.Application.Models
{

    public class StoreInfo
    {
        public int Id { get; set; }
        public string Avatar { get; set; }
        public string StoreName { get; set; }
        public string Address { get; set; }
        public string Distance { get; set; }
        public MapPosition Location { get; set; }
    }
}
