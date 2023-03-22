﻿using AppCore.IRepositories;
using AppCore.Models.OrderModels;
using MindTheCodeApp.Services.IServices;

namespace AppCore.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<List<Order>> GetAllOrder()
        {
            var orders = await _orderRepository.GetAllOrders();
            return orders;
        }
    }
}
