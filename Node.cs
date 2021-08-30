using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain_Bagis_Uygulamasi
{
    public class Node
    {
        public string privateKey { get; set; }
        public string publicKey { get; set; }
        //public int balance { get; set; }
        public int allDonateAmount = 0;

        public Node(string privateKey, string publicKey)
        {
            this.privateKey = privateKey;
            this.publicKey = publicKey;
        }

        public void Donate(int amount)
        {
            allDonateAmount += amount;
        }

    }
}
