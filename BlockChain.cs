using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain_Bagis_Uygulamasi
{
    public class BlockChain
    {
        public IList<Block> Chain { get; set; }
        public IList<Transaction> pendindTransactions = new List<Transaction>();
        public IList<Node> nodes = new List<Node>();
        public int difficulty { get; set; } = 2;
        public int reward { get; set; } = 1;

        public BlockChain()
        {
            //InitializeChain();
            //AddGenesisBlock();
        }

        public void InitializeChain(string privateKey, string publicKey)
        {
            Chain = new List<Block>();
            nodes.Add(new Node(privateKey, publicKey));
            AddGenesisBlock();
        }

        public Block CreateGenesisBlock()
        {
            Block block = new Block(DateTime.Now, null, pendindTransactions);
            block.Mine(difficulty);
            pendindTransactions = new List<Transaction>();

            return block;
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLastBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block lastBlock = GetLastBlock();
            block.index = lastBlock.index + 1;
            block.prevHash = lastBlock.hash;
            block.hash = block.CalcHash();
            block.Mine(this.difficulty);
            Chain.Add(block);
        }

        public void CreateTransaction(Transaction transaction)
        {
            pendindTransactions.Add(transaction);
        }

        public void ProcessPendingTransactions(string minerAddress)
        {
            CreateTransaction(new Transaction(null, minerAddress, reward, "Blok Reward"));
            Block block = new Block(DateTime.Now, GetLastBlock().hash, pendindTransactions);
            AddBlock(block);
            pendindTransactions = new List<Transaction>();
        }

        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block prevBlock = Chain[i - 1];
                if(currentBlock.hash != currentBlock.CalcHash())
                {
                    return false;
                }
                if(currentBlock.prevHash != prevBlock.hash)
                {
                    return false;
                }
            }
            return true;
        }

        public string Report(/*string address*/)
        {
            int donateBalance = 0, spentBalance = 0;
            for (int i = 1; i < Chain.Count; i++)
            {
                Block block = Chain[i];
                for (int j = 0; j < block.transactions.Count; j++)
                {
                    var transaction = block.transactions[j];
                    if(transaction.fromAddress == /*address*/"Vakıf" || transaction.fromAddress == "vakıf")
                    {
                        spentBalance += transaction.amount;
                    }
                    else if(transaction.toAddress == /*address*/"Vakıf" || transaction.toAddress == "vakıf")
                    {
                        //if(transaction.fromAddress != null)//blok ödüllerini saymamak
                        donateBalance += transaction.amount;
                    }
                }
            }
            return $"Toplam alınan bağış : {donateBalance}\nToplam yapılan harcama : {spentBalance}";
        }

        public int GetBalance(/*string address*/)
        {
            int balance = 0;
            for (int i = 1; i < Chain.Count; i++)
            {
                Block block = Chain[i];
                for (int j = 0; j < block.transactions.Count; j++)
                {
                    var transaction = block.transactions[j];
                    if (transaction.fromAddress == /*address*/"Vakıf" || transaction.fromAddress == "vakıf")
                    {
                        balance -= transaction.amount;
                    }
                    else if (transaction.toAddress == /*address*/"Vakıf" || transaction.toAddress == "vakıf")
                    {
                        //if(transaction.fromAddress != null)//blok ödüllerini saymamak
                        balance += transaction.amount;
                    }
                }
            }
            return balance;
        }

        public int GetBalance(string address)
        {
            int balance = 0;
            for (int i = 1; i < Chain.Count; i++)
            {
                Block block = Chain[i];
                for (int j = 0; j < block.transactions.Count; j++)
                {
                    var transaction = block.transactions[j];
                    if (transaction.fromAddress == address)
                    {
                        balance += transaction.amount;
                    }
                }
            }
            return balance;
        }
    }
}
