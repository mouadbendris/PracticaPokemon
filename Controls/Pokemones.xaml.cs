using PracticaPokemon.Models;
using System.Windows;
using System.Windows.Controls;

namespace PracticaPokemon.Controls
{
    public partial class Pokemones : UserControl
    {
        // Eliminado el AppDbContext db = new AppDbContext(); que bloqueaba la app

        public Pokemones()
        {
            InitializeComponent();
        }

        public Pokemon ThePokemon
        {
            get { return (Pokemon)GetValue(ThePokemonProperty); }
            set { SetValue(ThePokemonProperty, value); }
        }

        public static readonly DependencyProperty ThePokemonProperty =
            DependencyProperty.Register("ThePokemon", typeof(Pokemon), typeof(Pokemones), new PropertyMetadata(null));
    }
}