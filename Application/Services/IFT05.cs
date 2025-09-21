using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    [BasePath(ApiRoutes.FT05.BasePath)]
    public interface IFT05 : IRepository<Guid, FT05_ChartHoChua>
    {
    }
}
