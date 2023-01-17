using Application.Features.Categories.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Categories.Queries.GetListCategory
{
    public class GetListCategoryQuery : IRequest<CategoryListModel>
    {
        public PageRequest PageRequest { get; set; }
    }

    public class GetListCategoryQueryHandler : IRequestHandler<GetListCategoryQuery, CategoryListModel>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetListCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryListModel> Handle(GetListCategoryQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Category> categories = await _categoryRepository.GetListAsync(index: request.PageRequest.Page, size: request.PageRequest.PageSize);
            CategoryListModel categoryListModel = _mapper.Map<CategoryListModel>(categories);

            return categoryListModel;
        }
    }
}
