using AutoMapper;
using TheJitu_Commerce_Products.Models;
using TheJitu_Commerce_Products.Models.Dtos;

namespace TheJitu_Commerce_Products.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<ProductsRequestDto, Product>().ReverseMap();
        }
    }
}
