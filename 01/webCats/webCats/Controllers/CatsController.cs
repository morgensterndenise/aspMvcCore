using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using webCats.Models;
using webCats.Services;

namespace webCats.Controllers
{
    
    public class CatsController : Controller
    {
        private readonly ICatService cats;

        public CatsController(ICatService cats)
        {
            this.cats = cats;
        }

        //primer za async method 2:12: min
        public async Task<IActionResult> IndexAs()
        {
            var users = await this.db
                .Users
                .Where(u => u.Email == this.User.Identity.Name)//zimame tekushtia lognat user
                .Select(u => new
                {
                    u.Email,
                    u.Id

                })
                .FirstOrDefaultAsync();

            return View();
        }

       public IActionResult Index()
        {
            var model = new CatDetailsModel
            {
                Id = 1,
                Name = "kori"
            };

            return View(model);
        }


        public IActionResult Create(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else if(id < 10)
            {
                return View();
            }
            else
            {
                return Redirect("");
            }
        }

        public IActionResult IndexMy()
        {
            //taka osven da go redirektvame i mu podavame parameter
            return RedirectToAction(nameof(CreateMe), new { name = "Deni" });
        }
        //taka se zapisva sukrateno
        public IActionResult CreateMe(string name) => View(nameof(Create),name);

        //primer s get i post
        public IActionResult Index3() => View();

        [HttpPost]
        public IActionResult Index3(CatDetailsModel model) //mvcto populva dannite na modela ot formata vuv viewto kat se submitne
        {
            //proveriava validaciite nad modela kat atrbuti det gi pishem
            if (ModelState.IsValid) 
            {
                return RedirectToAction("");
            }
            return View();
        }
    }
}
