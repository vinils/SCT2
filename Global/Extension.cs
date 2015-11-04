using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public static class Extension
    {
        public static TransferIdLinkedList ToTransIdsLinkedList(this TransferInfo[] infos)
        {
            var transId = new TransferIdLinkedList();

            for(int x = 0; x<= infos.Length -1; x++)
            {
                transId.Add(infos[x].Value, infos[x].DestinyPk, x);
            }

            return transId;
        }

        public static TransferIdNode[] ToTransIdsArray(this TransferInfo[] infos)
        {
            TransferIdNode[] transIdArray = new TransferIdNode[infos.Length];
            TransferIdNode transId = null;

            for (int x = 0; x <= infos.Length - 1; x++)
            {
                transId = new TransferIdNode(transId, infos[x].Value, infos[x].DestinyPk, x);
                transIdArray[x] = transId;
            }

            return transIdArray;
        }
    }
}
