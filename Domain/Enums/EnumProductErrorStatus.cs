using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum EnumProductErrorStatus
    {
        Normal = 1,
        UnknowItem = 2,
        Mistake = 3,
        Expired = 4,
        Damage = 5,
        Lost = 6
    }
}
