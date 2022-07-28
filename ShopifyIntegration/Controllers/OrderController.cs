using Microsoft.AspNetCore.Mvc;
using ShopifyIntegration.Constant;
using ShopifySharp;
using System.Text.Json;

namespace ShopifyIntegration.Controllers
{
    [ApiController]
    [Route("[controller]/[Action]")]
    public class OrderController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateOrder(Order order)
        {
            try
            {
                var orderService = new OrderService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var orderModel = await orderService.CreateAsync(order);
                return Ok(orderModel);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(null);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrder()
        {
            try
            {
                var orderService = new OrderService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var orderList = await orderService.ListAsync();
                return Ok(orderList);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByOrderId(long orderId)
        {
            try
            {
                var orderService = new OrderService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var order = await orderService.GetAsync(orderId);
                return Ok(order);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetOrderByAppId(long appId)
        {
            try
            {
                var orderService = new OrderService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var order = await orderService.ListAsync();
                var result = order.Items.Where(x => x.AppId == appId).ToList();
                return Ok(result);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            try
            {
                var orderService = new OrderService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var orderModel = await orderService.GetAsync(order.Id.Value);
                orderModel.TotalWeight = order.Id;
                var orderList = await orderService.UpdateAsync(orderModel.Id.Value,new Order()
                {
                    Note = "Test Notes"
                });
                return Ok(orderList);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(null);
        }
    }
}
