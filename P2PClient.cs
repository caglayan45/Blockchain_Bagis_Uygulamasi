using System;
using System.Collections.Generic;
using WebSocketSharp;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace Blockchain_Bagis_Uygulamasi
{
    public class P2PClient
    {
        public IDictionary<string, WebSocket> wsDict = new Dictionary<string, WebSocket>();

        public void Connect(string url)
        {
            if (!wsDict.ContainsKey(url))
            {
                try
                {
                    WebSocket ws = new WebSocket(url);
                    ws.OnMessage += (sender, e) =>
                    {
                        if (e.Data[0] == '1')
                        {
                            string temp = e.Data.Substring(1, e.Data.Length-1);
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
                            }
                        }
                    };
                    ws.Connect();
                    ws.Send(Form1.Port.ToString() + JsonConvert.SerializeObject(Form1.ourBlockChain));
                    ws.Send(JsonConvert.SerializeObject(Form1.ourBlockChain));
                    wsDict.Add(url, ws);
                }
                catch(Exception e)
                {
                    
                }
            }
        }

        /*public void Send(string url, string data)
        {
            foreach (var item in wsDict)
            {
                if(item.Key == url)
                {
                    item.Value.Send(data);
                }
            }
        }*/

        public void BroadCast(string data)
        {
            foreach (var item in wsDict)
            {
                item.Value.Send(data);
            }
        }

        public IList<string> GetServers()
        {
            IList<string> servers = new List<string>();
            foreach (var item in wsDict)
            {
                servers.Add(item.Key);
            }
            return servers;
        }

        /*public void Close()
        {
            foreach (var item in wsDict)
            {
                item.Value.Close();
            }
        }*/
    }
}
