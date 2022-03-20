using AutoMapper;
using RetailManagement.Shared.Product;
using RetailManagementService.DataContext.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace RetailManagementService.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RetailProduct, Product>();
        }
    }
}
