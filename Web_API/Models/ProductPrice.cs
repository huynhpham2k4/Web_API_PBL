using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Web_API_PBL.Models
{
	[Table("ProductPrices")]
	public class ProductPrice
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("product_id")]
		[ForeignKey("Product")]
		public int ProductId { get; set; }

		[Column("website_id")]
		[ForeignKey("Website")]
		public int WebsiteId { get; set; }

		[Column("price", TypeName = "decimal(10, 2)")]
		public decimal Price { get; set; }

		[Column("url")]
		[MaxLength(255)]
		[Required]
		public string Url { get; set; }

		// Navigation properties
		public Product Product { get; set; }
		public Website Website { get; set; }
	}
}
