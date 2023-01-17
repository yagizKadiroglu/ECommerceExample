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

namespace Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommand : IRequest<UpdatedCategoryDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdataCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, UpdatedCategoryDto>
    {
        ICategoryRepository _categoryRepository;
        IMapper _mapper;

        public UpdataCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedCategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            Category mappedCategory = _mapper.Map<Category>(request);
            Category updatedCategory = await _categoryRepository.UpdateAsync(mappedCategory);
            UpdatedCategoryDto updatedCategoryDto = _mapper.Map<UpdatedCategoryDto>(updatedCategory);

            return updatedCategoryDto;
        }
    }
}
