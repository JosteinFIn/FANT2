using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FANT2.Models;

namespace FANT2.ViewModels
{
	public class UserAnnonse
	{

		public Annonse annonse {get; set; }
		public string userEmail { get; set; }

		public bool loggedIn { get; set; }
	}
}
