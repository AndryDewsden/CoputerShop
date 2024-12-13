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
        Users _curUser = new Users();
        Orders _curOrder = new Orders();

        public User(Users use)
        {
            InitializeComponent();
            user = use;
            Title = $"Личная страница {use.user_name}";

            c_list_chose.Items.Add("Заказы");
            c_list_chose.Items.Add("Корзины");
            c_list_chose.Items.Add("Логи");

            c_user_role.Items.Add("Все пользователи");
            c_user_role.Items.Add("Пользователи");
            c_user_role.Items.Add("Менеджеры");
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

            c_log_date.Items.Add("Старые");
            c_log_date.Items.Add("Новые");
            c_log_date.SelectedIndex = 0;

            for (int i = 0; i < AppConnect.entities.UserRoles.Count(); i++)
            {
                var ap = AppConnect.entities.UserRoles.ToList();

                if (ap[i].id_user_role != 3)
                {
                    t_user_role.Items.Add(ap[i]);
                }
            }

            for (int i = 0; i < AppConnect.entities.OrderStatuses.Count(); i++)
            {
                var status = AppConnect.entities.OrderStatuses.ToList();
                c_order_status_panel.Items.Add(status[i]);
            }

            switch (user.user_role_id)
            {
                case 1:
                    c_list_chose.IsEnabled = false;
                    c_list_chose.Visibility = Visibility.Collapsed;
                    t_searcher.IsEnabled = false;
                    t_searcher.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    c_list_chose.IsEnabled = true;
                    c_list_chose.Visibility = Visibility.Visible;
                    t_searcher.IsEnabled = true;
                    t_searcher.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    c_list_chose.IsEnabled = true;
                    c_list_chose.Visibility = Visibility.Visible;
                    t_searcher.IsEnabled = true;
                    t_searcher.Visibility = Visibility.Visible;
                    c_list_chose.Items.Add("Пользователи");
                    break;
                default:
                    break;
            }
        }

        Changelogs[] FindLogs()
        {
            var logs = AppConnect.entities.Changelogs.ToList();

            if (t_searcher.Text != null)
            {
                logs = logs.Where(x => x.changelog_message.ToLower().Contains(t_searcher.Text.ToLower())).ToList();
            }

            if (c_log_date.SelectedIndex > 0)
            {
                switch(c_log_date.SelectedIndex)
                {
                    case 0:
                        logs = logs.OrderBy(x => x.changelog_date).ToList();
                        break;
                    case 1:
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
            var uses = AppConnect.entities.Users.Where(x => x.user_role_id != 3 && x.user_login != user.user_login).ToList();

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
                    s_orders.IsEnabled = true;
                    s_orders.Visibility = Visibility.Visible;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_sells.IsEnabled = false;
                    s_sells.Visibility = Visibility.Collapsed;

                    t_searcher.IsEnabled = true;
                    t_searcher.Visibility = Visibility.Visible;
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
                
                case 1:
                    s_sells.IsEnabled = true;
                    s_sells.Visibility = Visibility.Visible;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;

                    t_searcher.IsEnabled = false;
                    t_searcher.Visibility = Visibility.Collapsed;
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
                    c_sell_count.IsEnabled = true;
                    c_sell_count.Visibility = Visibility.Visible;

                    l_sells_list.ItemsSource = FindSells();
                    break;
                case 2:
                    s_logs.IsEnabled = true;
                    s_logs.Visibility = Visibility.Visible;
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;
                    s_sells.IsEnabled = false;
                    s_sells.Visibility = Visibility.Collapsed;

                    t_searcher.IsEnabled = true;
                    t_searcher.Visibility = Visibility.Visible;
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
                case 3:
                    s_users.IsEnabled = true;
                    s_users.Visibility = Visibility.Visible;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;
                    s_sells.IsEnabled = false;
                    s_sells.Visibility = Visibility.Collapsed;

                    t_searcher.IsEnabled = true;
                    t_searcher.Visibility = Visibility.Visible;
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
                default:
                    s_users.IsEnabled = false;
                    s_users.Visibility = Visibility.Collapsed;
                    s_logs.IsEnabled = false;
                    s_logs.Visibility = Visibility.Collapsed;
                    s_orders.IsEnabled = false;
                    s_orders.Visibility = Visibility.Collapsed;
                    s_sells.IsEnabled = false;
                    s_sells.Visibility = Visibility.Collapsed;

                    t_searcher.IsEnabled = false;
                    t_searcher.Visibility = Visibility.Collapsed;
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
                    l_order_list.ItemsSource = FindOrders();
                    DataContext = _curOrder;
                    break;
                case 1:
                    l_sells_list.ItemsSource = FindSells();
                    break;
                case 2:
                    l_log_list.ItemsSource = FindLogs();
                    break;
                case 3:
                    l_user_list.ItemsSource = FindUsers();
                    DataContext = _curUser;
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

        private void b_exit_Click(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("Вы действительно хотите выйти?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (a == MessageBoxResult.Yes)
            {
                AppFrame.frameMain.Navigate(new Autorisation());
            }
        }

        private void b_user_add_Click(object sender, RoutedEventArgs e)
        {
            if (t_user_name.Text != null && t_user_login.Text != null && t_user_password.Text != null && t_user_role.SelectedIndex != -1)
            {
                _curUser.user_name = t_user_name.Text;
                _curUser.user_login = t_user_login.Text;
                _curUser.user_password = t_user_password.Text;
                _curUser.user_role_id = t_user_role.SelectedIndex +1;

                try
                {
                    AppConnect.entities.Users.Add(_curUser);

                    Changelogs changelogs = new Changelogs()
                    {
                        changelog_message = $"Пользователь: {user.id_user}:{user.user_login} успешно добавил нового пользователя: {_curUser.id_user}:{_curUser.user_login}",
                        changelog_date = DateTime.Now
                    };
                    AppConnect.entities.Changelogs.Add(changelogs);

                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Пользователь успешно добавлен.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    l_user_list.ItemsSource = FindUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void b_user_red_Click(object sender, RoutedEventArgs e)
        {
            if (t_user_name.Text != null && t_user_login.Text != null && t_user_password.Text != null && t_user_role.SelectedIndex != -1)
            {
                _curUser.user_name = t_user_name.Text;
                _curUser.user_login = t_user_login.Text;
                _curUser.user_password = t_user_password.Text;
                _curUser.user_role_id = t_user_role.SelectedIndex + 1;

                try
                {
                    Changelogs changelogs = new Changelogs()
                    {
                        changelog_message = $"Пользователь: {user.id_user}:{user.user_login} успешно редактировал пользователя: {_curUser.id_user}:{_curUser.user_login}",
                        changelog_date = DateTime.Now
                    };
                    AppConnect.entities.Changelogs.Add(changelogs);

                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Пользователь успешно редактирован.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    l_user_list.ItemsSource = FindUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void b_user_del_Click(object sender, RoutedEventArgs e)
        {
            var r = MessageBox.Show("Вы действительно хотите удалить этого пользователя? Вместе с пользователем удаляться и все связанные с ним данные.", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (r == MessageBoxResult.Yes)
            {
                _curUser = (Users)l_user_list.SelectedItem;
                try
                {
                    var a = AppConnect.entities.Orders.Where(x => x.order_user_id == _curUser.id_user).ToList();
                    for (int i = 0; i < a.Count(); i++)
                    {
                        var b = AppConnect.entities.Sells.Where(x => x.sell_order_id == a[i].id_order).ToList();
                        for(int j = 0; j < b.Count(); j++)
                        {
                            AppConnect.entities.Sells.Remove(b[j]);
                        }
                        AppConnect.entities.Orders.Remove(a[i]);
                    }
                    AppConnect.entities.Users.Remove(_curUser);

                    Changelogs changelogs = new Changelogs()
                    {
                        changelog_message = $"Пользователь: {user.id_user}:{user.user_login} успешно удалил пользователя: {_curUser.id_user}:{_curUser.user_login}",
                        changelog_date = DateTime.Now
                    };
                    AppConnect.entities.Changelogs.Add(changelogs);

                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Пользователь успешно удалён.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    l_user_list.ItemsSource = FindUsers();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void l_user_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (l_user_list.SelectedItem != null)
            {
                b_user_del.IsEnabled = true;
                _curUser = (Users)l_user_list.SelectedItem;

                t_user_name.Text = _curUser.user_name;
                t_user_login.Text = _curUser.user_login;
                t_user_password.Text = _curUser.user_password;
                t_user_role.SelectedIndex = _curUser.user_role_id - 1;
            }
            else
            {
                b_user_del.IsEnabled = false;
            }
        }

        private void l_order_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (l_order_list.SelectedItem != null)
            {
                b_order_red.IsEnabled = true;
                b_order_del.IsEnabled = true;
                _curOrder = (Orders)l_order_list.SelectedItem;

                l_user_order.Content = AppConnect.entities.Users.FirstOrDefault(x => x.id_user == _curOrder.order_user_id).user_name;
                t_order_num.Text = _curOrder.order_indification_number;
                d_order_start.SelectedDate = _curOrder.order_creating_date;
                d_order_end.SelectedDate = _curOrder.order_clossing_date;
                c_order_status_panel.SelectedIndex = _curOrder.order_status_id - 1;
            }
            else
            {
                l_user_order.Content = "";
                t_order_num.Text = "";
                d_order_start.SelectedDate = null;
                d_order_end.SelectedDate = null;
                c_order_status_panel.SelectedIndex = -1;

                b_order_red.IsEnabled = false;
                b_order_del.IsEnabled = false;
            }
        }

        private void b_order_del_Click(object sender, RoutedEventArgs e)
        {
            var r = MessageBox.Show("Вы действительно хотите удалить этот заказ? Вся связанная информация о нём будет удаленна", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (r == MessageBoxResult.Yes)
            {
                _curOrder = (Orders)l_order_list.SelectedItem;
                try
                {
                    var delSells = AppConnect.entities.Sells.Where(x => x.sell_order_id == _curOrder.id_order).ToList();
                    for(int i = 0; i < delSells.Count(); i++)
                    {
                        AppConnect.entities.Sells.Remove(delSells[i]);
                    }

                    AppConnect.entities.Orders.Remove(_curOrder);

                    Changelogs changelogs = new Changelogs()
                    {
                        changelog_message = $"Пользователь: {user.id_user}:{user.user_login} успешно удалил заказ: {_curOrder.id_order}:{_curOrder.order_indification_number}",
                        changelog_date = DateTime.Now
                    };
                    AppConnect.entities.Changelogs.Add(changelogs);

                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Заказ со всеми данными успешно удалён.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    l_order_list.ItemsSource = FindOrders();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void b_order_red_Click(object sender, RoutedEventArgs e)
        {
            if (t_order_num.Text != null && d_order_start.Text != null && c_order_status_panel.SelectedIndex != -1)
            {
                var a = MessageBox.Show("Вы действительно хотите редактировать этот заказ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (a == MessageBoxResult.Yes)
                {
                    _curOrder.order_indification_number = t_order_num.Text;
                    _curOrder.order_creating_date = (DateTime)d_order_start.SelectedDate;
                    _curOrder.order_clossing_date = (DateTime)d_order_end.SelectedDate;
                    _curOrder.order_status_id = c_order_status_panel.SelectedIndex + 1;

                    try
                    {
                        Changelogs changelogs = new Changelogs()
                        {
                            changelog_message = $"Пользователь: {user.id_user}:{user.user_login} успешно редактировал заказ: {_curOrder.id_order}:{_curOrder.order_indification_number}",
                            changelog_date = DateTime.Now
                        };
                        AppConnect.entities.Changelogs.Add(changelogs);

                        AppConnect.entities.SaveChanges();
                        MessageBox.Show("Заказ успешно редактирован.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                        l_order_list.ItemsSource = FindOrders();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
