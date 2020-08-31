using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FANT2.Models
{
	public class AnnonseCategoryModel
	{
		public List<Annonse> Annonses { get; set;}

		public SelectList Categorys { get; set; }

		public string AnnonseCategory { get; set; }

		public string SearchString { get; set; }


	}
}
