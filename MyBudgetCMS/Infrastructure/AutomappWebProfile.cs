using AutoMapper;
using MyBudgetCMS.Models.Dto;
using MyBudgetCMS.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBudgetCMS.Infrastructure
{
    public class AutomappWebProfile : Profile
    {
        public AutomappWebProfile()
        {
            //MonthlyBudget
            CreateMap<MonthlyBudget, MonthlyBudgetGridItemDto>()
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.ShortDate()));

            //PaymentPerMonth
            CreateMap<EditPaymentPerMonthDto, PaymentPerMonth>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ActionDate, opt => opt.MapFrom(src => src.ActionDate))
                .ForMember(dest => dest.CategoryID, opt => opt.MapFrom(src => src.CategoryID))
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));
            CreateMap<AddPaymentPerMonthDto, PaymentPerMonth>();
            CreateMap<PaymentPerMonth, PaymentGridItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.ActionDate))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Title));

            //Category
            CreateMap<Category, CategoryGridItemDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.TypeOfPaymentName, opt => opt.MapFrom(src => src.TypeOfPayment.Title))
               .ForMember(dest => dest.PaymentPerMonthsCount, opt => opt.MapFrom(src => src.PaymentPerMonths.Count()))
               .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Title));            
            CreateMap<AddCategoryDto, Category>();            
            CreateMap<EditCategoryDto, Category>();            

            //User
            CreateMap<UserNavPartialDto, User>();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a => { a.AddProfile<AutomappWebProfile>(); });
        }
    }
}