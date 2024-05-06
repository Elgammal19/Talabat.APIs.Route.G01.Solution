using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Aggregate
{ 
	public class Order : BaseEntity
	{
		//	Parameter less constructor for EF core to generate migrations
		private Order()
        {
            
        }

        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod? deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
		{
			BuyerEmail = buyerEmail;
			ShippingAddress = shippingAddress;
			DeliveryMethod = deliveryMethod;
			Items = items;
			SubTotal = subTotal;
		}


		// To confirm this order is for this user by checking the email & id from token 
		public string BuyerEmail { get; set; } = null!;

		// To sole the issue of the differnce of the time in server time & user time when creating the order 
		public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;

		public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; } = null!;

		/// Foregin key for the DeliveryMethod tabele --> DeliveryMethod[one] : Order[Many]
		/// To know which method order will be drived to the user
		/// <summary>
		/// public int DeliveryMethodId { get; set; } 
		/// </summary>

		// Navigational Property [one] --> For DeliveryMethod tabel
		public DeliveryMethod? DeliveryMethod { get; set; } = null!;

		// Navigational Property [Many] --> For OrderItem tabel
		public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();

		// Order Price without delivery cost
        public decimal SubTotal { get; set; }

        //public decimal Total => SubTotal + DeliveryMethod.Cost;

		// Drived Attribute --> Will n;t be store in DB
        public decimal GetTotal() => SubTotal + DeliveryMethod.Cost;

        public string PaymentIntentId { get; set; } = string.Empty;

    }
}
