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
using Microsoft.Win32;
namespace GraficadorSeñales
{
    /// <summary>
    /// Lógica de interacción para ControlAudio.xaml
    /// </summary>
    public partial class ControlAudio : UserControl
    {
        public ControlAudio()
        {
            InitializeComponent();
        }

        private void BtnElegirArchivo_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            if((bool)fileDialog.ShowDialog())
            {
                txtRutaArchivo.Text = fileDialog.FileName;
            }
        }
    }
}
