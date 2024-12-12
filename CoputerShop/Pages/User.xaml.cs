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

            c_user_role.Items.Add("Все пользователи");
            c_user_role.Items.Add("Пользователи");
            c_user_role.Items.Add("Менеджеры");
            c_user_role.Items.Add("Администраторы");
            c_user_role.SelectedIndex = 0;

            c_user_alp.Items.Add("По очереди");
            c_user_alp.Items.Add("А-Я / A-Z");
            c_user_alp.Items.Add("Я-А / Z-A");
            c_user_alp.SelectedIndex = 0;

            c_sell_count.Items.Add("По очереди");
            c_sell_count.Items.Add("По большенству");
            c_sell_count.Items.Add("По меньшенству");
            c_sell_count.SelectedIndex = 0;

            c_order_date.Items.Add("По очереди");
            c_order_date.Items.Add("По возрастающей");
            c_order_date.Items.Add("По убывающей");
            c_order_date.SelectedIndex = 0;

            c_order_status.Items.Add("Все заказы");
            c_order_status.Items.Add("В процессе");
            c_order_status.Items.Add("Завершён");
            c_order_status.Items.Add("Заморожен");
            c_order_status.SelectedIndex = 0;

            c_log_date.Items.Add("По очереди");
            c_log_date.Items.Add("По возрастающей");
            c_log_date.Items.Add("По убывающей");
            c_log_date.SelectedIndex = 0;
        }

        Changelogs[] FindLogs()
        {
            var logs = AppConnect.entities.Changelogs.ToList();

            //if (t_searcher.Text != null)
            //{
            //    var u = AppConnect.entities.Users.Where(x => x.user_login.ToLower().Contains(t_searcher.Text.ToLower())).ToList();
            //    for (int i = 0; i < u.Count; i++)
            //    {
            //        logs = logs.Contains(x => x. == u[i]).ToList();
            //    }
            //}

            if (c_log_date.SelectedIndex > 0)
            {
                switch(c_log_date.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        logs = logs.OrderBy(x => x.changelog_date).ToList();
                        break;
                    case 2:
                        logs = logs.OrderByDescending(x => x.changelog_date).ToList();
                        break;
                    default:
                        break;
                }
            }

            return logs.ToArray();
        }

        Users[] FindUsers()
        {
            var uses = AppConnect.entities.Users.ToList();

            if (t_searcher.Text != null)
            {
                uses = uses.Where(x => x.user_login.ToLower().Contains(t_searcher.Text.ToLower()) || x.user_name.ToLower().Contains(t_searcher.Text.ToLower())).ToList();
            }

            if(c_user_alp.SelectedIndex > 0)
            {
                switch(c_user_alp.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        uses = uses.OrderBy(x => x.user_name).ToList();
                        break;
                    case 2:
                        uses = uses.OrderByDescending(x => x.user_name).ToList();
                        break;
                    default:
                        break;
                }
            }

            if(c_user_role.SelectedIndex > 0)
            {
                switch(c_user_role.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        uses = uses.Where(x => x.user_role_id == 1).ToList();
                        break;
                    case 2:
                        uses = uses.Where(x => x.user_role_id == 2).ToList();
                        break;
                    case 3:
                        uses = uses.Where(x => x.user_role_id == 3).ToList();
                        break;
                    default:
                        break;
                }
            }

            return uses.ToArray();
        }

        Orders[] FindOrders()
        {
            var ord = AppConnect.entities.Orders.ToList();

            if (t_searcher.Text != null)
            {
                ord = ord.Where(x => x.order_indification_number.ToLower().Contains(t_searcher.Text.ToLower())).ToList();
            }

            if(c_order_date.SelectedIndex > 0)
            {
                switch(c_order_date.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        ord = ord.OrderBy(x => x.order_creating_date).ToList();
                        break;
                    case 2:
                        ord = ord.OrderByDescending(x => x.order_creating_date).ToList();
                        break;
                    default:
                        break;
                }
            }

            if(c_order_status.SelectedIndex > 0)
            {
                switch(c_order_status.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        ord = ord.Where(x => x.order_status_id == 1).ToList();
                        break;
                    case 2:
                        ord = ord.Where(x => x.order_status_id == 2).ToList();
                        break;
                    case 3:
                        ord = ord.Where(x => x.order_status_id == 3).ToList();
                        break;
                    default:
                        break;
                }
            }

            return ord.ToArray();
        }

        Sells[] FindSells()
        {
            var sel = AppConnect.entities.Sells.ToList();

            //if (t_searcher.Text != null)
            //{
            //    sel = sel.Where(x => x.order_indification_number.ToLower().Contains(t_searcher.Text.ToLower())).ToList();
            //}

            if (c_sell_count.SelectedIndex > 0)
            {
                switch(c_sell_count.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        sel = sel.OrderBy(x => x.sell_product_count).ToList();
                        break;
                    case 2:
                        sel = sel.OrderByDescending(x => x.sell_product_count).ToList();
                        break;
                    default:
                        break;
                }
            }

            return sel.ToArray();
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
                    s_sells.IsEnabled = false;
                    s_sells.Visibility = Visibility.Collapsed;

                    t_searcher.Text = "";
                    
                    c_user_alp.IsEnabled = true;
                    c_user_alp.Visibility = Visibility.Visible;
                    c_user_role.IsEnabled = true;
                    c_user_role.Visibility = Visibility.Visible;
                    c_log_date.IsEnabled = false;
                    c_log_date.Visibility = Visibility.Collapsed;
                    c_order_date.IsEnabled = false;
                    c_order_date.Visibility = Visibility.Collapsed;
                    c_order_status.IsEnabled = false;
                    c_order_status.Visibility = Visibility.Collapsed;
                    c_sell_count.IsEnabled = false;
                    c_sell_count.Visibility = Visibility.Collapsed;

                    l_user_list.ItemsSource = FindUsers();
                    break;
                case 1:
                    s_orders.IsEnabled = true;
                    s_orders.Visibility = Visibility.Visible;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_sells.IsEnabled = false;
                    s_sells.Visibility = Visibility.Collapsed;

                    t_searcher.Text = "";

                    c_user_alp.IsEnabled = false;
                    c_user_alp.Visibility = Visibility.Collapsed;
                    c_user_role.IsEnabled = false;
                    c_user_role.Visibility = Visibility.Collapsed;
                    c_log_date.IsEnabled = false;
                    c_log_date.Visibility = Visibility.Collapsed;
                    c_order_date.IsEnabled = true;
                    c_order_date.Visibility = Visibility.Visible;
                    c_order_status.IsEnabled = true;
                    c_order_status.Visibility = Visibility.Visible;
                    c_sell_count.IsEnabled = false;
                    c_sell_count.Visibility = Visibility.Collapsed;

                    l_order_list.ItemsSource = FindOrders();
                    break;
                
                case 2:
                    s_sells.IsEnabled = true;
                    s_sells.Visibility = Visibility.Visible;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;

                    t_searcher.Text = "";

                    c_user_alp.IsEnabled = false;
                    c_user_alp.Visibility = Visibility.Collapsed;
                    c_user_role.IsEnabled = false;
                    c_user_role.Visibility = Visibility.Collapsed;
                    c_log_date.IsEnabled = false;
                    c_log_date.Visibility = Visibility.Visible;
                    c_order_date.IsEnabled = false;
                    c_order_date.Visibility = Visibility.Collapsed;
                    c_order_status.IsEnabled = false;
                    c_order_status.Visibility = Visibility.Collapsed;
                    c_sell_count.IsEnabled = true;
                    c_sell_count.Visibility = Visibility.Visible;

                    l_sells_list.ItemsSource = FindSells();
                    break;
                case 3:
                    s_logs.IsEnabled = true;
                    s_logs.Visibility = Visibility.Visible;
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;
                    s_sells.IsEnabled = false;
                    s_sells.Visibility = Visibility.Collapsed;

                    t_searcher.Text = "";

                    c_user_alp.IsEnabled = false;
                    c_user_alp.Visibility = Visibility.Collapsed;
                    c_user_role.IsEnabled = false;
                    c_user_role.Visibility = Visibility.Collapsed;
                    c_log_date.IsEnabled = true;
                    c_log_date.Visibility = Visibility.Visible;
                    c_order_date.IsEnabled = false;
                    c_order_date.Visibility = Visibility.Collapsed;
                    c_order_status.IsEnabled = false;
                    c_order_status.Visibility = Visibility.Collapsed;
                    c_sell_count.IsEnabled = false;
                    c_sell_count.Visibility = Visibility.Collapsed;

                    l_log_list.ItemsSource = FindLogs();
                    break;
                default:
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;
                    s_sells.IsEnabled = false;
                    s_sells.Visibility = Visibility.Collapsed;

                    t_searcher.Text = "";

                    c_user_alp.IsEnabled = false;
                    c_user_alp.Visibility = Visibility.Collapsed;
                    c_user_role.IsEnabled = false;
                    c_user_role.Visibility = Visibility.Collapsed;
                    c_log_date.IsEnabled = false;
                    c_log_date.Visibility = Visibility.Collapsed;
                    c_order_date.IsEnabled = false;
                    c_order_date.Visibility = Visibility.Collapsed;
                    c_order_status.IsEnabled = false;
                    c_order_status.Visibility = Visibility.Collapsed;
                    c_sell_count.IsEnabled = false;
                    c_sell_count.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void t_searcher_TextChanged(object sender, TextChangedEventArgs e)
        {
            switch (c_list_chose.SelectedIndex)
            {
                case 0:
                    l_user_list.ItemsSource = FindUsers();
                    break;
                case 1:
                    l_order_list.ItemsSource = FindOrders();
                    break;
                case 2:
                    l_sells_list.ItemsSource = FindSells();
                    break;
                case 3:
                    l_log_list.ItemsSource = FindLogs();
                    break;
                default:
                    break;
        }
        }

        private void c_user_role_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l_user_list.ItemsSource = FindUsers();
        }

        private void c_user_alp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l_user_list.ItemsSource = FindUsers();
        }

        private void c_order_status_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l_order_list.ItemsSource = FindOrders();
        }

        private void c_order_date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l_order_list.ItemsSource = FindOrders();
        }

        private void c_sell_count_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l_sells_list.ItemsSource = FindSells();
        }

        private void c_log_date_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            l_log_list.ItemsSource = FindLogs();
        }
    }
}
