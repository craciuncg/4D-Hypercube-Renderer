using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace DimensionRenderer
{
    /*
        Author: Craciun Cristian-George
        Version: 1.0
        Description: This is the main code for rendering the 4D Hypercube using .NET library with some additional settings to it.
    */
    public partial class Form1 : Form
    {

        public Graphics gfx;
        private Pen pen = new Pen(Color.White);
        private SolidBrush brush = new SolidBrush(Color.White);

        static float angle = 0;

        private Thread thread;

        private Bitmap img;

        public bool applyrotation = false;

        float[,] projection =
        {
            {1, 0, 0 },
            {0, 1, 0 }
        };

        float Mult = 150;

        public bool rotatedX = false, rotatedY = false, rotatedZ = false, rotatedXW = false, rotatedYW = false, rotatedZW = false;

        private Graphics rg;

        public const int NR_POINTS = 16;
        public const int SIZE = 1;

        Vector4[] points = new Vector4[NR_POINTS];

        public Color backc;

        Vector2[] projected = new Vector2[NR_POINTS];

        public float MouseX, MouseY;

        public static float TRANSLATE_X, TRANSLATE_Y;

        public Form1()
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;

            points[0] = new Vector4(-SIZE, -SIZE, -SIZE, -SIZE);
            points[1] = new Vector4(SIZE, -SIZE, -SIZE, -SIZE);
            points[2] = new Vector4(SIZE, SIZE, -SIZE, -SIZE);
            points[3] = new Vector4(-SIZE, SIZE, -SIZE, -SIZE);
            points[4] = new Vector4(-SIZE, -SIZE, SIZE, -SIZE);
            points[5] = new Vector4(SIZE, -SIZE, SIZE, -SIZE);
            points[6] = new Vector4(SIZE, SIZE, SIZE, -SIZE);
            points[7] = new Vector4(-SIZE, SIZE, SIZE, -SIZE);

            points[8] = new Vector4(-SIZE, -SIZE, -SIZE, SIZE);
            points[9] = new Vector4(SIZE, -SIZE, -SIZE, SIZE);
            points[10] = new Vector4(SIZE, SIZE, -SIZE, SIZE);
            points[11] = new Vector4(-SIZE, SIZE, -SIZE, SIZE);
            points[12] = new Vector4(-SIZE, -SIZE, SIZE, SIZE);
            points[13] = new Vector4(SIZE, -SIZE, SIZE, SIZE);
            points[14] = new Vector4(SIZE, SIZE, SIZE, SIZE);
            points[15] = new Vector4(-SIZE, SIZE, SIZE, SIZE);

            InitializeComponent();

            TRANSLATE_X = pictureBox1.Width / 2;
            TRANSLATE_Y = pictureBox1.Height / 2;

            img = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gfx = Graphics.FromImage(img);
            gfx.TranslateTransform(TRANSLATE_X, TRANSLATE_Y);
            gfx.Clear(Color.Black);


            rg = pictureBox1.CreateGraphics();

            button1.BackColor = Color.White;
            button2.BackColor = Color.Black;
            button3.BackColor = Color.White;

            backc = new Color();
            backc = Color.Black;

            trackBar1.Value = (int)Mult;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            
        }

        public float Map(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
        {

            float OldRange = (OldMax - OldMin);
            float NewRange = (NewMax - NewMin);
            float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

            return (NewValue);
        }

        private void draw()
        {
            float[,] rotationX =
            {
                {1, 0, 0, 0},
                {0, cos(angle),  -sin(angle), 0},
                {0, sin(angle), cos(angle), 0},
                {0, 0, 0, 1}
            };


            float[,] rotationY =
            {
                {cos(angle),0 ,-sin(angle), 0},
                {0, 1, 0, 0},
                {sin(angle), 0, cos(angle), 0},
                {0, 0, 0, 1}
            };

            float[,] rotationZ =
            {
                {cos(angle),  -sin(angle), 0, 0},
                {sin(angle), cos(angle), 0, 0},
                {0, 0, 1, 0},
                {0 , 0, 0, 1}
            };

            float[,] rotationZW =
            {
                {1, 0, 0, 0},
                {0, 1, 0, 0}, 
                {0, 0, cos(angle), -sin(angle)},
                {0, 0, sin(angle), cos(angle)}
            };

            float[,] rotationXW =
            {
                {cos(angle), 0, 0, -sin(angle)},
                {0, 1, 0, 0},
                {0, 0, 1, 0},
                {sin(angle), 0, 0, cos(angle)}
            };

            float[,] rotationYW =
            {
                {1, 0, 0, 0},
                {0, cos(angle), 0, -sin(angle)},
                {0, 0, 1, 0},
                {0, sin(angle), 0, cos(angle)}

            };
            



            gfx.Clear(backc);
            int index = 0;
            foreach (Vector4 v in points)
            {

                Vector4 vr = v;

                if (rotatedX)
                   vr = MatMul.MatrixtoVec4(MatMul.MatrixMult(rotationX, vr));
                if (rotatedY)
                   vr = MatMul.MatrixtoVec4(MatMul.MatrixMult(rotationY, vr));
                if (rotatedZ)
                    vr = MatMul.MatrixtoVec4(MatMul.MatrixMult(rotationZ, vr));
                if (rotatedXW)
                    vr = MatMul.MatrixtoVec4(MatMul.MatrixMult(rotationXW, vr));
                if (rotatedYW)
                    vr = MatMul.MatrixtoVec4(MatMul.MatrixMult(rotationYW, vr));
                if (rotatedZW)
                    vr = MatMul.MatrixtoVec4(MatMul.MatrixMult(rotationZW, vr));


                if (applyrotation)
                {
                     float a = Map(0, pictureBox1.Width / 2, 0, 2, MouseX);
                     float[,] rotation =
                     {
                         {cos(a),0 ,-sin(a), 0},
                         {0, 1, 0, 0},
                         {sin(a), 0, cos(a), 0},
                         {0, 0, 0, 1}
                     };
                     vr = MatMul.MatrixtoVec4(MatMul.MatrixMult(rotation, vr));

                    float a1 = Map(0, pictureBox1.Height / 2, 0, 2, MouseY);
                    float[,] rotation1 =
                    {
                        {1, 0, 0, 0},
                        {0, cos(a1),  -sin(a1), 0},
                        {0, sin(a1), cos(a1), 0},
                        {0, 0, 0, 1}
                    };
                    vr = MatMul.MatrixtoVec4(MatMul.MatrixMult(rotation1, vr));
                }

                float d = 2;
                float c = 1 / (d - vr.w);


                float[,] projection3d =
                {
                    {c, 0, 0, 0},
                    {0, c, 0, 0},
                    {0, 0, c, 0}
                };

                float[,] mult =
                {
                    {Mult, 0, 0},
                    {0, Mult, 0},
                    {0, 0, Mult}
                };
                
                Vector3 v3d;
               // if (rotatedX || rotatedY || rotatedZ || rotatedXW || rotatedYW || rotatedZW)
                    v3d = MatMul.MatrixtoVec3(MatMul.MatrixMult(projection3d, vr));
               // else
                    //v3d = MatMul.MatrixtoVec3(MatMul.MatrixMult(projection3d, v));
                v3d = MatMul.MatrixtoVec3(MatMul.MatrixMult(mult, v3d));
                

                projected[index] = MatMul.MatrixtoVec2(MatMul.MatrixMult(projection, v3d));

                point(projected[index].x, projected[index].y);

                index++;
            }

            for (int i = 0; i < 4; i++)
            {
                line(projected[i], projected[(i + 1) % 4]);
                line(projected[i + 4], projected[(i + 1) % 4 + 4]);
                line(projected[i], projected[i + 4]);


                line(projected[i + 8], projected[(i + 1) % 4 + 8]);
                line(projected[i + 4 + 8], projected[(i + 1) % 4 + 4 + 8]);
                line(projected[i + 8], projected[i + 4 + 8]);

            }

            for (int i = 0; i < 8; i++)
            {
                line(projected[i], projected[i + 8]);
            }

            angle += 0.03f;
            if (angle >= 360)
                angle = 0;
            
            
        }
        

        private void point(float x, float y)
        {
            gfx.FillEllipse(brush, new RectangleF(x - 10, y - 10, 20, 20));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            rotatedX = checkBox1.Checked;
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            rotatedY = checkBox2.Checked;
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            rotatedZ = checkBox3.Checked;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            rotatedXW = checkBox4.Checked;
        }

        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            rotatedYW = checkBox6.Checked;
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            rotatedZW = checkBox5.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                pen = new Pen(cd.Color);
                button1.BackColor = cd.Color;
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                backc = cd.Color;
                button2.BackColor = cd.Color;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                brush = new SolidBrush(cd.Color);
                button3.BackColor = cd.Color;
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            thread = new Thread(() =>
            {
                while (true)
                {
                    draw();

                    rg.DrawImage(img, 0, 0);
                }
            });
            thread.Start();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            Mult = trackBar1.Value;
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            MouseX = e.X;
            MouseY = e.Y;
            applyrotation = !(rotatedX || rotatedY || rotatedZ || rotatedXW || rotatedYW || rotatedZW);
        }

        private void pictureBox1_Resize(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
        }

        private static float cos(double angle)
        {
            return (float)Math.Cos(angle);
        }

        private static float sin(double angle)
        {
            return (float)Math.Sin(angle);
        }

        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void line(Vector2 v1, Vector2 v2)
        {
            gfx.DrawLine(pen, v1.x, v1.y, v2.x, v2.y);
        }
    }
}
