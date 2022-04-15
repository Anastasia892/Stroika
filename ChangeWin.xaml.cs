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
using System.Windows.Shapes;

namespace Stroika
{
    /// <summary>
    /// Логика взаимодействия для ChangeWin.xaml
    /// </summary>
    public partial class ChangeWin : Window
    {
        Material listmaterial = new Material();
        bool Flag;
        public ChangeWin()
        {
            InitializeComponent();
            Type_CB.ItemsSource = BaseDate.DB.TypeMaterial.ToList();
            Type_CB.SelectedValuePath = "IDTypeMaterial";
            Type_CB.DisplayMemberPath = "Name";
            Postavska_LB.ItemsSource = BaseDate.DB.Supplier.ToList();
            Postavska_LB.SelectedValuePath = "IDSupplier";
            Postavska_LB.DisplayMemberPath = "NameSupplier";
            Flag = true;
        }
        public ChangeWin(Material material)
        {
            InitializeComponent();
            listmaterial = material;
            Type_CB.ItemsSource = BaseDate.DB.TypeMaterial.ToList();
            Type_CB.SelectedValuePath = "IDTypeMaterial";
            Type_CB.DisplayMemberPath = "Name";
            Postavska_LB.ItemsSource = BaseDate.DB.Supplier.ToList();
            Postavska_LB.SelectedValuePath = "IDSupplier";
            Postavska_LB.DisplayMemberPath = "NameSupplier";
            Name_TB.Text = listmaterial.Title;
            Art_TB.Text = listmaterial.Article.ToString();
            Cost_TB.Text = listmaterial.Cost.ToString();
            Descript_TB.Text = listmaterial.Description;
            Type_CB.SelectedIndex = listmaterial.TypeMaterial - 1;
            Color_TB.Text = listmaterial.Color;
            Flag = false;
        }
        private void Add_but_Click(object sender, RoutedEventArgs e)
        {
            listmaterial.Title = Name_TB.Text;
            listmaterial.Article = Convert.ToInt32(Art_TB.Text);
            listmaterial.Cost = Convert.ToInt32(Cost_TB.Text);
            listmaterial.Description = Descript_TB.Text;
            listmaterial.TypeMaterial = Type_CB.SelectedIndex + 1;
            listmaterial.Color = Color_TB.Text;
            List<MaterialSupplier> OldMaterialSupp;

            if(Flag == true)
            {
                BaseDate.DB.Material.Add(listmaterial);
            }
            foreach (Supplier a in Postavska_LB.SelectedItems)
            {
                MaterialSupplier ms = new MaterialSupplier();
                ms.IDMaterial = listmaterial.IDMaterial;
                ms.IDSupplier = a.IDSupplier;
                BaseDate.DB.MaterialSupplier.Add(ms);
            }
            BaseDate.DB.SaveChanges();
            MessageBox.Show("Запись добавленна!");
            this.Close();
        }

        private void Del_but_Click(object sender, RoutedEventArgs e)
        {
            if(Flag == false)
            {
                BaseDate.DB.Material.Remove(listmaterial);
                BaseDate.DB.SaveChanges();
                MessageBox.Show("Материал удален");
                this.Close();
            }    
        }
    }
}


