using Application.Features.Products.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetListProduct
{
    public class GetListProductQuery : IRequest<ProductListModels>
    {
        public PageRequest PageRequest { get; set; }
    }

    public class GetListProductQueryHandler : IRequestHandler<GetListProductQuery, ProductListModels>
    {
        IProductRepository _productRepository;
        IMapper _mapper;

        public GetListProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductListModels> Handle(GetListProductQuery request, CancellationToken cancellationToken)
        {
            IPaginate<Product> products = await _productRepository.GetListAsync(include:
                p => p.Include(c => c.Category),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize
                );

            ProductListModels productListModels = _mapper.Map<ProductListModels>(products);

            return productListModels;
        }
    }
}
