using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AsyncThreadDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Text = "1";
            Thread.Sleep(1000);

            button.Text = "2";
            Thread.Sleep(1000);

            button.Text = "3";
            Thread.Sleep(1000);

            button.Text = "4";
            Thread.Sleep(1000);

            button.Text = "5";
            Thread.Sleep(1000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                Button button = sender as Button;
                button.Text = "1";
                Thread.Sleep(1000);

                button.Text = "2";
                Thread.Sleep(1000);

                button.Text = "3";
                Thread.Sleep(1000);

                button.Text = "4";
                Thread.Sleep(1000);

                button.Text = "5";
                Thread.Sleep(1000);
            });
            thread.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(() =>
            {
                Button button = sender as Button;

                Action set1 = () =>
                {
                    button.Text = "1";
                };
                button.Invoke(set1);
                Thread.Sleep(1000);

                Action set2 = () =>
                {
                    button.Text = "2";
                };
                button.Invoke(set2);
                Thread.Sleep(1000);

                Action set3 = () =>
                {
                    button.Text = "3";
                };
                button.Invoke(set3);
                Thread.Sleep(1000);

                Action set4 = () =>
                {
                    button.Text = "4";
                };
                button.Invoke(set4);
                Thread.Sleep(1000);

                Action set5 = () =>
                {
                    button.Text = "5";
                };
                button.Invoke(set5);
                Thread.Sleep(1000);
            });
            thread.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            state = 1;
            button4.Text = "1";
            timer1.Start();
        }

        int state = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (state)
            {
                case 1:
                    button4.Text = "2";
                    break;
                case 2:
                    button4.Text = "3";
                    break;
                case 3:
                    button4.Text = "4";
                    break;
                case 4:
                    button4.Text = "5";
                    timer1.Stop();
                    break;
            }
            state++;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            button.Text = "1";

                var retTask = Task.Delay(1000).ContinueWith((t) =>
                {
                    button.Text = "2";
                    return Task.Delay(1000).ContinueWith((t2) =>
                    {
                        button.Text = "3";
                        throw new InvalidOperationException();
                        
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }, TaskScheduler.FromCurrentSynchronizationContext());
            
        }

        private async Task<int> ThrowAnException()
        {
            await Task.Delay(100).ConfigureAwait(false);
            throw new InvalidOperationException();
        }

        private async void button6_Click(object sender, EventArgs e)
        {
            

            Button button = sender as Button;
            button.Text = "1";
            await Task.Delay(1000);

            try
            {
                int v = await ThrowAnException();
            }
            catch (InvalidOperationException ex)
            {
                ;
            }

            button.Text = "2";
            await Task.Delay(1000);

            button.Text = "3";
            await Task.Delay(1000);

            button.Text = "4";
            await Task.Delay(1000);

            button.Text = "5";
            await Task.Delay(1000);
        }
    }
}
