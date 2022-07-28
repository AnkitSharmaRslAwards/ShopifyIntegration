using Microsoft.AspNetCore.Mvc;
using ShopifyIntegration.Constant;
using ShopifySharp;
using ShopifySharp.Filters;
using System.Collections.Specialized;
using System.Text.Json;
using System.Web;
using static System.Collections.Specialized.NameObjectCollectionBase;

namespace ShopifyIntegration.Controllers
{
    [Route("[controller]/[Action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {
            try
            {
                var productService = new ProductService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var productList = await productService.ListAsync();
                return Ok(productList);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }

        [HttpGet]
        public async Task<IActionResult> GetProductByProductId(long productId)
        {
            try
            {
                var productService = new ProductService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var product = await productService.GetAsync(productId);
                return Ok(product);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }


        [HttpGet]
        public async Task<IActionResult> GetAllInventory()
        {
            try
            {
                var inventoryService = new InventoryLevelService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
               // var productList = await inventoryService.ListAsync();
               // return Ok(productList);
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateInventoryQuantityByProductId(long variantId, int quantity)
        {
            try
            {
                //var productService = new ProductService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                //var product = await productService.GetAsync(variantId);
                //var singleVariant = product.Variants.Where(x=>x.Id == variantId).FirstOrDefault();

                //var list = await Fixture.Service.ListAsync(new InventoryLevelListFilter { InventoryItemIds = new[] { Fixture.InventoryItemId } });
               
                var productVariantService = new ProductVariantService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var variant = await productVariantService.GetAsync(variantId);

                
                var inventoryItemService = new InventoryItemService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var inventoryItem = await inventoryItemService.GetAsync(variant.InventoryItemId.Value);


                var inventoryLevelService = new InventoryLevelService(ConstantStrings.StoreUrl, ConstantStrings.ShopifyAccessToken);
                var inventoryLevel = await inventoryLevelService.ListAsync(new InventoryLevelListFilter { InventoryItemIds = new[] { variant.InventoryItemId.Value } });
                inventoryLevel.Items.FirstOrDefault().Available = quantity;
                //inventoryLevel.Items.FirstOrDefault().UpdatedAt = DateTimeOffset.UtcNow;
                var ch = await inventoryLevelService.SetAsync(inventoryLevel.Items.FirstOrDefault());

                //var variantFinal = await productVariantService.UpdateAsync(variantId, new ProductVariant()
                //{
                //    Id = variantId,
                //    InventoryQuantity = quantity
                //});
                return Ok("Success");
            }
            catch (Exception ex)
            {

                throw;
            }
            return Ok(null);
        }
    }
}
