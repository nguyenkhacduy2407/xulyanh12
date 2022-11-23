using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace videoeditdemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        Point zoom = new Point();
        Size zsize = new Size();
        Panel panel;
        PictureBox picture;
        Label label;
        private void Form1_Load(object sender, EventArgs e)
        {
            zoom = pictureBox1.Location;
            zsize = pictureBox1.Size;
            //axTimelineControl1.Size = new Size(0, 350);
            axTimelineControl1.SetScale((float)0.05);
            axTimelineControl1.SetPreviewWnd((int)pictureBox1.Handle);
            panel = panel2;
            picture = pictureBox2;
            label = label2;
            flowLayoutPanel1.Controls.Remove(panel2);
            
        }
   

        private void addvideo_btn_Click(object sender, EventArgs e)
        {
            float iduration;
            int iwidth;
            int iheight;
            float iframerate;
            int ivideobitrate;
            int iaudiobitrate;
            int iaudiochannel;
            int iaudiosamplerate;
            int ivideostreamcount;
            int iaudiostreamcount;
            string strmediacontainer;
            string strvideostramformat;
            string straudiostramformat;
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string strFile = openFileDialog1.FileName;
                string filename = Path.GetFileName(strFile);
                axTimelineControl1.GetMediaInfo(strFile, out iduration, out iwidth, out iheight, out iframerate, out ivideobitrate, out iaudiobitrate, out iaudiosamplerate, out iaudiochannel, out ivideostreamcount, out iaudiostreamcount, out strmediacontainer, out strvideostramformat, out straudiostramformat);

                axTimelineControl1.SetVideoTrackResolution(iwidth, iheight);
                MessageBox.Show(iwidth.ToString());
                axTimelineControl1.AddVideoClip(1, strFile, 0, axTimelineControl1.GetMediaDuration(strFile), 0, 2);   
                axTimelineControl1.AddAudioClip(5, strFile, 0, axTimelineControl1.GetMediaDuration(strFile), 0, (float)1.0);

                //axTimelineControl1.AddImageClip(3, strFile, 0, 8, 1);
                //axTimelineControl1.AddImageClip(4, strFile, 0, 8, 1);
                //axTimelineControl1.AddImageClip(7, strFile, 0, 8, 1);
                //axTimelineControl1.AddImageClip(6, strFile, 0, 8, 1);
                try
                {
                    guna2TrackBar1.Maximum = Convert.ToInt32((int)axTimelineControl1.GetMediaDuration(strFile));
                }
                catch
                { 
                }
                Panel pn = new Panel();
                pn = initcontrol("Video", filename);
                flowLayoutPanel1.Controls.Add(pn);
                Panel pn1 = initcontrol("Audio", filename);              
                flowLayoutPanel1.Controls.Add(pn1);
               
            }
        }

        private Panel initcontrol(string type, string name)
        {
            Panel pn = new Panel();
            pn.Size = panel.Size;
            PictureBox pc = new PictureBox();
            pc.Size = picture.Size;
            pc.Location = picture.Location;
            pc.SizeMode = PictureBoxSizeMode.Zoom;
            Label lb = new Label();
            lb.AutoSize = false;
            lb.ForeColor = Color.White;
            lb.Size = label.Size;
            lb.Location = label.Location;
            lb.TextAlign = ContentAlignment.MiddleCenter;
            lb.Text = name;
            if(type == "Video")
            {
                pc.Image = global::videoeditdemo.Properties.Resources.video;
                pn.Controls.Add(pc);
                pn.Controls.Add(lb);
            }   
            else if(type == "Image")
            {
                pc.Image = global::videoeditdemo.Properties.Resources.image__1_;
                pn.Controls.Add(pc);
                pn.Controls.Add(lb);
            }   
            else if(type == "Text")
            {
                pc.Image = global::videoeditdemo.Properties.Resources.text_font;
                pn.Controls.Add(pc);
                pn.Controls.Add(lb);
            }    
            else if(type == "Audio")
            {
                pc.Image = global::videoeditdemo.Properties.Resources.list;
                pn.Controls.Add(pc);
                pn.Controls.Add(lb);
            }
            return pn;
        }

        private void play_btn_Click(object sender, EventArgs e)    
        {
            play_btn.BackgroundImage = play_btn.Tag.ToString() == "0" ? global::videoeditdemo.Properties.Resources.play : global::videoeditdemo.Properties.Resources.pause_multimedia_big_gross_symbol_lines;
            play_btn.Tag = play_btn.Tag.ToString() == "0" ? "1" : "0";
            if(play_btn.Tag.ToString() == "0")
            {
                axTimelineControl1.Play();
                timer1.Start();
            }    
            else
            {
                axTimelineControl1.Pause();
                timer1.Stop();
            }
           
       
        }


 
        private void zoom_btn_Click(object sender, EventArgs e)
        {
            guna2TrackBar1.Value = 0;
            play_btn_Click(sender, e);
            pictureBox1.Size = zsize;
            pictureBox1.Location = zoom;
            pictureBox1.Image = null;
            axTimelineControl1.Stop();
            pictureBox1.Image = null;
            axTimelineControl1.SetPreviewWnd((int)pictureBox1.Handle);
        }

   
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (guna2TrackBar1.Value + 1 <= guna2TrackBar1.Maximum)
                guna2TrackBar1.Value += 1;
        }

        private void axTimelineControl1_Enter(object sender, EventArgs e)
        {
            if(MousePosition.Y >= 515 && MousePosition.Y <= 533)
            {
                int x = MousePosition.X;
                guna2TrackBar1.Maximum = axTimelineControl1.Width;
                guna2TrackBar1.Value = x;
                
            }    
           
        }

        private void text_btn_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowDialog();
            colorDialog1.ShowDialog();
            panel1.Visible = true;
            guna2TextBox1.Focus();
           
        }
        public uint  Color2Uint32(Color color)
        {
            int t;
            byte[] a;
            t = ColorTranslator.ToOle(color);
            a = BitConverter.GetBytes(t);
            return BitConverter.ToUInt32(a, 0);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            textinput = guna2TextBox1.Text;
            modetext = true;
            hideshowbutton();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }
        string textinput = "";
        Point textlocate = new Point(20,20);
        bool modetext = false;
        private void middleright_Click(object sender, EventArgs e)
        {
            Button b = sender as Button;
            switch(b.Text)
            {
                case "TopLeft":
                    textlocate = topleft.Location;
                    break;
                case "TopRight":
                    textlocate = topright.Location;
                    break;
                case "TopCenter":
                    textlocate = middletop.Location;
                    break;
                case "MiddleLeft":
                    textlocate = middleleft.Location;
                    break;
                case "MiddleRight":
                    textlocate = middleright.Location;
                    break;
                case "MiddleCenter":
                    textlocate = middlecenter.Location;
                    break;
                case "BottomLeft":
                    textlocate = buttomleft.Location;
                    break;
                case "BottomRight":
                    textlocate = bottomright.Location;
                    break;
                case "BottomCenter":
                    textlocate = middlebottom.Location;
                    break;
            }
            modetext = false;
            hideshowbutton();
            axTimelineControl1.AddTextClip(7, textinput, 0, 8, (int)fontDialog1.Font.ToHfont(), textlocate.X, textlocate.Y, Color2Uint32(colorDialog1.Color));
            Panel pn = initcontrol("Text", "Text");
            flowLayoutPanel1.Controls.Add(pn);
        }

        private void hideshowbutton()
        {
            if(modetext)
            {
                topleft.Visible = true;
                topright.Visible = true;
                middletop.Visible = true;
                middleleft.Visible = true;
                middleright.Visible = true;
                middlecenter.Visible = true;
                bottomright.Visible = true;
                middlebottom.Visible = true;
                buttomleft.Visible = true;

            }   
            else
            {
                topleft.Visible = false;
                topright.Visible = false;
                middletop.Visible = false;
                middleleft.Visible = false;
                middleright.Visible = false;
                middlecenter.Visible = false;
                bottomright.Visible = false;
                middlebottom.Visible = false;
                buttomleft.Visible = false;
            }    
        }
        private void button7_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Files (*.*)|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                axTimelineControl1.AddImageClip(2, openFileDialog1.FileName, 0, 20, 2);
                Panel pn = initcontrol("Image", Path.GetFileName(openFileDialog1.FileName));
                flowLayoutPanel1.Controls.Add(pn);
                //using (Image image = Image.FromFile(openFileDialog1.FileName))
                //{
                //    using (MemoryStream m = new MemoryStream())
                //    {
                //        image.Save(m, image.RawFormat);
                //        byte[] imageBytes = m.ToArray();

                //        // Convert byte[] to Base64 String
                //        string base64String = Convert.ToBase64String(imageBytes);
                //        //axTimelineControl1.AddTextClip(7, base64String, 0, 8, (int)fontDialog1.Font.ToHfont(), 0, 0, Color2Uint32(colorDialog1.Color));

                //    }
                //}


            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            guna2TrackBar1.Value = 0;
            int mid = pictureBox1.Location.X + (pictureBox1.Width / 2);
            pictureBox1.Size = new Size(440, pictureBox1.Height);
            pictureBox1.Location = new Point(mid - 220, 30);
            axTimelineControl1.Stop();
            play_btn_Click(sender, e);
            pictureBox1.Image = null;
            axTimelineControl1.SetPreviewWnd((int)pictureBox1.Handle);

        }

        private void zoomTrackBarControl1_ValueChanged(object sender, EventArgs e)
        {
            axTimelineControl1.SetScale((float)zoomTrackBarControl1.Value / 100);
        }

        private void mp4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "MP4 files (*.mp4)|*.mp4";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                axTimelineControl1.Save(saveFileDialog1.FileName);
                
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

    }
}
