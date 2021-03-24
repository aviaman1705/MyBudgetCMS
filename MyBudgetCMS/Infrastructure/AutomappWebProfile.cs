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
            CreateMap<Dashboard, DashboardDto>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.GetDate()));

            CreateMap<ExpenseDashboardItem, ExpenseDashboardItemDto>();

            //MonthlyBudget
            CreateMap<MonthlyBudgetDto, MonthlyBudget>();

            CreateMap<MonthlyBudget, MonthlyBudgetGridItemDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Budget, opt => opt.MapFrom(src => src.Budget))
            .ForMember(dest => dest.ShortDesc, opt => opt.MapFrom(src => src.ShortDesc))            
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.ShortDate()));

            CreateMap<MonthlyBudgetDto, MonthlyBudget>();

            //PaymentPerMonth
            CreateMap<PaymentPerMonth, AddPaymentPerMonthDto>();
            CreateMap<AddPaymentPerMonthDto, PaymentPerMonth>();
            CreateMap<PaymentPerMonth, PaymentGridItemDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.ActionDate))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Title))
                .ForMember(dest => dest.Sum, opt => opt.MapFrom(src => src.Sum));

            //Category
            CreateMap<Category, CategoryGridItemDto>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Title))
               .ForMember(dest => dest.TypeOfPaymentName, opt => opt.MapFrom(src => src.TypeOfPayment.Title))
               .ForMember(dest => dest.PaymentPerMonthsCount, opt => opt.MapFrom(src => src.PaymentPerMonths.Count()))
               .ForMember(dest => dest.ParentName, opt => opt.MapFrom(src => src.Parent.Title));
            CreateMap<CategorySpentItem, CategorySpentItemDto>();

            CreateMap<AddCategoryDto, Category>();
            CreateMap<Category, AddCategoryDto>();
            CreateMap<EditCategoryDto, Category>();
            CreateMap<Category, EditCategoryDto>();
        }

        public static void Run()
        {
            AutoMapper.Mapper.Initialize(a => { a.AddProfile<AutomappWebProfile>(); });
        }
    }
}