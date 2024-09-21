using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API_PBL.Models
{

	[Table("Websites")]
	public class Website
	{
		[Column("id")]
		public int Id { get; set; }

		[Column("name")]
		[MaxLength(255)]
		[Required]
		public string Name { get; set; }

		[Column("url")]
		[MaxLength(255)]
		[Required]
		public string Url { get; set; }

		[Column("url_logo")]
		[MaxLength(255)]
		[Required]
		public string UrlLogo { get; set; }

		public ICollection<ProductPrice> ProductPrices { get; set; }
	}
}
