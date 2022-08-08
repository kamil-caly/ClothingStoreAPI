﻿using ClothingStoreAPI.Services;
using ClothingStoreModels.Dtos;
using ClothingStoreModels.Dtos.Dispaly;
using ClothingStoreModels.Dtos.Update;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Route("api/ClothingStore/{storeId}/product")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ProductDto>> GetAll([FromRoute] int storeId)
        {
            var products = productService.GetAll(storeId);

            return Ok(products);
        }

        [HttpGet("{productId}")]
        public ActionResult<ProductDto> Get([FromRoute] int storeId, [FromRoute] int productId)
        {
            var product = productService.GetById(storeId, productId);

            return Ok(product);
        }

        [HttpPost]
        public ActionResult Create([FromRoute] int storeId, [FromBody] CreateProductDto dto)
        {
            var newProductId = productService.Create(storeId, dto);

            return Created($"api/ClothingStore/{storeId}/product/{newProductId}", null);
        }

        [HttpPut("{productId}")]
        public ActionResult Update([FromRoute] int storeId, [FromRoute] int productId ,[FromBody] UpdateProductDto dto)
        {
            productService.Update(storeId, productId, dto);

            return Ok();
        }

        [HttpDelete]
        public ActionResult DeleteAll([FromRoute] int storeId)
        {
            productService.DeleteAll(storeId);

            return NoContent();
        }

        [HttpDelete("{productId}")]
        public ActionResult Delete([FromRoute] int storeId, [FromRoute] int productId)
        {
            productService.Delete(storeId, productId);

            return NoContent();
        }
    }
}
