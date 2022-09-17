using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Sistema_Operacional
{
    public partial class frm_Algoritmo : MetroFramework.Forms.MetroForm
    {
        private List<Processo> P;
        private List<Processo> Pa;
        private Processo Proc;
        private int Ind_Troca = 0;
        private int tempo = 3;
        public frm_Algoritmo()
        {
            InitializeComponent();
            P = new List<Processo>();
            Pa = new List<Processo>();
        }
        private void Atualizar()
        {
            int aux = 1,C=0;
            foreach (MetroControlBase item in Panel1.Controls)
            {
                if (item is MetroTextBox)
                {
                    foreach (MetroControlBase item1 in Panel1.Controls)
                    {
                        if ((item1 is MetroTextBox) && (item1.Name == "txt_Pag" + aux.ToString()))
                        {
                            item1.Text = P[C].NomeProcesso;
                            C++;
                            aux++;
                        }
                    }
                }
            }
            C = 0;
            aux = 1;
            foreach (MetroControlBase item in Panel1.Controls)
            {
                if (item is MetroTextBox)
                {
                    foreach (MetroControlBase item1 in Panel1.Controls)
                    {
                        if ((item1 is MetroTextBox) && (item1.Name == "txt_Chance" + aux.ToString()))
                        {
                            item1.Text = (P[C].Validacao-1).ToString();
                            C++;
                            aux++;
                        }
                    }
                }
            }
            richTextBox1.Text = "";
            richTextBox2.Text = "";
            foreach (Processo item in P)
            {
                richTextBox1.Text += item.NomeProcesso + "\n";
            }
            foreach (Processo item in Pa)
            {
                richTextBox2.Text += item.NomeProcesso + "\n";
            }
        }

        private void cmd_Processar_Click(object sender, EventArgs e)
        {
            if (cmd_Processar.Text == "Iniciar")
            {
                for (int i = 0; i < (int.Parse(txt_NumeroProcessos.Text)); i++)
                {
                    Proc = new Processo();
                    if (i < 5)
                    {
                        Proc.NomeProcesso = "Processo " + (i + 1).ToString();
                        P.Add(Proc);
                    }
                    else
                    {
                        Proc.NomeProcesso = "Processo " + (i + 1).ToString();
                        Pa.Add(Proc);
                    }
                }
                cmd_Processar.Text = "Parar";
                Atualizar();
                timer1.Start();
            }
            else if(cmd_Processar.Text=="Parar")
            {
                timer1.Stop();
                cmd_Processar.Text = "Continuar";
            }
            else
            {
                timer1.Start();
                cmd_Processar.Text = "Parar";
            }
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Processo Troca = new Processo();
            if (tempo == 0)
            {
                txt_ProxProcesso.Text = Pa[0].NomeProcesso;
                foreach (Processo item in Pa)
                {
                    if (item.NomeProcesso == txt_ProxProcesso.Text)
                    {
                        Troca = item;
                    }
                }
                if (P[Ind_Troca].NomeProcesso == Troca.NomeProcesso)
                {
                    Ind_Troca++;
                }
                else
                {
                    P[Ind_Troca].Validacao -= 1;
                    for (int i = 0; i < P.Count; i++)
                    {
                        if (P[i].Validacao == 0)
                        {
                            Proc = P[i];
                            Proc.Validacao = 2;
                            P.RemoveAt(i);
                            P.Insert(0, Troca);
                            Pa.RemoveAt(0);
                            Pa.Add(Proc);
                            Atualizar();
                            Ind_Troca++;
                            break;
                        }
                        else if (P[i].Validacao == 1)
                        {
                            Proc = P[i];
                            P.RemoveAt(i);
                            P.Add(Proc);
                            Atualizar();
                            break;
                        }
                    }
                }
                txt_ProxProcesso.Text = Pa[0].NomeProcesso;
                if (Ind_Troca == P.Count)
                {
                    Ind_Troca = 0;
                }
                tempo = 3;
                timer1.Start();
                Panel1.Refresh();   
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            tempo--;
            lbl_Time.Text = "00:00:0"+tempo.ToString();
            if (tempo == 0)
            {
                timer1.Stop();
            }
            Panel1.Refresh();
        }
    }
}
