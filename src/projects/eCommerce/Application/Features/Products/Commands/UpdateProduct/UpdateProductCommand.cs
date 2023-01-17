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

namespace Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<UpdatedProductDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string Description { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdatedProductDto>
    {
        IProductRepository _productRepository;
        IMapper _mapper;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedProductDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product mappedProduct = _mapper.Map<Product>(request);
            Product updatedProduct = await _productRepository.UpdateAsync(mappedProduct);
            UpdatedProductDto updatedProductDto = _mapper.Map<UpdatedProductDto>(updatedProduct);

            return updatedProductDto;

        }
    }
}
