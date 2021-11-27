using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hackathon_campus.Core.ViewModels
{
    public class CreateCategoryViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
