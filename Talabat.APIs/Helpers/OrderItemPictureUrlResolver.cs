﻿using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.APIs.Helpers
{
	public class OrderItemPictureUrlResolver : IValueResolver<OrderItem, OrderItemDto, string>
	{
		private readonly IConfiguration _configuration;

		public OrderItemPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
		{
			if (!String.IsNullOrEmpty(source.Product.PictureUrl))
				return $"{_configuration["BaseApiUrl"]}/{source.Product.PictureUrl}";

			return String.Empty;
		}

	}
}
