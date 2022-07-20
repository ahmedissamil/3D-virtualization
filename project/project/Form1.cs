using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace project
{
    public partial class Form1 : Form
    {
        Bitmap off;

        _3D_Model Cube = new _3D_Model();

        Camera cam = new Camera();


        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.Load += new EventHandler(Form1_Load);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);

        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Right:
                    Cube.TransX(5);
                    break;
                case Keys.Left:
                    Cube.TransX(-5);
                    break;

                case Keys.Up:
                    Cube.TransY(-5);
                    break;
                case Keys.Down:
                    Cube.TransY(5);
                    break;

                case Keys.A:
                    Cube.TransZ(5);
                    break;
                case Keys.S:
                    Cube.TransZ(-5);
                    break;

                case Keys.Space:
                    Cube.RotateAroundEdge2(2, 1);
                    break;

                case Keys.M:
                    Cube.RotateAroundEdge(2, 1);
                    break;

            }

            DrawDubble(this.CreateGraphics());
        }

        void CreateCube(_3D_Model M, float XS, float YS, float ZS, Color vvv)
        {
            float[] vert =
                            {
                                    300,300,300,
                                    300,300,500,
                                    100,300,500,
                                    100,300,300,
                                    300,100,300,
                                    300,100,500,
                                    100,100,500,
                                    100,100,300

                            };


            _3D_Point pnn;
            int j = 0;
            for (int i = 0; i < 8; i++)
            {
                pnn = new _3D_Point(vert[j] + XS, vert[j + 1] + YS, vert[j + 2] + ZS);
                j += 3;
                M.AddPoint(pnn);
            }


            int[] Edges = {
                                0,1,
                                1,2,
                                2,3,
                                3,0,
                                4,5,
                                5,6,
                                6,7,
                                7,4,
                                0,4,
                                3,7,
                                2,6,
                                1,5
                          };
            j = 0;
            //Color[] cl = { Color.Red, Color.Yellow, Color.Black, Color.Blue };
            for (int i = 0; i < 12; i++)
            {
                M.AddEdge(Edges[j], Edges[j + 1], vvv); //cl[i % 4]);

                j += 2;
            }
        }

        void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            int cx = 900;
            int cy = 900;
            cam.ceneterX = (this.ClientSize.Width / 2);
            cam.ceneterY = (this.ClientSize.Height / 2);
            cam.cxScreen = cx;
            cam.cyScreen = cy;
            cam.BuildNewSystem();

            Cube.cam = cam;
            CreateCube(Cube, 0, 0, 0, Color.Red);

        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            DrawDubble(e.Graphics);
        }

        void DrawScene(Graphics g)
        {
            g.Clear(Color.White);
            Cube.DrawYourSelf(g);
        }

        void DrawDubble(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            DrawScene(g2);
            g.DrawImage(off, 0, 0);
        }
    }
}
