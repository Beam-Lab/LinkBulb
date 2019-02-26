using System;

namespace LinkBulb.Models
{
    public class Link
    {

        public Guid ID { get; set; }

        public string Title { get; set; }

        public string Url { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public DateTime PublishDate { get; set; }

        public bool Deleted { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

    }
}
