using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using PracticaPokemon.Models;

namespace PracticaPokemon
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AppDbContext app = new AppDbContext();
            List<Pokemon> pokemons;
            pokemons = app.Pokemons.Include(c => c.PokemonAbilities).ThenInclude(ca => ca.Abil).Include(c => c.BaseStat).Include(c => c.PokemonTypes).ThenInclude(c => c.Type).ToList();
            LstPokemons.ItemsSource = pokemons.ToList();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CmbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LstPokemons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pokemon p;
            p = (Pokemon)LstPokemons.SelectedItem;
            
        }
    }
}