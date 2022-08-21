using AutoMapper;
using BussinessLogicLayer.Dtos.CategoryDtos;
using BussinessLogicLayer.Dtos.ProductDtos;
using BussinessLogicLayer.ViewModels.AuthViewModels;
using DataAccessLayer.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region ApplicationUser

            CreateMap<RegisterVM, ApplicationUser>().ReverseMap();

            CreateMap<RegisterVM, AuthVM>().ReverseMap();

            #endregion

            #region Category

            CreateMap<CreateCategoryRequestDto, Category>().ReverseMap();

            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();

            CreateMap<GetCategoryRequestDto, Category>().ReverseMap();

            CreateMap<GetAllCategoryRequestDto, Category>().ReverseMap();

            CreateMap<GetCategoryRequestDto, UpdateCategoryRequestDto>().ReverseMap();

            #endregion

            #region Product

            CreateMap<CreateProductRequestDto, Product>().ReverseMap();

            CreateMap<UpdateProductRequestDto, Product>().ReverseMap();

            CreateMap<GetProductRequestDto, Product>().ReverseMap();

            CreateMap<GetAllProductRequestDto, Product>().ReverseMap();

            CreateMap<GetProductRequestDto, UpdateProductRequestDto>().ReverseMap();


            #endregion

        }

    }
}
