using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DemoStockApp.Models;

namespace DemoStockApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksOrdersController : ControllerBase
    {
        private readonly DemoContext _context;

        public StocksOrdersController(DemoContext context)
        {
            _context = context;
        }

        // GET: api/StocksOrders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StocksOrders>>> GetStocksOrders()
        {
            return await _context.StocksOrders.ToListAsync();
        }

        //// GET: api/StocksOrders/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<StocksOrders>> GetStocksOrders(int id)
        //{
        //    var stocksOrders = await _context.StocksOrders.FindAsync(id);

        //    if (stocksOrders == null)
        //    {
        //        return NotFound();
        //    }

        //    return stocksOrders;
        //}

        [HttpGet("{name}")]
        public IQueryable<StocksOrders> GetStocksOrders(string name)
        {
            var stocksOrders = _context.StocksOrders.Where(x => x.CustomerName == name);

            if (stocksOrders == null)
            {
                return null;
            }

            return stocksOrders;
        }

        [HttpGet("{names}/{all}")]
        public List<string> GetNamesOfClients()
        {
            List<string> clients = new List<string>();
            var stocksOrders = _context.StocksOrders.ToList();
            if (stocksOrders == null)
            {
                return null;
            }

            var result = stocksOrders.GroupBy(x => x.CustomerName).Select(grp => grp.First()).ToList();

            foreach(var res in result)
            {
                clients.Add(res.CustomerName);
            }
            return clients;
        }


        // PUT: api/StocksOrders/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStocksOrders(int id, StocksOrders stocksOrders)
        {
            if (id != stocksOrders.OrderId)
            {
                return BadRequest();
            }

            _context.Entry(stocksOrders).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StocksOrdersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/StocksOrders
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<StocksOrders>> PostStocksOrders(StocksOrders stocksOrders)
        {
            _context.StocksOrders.Add(stocksOrders);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStocksOrders", new { id = stocksOrders.OrderId }, stocksOrders);
        }

        // DELETE: api/StocksOrders/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StocksOrders>> DeleteStocksOrders(int id)
        {
            var stocksOrders = await _context.StocksOrders.FindAsync(id);
            if (stocksOrders == null)
            {
                return NotFound();
            }

            _context.StocksOrders.Remove(stocksOrders);
            await _context.SaveChangesAsync();

            return stocksOrders;
        }

        private bool StocksOrdersExists(int id)
        {
            return _context.StocksOrders.Any(e => e.OrderId == id);
        }
    }
}
