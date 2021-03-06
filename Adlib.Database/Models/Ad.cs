using System;
using System.ComponentModel.DataAnnotations;

namespace Adlib.Database.Models
{
	public class Ad
	{
		[Key]
		public int Id {get; set;}
		[Required]
		public DateTime Created {get; set;}
		[Required]
		public string Subject {get; set;}
		[Required]
		public string Body {get; set;}
		public long? Price {get; set;}
		[Required]
		public string EmailAddress {get; set;}
	}
}
