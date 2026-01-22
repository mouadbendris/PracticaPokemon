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

namespace PracticaPokemon.Controls
{
    /// <summary>
    /// Lógica de interacción para Tipus.xaml
    /// </summary>
    public partial class Tipus : UserControl
    {
        public Tipus()
        {
            InitializeComponent();
        }

        public PracticaPokemon.Models.Type TheType
        {
            get { return (PracticaPokemon.Models.Type)GetValue(TheTypeProperty); }
            set { SetValue(TheTypeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheType.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheTypeProperty =
            DependencyProperty.Register("TheType", typeof(PracticaPokemon.Models.Type), typeof(Tipus), new PropertyMetadata(null));


    }
}
