using AutoMapper;
using kd_pw_transfers_backend.Models;
using kd_pw_transfers_backend.Resources;
using System.Linq;

namespace kd_pw_transfers_backend.Services
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, RegisterModel>();
            CreateMap<RegisterModel, User>();
            CreateMap<User, UserForOthers>();
            CreateMap<UserForOthers, User>();

            CreateMap<Transfer, TransferForListing>()
                .ForMember(x => x.PayeeName, opt =>
                    opt.MapFrom(src =>
                    src.Payee.Name
                ))
                .ForMember(x => x.Balance, opt =>
                    opt.MapFrom(src =>
                    src.Payer.TransfersPayer.Where(y => y.Id <= src.Id).Sum(y => (-1) * y.Amount)
                    + src.Payer.TransfersPayee.Where(y => y.Id <= src.Id).Sum(y => y.Amount)))
            ;
        }
    }
}
