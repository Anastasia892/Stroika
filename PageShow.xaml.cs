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

namespace Stroika
{
    /// <summary>
    /// Логика взаимодействия для PageShow.xaml
    /// </summary>
    public partial class PageShow : Page
    {
        List<Material> materialsStart = BaseDate.DB.Material.ToList();
        public PageShow()
        {
            InitializeComponent();
            LV.ItemsSource = materialsStart;
            CB_Filter.Items.Add("Все");
            List<TypeMaterial> tp = BaseDate.DB.TypeMaterial.ToList();
            for (int i = 0; i < tp.Count; i++)
            {
                CB_Filter.Items.Add(tp[i].Name);
            }
            CB_Filter.SelectedIndex = 0;
        }

        private void TB_Serch_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterSort();
        }

        private void CB_Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSort();
        }

        private void CB_Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterSort();
        }

        List<Material> materialsFilter = BaseDate.DB.Material.ToList();
        private void FilterSort()
        {
            if(TB_Serch.Text != String.Empty) //поиск
            {
                materialsFilter = materialsStart.Where(x => x.Title.Contains(TB_Serch.Text)).ToList();
            }
            else
            {
                materialsFilter = materialsStart;
            }

            if (CB_Filter.SelectedIndex !=0) //фильтр
            {
                materialsFilter = materialsFilter.Where(x => x.TypeMaterial == CB_Filter.SelectedIndex).ToList();
            }

            switch(CB_Sort.SelectedIndex)
            {
                case 0:
                    materialsFilter.Sort((x, y) => x.Title.CompareTo(y.Title));
                    break;
                case 1:
                    materialsFilter.Sort((x, y) => x.TypeMaterial.CompareTo(y.TypeMaterial));
                    break;
                case 2:
                    materialsFilter.Sort((x, y) => x.Title.CompareTo(y.Title));
                    materialsFilter.Reverse();
                    break;
                case 3:
                    materialsFilter.Sort((x, y) => x.TypeMaterial.CompareTo(y.TypeMaterial));
                    materialsFilter.Reverse();
                    break;
            }
            LV.ItemsSource = materialsFilter;
            LV.Items.Refresh();
        }

        private void Add_but_Click(object sender, RoutedEventArgs e)
        {
            ChangeWin win = new ChangeWin();
            win.ShowDialog();
            FrameClass.a.Navigate(new PageShow());
        }

        private void Add_but2_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            int ID = Convert.ToInt32(b.Uid);
            Material mat = BaseDate.DB.Material.FirstOrDefault(x => x.IDMaterial == ID);
            ChangeWin win = new ChangeWin(mat);
            win.ShowDialog();
            FrameClass.a.Navigate(new PageShow());
        }
    }
}
