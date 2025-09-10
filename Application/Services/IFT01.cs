using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    [BasePath(ApiRoutes.FT01.BasePath)]
    public interface IFT01: IRepository<Guid,FT01>
    {
        [Post(ApiRoutes.FT01.DeleteLocation)]
        Task<Result<bool>> DeleteLocationAsync([Path]int? locationId);
        [Post(ApiRoutes.FT01.AddOrUpdateLocation)]
        Task<Result<LocationInfoModel>> AddOrUpdateLocationAsync([Body] LocationInfoModel location);
        
        [Get(ApiRoutes.FT01.GetConfig)]
        Task<Result<ConfigModel>> GetConfigAsync();
        
        [Post(ApiRoutes.FT01.UpdateConfig)]
        Task<Result<ConfigModel>> UpdateConfigAsync([Body] ConfigModel config);
    }
}
