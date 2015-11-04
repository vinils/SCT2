using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Global
{
    public class SerializedUserSignedTrans : Serialized
    {
        public SerializedUserSignedTrans(UserSignedTrans userSgndTrans)
            : base(userSgndTrans)
        {
        }

        public UserSignedTrans DeserializeTransfer()
        {
            return (UserSignedTrans)DeserializeObject(this.SerializedObj);
        }
    }
}
