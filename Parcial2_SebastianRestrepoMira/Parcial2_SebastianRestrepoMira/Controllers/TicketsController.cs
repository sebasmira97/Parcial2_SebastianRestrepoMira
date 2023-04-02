using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.EntityFrameworkCore;
using Parcial2_SebastianRestrepoMira.DAL;
using Parcial2_SebastianRestrepoMira.DAL.Entities;

namespace Parcial2_SebastianRestrepoMira.Controllers
{
    public class TicketsController : Controller
    {
        private readonly DatabaseContext _context;

        public TicketsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
              return _context.Tickets != null ? 
                          View(await _context.Tickets.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.Tickets'  is null.");
        }

        //POST: Formualario
        [HttpPost]
        public async Task<IActionResult> Index(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return RedirectToAction(nameof(NotExist));
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);

            if (ticket != null)
            {
                if(ticket.IsUsed == false)
                {
                    Console.WriteLine("Entro");
                    return RedirectToAction(nameof(Validate), new { id });
                }
                else
                {
                    return RedirectToAction(nameof(ErrorValidate), new {id});
                }
            }else
            {
                return RedirectToAction(nameof(NotExist));
            }

            return View();
        }

        public async Task<IActionResult> NotExist()
        {
            return View();
        }

        public async Task<IActionResult> ErrorValidate(Guid? id)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Validate/5
        public async Task<IActionResult> Validate(Guid? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Validate/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Validate(Guid id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ticket.IsUsed = true;
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        private bool TicketExists(Guid id)
        {
          return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
