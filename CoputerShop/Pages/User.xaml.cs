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
    public partial class User : Page
    {
        Users user = new Users();
        public User(Users use)
        {
            InitializeComponent();
            user = use;

            c_list_chose.Items.Add("Пользователи");
            c_list_chose.Items.Add("Заказы");
            c_list_chose.Items.Add("Корзины");
            c_list_chose.Items.Add("Логи");
        }

        Changelogs[] FindLogs()
        {
            var logs = AppConnect.entities.Changelogs.ToList();

            return logs.ToArray();
        }

        Users[] FindUsers()
        {
            var uses = AppConnect.entities.Users.ToList();

            return uses.ToArray();
        }

        Orders[] FindOrders()
        {
            var ord = AppConnect.entities.Orders.ToList();

            return ord.ToArray();
        }
        private void b_shop_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Product(user));
        }

        private void b_cart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Cart(user));
        }

        private void c_list_chose_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch(c_list_chose.SelectedIndex)
            {
                case 0:
                    s_users.IsEnabled = true;
                    s_users.Visibility = Visibility.Visible;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;

                    l_user_list.ItemsSource = FindUsers();
                    break;
                case 1:
                    s_orders.IsEnabled = true;
                    s_orders.Visibility = Visibility.Visible;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;

                    l_order_list.ItemsSource = FindOrders();
                    break;
                case 2:

                    break;
                case 3:
                    s_logs.IsEnabled = true;
                    s_logs.Visibility = Visibility.Visible;
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;

                    l_log_list.ItemsSource = FindLogs();
                    break;
                default:
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;
                    break;
            }
        }
    }
}
