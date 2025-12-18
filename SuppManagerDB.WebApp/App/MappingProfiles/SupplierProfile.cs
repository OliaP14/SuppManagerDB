using AutoMapper;
using SuppManagerDB.DTO;
using SuppManagerDB.WebApp.Models;

namespace SuppManagerDB.WebApp.App.MappingProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile()
        {
            CreateMap<Supplier, EditSupplierModel>()
                .ForMember(d => d.SupplierId,
                           o => o.MapFrom(s => s.SupplierID))
                .ReverseMap()
                .ForMember(d => d.SupplierID,
                           o => o.MapFrom(s => s.SupplierId));

            // Для Details 
            CreateMap<Supplier, SupplierDetailsModel>()
           .ForMember(d => d.SupplierId,
                      o => o.MapFrom(s => s.SupplierID));
        }
    }
}
