using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain_Bagis_Uygulamasi
{
    public class Block
    {
        public int index { get; set; }
        public DateTime timeStamp { get; set; }
        public string prevHash { get; set; }
        public string hash { get; set; }
        //public string data { get; set; }
        public IList<Transaction> transactions { get; set; }

        public int nonce { get; set; } = 0;

        public Block(DateTime timeStamp, string prevHash, IList<Transaction> transactions)
        {
            this.index = 0;
            this.timeStamp = timeStamp;
            this.prevHash = prevHash;
            //this.data = data;
            this.transactions = transactions;
            //this.hash = CalcHash();
        }

        public string CalcHash()
        {
            SHA256 sha256 = SHA256.Create();
            byte[] inBytes = Encoding.ASCII.GetBytes($"{this.timeStamp}-{this.prevHash ?? ""}-{JsonConvert.SerializeObject(transactions)}-{nonce}");
            byte[] outBytes = sha256.ComputeHash(inBytes);
            return Convert.ToBase64String(outBytes);
        }

        public void Mine(int difficulty)
        {
            var leadingZeros = new string('0', difficulty);
            while(this.hash == null || this.hash.Substring(0,difficulty) != leadingZeros)
            {
                this.nonce++;
                this.hash = this.CalcHash();

            }
        }

    }
}
