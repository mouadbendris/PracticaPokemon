using Microsoft.EntityFrameworkCore;
using PracticaPokemon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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
            CmbTypes.SelectedItem = null;
        }

        private void CmbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CmbTypes.SelectedItem == null)
            {
                return;
            }
            String name = TxtSearch.Text.ToLower();

            Models.Type tipus = (Models.Type) CmbTypes.SelectedItem;
            List<Pokemon> list = new List<Pokemon>();
            List<Pokemon> pok = app.Pokemons.ToList();
            
            foreach (Pokemon pokemon in pok)
            {
                for (int i = 0; pokemon.PokemonTypes.Count > i; i++)
                {
                    if(pokemon.PokemonTypes.ElementAt(i).Type.TypeId == tipus.TypeId)
                    {
                        if (pokemon.PokName.Contains(name))
                        {
                            list.Add(pokemon);
                        }
                    }
                }
            }
            LstPokemons.ItemsSource = list.ToList();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            String nom = TxtSearch.Text.ToLower();
            Models.Type tipus = CmbTypes.SelectedItem as Models.Type;
            List<Pokemon> list = new List<Pokemon>();
            ObservableCollection<Pokemon> list1 = new ObservableCollection<Pokemon>();
            list = app.Pokemons.Include(x=>x.PokemonTypes).ThenInclude(x=>x.Type).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].PokemonTypes.Count; j++) {

                    if (tipus == list[i].PokemonTypes.ElementAt(j).Type)
                    {
                        if (list[i].PokName.Contains(nom))
                        {
                            list1.Add(list[i]);
                        }
                    }
                }
            }

            LstPokemons.ItemsSource = list1.ToList();
        }

        private void LstPokemons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Pokemon p;
            p = (Pokemon)LstPokemons.SelectedItem;
            
        }
    }
}