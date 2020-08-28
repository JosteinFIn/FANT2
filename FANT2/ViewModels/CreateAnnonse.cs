using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FANT2.ViewModels
{
	public class CreateAnnonse
	{
		public List<SelectListItem> Categories { get; set; }

		public int CategoryId { get; set; }

		public bool TypeAnnonse { get; set; }
		public bool IsValuable { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
		public DateTime Date { get; set; }
		public string Image { get; set; }

	}
}
