using Core.Entity;
using Core.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web.Models;
using Web.ViewModels;

namespace Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork<Owner> _owner;
        private readonly IUnitOfWork<ProtfolioItem> _protfolioitem;
        public HomeController(IUnitOfWork<Owner> owner, IUnitOfWork<ProtfolioItem> protfolioitem)
        {
            _owner = owner;
            _protfolioitem = protfolioitem;
        }

        public IActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {

                owner = _owner.Repository.GetAll().First(),
                protfolioItem = _protfolioitem.Repository.GetAll().ToList()


            };
              
            return View(homeViewModel);
        }

     
    }
}