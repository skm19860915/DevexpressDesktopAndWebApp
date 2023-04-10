using System.Collections.Generic;

namespace BlitzerCore.Models.UI
{
    public class UICategory
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<UITag> Tags { get; set; }
    }
}
