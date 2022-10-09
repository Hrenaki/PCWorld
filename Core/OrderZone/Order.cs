using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using Core.UserZone;

namespace Core.OrderZone
{
	public enum OrderStatus
	{
		Created,
		Paid,
		Delivering,
		Delivered,
		Completed
	}

	public class Order
	{
		public string Hash => Entity.Hash;

		public OrderStatus Status => Enum.Parse<OrderStatus>(Entity.Status.Name);

		internal OrderEntity Entity { get; set; }

      private List<OrderProduct> products = new List<OrderProduct>();
		public IReadOnlyCollection<OrderProduct> Items => products.AsReadOnly();

      public Order(OrderEntity entity)
		{
			Entity = entity;
		}

		internal Order(OrderEntity entity, IEnumerable<OrderProduct> items)
		{
			Entity = entity;
			products.AddRange(items);
		}
	}

	public class OrderProduct
	{
		internal OrderProductEntity Entity { get; init; }

		public string ProductName => Entity.Product.Name;
		public int Quantity => Entity.Quantity;

		public OrderProduct(OrderProductEntity entity)
		{
			Entity = entity;
		}
	}
}