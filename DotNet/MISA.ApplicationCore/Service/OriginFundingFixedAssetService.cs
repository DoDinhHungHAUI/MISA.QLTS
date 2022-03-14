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
    public class OriginFundingFixedAssetService : BaseService<OriginFundingFixedAsset>, IOriginFundingFixedAssetService
    {
        #region Constructor
        public OriginFundingFixedAssetService(IOriginFundingFixedAssetRepository originFundingFixedAssetRepository) : base(originFundingFixedAssetRepository)
        {

        }

        #endregion

    }
}
