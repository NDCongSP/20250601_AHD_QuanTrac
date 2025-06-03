using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public enum EnumReturnOrderStatus
    {
        Open = 0,
        Receiving = 1,
        Received = 2,
        Putaway = 3,
        Completed = 4
    }
}
