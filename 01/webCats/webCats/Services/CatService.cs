using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webCats.Models;

namespace webCats.Services
{
    public class CatService : ICatService
    {
        public ICollection<CatDetailsModel> cats { get; set; }
    }
}
