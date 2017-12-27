using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webCats.Models;

namespace webCats.Services
{
   public interface ICatService
    {
        ICollection<CatDetailsModel> cats { get; set; }
    }
}
