using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_API_PBL.Models
{
	[Table("CrawlData")]
	public class CrawlData
	{
		[Column("id")]
		[Key]
		public int Id { get; set; }

		[Column("site_name")]
		[MaxLength(255)]
		public string SiteName { get; set; }

		[Column("Category_id")]
		public int CategoryId { get; set; }

		[Column("name_selector")]
		[MaxLength(255)]
		public string NameSelector { get; set; }

		[Column("price_selector")]
		[MaxLength(255)]
		public string PriceSelector { get; set; }

		[Column("link_selector")]
		[MaxLength(255)]
		public string LinkSelector { get; set; }

		[Column("image_selector")]
		[MaxLength(255)]
		public string ImageSelector { get; set; }

		[Column("url")]
		[MaxLength(255)]
		public string Url { get; set; }
	}
}
