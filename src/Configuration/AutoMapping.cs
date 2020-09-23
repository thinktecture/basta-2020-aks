using System;
using AutoMapper;
using Thinktecture.AKS.Sample.Entities;
using Thinktecture.AKS.Sample.Models;

namespace Thinktecture.AKS.Sample.Configuration
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Product, ProductListItem>();
            CreateMap<Product, ProductDetailsItem>();
            CreateMap<NewProductItem, Product>();
            CreateMap<UpdateProductItem, Product>();

        }
    }
}
