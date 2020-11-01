using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using ClientCsharp.App_Code;
using System.Runtime.CompilerServices;

namespace ClientCsharp
{
    /// <summary>
    /// Interaction logic for BoardcastView.xaml
    /// </summary>
    public partial class BoardcastView : Window 
    {
        private readonly Object m_lock; 
        private bool isAlivel;
        private Connction server;
        private UserDetails user;
        private string message;

        public BoardcastView(Connction server, UserDetails user)
        {
            InitializeComponent();
           
            this.server = server;
            this.user = user;
            this.isAlivel = true;
            m_lock = new Object();
            Thread thred1 = new Thread(GetBordCast);
            thred1.Start();
            message = "";
            
          
        }

       

        public void ShowNewMessage(string message)
        {
        this.Dispatcher.Invoke(() =>
            {
            textBoxChat.Clear();
            TextBlock textBlock = new TextBlock(new Run(message));
            textBlock.FontSize = 20;
            textBlock.TextAlignment = TextAlignment.Left;
            ViewContainer.Children.Add(textBlock);
            });
        }
        public void GetBordCast()
        {
            Monitor.Enter(m_lock); // Acquire a mutual-exclusive lock
                                 // While under the lock, test the complex condition "atomically"
            while (isAlivel)
            {
                    try
                    {
                         string recv = server.Recv();
                        string[] aswer = recv.Split('¨');
                    if (aswer[0] == "Boardcast")
                        ShowNewMessage(aswer[1]);
                    else
                    {
                        message = aswer[1];
                        Monitor.Pulse(m_lock);
                        Monitor.Wait(m_lock);
                    }   

                }
                    catch {
                    throw;
                }
                Thread.Sleep(100);
            }
            
            Monitor.Exit(m_lock);
        }
        public void Thread2()
        {
            Monitor.Enter(m_lock); // Acquire a mutual-exclusive lock
                                   // Process data and modify the condition..
            // Monitor.Pulse(m_lock); // Wakes one waiter AFTER lock is released
            Monitor.PulseAll(m_lock); // Wakes all waiters AFTER lock is released
            Monitor.Exit(m_lock); // Release lock
        }
        [MethodImpl(MethodImplOptions.Synchronized)]
        public String Recv()
        {
            Monitor.Enter(m_lock);
            if(message == "")
            Monitor.Wait(m_lock);
            string send = message;
            message = "";
            Monitor.Pulse(m_lock);
            Monitor.Exit(m_lock); // Release lock
            return send;
        }
        private void buttonSend_Click(object sender, RoutedEventArgs e)
        {
            string message = textBoxChat.Text;
            server.Send("Boardcast@" + user.UserName+": "+message + "\n");
        }
    }
}

