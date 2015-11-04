namespace Scrooge
{
    using Global;

    public static class Extensions
    {
        public static OriginLinkedList ConvertToOrigin(this IOrigin[] usrOrigins, OriginLinkedList nonExpended)
        {
            // re-create the user origin with scrooge linked origins
            OriginLinkedList linkedOrigins = new OriginLinkedList();

            foreach (var usrOrig in usrOrigins)
            {
                var nonExpendedOrig = nonExpended.Find(usrOrig.Hash.HashCode);

                TransferIdNode linkedUsrIds = null;

                foreach (var origIds in usrOrig.OriginIds)
                {
                    var nonExpendedOrigId = nonExpendedOrig.FindTransferId(origIds.Id);
                    linkedUsrIds = new TransferIdNode(linkedUsrIds, nonExpendedOrigId.Value, nonExpendedOrigId.DestinyPk, nonExpendedOrigId.Id);
                }

                linkedOrigins.Add(nonExpendedOrig.Hash, linkedUsrIds);
            }

            return linkedOrigins;
        }

        //public static Global.IOrigin[] ToGlobal(this Origin[] origins)
        //{
        //    if (origins == null)
        //        return null;

        //    var ret = new Global.IOrigin[origins.Length];

        //    for (int i = 0; i < ret.Length; i++)
        //        ret[i] = origins[i];

        //    return ret;
        //}
    }
}
