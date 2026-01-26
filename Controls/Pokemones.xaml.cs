using Microsoft.EntityFrameworkCore;
using PracticaPokemon.Models;
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
    /// Lógica de interacción para Pokemones.xaml
    /// </summary>
    public partial class Pokemones : UserControl
    {
        AppDbContext db = new AppDbContext();
        public Pokemones()
        {
            InitializeComponent();
        }

        public Pokemon ThePokemon
        {
            get { return (Pokemon)GetValue(ThePokemonProperty); }
            set { SetValue(ThePokemonProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ThePokemon.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ThePokemonProperty =
            DependencyProperty.Register("ThePokemon", typeof(Pokemon), typeof(Pokemones), new PropertyMetadata(null));


        

        

        
    }
}
