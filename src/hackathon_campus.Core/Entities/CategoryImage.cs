using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.Entities
{
    public class CategoryImage
    {
        public Image Image { get; set; }

        public Guid ImageId { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
