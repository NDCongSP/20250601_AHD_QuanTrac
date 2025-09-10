using System;
using System.Collections.Generic;

namespace Domain
{
    public class RealtimeDisplays : List<RealtimeDisplayModel>
    {
        internal object FirstOrDefault(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }
    }
}
