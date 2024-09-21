using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API_PBL.Models
{
	[Table("Products")]
	public class Product
	{
		[Column("id")]
		[Key]
		public int Id { get; set; }

		[Column("name")]
		[MaxLength(255)]
		public string Name { get; set; }

		[Column("image_url")]
		[MaxLength(255)]
		public string? ImageUrl { get; set; }


		[ForeignKey("Category")]
		[Column("category_id")]
		public int CategoryId { get; set; }
		public Category Category { get; set; }
	}
}
