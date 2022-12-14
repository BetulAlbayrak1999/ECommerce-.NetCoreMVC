using AutoMapper;
using BussinessLogicLayer.Dtos.ApplicationUserDtos;
using BussinessLogicLayer.Dtos.CategoryDtos;
using BussinessLogicLayer.Dtos.OrderDtos;
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


            CreateMap<UpdateApplicationUserRequestDto, ApplicationUser>().ReverseMap();

            CreateMap<GetApplicationUserRequestDto, ApplicationUser>().ReverseMap();

            CreateMap<GetAllApplicationUserRequestDto, ApplicationUser>().ReverseMap();

            CreateMap<GetApplicationUserRequestDto, UpdateApplicationUserRequestDto>().ReverseMap();


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

            #region Order

            CreateMap<CreateOrderRequestDto, Order>().ReverseMap();

            CreateMap<UpdateOrderRequestDto, Order>().ReverseMap();

            CreateMap<GetOrderRequestDto, Order>().ReverseMap();

            CreateMap<GetAllOrderRequestDto, Order>().ReverseMap();

            CreateMap<GetOrderRequestDto, UpdateOrderRequestDto>().ReverseMap();

            #endregion
        }

    }
}
