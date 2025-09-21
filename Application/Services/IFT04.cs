using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    [BasePath(ApiRoutes.FT04.BasePath)]
    public interface IFT04 : IRepository<Guid, FT04>
    {
    }
}
