﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FANT2.Models
{
	public class Message
	{
		public int Id { get; set; }
		public string Melding { get; set; }
		public string FromUserId { get; set;}

		public int AnnonseId { get; set; }
	
	}
}
