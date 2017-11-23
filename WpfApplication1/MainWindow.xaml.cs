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
using System.Windows.Navigation;
using System.Windows.Shapes;
using OpenGlove_API_C_Sharp_HL;
using OpenGlove_API_C_Sharp_HL.OGServiceReference;

namespace WpfApplication1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OpenGloveAPI gloves = OpenGloveAPI.GetInstance();
        private Glove selectedGlove;
        bool test;
        public MainWindow()
        {

            InitializeComponent();
            foreach(Glove g in gloves.Devices){
                if(g.Connected == true)
                {
                    selectedGlove = g;
                }
            }
            test = false;
        }

        private void testFingers(int index, int value)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                labelIndex.Content = index;
                labelValue.Content = value;
            }));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (test == false)
            {
                gloves.startCaptureData(selectedGlove);
                gloves.fingersFunction += testFingers;
                test = true;
            }else
            {
                gloves.stopCaptureData();
                gloves.fingersFunction -= testFingers;
                test = false;
            }
            
        }
    }
}
