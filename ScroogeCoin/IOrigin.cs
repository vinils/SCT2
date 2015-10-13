using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    public interface IOrigin
    {
        ITransferHash Hash { get; }
        IOriginId[] OriginId { get; }
    }
}
