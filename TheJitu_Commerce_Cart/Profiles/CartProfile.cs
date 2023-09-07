using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TheJitu_Commerce_Cart.Models;
using TheJitu_Commerce_Cart.Models.Dtos;

namespace TheJitu_Commerce_Cart.Profiles
{
    
    public class CartProfile : Profile
    {
        public CartProfile()
        {
            CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
            CreateMap<CartDetail, CartDetailDto>().ReverseMap();
        }
    }
}