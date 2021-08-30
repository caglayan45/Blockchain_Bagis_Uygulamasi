using Microsoft.VisualBasic;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blockchain_Bagis_Uygulamasi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static BlockChain ourBlockChain = new BlockChain();
        public static P2PClient client = new P2PClient();
        public static P2PServer server = null;
        public static string name = null;
        public static int Port = 0;
        public string tempVeri = null;

        private void btnAgKur_Click(object sender, EventArgs e)
        {
            do
            {
                name = Interaction.InputBox("İsminizi giriniz.", "Blockchain ağını başlat").ToString();
            } while (string.IsNullOrEmpty(name));

            do
            {
                tempVeri = Interaction.InputBox("Port numarası giriniz.", "Blockchain ağını başlat").ToString();
            } while (!int.TryParse(tempVeri, out Port) && Port < 0);

            ourBlockChain.InitializeChain(Port.ToString(), name);
            server = new P2PServer();
            server.Start();

            MessageBox.Show($"Giriş yapan kullanıcı : {name}", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnAgKur.Enabled = false;
            btnTransactionEkle.Enabled = btnZincir.Enabled = btnToplananBagis.Enabled = btnBagisSorgulama.Enabled = true;

            IPEndPoint[] liste = System.Net.NetworkInformation.IPGlobalProperties.GetIPGlobalProperties().GetActiveTcpListeners();
            foreach (var item in liste)
            {
                for (int i = 5000; i < 5010; i++)
                {
                    if (i != Port && item.Port == i)
                    {
                        client.Connect($"ws://127.0.0.1:{i}/BlockChain");
                    }
                }
            }

            if (Port == 5000)
            {
                btnTransactionEkle.Text = "Payment";
            }
        }

        private void btnTransactionEkle_Click(object sender, EventArgs e)
        {
            string aliciAdi = "Vakıf", aciklama = null, tempName = name;
            int miktar = -1;
            bool kontrol = false;
            DialogResult dr = DialogResult.No;

            if (name == "vakıf" || name == "Vakıf")
            {//kullanıcı vakıf ise
                aliciAdi = "";
                do
                {
                    aliciAdi = Interaction.InputBox("Alıcı adını giriniz.", "Ödeme").ToString();
                } while (string.IsNullOrEmpty(aliciAdi));

                do
                {
                    tempVeri = Interaction.InputBox("Miktarı giriniz", "Ödeme").ToString();
                } while (!int.TryParse(tempVeri, out miktar) || miktar < 0 || string.IsNullOrEmpty(tempVeri));

                do
                {
                    aciklama = Interaction.InputBox("Açıklama giriniz.", "Ödeme").ToString();
                } while (string.IsNullOrEmpty(aciklama));

                dr = MessageBox.Show($"{aliciAdi} adlı kişiye {miktar} birim para göndermek istediğinize emin misiniz ? devam etmek için Evet butonuna, iptal etmek için Hayır butonuna basınız.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    if (miktar > ourBlockChain.GetBalance())
                    {
                        MessageBox.Show("Bakiye yetersiz, işlem iptal edildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                foreach (var item in ourBlockChain.nodes)
                {
                    if (item.publicKey == name)
                    {
                        foreach (var item1 in ourBlockChain.nodes)
                        {
                            if(item1.publicKey == aliciAdi)
                            {
                                ourBlockChain.CreateTransaction(new Transaction(tempName, aliciAdi, miktar, aciklama));
                                item.Donate(miktar);
                                ourBlockChain.ProcessPendingTransactions(name);
                                client.BroadCast(JsonConvert.SerializeObject(ourBlockChain));
                                kontrol = true;
                                break;
                            }
                        }
                        break;
                    }
                }
                if(!kontrol)
                    MessageBox.Show($"{aliciAdi} kullanıcısı ağda bulunamadı, işlem iptal edildi.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else//kullanıcı vakıf değil ise
            {
                do
                {
                    tempVeri = Interaction.InputBox("Bağış yapacağınız miktarı giriniz", "Bağış yap").ToString();
                } while (!int.TryParse(tempVeri, out miktar) || miktar < 0 || string.IsNullOrEmpty(tempVeri));

                do
                {
                    aciklama = Interaction.InputBox("Açıklama giriniz.", "Bağış yap").ToString();
                } while (string.IsNullOrEmpty(aciklama));

                do
                {
                    tempVeri = Interaction.InputBox("İsminizin gözükmesini istemiyorsanız H/h harfini yazıp onatlayınız.", "Bağış yap", "E/e").ToString();
                } while (string.IsNullOrEmpty(tempVeri));

                if (tempVeri == "h" || tempVeri == "H")
                {
                    tempName = "Anonim";
                }

                dr = MessageBox.Show($"{miktar} birim para bağışlayacaksınız işleme devam etmek için Evet butonuna, iptal etmek için Hayır butonuna basınız.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.Yes)
                {
                    foreach (var item in ourBlockChain.nodes)
                    {
                        if (item.publicKey == name)
                        {
                            ourBlockChain.CreateTransaction(new Transaction(tempName, aliciAdi, miktar, aciklama));
                            item.Donate(miktar);
                            ourBlockChain.ProcessPendingTransactions(name);//mining yapan kişi
                            client.BroadCast(JsonConvert.SerializeObject(ourBlockChain));
                            break;
                        }
                    }
                }
            }
        }

        private void btnZincir_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "\n===========================================================================\n\n" + JsonConvert.SerializeObject(ourBlockChain, Formatting.Indented);
        }

        private void btnToplananBagis_Click(object sender, EventArgs e)
        {
            MessageBox.Show(ourBlockChain.Report(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IList<string> servers = client.GetServers();
            foreach (var item in servers)
            {
                MessageBox.Show(item);
            }
        }

        private void btnBagisSorgulama_Click(object sender, EventArgs e)
        {
            string isim = "";
            bool kontrol = false;

            do
            {
                isim = Interaction.InputBox("Sorgulamak istediğiniz kişinin ismini yazınız.", "Sorgulama yap").ToString();
            } while (string.IsNullOrEmpty(isim));

            if(isim == "Anonim")
            {
                MessageBox.Show($"{isim} kullanıcıların yaptığı toplam bağış : {ourBlockChain.GetBalance(isim)}", "Sorgulama", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                foreach (var item in ourBlockChain.nodes)
                {
                    if (item.publicKey == isim)
                    {
                        kontrol = true;
                        MessageBox.Show($"{isim} adlı kullanıcının yaptığı toplam bağış : {ourBlockChain.GetBalance(isim)}", "Sorgulama", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                }
            }

            if (!kontrol)
                MessageBox.Show($"{isim} kullanıcısı bulunamadı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
    }
}
