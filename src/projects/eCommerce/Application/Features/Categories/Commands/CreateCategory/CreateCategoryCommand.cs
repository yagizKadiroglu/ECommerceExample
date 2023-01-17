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

namespace Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<CreatedCategoryDto>
    {
        public string Name { get; set; }
    }

    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CreatedCategoryDto>
    {
        ICategoryRepository _categoryRepository;
        IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CreatedCategoryDto> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category mappedCategory = _mapper.Map<Category>(request);
            Category createdCategory = await _categoryRepository.AddAsync(mappedCategory);
            CreatedCategoryDto createdCategoryDto = _mapper.Map<CreatedCategoryDto>(createdCategory);

            return createdCategoryDto;
        }
    }
}
