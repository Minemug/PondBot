using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;
using Point = System.Drawing.Point;

namespace MinigameBot
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
        MouseOperations MouseOperation = new MouseOperations();
        [DllImportAttribute("User32.dll")]
        private static extern int FindWindow(String ClassName, String WindowName);
        [DllImportAttribute("User32.dll")]
        private static extern IntPtr SetForegroundWindow(int hWnd);

        bool StartGameButtonFound = false;
        int count = 0;        
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int hWnd = FindWindow(null, "NosTale");
            //System.Windows.MessageBox.Show($"{ hWnd}");
            if (hWnd == 0)
            {
                System.Windows.MessageBox.Show("Nie znalazłem NosTale");

            }
            SetForegroundWindow(hWnd);
            Thread.Sleep(500);
            Bitmap screenshot = Screenshot();
            Screenshot();
            Color StartGame = screenshot.GetPixel((int)218, 302);
            if (StartGame.R == 56)
            {
                MouseOperations.SetCursorPosition(950, 760);
                StartGameButtonFound = true;
                MouseOperations.MouseClick();
            }
            else
            {
                MessageBox.Show("nie znalazlem przycisku start");
            }
            Thread.Sleep(3000);
            while (StartGameButtonFound == true)
            {               
                Bitmap screenshotthegame = Screenshot();
                Screenshot();               
                Color Fishleft = screenshotthegame.GetPixel(157, 84);
                Color Fishdown = screenshotthegame.GetPixel(301, 157);
                Color Fishup = screenshotthegame.GetPixel(308, 21);
                Color Fishright = screenshotthegame.GetPixel(458, 82);
                if (Fishleft.R == 198)
                {
                    Color trapleft = screenshotthegame.GetPixel(142, 93);
                    if (trapleft.B < 60)
                    {
                        goto trapleft;
                    }
                    else
                    {
                        SendKeys.SendWait("{LEFT}");
                        Thread.Sleep(300);
                    }
                }
                trapleft:
                if (Fishdown.R == 198)
                {
                    Color trapdown = screenshotthegame.GetPixel(285, 164);
                    if(trapdown.B < 60)
                    {
                        goto trapdown;
                    }
                    else
                    {
                        SendKeys.SendWait("{DOWN}");
                        Thread.Sleep(300);
                    }
                    
                }
                trapdown:
                if (Fishup.R == 198)
                {
                    Color trapup = screenshotthegame.GetPixel(332, 32);
                    if (trapup.B < 60)
                    {
                        goto trapup;
                    }
                    else
                    {
                        SendKeys.SendWait("{UP}");
                        Thread.Sleep(300);
                    }
                }
                trapup:
                if (Fishright.R == 198)
                {
                    Color trapright = screenshotthegame.GetPixel(485, 96);
                    if (trapright.B < 60)
                    {
                        goto trapright;
                    }
                    else
                    {
                        SendKeys.SendWait("{RIGHT}");
                        Thread.Sleep(300);
                    }
                    
                }
                trapright:
                Color Fishbar = screenshotthegame.GetPixel(24, 112);
                if (Fishbar.R < 50)
                {                   
                        Moveonthebar(screenshotthegame);                
                }
                count++;
                if (count % 20 == 0)
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                }
                
            }
            
        }
        
        private void Moveonthebar(Bitmap screenshot)
        {
            int arrdw1x = 105;
            int arrdw1y = 117;
            int arrdw2x = 115;
            int arrdw2y = 99;
            Color arrowdown1 = screenshot.GetPixel(arrdw1x, arrdw1y);
            Color arrowdown2 = screenshot.GetPixel(arrdw2x, arrdw2y);
            if(arrowdown1.G < 20 && arrowdown2.G < 20)
            {
                SendKeys.SendWait("{DOWN}");                
            }
            else
            {
                Color arrowup1 = screenshot.GetPixel(104, 109);
                Color arrowup2 = screenshot.GetPixel(115, 127);
                if (arrowup1.G < 20 && arrowup2.G < 20)
                {
                    SendKeys.SendWait("{UP}");                    
                }
                else
                {
                    Color arrowleft1 = screenshot.GetPixel(110, 104);
                    Color arrowleft2 = screenshot.GetPixel(129, 113);
                    if (arrowleft1.G < 20 && arrowleft2.G < 20)
                    {
                        SendKeys.SendWait("{LEFT}");                       
                    }
                    else
                    {
                        Color arrowright1 = screenshot.GetPixel(100, 114);
                        Color arrowright2 = screenshot.GetPixel(119, 104);
                        if (arrowright1.G < 20 && arrowright2.G < 20)
                        {
                            SendKeys.SendWait("{right}");                            
                        }
                    }
                }
            }
            Thread.Sleep(5000);
        }

        private Bitmap Screenshot()
        {
                     
            Rectangle rect = new Rectangle(658, 455, 600, 330);
            Bitmap bmpScreeenshot = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(bmpScreeenshot);
            g.CopyFromScreen(rect.Left, rect.Top, 0, 0, bmpScreeenshot.Size, CopyPixelOperation.SourceCopy);
            //bmpScreeenshot.Save("C:/tmp/Pond.jpeg", ImageFormat.Jpeg);

            //returns the screenshot
            return bmpScreeenshot;

        }                  
    }
}
