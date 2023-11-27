using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ProgressBar = System.Windows.Forms.ProgressBar;

namespace Test_thread.modls
{
    internal class LineModels : Form1
    {
        private readonly ProgressBar progressBar = new ProgressBar();
        private readonly Label label1 = new Label();
        private readonly Label label2 = new Label();
        private int multiplier;
        private int value;
        private Thread CurrentThread;


        public void AddNewLine(int cnt, LineDelegate add)
        {
            Random Random = new Random();
            multiplier = Random.Next(0, 8);

// решить проблему с повторным использованием кнопаок

            label2.Location = new System.Drawing.Point(3, 41 + 6 + (25 * cnt));
            label2.AutoSize = true;
            label2.Name = "label1";
            label2.Size = new System.Drawing.Size(35, 13);
            label2.TabIndex = 4;
            //label2.Text = "Поток " + CurrentThread.ManagedThreadId;
            //label2.Text = "Поток " + CurrentThread.GetHashCode();
            label2.Text = "Поток " + cnt;

            progressBar.Location = new System.Drawing.Point(80, 41 + (25 * cnt));
            progressBar.Name = "progressBar" + cnt;
            progressBar.Size = new System.Drawing.Size(100, 23);
            progressBar.TabIndex = 4;

            label1.Location = new System.Drawing.Point(190, 41 + 6 + (25 * cnt));
            label1.AutoSize = true;
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(35, 13);
            label1.TabIndex = 4;
            label1.Text = $"х: {multiplier}";

            add(progressBar, label1, label2);

        }

        public void DeleteLine(LineDelegate del)
        {
            del(progressBar, label1, label2);
        }

        public void StartThread()
        {
            progressBar.Value = 0;
            value = 0;
            CurrentThread = new Thread(
                new ThreadStart(this.ProcessThread));
            CurrentThread.Start();
            label2.Text = "Поток " + CurrentThread.GetHashCode();
        }

        public void StopThread()
        {
            //ждем завершения потока
            //CurrentThread.Join();
            //удалить поток
            SetLabel2($"Поток {CurrentThread.ManagedThreadId} end");
            CurrentThread.Abort();
            CurrentThread.Interrupt();

        }

        public void ProcessThread()
        {
            while (true)
            {
                        if (++value == 100)
                        {
                            StopThread();
                        }
                    progressBar.Invoke((Action)(() =>
                    {
                        if(value<=100) progressBar.Value = value;
                    }));
                    Thread.Sleep(10 * multiplier);

            }
        }

        private void SetLabel2(string label)
        {
            label2.Invoke((Action)(() =>
            {
                label2.Text = label;
            }));
        }
    }
}
