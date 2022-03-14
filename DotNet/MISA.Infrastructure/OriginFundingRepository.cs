using Microsoft.Extensions.Configuration;
using MISA.ApplicationCore.Entity;
using MISA.ApplicationCore.Interface.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Infrastructure
{
    public class OriginFundingRepository : BaseRepository<OriginFunding>, IOriginFundingRepository
    {
        #region Constructor
        public OriginFundingRepository(IConfiguration configuration) : base(configuration)
        {

        }
        #endregion


    }
}
