using System.Windows.Forms;

namespace HMMOD4_TASK2
{
    public partial class Form1 : Form
    {
        Mutex mutex = new Mutex();
        public Form1()
        {
            InitializeComponent();
            buttonStart.Click += ButtonStart_Click;
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            Thread thread1 = new Thread(Thread1Numbers);
            thread1.Name = "Поток 1";
            Thread thread2 = new Thread(Thread2Numbers);
            thread2.Name = "Поток 2";
            thread1.Start();
            thread2.Start();
        }

        private void Thread1Numbers()
        {
            mutex.WaitOne();
            for (int i = 0; i <= 20; i++)
            {
                Thread.Sleep(1000);
                Invoke(new Action<string>((threanName) =>
                {
                    labelInfo.Text = threanName + " начал свою работy";
                    listBoxNumber.Items.Add(i);
                }), Thread.CurrentThread.Name);
            }
            mutex.ReleaseMutex();
        }

        private void Thread2Numbers()
        {
            mutex.WaitOne();
            for (int i = 10; i >= 0; i--)
            {
                Thread.Sleep(1000);
                Invoke(new Action<string>((threanName) =>
                {
                    labelInfo.Text = "\n" + threanName + " начал свою работу";
                    listBoxNumber.Items.Add(i);
                }), Thread.CurrentThread.Name);
            }
            mutex.ReleaseMutex();
        }

        private void labelInfo_Click(object sender, EventArgs e)
        {

        }
    }
}