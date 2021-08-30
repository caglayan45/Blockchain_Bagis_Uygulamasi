using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain_Bagis_Uygulamasi
{
    public class Transaction
    {
        public string fromAddress { get; set; }
        public string toAddress { get; set; }
        public int amount { get; set; }
        public string description { get; set; }

        public Transaction(string fromAddress, string toAddress, int amount, string description)
        {
            this.fromAddress = fromAddress;
            this.toAddress = toAddress;
            this.amount = amount;
            this.description = description;
        }
    }
}
