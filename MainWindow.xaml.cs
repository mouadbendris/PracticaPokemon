using Microsoft.EntityFrameworkCore;
using PracticaPokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PracticaPokemon
{
    public partial class MainWindow : Window
    {
        AppDbContext app = new AppDbContext();

        // Variable para guardar la lista completa en memoria y no saturar la BD
        private List<Pokemon> _todosLosPokemons;

        public MainWindow()
        {
            InitializeComponent();
            CargarDatosIniciales();
        }

        private void CargarDatosIniciales()
        {
            // Cargamos todos los datos necesarios de una sola vez
            _todosLosPokemons = app.Pokemons
                .Include(c => c.PokemonAbilities).ThenInclude(ca => ca.Abil)
                .Include(c => c.BaseStat)
                .Include(c => c.PokemonTypes).ThenInclude(c => c.Type)
                .ToList();

            LstPokemons.ItemsSource = _todosLosPokemons;

            // Llenamos el ComboBox de tipos
            CmbTypes.ItemsSource = app.Types.ToList();
        }

        private void BtnClear_Click(object sender, RoutedEventArgs e)
        {
            // Limpiamos los controles y restauramos la lista completa
            TxtSearch.Text = "";
            CmbTypes.SelectedItem = null;
            LstPokemons.ItemsSource = _todosLosPokemons;
        }

        private void CmbTypes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AplicarFiltros();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            AplicarFiltros();
        }

        // Método único que combina el filtro de texto y el de tipo (Lógica AND)
        private void AplicarFiltros()
        {
            if (_todosLosPokemons == null) return;

            string searchText = TxtSearch.Text.ToLower();
            Models.Type tipoSeleccionado = CmbTypes.SelectedItem as Models.Type;

            var listaFiltrada = _todosLosPokemons.Where(p =>
            {
                // 1. Filtro por Nombre o Código (Si está vacío, da true)
                bool coincideTexto = string.IsNullOrEmpty(searchText) ||
                                     p.PokName.ToLower().Contains(searchText) ||
                                     p.PokId.ToString().Contains(searchText);

                // 2. Filtro por Tipo (Si no hay nada seleccionado, da true)
                bool coincideTipo = tipoSeleccionado == null ||
                                    p.PokemonTypes.Any(pt => pt.Type.TypeId == tipoSeleccionado.TypeId);

                // 3. Devolvemos true solo si cumple AMBOS (Lógica AND)
                return coincideTexto && coincideTipo;
            }).ToList();

            LstPokemons.ItemsSource = listaFiltrada;
        }

        private void LstPokemons_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Asigna el pokemon seleccionado. Asumo que tienes el binding correcto en el XAML
            Pokemon p = (Pokemon)LstPokemons.SelectedItem;
        }
    }
}