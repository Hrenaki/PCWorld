using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.UserZone;
using Data.EntityFramework;

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

		internal EfOrderEntity Entity { get; set; }

      private List<OrderProduct> products = new List<OrderProduct>();
		public IReadOnlyCollection<OrderProduct> Items => products.AsReadOnly();

      public Order(EfOrderEntity entity)
		{
			Entity = entity;
		}

		internal Order(EfOrderEntity entity, IEnumerable<OrderProduct> items)
		{
			Entity = entity;
			products.AddRange(items);
		}
	}

	public class OrderProduct
	{
		internal EfOrderProductEntity Entity { get; init; }

		public string ProductName => Entity.Product.Name;
		public int Quantity => Entity.Quantity;

		public OrderProduct(EfOrderProductEntity entity)
		{
			Entity = entity;
		}
	}
}