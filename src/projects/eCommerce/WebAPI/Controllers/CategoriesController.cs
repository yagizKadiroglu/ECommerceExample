using Application.Features.Categories.Commands.CreateCategory;
using Application.Features.Categories.Commands.DeleteCategory;
using Application.Features.Categories.Commands.UpdateCategory;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Models;
using Application.Features.Categories.Queries.GetByIdCategory;
using Application.Features.Categories.Queries.GetListCategory;
using Core.Application.Requests;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] CreateCategoryCommand createCategoryCommand)
        {
            CreatedCategoryDto result = await Mediator.Send(createCategoryCommand);
            return Created("", result);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete([FromQuery] DeleteCategoryCommand deleteCategoryCommand)
        {
            DeletedCategoryDto result = await Mediator.Send(deleteCategoryCommand);
            return Ok(result);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateCategoryCommand updateCategoryCommand)
        {
            UpdatedCategoryDto result = await Mediator.Send(updateCategoryCommand);
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult> GetList([FromQuery] PageRequest pageRequest)
        {
            GetListCategoryQuery getListProductQuery = new() { PageRequest = pageRequest };
            CategoryListModel result = await Mediator.Send(getListProductQuery);
            return Ok(result);
        }

        [HttpGet("{Id}")]
        public async Task<ActionResult> GetById([FromRoute] GetByIdCategoryQuery getByIdCategoryQuery)
        {
            CategoryDto result = await Mediator.Send(getByIdCategoryQuery);
            return Ok(result);
        }
    }
}