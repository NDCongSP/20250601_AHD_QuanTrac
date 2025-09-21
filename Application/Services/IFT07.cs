using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    [BasePath(ApiRoutes.FT07.BasePath)]
    public interface IFT07 : IRepository<Guid, FT07_ChartMucNuoc>
    {
    }
}
