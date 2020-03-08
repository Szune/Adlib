using System;

namespace Adlib.Api.Models
{
    public class GetAdModel
    {
		public int Id {get; set;}
		public DateTime Created {get; set;}
		public string Subject {get; set;}
		public string Body {get; set;}
		public long? PriceSek {get; set;}
		public string EmailAddress {get; set;}
    }
}