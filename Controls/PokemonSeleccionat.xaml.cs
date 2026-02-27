using Microsoft.EntityFrameworkCore;
using PracticaPokemon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PracticaPokemon.Controls
{
    public partial class PokemonSeleccionat : UserControl
    {
        AppDbContext app = new AppDbContext();
        List<Pokemon> list;   // Lista cacheada de la BD para no hacer queries constantes
        List<Pokemon> list1;  // Lista para la Fase 2 de evolución

        public PokemonSeleccionat()
        {
            InitializeComponent();
            // Cargamos las relaciones de evolución una vez al instanciar el control
            list = app.Pokemons.Include(c => c.PokemonEvolutionMatchup).ToList();
        }

        public Pokemon ThePokemon
        {
            get { return (Pokemon)GetValue(ThePokemonProperty); }
            set { SetValue(ThePokemonProperty, value); }
        }

        public static readonly DependencyProperty ThePokemonProperty =
            DependencyProperty.Register("ThePokemon", typeof(Pokemon), typeof(PokemonSeleccionat), new PropertyMetadata(null));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == ThePokemonProperty && ThePokemon != null)
            {
                pokemones.ThePokemon = ThePokemon;
                calcular();  // Daños
                calcular2(); // Hijos
                calcular3(); // Nietos
            }
        }

        // Calcula las debilidades multiplicando tipos dobles
        public void calcular()
        {
            if (ThePokemon == null) return;

            List<Models.Type> debilidades = new List<Models.Type>();
            var todosLosTipos = app.Types.ToList();
            var todasEficacias = app.TypeEfficacies.ToList();

            // Obtenemos los IDs de los tipos de nuestro Pokémon
            var misTiposIds = ThePokemon.PokemonTypes.Select(pt => pt.Type.TypeId).ToList();

            // Comparamos contra cada tipo de ataque
            foreach (var tipoAtacante in todosLosTipos)
            {
                double factorMultiplicado = 1.0;

                foreach (int miTipoId in misTiposIds)
                {
                    var eficacia = todasEficacias.FirstOrDefault(e =>
                        e.DamageTypeId == tipoAtacante.TypeId &&
                        e.TargetTypeId == miTipoId);

                    if (eficacia != null)
                    {
                        // Los datos en BD vienen como 200, 50, 0. Convertimos a multiplicador decimal.
                        factorMultiplicado *= (eficacia.DamageFactor / 100.0);
                    }
                }

                // Solo añadimos si el factor final es daño súper efectivo (x2 o x4)
                if (factorMultiplicado >= 2.0)
                {
                    debilidades.Add(tipoAtacante);
                }
            }

            itemcontroldebs.ItemsSource = debilidades;
        }

        // Calcula la Fase 2 (Hijos directos)
        private void calcular2()
        {
            list1 = new List<Pokemon>();
            if (ThePokemon == null || list == null) return;

            foreach (Pokemon p in list)
            {
                if (p.PokemonEvolutionMatchup != null && p.PokemonEvolutionMatchup.EvolvesFromSpeciesId == ThePokemon.PokId)
                {
                    list1.Add(p);
                }
            }
            evo1.ItemsSource = list1;
        }

        // Calcula la Fase 3 (Nietos)
        public void calcular3()
        {
            List<Pokemon> list2 = new List<Pokemon>();
            if (ThePokemon == null || list == null || list1 == null) return;

            foreach (Pokemon posibleNieto in list)
            {
                if (posibleNieto.PokemonEvolutionMatchup != null)
                {
                    foreach (Pokemon hijo in list1)
                    {
                        // Si el candidato evoluciona de alguno de nuestros hijos directos, es un nieto
                        if (posibleNieto.PokemonEvolutionMatchup.EvolvesFromSpeciesId == hijo.PokId)
                        {
                            list2.Add(posibleNieto);
                        }
                    }
                }
            }
            evo2.ItemsSource = list2;
        }
    }
}