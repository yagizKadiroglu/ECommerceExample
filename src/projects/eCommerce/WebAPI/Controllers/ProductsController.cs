using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Dtos;
using Application.Features.Products.Models;
using Application.Features.Products.Queries.GetByIdProduct;
using Application.Features.Products.Queries.GetListProduct;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateProductCommand createProductCommand)
        {
            CreatedProductDto result = await Mediator.Send(createProductCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteProductCommand deleteProductCommand)
        {
            DeletedProductDto result = await Mediator.Send(deleteProductCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateProductCommand updateProductCommand)
        {
            UpdatedProductDto result = await Mediator.Send(updateProductCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListProductQuery getListProductQuery = new() { PageRequest = pageRequest };
            ProductListModels result = await Mediator.Send(getListProductQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById([FromRoute] GetByIdProductQuery getByIdProductQuery)
        {
            ProductDto result = await Mediator.Send(getByIdProductQuery);
            return Ok(result);
        }
    }
}
