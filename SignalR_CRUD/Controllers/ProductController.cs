﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using SignalR_CRUD.Models;

namespace SignalR_CRUD.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<SignalrServer> _signalrHub;
   
        public ProductController(ApplicationDbContext context, IHubContext<SignalrServer> signalrHub)
        {
            _context = context;
            _signalrHub = signalrHub;
        }
        public IActionResult Blazor()
        {
            return View("_Host");
        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var res = _context.Product.ToList();
            return Ok(res);
        }
        // GET: Product
        public async Task<IActionResult> Index()
        {
              return _context.Product != null ? 
                          View(await _context.Product.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Product'  is null.");
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProdId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProdId,Name,Category")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();

                await _signalrHub.Clients.All.SendAsync("LoadProducts");
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int ProdId, [Bind("ProdId,Name,Category")] Product product)
        {
            if (ProdId != product.ProdId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();

                    await _signalrHub.Clients.All.SendAsync("LoadProducts");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProdId))
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
            return View(product);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.ProdId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int ProdId)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Product'  is null.");
            }
            var product = await _context.Product.FindAsync(ProdId);
            if (product != null)
            {
                _context.Product.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            await _signalrHub.Clients.All.SendAsync("LoadProducts");

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Product?.Any(e => e.ProdId == id)).GetValueOrDefault();
        }
    }
}
