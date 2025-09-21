using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    [BasePath(ApiRoutes.FT06.BasePath)]
    public interface IFT06: IRepository<Guid, FT06_InterpolationTable> 
    {
    }
}
