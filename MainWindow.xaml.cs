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
        AppDbContext app = new AppDbContext();
        public MainWindow()
        {
            InitializeComponent();
            
            List<Pokemon> pokemons;
            pokemons = app.Pokemons.Include(c => c.PokemonAbilities).ThenInclude(ca => ca.Abil).Include(c => c.BaseStat).Include(c => c.PokemonTypes).ThenInclude(c => c.Type).ToList();
            LstPokemons.ItemsSource = pokemons.ToList();
            List<Models.Type> tipus = app.Types.ToList();
            CmbTypes.ItemsSource = tipus.ToList();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            List<Pokemon> pokemons;
            pokemons = app.Pokemons.Include(c => c.PokemonAbilities).ThenInclude(ca => ca.Abil).Include(c => c.BaseStat).Include(c => c.PokemonTypes).ThenInclude(c => c.Type).ToList();
            LstPokemons.ItemsSource = pokemons.ToList();
            
        }

        private void CmbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Models.Type tipus = (Models.Type) CmbTypes.SelectedItem;
            List<Pokemon> list = new List<Pokemon>();
            List<Pokemon> pok = app.Pokemons.ToList();
            foreach (Pokemon pokemon in pok)
            {
                for (int i = 0; pokemon.PokemonTypes.Count > i; i++)
                {
                    if(pokemon.PokemonTypes.ElementAt(i).Type.TypeId == tipus.TypeId)
                    {
                        list.Add(pokemon);
                    }
                }
            }
            LstPokemons.ItemsSource = list.ToList();
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