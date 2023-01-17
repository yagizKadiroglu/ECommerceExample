using Application.Features.Products.Commands.CreateProduct;
using Application.Features.Products.Commands.DeleteProduct;
using Application.Features.Products.Commands.UpdateProduct;
using Application.Features.Products.Dtos;
using Application.Features.Products.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, CreatedProductDto>().ReverseMap();
            CreateMap<Product, CreateProductCommand>().ReverseMap();
            CreateMap<Product, DeletedProductDto>().ReverseMap();
            CreateMap<Product, DeleteProductCommand>().ReverseMap();
            CreateMap<Product, UpdatedProductDto>().ReverseMap();
            CreateMap<Product, UpdateProductCommand>().ReverseMap();

            CreateMap<IPaginate<Product>, ProductListModels>().ReverseMap();
            CreateMap<Product, ProductListDto>().ReverseMap();


        }
    }
}
