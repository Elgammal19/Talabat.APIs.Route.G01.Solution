using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{
	public class Address   // --> Composite Attribute
	{
		// required --> C# Keyword make the property can't be initialized with null
		// && != [Required] --> Data Annotaion make this column rquired in DB & used for validation
        public required string FirstName { get; set; } 
        public string LastName { get; set; } = null!;
        public string Street { get; set; } = null!;
		public string City { get; set; } = null!;
        public string Country { get; set; } = null!;
	}
}
