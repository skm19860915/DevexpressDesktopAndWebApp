using Microsoft.AspNetCore.Http;

namespace BlitzerCore.Models
{
    public class MerchantBlob
    {
        public int Slot { get; set; }
        public string Header { get; set; }
        public string Description { get; set;  }
        public IFormFile Picture { get; set; }
    }
}
