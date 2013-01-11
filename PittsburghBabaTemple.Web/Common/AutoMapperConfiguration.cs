using System;
using AutoMapper;
//using PittsburghBabaTemple.Core.Constants;
using PittsburghBabaTemple.Core.Interfaces;
using PittsburghBabaTemple.Core.Model;
//using PittsburghBabaTemple.Web.Dtos.FactSheet;
//using PittsburghBabaTemple.Web.Dtos.PreAward;
//using PittsburghBabaTemple.Web.ViewModels.Account;
//using PittsburghBabaTemple.Web.ViewModels.Admin;
//using PittsburghBabaTemple.Web.ViewModels.Administration;
//using PittsburghBabaTemple.Web.ViewModels.FactSheet;
//using PittsburghBabaTemple.Web.ViewModels.Help;
//using PittsburghBabaTemple.Web.ViewModels.PreAward;

namespace PittsburghBabaTemple.Web.Common
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            //Mapper.CreateMap<ManageAccountViewModel, IUserAccount>();
            //Mapper.CreateMap<IUserAccount, ManageAccountViewModel>();
            //Mapper.CreateMap<AccountRequestViewModel, AccountRequest>()
            //    .ForMember(t => t.ZipCode, opt => opt.MapFrom(m => m.ZipCode ?? ""))
            //    .ForMember(t => t.PhoneNumber, opt => opt.MapFrom(m => m.PhoneNumber ?? ""))
            //    .ForMember(t => t.RequestedAt, opt => opt.MapFrom(m => DateTime.Now))
            //    .ForMember(t => t.Status, opt => opt.MapFrom(m => AccountRequestStatus.Pending));
            //Mapper.CreateMap<RequestRoleChangeViewModel, RoleChangeRequest>()
            //    .ForMember(t => t.User, opt => opt.MapFrom(m => User.Find(m.UserId)))
            //    .ForMember(t => t.RequestedAt, opt => opt.MapFrom(m => DateTime.Now))
            //    .ForMember(t => t.Status, opt => opt.MapFrom(m => String.IsNullOrEmpty(m.Status) ? AccountRequestStatus.Pending : m.Status));
            //Mapper.CreateMap<RoleChangeRequest, RequestRoleChangeViewModel>()
            //    .ForMember(t => t.UserId, opt => opt.MapFrom(m => m.User.Id));
            //Mapper.CreateMap<EditRoleChangeRequestViewModel, RoleChangeRequest>();
            ////Mapper.CreateMap<AnnouncementViewModel, Announcement>();
            //Mapper.CreateMap<Announcement, AnnouncementViewModel>();
        }
    }

    public class CostFormatter : ValueFormatter<decimal?>
    {
        protected override string FormatValueCore(decimal? value)
        {
            if (!value.HasValue) return "";
            return String.Format("{0:c0}", value.Value);
        }
    }

    public class PercentFormatter : ValueFormatter<decimal?>
    {
        public string Format(decimal? val)
        {
            return FormatValueCore(val);
        }

        protected override string FormatValueCore(decimal? value)
        {
            if (!value.HasValue) return "";
            return String.Format("{0:P0}", value.Value / 100);
        }
    }
}