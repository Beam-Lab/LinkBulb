using System;
using System.Collections.Generic;
using System.Text;

namespace LinkBulb.Models
{
    public class LinkStatistic
    {

        public Guid ID { get; set; }

        public Guid LinkID { get; set; }

        public DateTime ClickDate { get; set; }

        public string Browser { get; set; }

        public string BrowserVersion { get; set; }

        public string OS { get; set; }

        public string DeviceType { get; set; }
    }
}
