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
    /// Lógica de interacción para Barra.xaml
    /// </summary>
    public partial class Barra : UserControl
    {
        public Barra()
        {
            InitializeComponent();
            rect.Width = TheBarra; rect.Height = 200;
        }


        public int TheBarra
        {
            get { return (int)GetValue(TheBarraProperty); }
            set { SetValue(TheBarraProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TheBarra.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TheBarraProperty =
            DependencyProperty.Register("TheBarra", typeof(int), typeof(Barra), new PropertyMetadata(null));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == TheBarraProperty)
            {
                int value = (int)e.NewValue;

                rect.Width = value;
            }
        }

    }
}
