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
	public class ProductPriceController : ControllerBase
	{
		private readonly DataContext _context;

		public ProductPriceController(DataContext context)
		{
			_context = context;
		}

		// GET: api/ProductPrice
		[HttpGet]
		public async Task<ActionResult<IEnumerable<ProductPrice>>> GetProductPrices()
		{
			if (_context.ProductPrices == null)
			{
				return NotFound();
			}
			return await _context.ProductPrices.ToListAsync();
		}

		// GET: api/ProductPrice/5
		[HttpGet("{productId}")]
		public async Task<IEnumerable<ProductPrice>> GetProductPrice(int productId)
		{
			if (_context.ProductPrices == null)
			{
				return Enumerable.Empty<ProductPrice>();
			}

			var productPrices = await _context.ProductPrices
			.Where(pp => pp.ProductId == productId)
			.ToListAsync();


			if (productPrices == null)
			{
				return Enumerable.Empty<ProductPrice>();
			}

			return productPrices;
		}

		// PUT: api/ProductPrice/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProductPrice(int id, ProductPrice productPrice)
		{
			if (id != productPrice.Id)
			{
				return BadRequest();
			}

			_context.Entry(productPrice).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductPriceExists(id))
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

		// POST: api/ProductPrice
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<ProductPrice>> PostProductPrice(ProductPrice productPrice)
		{
			if (_context.ProductPrices == null)
			{
				return Problem("Entity set 'DataContext.ProductPrices'  is null.");
			}
			_context.ProductPrices.Add(productPrice);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProductPrice", new { id = productPrice.Id }, productPrice);
		}

		// DELETE: api/ProductPrice/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProductPrice(int id)
		{
			if (_context.ProductPrices == null)
			{
				return NotFound();
			}
			var productPrice = await _context.ProductPrices.FindAsync(id);
			if (productPrice == null)
			{
				return NotFound();
			}

			_context.ProductPrices.Remove(productPrice);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProductPriceExists(int id)
		{
			return (_context.ProductPrices?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
