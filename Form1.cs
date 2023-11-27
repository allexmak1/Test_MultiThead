using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Test_thread.modls;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Label = System.Windows.Forms.Label;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace Test_thread
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            /*//пример обработки всех кнопок
            foreach(Button button in Controls.OfType<Button>())
            {
                button.Click += (s, e) => { Process.Sta("notepad"); };
            }*/
            //пример лямбда выражения
            //button1.Clik +=(s, e) => Process.Sta("notepad");
        }

        readonly List<LineModels> listLine = new List<LineModels>();
        private int cnt = 0;

        public delegate void LineDelegate(ProgressBar progressBar, Label label1, Label label2);
        
        private void AddOnForm(ProgressBar progressBar, Label label1, Label label2)
        {
            this.Controls.Add(progressBar);
            this.Controls.Add(label1);
            this.Controls.Add(label2);
        }
        private void RemoveOnForm(ProgressBar progressBar, Label label1, Label label2)
        {
            this.Controls.Remove(progressBar);
            this.Controls.Remove(label1);
            this.Controls.Remove(label2);
        }


        private void ButtonAdd_Click(object sender, EventArgs e)
        {
            listLine.Add(new LineModels());
            listLine[cnt++].AddNewLine(cnt, AddOnForm);
        }

        private void ButtonDel_Click(object sender, EventArgs e)
        {
            if (cnt != 0)
            {
                listLine[--cnt].DeleteLine(RemoveOnForm);
                listLine[cnt].StopThread();
                listLine.RemoveAt(cnt);
            }
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            foreach (var line in listLine)
            {
                line.StartThread();
            }
        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            foreach (var line in listLine)
            {
                line.StopThread();
            }
        }


    }
}
