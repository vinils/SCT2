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

            for(int x = 0; x<= infos.Length -1; x++)
            {
                transId.Add(infos[x].Value, infos[x].DestinyPk, x);
            }

            return transId;
        }
    }
}
