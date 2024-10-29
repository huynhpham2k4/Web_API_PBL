using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web_API_PBL.Models;

namespace Web_API_PBL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrawlDataController : ControllerBase
    {
        private readonly DataContext _context;

        public CrawlDataController(DataContext context)
        {
            _context = context;
        }

        // GET: api/CrawlData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrawlData>>> GetCrawlDatas()
        {
          if (_context.CrawlDatas == null)
          {
              return NotFound();
          }
            return await _context.CrawlDatas.ToListAsync();
        }

        // GET: api/CrawlData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CrawlData>> GetCrawlData(int id)
        {
          if (_context.CrawlDatas == null)
          {
              return NotFound();
          }
            var crawlData = await _context.CrawlDatas.FindAsync(id);

            if (crawlData == null)
            {
                return NotFound();
            }

            return crawlData;
        }

        // PUT: api/CrawlData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrawlData(int id, CrawlData crawlData)
        {
            if (id != crawlData.Id)
            {
                return BadRequest();
            }

            _context.Entry(crawlData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CrawlDataExists(id))
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

        // POST: api/CrawlData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CrawlData>> PostCrawlData(CrawlData crawlData)
        {
          if (_context.CrawlDatas == null)
          {
              return Problem("Entity sett 'DataContext.CrawlDatas'  is null.");
          }
            _context.CrawlDatas.Add(crawlData);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCrawlData", new { id = crawlData.Id }, crawlData);
        }

        // DELETE: api/C  rawlData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrawlData(int id)
        {
            if (_context.CrawlDatas == null)
            {
                return NotFound();
            }
            var crawlData = await _context.CrawlDatas.FindAsync(id);
            if (crawlData == null)
            {
                return NotFound();
            }

            _context.CrawlDatas.Remove(crawlData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CrawlDataExists(int id)
        {
            return (_context.CrawlDatas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
