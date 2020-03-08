using System.ComponentModel.DataAnnotations;

namespace Adlib.Api.Models
{
    public class AddAdModel
    {
	    [Required]
		public string Subject {get; set;}
	    [Required]
		public string Body {get; set;}
		[Range(0, long.MaxValue)]
		public long? PriceSek {get; set;}
	    [Required]
		public string EmailAddress {get; set;}
    }
}