﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Sockets;
using ClientCsharp.App_Code;

namespace ClientCsharp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void buttonEnterToServer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                Connction connction = new Connction("127.0.0.1", 2212);
                labelErr.Content = "succes";
                Login log = new Login(connction);
                log.Show();
                this.Close();
            }
            catch (Exception err)
            {

                labelErr.Content = err.Message;
            }
           
        }
    }
}
