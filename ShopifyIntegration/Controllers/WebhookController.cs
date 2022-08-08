using Microsoft.AspNetCore.Mvc;
using ShopifyIntegration.Constant;
using ShopifySharp;

namespace ShopifyIntegration.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class WebhookController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateWebhook(Webhook hook)
        {
            try
            {
                var webhookService = new WebhookService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                //Webhook hook = new Webhook()
                //{
                //    Address = "https://my.webhook.url.com/path",
                //    CreatedAt = DateTime.Now,
                //    Fields = new List<string>() { "field1", "field2" },
                //    Format = "json",
                //    MetafieldNamespaces = new List<string>() { "metafield1", "metafield2" },
                //    Topic = "app/uninstalled",
                //};

                hook = await webhookService.CreateAsync(hook);

                return Ok(hook);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllWebhook()
        {
            try
            {
                var webhookService = new WebhookService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var webhookList = await webhookService.ListAsync();
                return Ok(webhookList);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWebhook(long webhookId,string address)
        {
            try
            {
                var webhookService = new WebhookService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var webhook = await webhookService.UpdateAsync(webhookId, new Webhook()
                {
                    Address = address
                });
                return Ok(webhook);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Ok(null);
        }

        [HttpPost]
        public async Task<IActionResult> NotificationOrderCreated(Order orderModel)
        {
            try
            {
                var orderService = new OrderService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var order = await orderService.GetAsync(orderModel.Id.Value);
                return Ok(order);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }
    }
}
