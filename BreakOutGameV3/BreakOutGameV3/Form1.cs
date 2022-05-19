using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BreakOutGameV3
{
    public partial class Form1 : Form
    {
        PictureBox[] låda = new PictureBox[17];
        Random rgen = new Random();
        int bollLeft = 5, bollTop = 5, poäng = 0;
        

        public Form1()
        {
            InitializeComponent();
            //rutor
            låda[0] = pictureBox1;
            låda[1] = pictureBox2;
            låda[2] = pictureBox3;
            låda[3] = pictureBox4;
            låda[4] = pictureBox5;
            låda[5] = pictureBox6;
            låda[6] = pictureBox7;
            låda[7] = pictureBox8;
            låda[8] = pictureBox9;
            låda[9] = pictureBox10;
            låda[10] = pictureBox11;
            låda[11] = pictureBox12;
            låda[12] = pictureBox13;
            låda[13] = pictureBox14;
            låda[14] = pictureBox15;
            låda[15] = pictureBox16;

            låda[16] = pictureBox17; //boll

            //bollposition vid start
            int slumptal;
            slumptal = rgen.Next(88, 550);
            låda[16].Top = 270;
            låda[16].Left = slumptal;

            for(int i = 0; i <= 15; i++)
            {
                int red = rgen.Next(1, 256);
                int blue = rgen.Next(1, 256);
                int green = rgen.Next(1, 256);
                låda[i].BackColor = Color.FromArgb(red, blue, green);
            }

        }

        
        
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //spelare ruta följer muspekaren
            //spelarens position på x-axeln skall vara muspekarens position i "x"-led.
            //minus hälften av spelarens bredd så att muspekaren är i mitten av spelaren.
            button1.Left = e.X - (button1.Width / 2);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = "Poäng: " + poäng;
            låda[16].Left = låda[16].Left + bollLeft;
            låda[16].Top = låda[16].Top + bollTop;

            //if (vänster == true)
            //{
            //    button1.Left = button1.Left - 10;
            //}

            //if (höger == true)
            //{
            //    button1.Left = button1.Left + 10;
            //}

            //stoppar spelaren från att lämna skärmen
            //if (button1.Left < 1)
            //{
            //    vänster = false;
            //}

            //else if (button1.Left + button1.Width > 600)
            //{
            //    höger = false;
            //}

            //funktion för när bollen träffar kanterna
            if (låda[16].Left + låda[16].Width > 600 || låda[16].Left < 0)
            {
                //ändrar riktning från vägg till vägg
                bollLeft = -bollLeft;
            }

            //funktion för när boll nuddar spelaren
            if (låda[16].Top < 0 || låda[16].Bounds.IntersectsWith(button1.Bounds))
            {
                //ändrar riktning från spelare mot rutorna
                bollTop = -bollTop;
            }

            //funktion för när bollen träffar en ruta
            for (int i = 0; i <= 15; i++)
            {
                if (låda[16].Bounds.IntersectsWith(låda[i].Bounds))
                {
                    låda[i].Left = låda[i].Left + 600;
                    //ökar bollens hastighet varje träff

                    //om bollTop är negativ
                    if (bollTop < 0)
                    {
                        //inverterar för att endast öka hastigheten positivt
                        bollTop = -bollTop;
                        bollTop = bollTop + 1;
                    }
                    //om bollTop är positiv
                    else if (bollTop > 0)
                    {
                        //inverterar eftersom den är positiv från början
                        bollTop = bollTop + 1;
                        bollTop = -bollTop;
                    }
                    poäng++;
                    
                    for (int j = 0; j <= 15; j++)
                    {
                        int red = rgen.Next(1, 256);
                        int blue = rgen.Next(1, 256);
                        int green = rgen.Next(1, 256);
                        låda[j].BackColor = Color.FromArgb(red, blue, green);
                    }
                }
            }

            //Förlust
            if (låda[16].Top + låda[16].Height > 600)
            {
                DialogResult svar;
                timer1.Enabled = false;
                svar=MessageBox.Show("Du förlorade. Vill du spela igen?", "Game over", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (svar == DialogResult.Yes)
                { 
                    //Omspel
                    poäng = 0;
                    for (int i = 0; i <= 15; i++)
                    {
                        //om ruta träffats
                        if(låda[i].Left > 600)
                        {
                            låda[i].Left = låda[i].Left - 600;
                        }
                        
                    }
                    int slumptal;
                    slumptal = rgen.Next(88, 550);
                    låda[16].Top = 270;
                    låda[16].Left = slumptal;
                    bollTop = 5;
                    timer1.Enabled = true;

                }
                else if (svar == DialogResult.No)
                {
                    System.Windows.Forms.Application.Exit();
                }
            }

            //Vinst
            if (poäng == 16)
            {
                DialogResult svar;
                timer1.Enabled = false;
                label1.Text = "Poäng: " + poäng;
                svar = MessageBox.Show("Du vann spelet! Vill du spela igen?", "Game over", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (svar == DialogResult.Yes)
                {
                    //omspel
                    poäng = 0;
                    for(int i = 0; i<=15; i++)
                    {
                        låda[i].Left= låda[i].Left - 600;
                    }
                    int slumptal;
                    slumptal = rgen.Next(88, 550);
                    låda[16].Top = 270;
                    låda[16].Left = slumptal;
                    timer1.Enabled = true;
                }
                else if (svar == DialogResult.No)
                {
                    System.Windows.Forms.Application.Exit();
                }

            }

        } 
        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }

        private void KnappNedåt(object sender, KeyEventArgs e)
        {
           
        }

        private void KnappUppåt(object sender, KeyEventArgs e)
        {

        }

        private void knappupp(object sender, KeyEventArgs e)
        {
        //    if (e.KeyCode == Keys.Left)
        //    {
        //        vänster = false;
        //    }

        //    if (e.KeyCode == Keys.Right)
        //    {
        //        höger = false;
        //    }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void knappned(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Left && button1.Left > 0)
            //{
            //    vänster = true;
            //}

            //if (e.KeyCode == Keys.Right && button1.Left + button1.Width < 600)
            //{
            //    höger = true;
            //}
        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}
