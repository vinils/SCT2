using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScroogeCoin
{
    class Program
    {
        static void Main(string[] args)
        {
            var scroogeCoin = new Authority();
            var alice = new Person();
            var bob = new Person();
            var clark = new Person();
            var david = new Signature(256);

            var coin = scroogeCoin.CreateCoin(alice.PublicKey);

            var trans1 = alice.PayTo(coin, bob.PublicKey);

            var trans2 = bob.PayTo(coin, clark.PublicKey);

            var trans3 = clark.PayTo(coin, david.PublicKey);

            var b = trans3.Previous.isValidHash();
        }
    }

}
