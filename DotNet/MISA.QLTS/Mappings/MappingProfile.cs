using AutoMapper;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<VoucherViewModel , Voucher>();
            CreateMap<FixedAssetViewModel, FixedAsset>();
        }
    }
}
