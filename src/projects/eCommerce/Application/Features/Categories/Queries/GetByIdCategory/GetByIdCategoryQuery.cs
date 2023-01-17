using Application.Features.Categories.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetByIdCategory
{
    public class GetByIdCategoryQuery : IRequest<CategoryDto>
    {
        public int Id { get; set; }
    }

    public class GetByIdCategoryQueryHandler : IRequestHandler<GetByIdCategoryQuery, CategoryDto>
    {
        ICategoryRepository _categoryRepository;
        IMapper _mapper;

        public GetByIdCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDto> Handle(GetByIdCategoryQuery request, CancellationToken cancellationToken)
        {
            Category? category = await _categoryRepository.GetAsync(c => c.Id == request.Id);
            CategoryDto categoryDto = _mapper.Map<CategoryDto>(category);

            return categoryDto;
        }
    }
}
