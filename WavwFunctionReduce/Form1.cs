using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WaveFunctionReduce
{
    public partial class Form1 : Form
    {
        Mappa m;
        int righe = 100;
        int colonne = 100;
        int width, heigth;
        public Form1()
        {

            InitializeComponent();
            Size = new Size(1480, 800);
            pictureBox1.Size = new Size(1000, 1000);
            width = 1000 / righe;
            heigth = 1000 / colonne;
            //m.mappa[0][0].tail = p.pezzi[3];
            pictureBox1.Paint += PictureBox1_Paint;
            /*m.mappa[0][4].tail = p.pezzi[3];

            m.mappa[9][8].tail = p.pezzi[3];*/
            DateTime time = DateTime.Now;
            Pattern p = Pattern.GetInstance();
            p.GeneraAlberoCombinazioni(2);
            m = new Mappa(righe, colonne) { bordiVuoti = false };
            m.AggiornaMappa();

            DateTime fine = DateTime.Now;
            Console.WriteLine(fine - time);
        }

        private void PictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            disegna(g);
        }

        private void disegna(Graphics g)
        {
            Pattern p = Pattern.GetInstance();
            /*Pezzo[] possibili = p.NavigaAlbero("1?0?");
            for (int i = 0; i < possibili.Length; i++)
                g.DrawImage(possibili[i].immagine, new Rectangle(i / 10 * 64, (i % 10) * 64, 64, 64));
            */
            /*

             for(int i=0;i<p.pezzi.Length;i++)
                 g.DrawImage(p.pezzi[i].immagine, new Rectangle(i/10 * 64,(i%10)*64,64,64));
             */
             for (int i = 0; i < righe; i++)
                 for (int j = 0; j < colonne; j++)
                     if (m.mappa[i][j] != null && m.mappa[i][j].ridotto)
                         g.DrawImage(m.mappa[i][j].tail.immagine, new Rectangle(j * width,i* heigth,width,heigth));
          
        }

        public void Ridisegna()
        {
            this.Refresh();
        }
    }
}
