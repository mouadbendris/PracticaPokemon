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
    }
}
