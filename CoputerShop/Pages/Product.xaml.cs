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
using CoputerShop.ApplicationData;

namespace CoputerShop.Pages
{
    public partial class Product : Page
    {
        Users user = new Users();
        public Product(Users us)
        {
            InitializeComponent();
            user = us;

            b_user.Content = user.user_name;
            
            switch (us.user_role_id)
            {
                case 1:
                    b_add.Visibility = Visibility.Collapsed;
                    b_add.IsEnabled = false;
                    b_red.Visibility = Visibility.Collapsed;
                    b_red.IsEnabled = false;
                    b_del.Visibility = Visibility.Collapsed;
                    b_del.IsEnabled = false;
                    break;
                case 2:
                    b_add.Visibility = Visibility.Visible;
                    b_add.IsEnabled = true;
                    b_red.Visibility = Visibility.Visible;
                    b_red.IsEnabled = true;
                    b_del.Visibility = Visibility.Visible;
                    b_del.IsEnabled = true;
                    break;
                case 3:
                    b_add.Visibility = Visibility.Visible;
                    b_add.IsEnabled = true;
                    b_red.Visibility = Visibility.Visible;
                    b_red.IsEnabled = true;
                    b_del.Visibility = Visibility.Visible;
                    b_del.IsEnabled = true;
                    break;
                default:
                    break;
            }
            
            c_filter.ItemsSource = new filt[]
            {

            };
            c_filter.SelectedIndex = 0;

            c_sorter.ItemsSource = new sort[]
            {

            };
            c_sorter.SelectedIndex = 0;

            l_productList.ItemsSource = FindProduct();
        }

        public class filt
        {
            public string Name_filter { get; set; } = "";
            public override string ToString() => $"{Name_filter}";
        }

        public class sort
        {
            public string Name_sorter { get; set; } = "";
            public override string ToString() => $"{Name_sorter}";
        }

        Products[] FindProduct()
        {
            var pro = AppConnect.entities.Products.ToList();

            if(t_searcher_t.Text != null)
            {
                pro = pro.Where(x => x.product_name.ToLower().Contains(t_searcher_t.Text.ToLower())).ToList();
            }

            if(c_filter.SelectedIndex > 0)
            {

            }

            if(c_sorter.SelectedIndex > 0)
            {

            }

            return pro.ToArray();
        }

        private void b_user_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new User());
        }

        private void b_cart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Cart());
        }

        private void b_add_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new AddRedact(user, null));
        }

        private void b_red_Click(object sender, RoutedEventArgs e)
        {
            if (l_productList.SelectedItem != null)
            {
                Products product = (Products)l_productList.SelectedItem;
                AppFrame.frameMain.Navigate(new AddRedact(user, product));
            }
            else
            {
                MessageBox.Show("Выберите редактируемый товар!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void b_del_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_france_Click(object sender, RoutedEventArgs e)
        {

        }

        private void b_exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void t_searcher_t_TextChanged(object sender, TextChangedEventArgs e)
        {
            l_productList.ItemsSource = FindProduct();
        }

        private void c_sorter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l_productList.ItemsSource = FindProduct();
        }

        private void c_filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l_productList.ItemsSource = FindProduct();
        }
    }
}
