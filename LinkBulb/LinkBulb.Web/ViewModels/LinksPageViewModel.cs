using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinkBulb.Models;

namespace LinkBulb.Web.ViewModels
{
    public class LinksPageViewModel
    {
        public string UserName { get; set; }

        public string BackgroundColor { get; set; }

        public List<Link> Links { get; set; }

    }
}
