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

namespace Application.Features.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<CreatedProductDto>
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
    }

    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreatedProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<CreatedProductDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product mappedProduct = _mapper.Map<Product>(request);
            Product createdProduct = await _productRepository.AddAsync(mappedProduct);
            CreatedProductDto createdProductDto = _mapper.Map<CreatedProductDto>(createdProduct);

            return createdProductDto;
        }
    }
}
