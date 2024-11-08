using Microsoft.AspNetCore.Mvc.Rendering;
using Core.Entity;
using Microsoft.AspNetCore.Hosting;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.ViewModels;
using System.IO;
using Core.Interface;

namespace Web.Controllers
{
    public class ProtfolioItemsController : Controller
    {
       // private readonly DataContext _context;
        private readonly IUnitOfWork<ProtfolioItem> _portfolio;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hosting;

        public ProtfolioItemsController(IUnitOfWork<ProtfolioItem> portfolio, Microsoft.AspNetCore.Hosting.IHostingEnvironment hosting)
        {
            //_context = context;
           _portfolio = portfolio;
            _hosting = hosting;

            //this.context = context;
        }

        // GET: ProtfolioItemss
        public  IActionResult Index()
        {
            //return _context.ProtfolioItems != null ?
            //            View(await _context.ProtfolioItems.ToListAsync()) :
            //            Problem("Entity set 'WebContext.ProtfolioItems'  is null.");
            return View(_portfolio.Repository.GetAll());
        }

        // GET: ProtfolioItemss/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protfolioItem = _portfolio.Repository.Get(id);
                //.FirstOrDefaultAsync(m => m.Id == id);
            if (protfolioItem == null)
            {
                return NotFound();
            }

            return View(protfolioItem);
        }

        // GET: ProtfolioItemss/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProtfolioItemss/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProtfolioViewModel model)
        {
            if (ModelState.IsValid)
            {

                if (model.File != null)
                {
                    string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                    string fullpath = Path.Combine(uploads, model.File.FileName);
                    model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                }
                ProtfolioItem protfolioItem = new ProtfolioItem
                {

                    ProjectName = model.ProjectName,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,


                };
                _portfolio.Repository.Insert(protfolioItem);
                _portfolio.Save();

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ProtfolioItemss/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var protfolioItem = _portfolio.Repository.Get(id);
            if (protfolioItem == null)
            {
                return NotFound();
            }
            ProtfolioViewModel protfolioViewModel = new ProtfolioViewModel { 
            
            Id= protfolioItem.Id,
            Description= protfolioItem.Description,
            ImageUrl= protfolioItem.ImageUrl,
            ProjectName= protfolioItem.ProjectName, 

            };

            return View(protfolioViewModel);
        }

        // POST: ProtfolioItemss/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, ProtfolioViewModel model )
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (model.File != null)
                    {
                        string uploads = Path.Combine(_hosting.WebRootPath, @"img\portfolio");
                        string fullpath = Path.Combine(uploads, model.File.FileName);
                        model.File.CopyTo(new FileStream(fullpath, FileMode.Create));
                    }
                    ProtfolioItem protfolioItem = new ProtfolioItem
                    {
                        Id= model.Id,
                        ProjectName = model.ProjectName,
                        Description = model.Description,
                        ImageUrl = model.ImageUrl,


                    };
                    _portfolio.Repository.Update(protfolioItem);
                    _portfolio.Save(); 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProtfolioItemsExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: ProtfolioItemss/Delete/5
        public IActionResult Delete(Guid? id)
        {
            if (id == null )
            {
                return NotFound();
            }

            var protfolioItem = _portfolio.Repository.Get(id);
                //.FirstOrDefaultAsync(m => m.Id == id);
            if (protfolioItem == null)
            {
                return NotFound();
            }

            return View(protfolioItem);
        }

        // POST: ProtfolioItemss/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            //if (_context.ProtfolioItems == null)
            //{
            //    return Problem("Entity set 'Web1Context.ProtfolioItems'  is null.");
            //}
            //var protfolioItem = await _context.ProtfolioItems.FindAsync(id);
            //if (protfolioItem != null)
            //{
            //    _context.ProtfolioItems.Remove(protfolioItem);
            //}

            //await _context.SaveChangesAsync();
            _portfolio.Repository.Delete(id);
            _portfolio.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool ProtfolioItemsExists(Guid id)
        {
            return _portfolio.Repository.GetAll().Any(e => e.Id == id);
        }
    }
}