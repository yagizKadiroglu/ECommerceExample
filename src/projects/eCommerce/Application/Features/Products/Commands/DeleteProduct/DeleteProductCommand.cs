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

namespace Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<DeletedProductDto>
    {
        public int Id { get; set; }
    }

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeletedProductDto>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public DeleteProductCommandHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<DeletedProductDto> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product mappedProduct = _mapper.Map<Product>(request);
            Product? getDeletedProduct = await _productRepository.GetAsync(p => p.Id == mappedProduct.Id);
            Product deletedProduct = await _productRepository.DeleteAsync(getDeletedProduct);
            DeletedProductDto deletedProductDto = _mapper.Map<DeletedProductDto>(deletedProduct);
            return deletedProductDto;
        }
    }
}
