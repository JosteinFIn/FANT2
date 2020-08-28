using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FANT2.Models
{
    public class Annonse
    {
		public int Id { get; set; }

		public string UserId { get; set; }
		public Category Kategori { get; set; }
		[Required]
		public bool TypeAnnonse { get; set; }
		public bool IsValuable { get; set; }
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		public string Image { get; set; }
		public DateTime Date { get; set; }
	}
}
