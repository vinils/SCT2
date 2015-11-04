using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public static class Extension
    {
        public static TransferIdLinkedList ToTransIds(this TransferInfo[] infos)
        {
            var transId = new TransferIdLinkedList();

            foreach (var info in infos)
            {
                transId.Add(info.Value, info.DestinyPk);
            }

            return transId;
        }
    }
}
