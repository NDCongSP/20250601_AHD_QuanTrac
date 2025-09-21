using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    [BasePath(ApiRoutes.FT03.BasePath)]
    public interface IFT03 : IRepository<Guid, FT03>
    {
    }
}
