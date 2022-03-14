using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using MISA.ApplicationCore.Interface.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.ApplicationCore.Service
{
    public class OriginFundingService : BaseService<OriginFunding>, IOriginFundingService
    {
        #region Constructor
        public OriginFundingService(IOriginFundingRepository originFundingRepository) : base(originFundingRepository)
        {

        }

        #endregion

    }
}
