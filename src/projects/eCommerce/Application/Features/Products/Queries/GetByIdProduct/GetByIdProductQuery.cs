using Application.Features.Products.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Queries.GetByIdProduct
{
    public class GetByIdProductQuery : IRequest<ProductDto>
    {
        public int Id { get; set; }
    }

    public class GetByIdProductQueryHandler : IRequestHandler<GetByIdProductQuery, ProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetByIdProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ProductDto> Handle(GetByIdProductQuery request, CancellationToken cancellationToken)
        {

            Product? product = await _productRepository.GetAsync(p => p.Id == request.Id);
            ProductDto productDto = _mapper.Map<ProductDto>(product);

            return productDto;
        }
    }
}
