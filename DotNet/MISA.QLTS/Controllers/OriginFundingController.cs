using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.QLTS.Controllers
{
    public class OriginFundingController : BaseEntitiesController<OriginFunding>
    {
        public OriginFundingController(IOriginFundingService originFundingService) : base(originFundingService)
        {
           
        }
    }
}
