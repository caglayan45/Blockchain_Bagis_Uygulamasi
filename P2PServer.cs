using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Blockchain_Bagis_Uygulamasi
{
    public class P2PServer : WebSocketBehavior
    {
        WebSocketServer wss = null;
        bool chainSynced = false;
        public void Start()
        {
            wss = new WebSocketServer($"ws://127.0.0.1:{Form1.Port}");
            wss.AddWebSocketService<P2PServer>("/BlockChain");
            wss.Start();
            Console.WriteLine($"Server şu adreste başlatıldı ws://127.0.0.1:{Form1.Port}");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data[0] == '5')
            {
                string tempPort = e.Data.Substring(0, 4);
                string temp = e.Data;
                temp = temp.Substring(4, temp.Length - 4);
                Console.WriteLine(temp);
                BlockChain tempChain = JsonConvert.DeserializeObject<BlockChain>(temp);
                foreach (var item in Form1.ourBlockChain.nodes)
                {
                    bool isThere = false;
                    foreach (var item1 in tempChain.nodes)
                    {
                        if (item.privateKey == item1.privateKey)
                        {
                            isThere = true;
                            break;
                        }
                    }
                    if (!isThere)
                    {
                        tempChain.nodes.Add(item);
                    }
                }
                Form1.ourBlockChain = tempChain;
                Send("1" + JsonConvert.SerializeObject(Form1.ourBlockChain));
                Form1.client.Connect($"ws://127.0.0.1:{tempPort}/BlockChain");
            }
            else
            {
                BlockChain newChain = JsonConvert.DeserializeObject<BlockChain>(e.Data);
                if (newChain.IsValid() && newChain.Chain.Count > Form1.ourBlockChain.Chain.Count)
                {
                    List<Transaction> newTransactions = new List<Transaction>();
                    newTransactions.AddRange(newChain.pendindTransactions);
                    newTransactions.AddRange(Form1.ourBlockChain.pendindTransactions);
                    newChain.pendindTransactions = newTransactions;
                    Form1.ourBlockChain = newChain;
                    chainSynced = true;
                }
            }
            if (!chainSynced)
            {
                Send(JsonConvert.SerializeObject(Form1.ourBlockChain));
                //chainSynced = true;
            }
        }
    }
}
