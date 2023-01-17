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

namespace Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<DeletedCategoryDto>
    {
        public int Id { get; set; }
    }
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, DeletedCategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<DeletedCategoryDto> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            Category mappedProduct = _mapper.Map<Category>(request);
            Category? getDeletedCategory = await _categoryRepository.GetAsync(p => p.Id == mappedProduct.Id);
            Category deletedCategory = await _categoryRepository.DeleteAsync(getDeletedCategory);
            DeletedCategoryDto deletedCategoryDto = _mapper.Map<DeletedCategoryDto>(deletedCategory);
            return deletedCategoryDto;
        }
    }
}
