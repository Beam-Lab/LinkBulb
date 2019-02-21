using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LinkBulb.Models;
using LinkBulb.Web.Data;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace LinkBulb.Web.Controllers
{
    [Authorize]
    public class LinksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LinksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Links
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;

            return View(await _context.Links.Where(l => l.UserId == Guid.Parse(userId)).ToListAsync());
        }

        // GET: Links/Create
        public IActionResult Create()
        {
            var link = new Link();
            link.PublishDate = DateTime.Now;

            return View(link);
        }

        // POST: Links/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Title,Url,CreationDate,LastUpdateDate,PublishDate,Deleted,UserId")] Link link)
        {
            if (ModelState.IsValid)
            {
                link.ID = Guid.NewGuid();

                link.CreationDate = DateTime.UtcNow;
                link.LastUpdateDate = DateTime.UtcNow;
                link.Deleted = false;

                var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
                link.UserId = Guid.Parse(userId);

                _context.Add(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(link);
        }

        // GET: Links/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links.FindAsync(id);

            if (link == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (Guid.Parse(userId) != link.UserId)
            {
                return NotFound();
            }

            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ID,Title,Url,CreationDate,LastUpdateDate,PublishDate,Deleted,UserId")] Link link)
        {
            if (id != link.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    link.LastUpdateDate = DateTime.UtcNow;

                    _context.Update(link);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkExists(link.ID))
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
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.ID == id);
            if (link == null)
            {
                return NotFound();
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (Guid.Parse(userId) != link.UserId)
            {
                return NotFound();
            }

            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var link = await _context.Links.FindAsync(id);
            _context.Links.Remove(link);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LinkExists(Guid id)
        {
            return _context.Links.Any(e => e.ID == id);
        }
    }
}
