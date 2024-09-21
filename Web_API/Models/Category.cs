using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API_PBL.Models
{
	[Table("Categories")]
	public class Category
	{
		[Column("id")]
		[Key]
		public int Id { get; set; }

		[Column("name")]
		[MaxLength(255)]
		public string Name { get; set; }

		public ICollection<Product> Products { get; set; }
	}
}
