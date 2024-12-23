﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Web_API_PBL.Models;

namespace Web_API_PBL.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly DataContext _context;
		private static int PAGE_SIZE { get; set; } = 24;

		public ProductController(DataContext context)
		{
			_context = context;
		}

		// GET: api/Product
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			if (_context.Products == null)
			{
				return NotFound();
			}
			return await _context.Products.ToListAsync();
		}

		[HttpGet("category")]
		public async Task<IEnumerable<Product>> GetProductByCategoryId(int categoryId = 0, string? search = null, int pageNumber = 1)
		{
			if (_context.ProductPrices == null)
			{
				return Enumerable.Empty<Product>();
			}

			IQueryable<Product> query = _context.Products;

			//filter theo categoryId
			if (categoryId > 0)
			{
				query = query.Where(p => p.CategoryId == categoryId);
			}

			//filter theo search
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(p => p.Name.Contains(search));
			}

			// Thực hiện phân trang
			var products = await query
				.Skip((pageNumber - 1) * PAGE_SIZE)
				.Take(PAGE_SIZE)
				.ToListAsync();

			// Trả về kết quả, kiểm tra null không cần thiết vì ToListAsync sẽ trả về danh sách rỗng nếu không có kết quả
			return products;
		}



		// GET: api/product/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}
			var product = await _context.Products.FindAsync(id);

			if (product == null)
			{
				return NotFound();
			}

			return product;
		}

		// PUT: api/Product/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
		public async Task<IActionResult> PutProduct(int id, Product product)
		{
			if (id != product.Id)
			{
				return BadRequest();
			}

			_context.Entry(product).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!ProductExists(id))
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

		// POST: api/Product
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPost]
		public async Task<ActionResult<Product>> PostProduct(Product product)
		{
			if (_context.Products == null)
			{
				return Problem("Entity set 'DataContext.Products'  is null.");
			}
			_context.Products.Add(product);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetProduct", new { id = product.Id }, product);
		}

		// DELETE: api/Product/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteProduct(int id)
		{
			if (_context.Products == null)
			{
				return NotFound();
			}
			var product = await _context.Products.FindAsync(id);
			if (product == null)
			{
				return NotFound();
			}

			_context.Products.Remove(product);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool ProductExists(int id)
		{
			return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
