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
    /// Lógica de interacción para PokemonSeleccionat.xaml
    /// </summary>
    public partial class PokemonSeleccionat : UserControl
    {
        AppDbContext app = new AppDbContext();
        List<Pokemon> list;
        List<Pokemon> list1;
        public PokemonSeleccionat()
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
            DependencyProperty.Register("ThePokemon", typeof(Pokemon), typeof(PokemonSeleccionat), new PropertyMetadata(null));


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if(e.Property == ThePokemonProperty){
                calcular();
                calcular2();
                calcular3();
                pokemones.ThePokemon = (Pokemon)GetValue(ThePokemonProperty);
            }

            
        }

        public void calcular()
        {

            List<Models.Type> list = new List<Models.Type>();
            List<Models.Type> list2 = new List<Models.Type>();
            List<Models.Type> list3 = new List<Models.Type>();
            List<Models.TypeEfficacy> list4 = new List<Models.TypeEfficacy>();
            List<int> list5 = new List<int>();
            
            list2 = app.Types.ToList();
            list4 = app.TypeEfficacies.ToList();
            int? id;
            if (ThePokemon != null)
            {
                for (int i = 0; i < ThePokemon.PokemonTypes.Count; i++)
                {
                    id = ThePokemon.PokemonTypes.ElementAt(i).Type.DamageTypeId;

                    for (int j = 0; j < list2.Count; j++)
                    {
                        if (id == list2[j].TypeId)
                        {
                            list3.Add(list2[j]);
                        }
                    }
                }

                for (int i = 0;i < list3.Count; i++)
                {
                    for(int j = 0;j < list4.Count; j++)
                    {
                        if (list3[i].DamageTypeId == list4[j].DamageTypeId && list4[j].DamageFactor != 100)
                        {
                            list5.Add(list4[j].TargetTypeId);
                        }
                    }
                }

                for(int i = 0; i < list5.Count; i++)
                {
                    for (int j = 0; j < list2.Count; j++)
                    {
                        if (list5[i] == list2[j].TypeId ) {
                            list.Add(list2[j]);
                        }
                    }
                }

            }
            itemcontroldebs.ItemsSource = list;
        }

        private void calcular2()
        {
            if (ThePokemon == null)
            {
                return;
            }
            list = new List<Pokemon>();
            list1 = new List<Pokemon>();
            list = app.Pokemons.Include(c => c.PokemonEvolutionMatchup).ThenInclude(c => c.PokemonEvolutions).Include(c=>c.PokemonTypes).ThenInclude(c=>c.Type).ToList();
            foreach (Pokemon pokemon in list)
            {
                if (pokemon.PokemonEvolutionMatchup != null)
                {
                    if (pokemon.PokemonEvolutionMatchup.EvolvesFromSpeciesId == ThePokemon.PokId)
                    {
                        list1.Add(pokemon);
                    }
                }
            }
            evo1.ItemsSource = list1;
        }

        public void calcular3()
        {
            List<Pokemon> list2 = new List<Pokemon>();

            if (ThePokemon == null)
            {
                return;
            }

            foreach (Pokemon pokemon in list)
            {
                if(pokemon.PokemonEvolutionMatchup != null)
                {
                    if(ThePokemon.PokId == pokemon.PokemonEvolutionMatchup.EvolvesFromSpeciesId)
                    {
                        //tengo el pokemon evo2 que es pokemon y tengo pokemon principal
                        foreach (Pokemon pokemon1 in list1)
                        {
                            //recorro la lista con los hijos que tiene el evo1 para saber el id
                            foreach(Pokemon pokemon2 in list)
                            {
                                if(pokemon2.PokemonEvolutionMatchup != null)
                                {
                                    if(pokemon2.PokemonEvolutionMatchup.EvolvesFromSpeciesId == pokemon1.PokId)
                                    {
                                        list2.Add(pokemon2);
                                    }
                                }
                            }
                        }
                    }
                    
                }
            }
            evo2.ItemsSource = list2;
        }
    }
}
