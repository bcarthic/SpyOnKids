using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpyOnKids
{
    public class Kids
    {
        public string Id { get; set; }
        public string Item { get; set; }

        public string ImageName { get; set; }
        public Kids(string id, string item,string imagename)
        {
            this.Id = id;
            this.Item = item;
            this.ImageName = imagename;
        }
    }
}
