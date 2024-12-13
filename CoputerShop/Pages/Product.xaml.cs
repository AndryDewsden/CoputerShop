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

                    b_user.IsEnabled = false;
                    b_user.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    b_add.Visibility = Visibility.Visible;
                    b_add.IsEnabled = true;
                    b_red.Visibility = Visibility.Visible;
                    b_red.IsEnabled = true;
                    b_del.Visibility = Visibility.Visible;
                    b_del.IsEnabled = true;

                    b_user.IsEnabled = true;
                    b_user.Visibility = Visibility.Visible;
                    break;
                case 3:
                    b_add.Visibility = Visibility.Visible;
                    b_add.IsEnabled = true;
                    b_red.Visibility = Visibility.Visible;
                    b_red.IsEnabled = true;
                    b_del.Visibility = Visibility.Visible;
                    b_del.IsEnabled = true;

                    b_user.IsEnabled = true;
                    b_user.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }

            //c_filter.ItemsSource = new filt[]
            //{
            //    new filt { Name_filter = "Обыкновенно"},
            //    new filt { Name_filter = "Мониторы"},
            //    new filt { Name_filter = "Системный блок"},
            //    new filt { Name_filter = ""}
            //};

            c_filter.Items.Add("");
            for (int i = 0; i < AppConnect.entities.ProductTypes.Count(); i++)
            {
                c_filter.Items.Add(AppConnect.entities.ProductTypes.ToList()[i]);
            }
            c_filter.SelectedIndex = 0;

            c_sorter.ItemsSource = new sort[]
            {
                new sort { Name_sorter = "Обыкновенно"},
                new sort { Name_sorter = "К большему по розничной"},
                new sort { Name_sorter = "К меньшему по розничной"},
                new sort { Name_sorter = "К большему по оптовой"},
                new sort { Name_sorter = "К меньшему по оптовой"}
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
                pro = pro.Where(x => x.product_type_id == c_filter.SelectedIndex).ToList();
            }

            if (c_sorter.SelectedIndex > 0)
            {
                switch (c_sorter.SelectedIndex)
                {
                    case 0:
                        break;
                    case 1:
                        pro = pro.OrderBy(x => x.product_retail_price).ToList();
                        break;
                    case 2:
                        pro = pro.OrderByDescending(x => x.product_retail_price).ToList();
                        break;
                    case 3:
                        pro = pro.OrderBy(x => x.product_wholesale_price).ToList();
                        break;
                    case 4:
                        pro = pro.OrderByDescending(x => x.product_wholesale_price).ToList();
                        break;
                    default:
                        break;
                }
            }

            return pro.ToArray();
        }

        private void b_user_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new User(user));
        }

        private void b_cart_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Cart(user));
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
            if (l_productList.SelectedItem != null)
            {
                Products product = (Products)l_productList.SelectedItem;
                var a = MessageBox.Show($"Вы дейтвительно хотите удалить этот товар?\nКод|Название: {product.id_product}:{product.product_name}", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if(a == MessageBoxResult.Yes)
                {
                    try
                    {
                        AppConnect.entities.Products.Remove(product);

                        Changelogs changelogs = new Changelogs()
                        {
                            changelog_message = $"Пользователь: {user.id_user}:{user.user_login} успешно удалил товар {product.id_product}:{product.product_name}",
                            changelog_date = DateTime.Now
                        };

                        AppConnect.entities.Changelogs.Add(changelogs);

                        AppConnect.entities.SaveChanges();
                        MessageBox.Show("Товар успешно удалён.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении данных:\n{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите удаляемый товар!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void b_exit_Click(object sender, RoutedEventArgs e)
        {
            var a = MessageBox.Show("Вы действительно хотите выйти?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (a == MessageBoxResult.Yes)
            {
                AppFrame.frameMain.Navigate(new Autorisation());
            }
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

        private void b_add_to_cart_Click(object sender, RoutedEventArgs e)
        {
            string numOrder;

            Orders userList = AppConnect.entities.Orders.FirstOrDefault(x => x.order_user_id == user.id_user && x.order_status_id != 2 && x.order_status_id != 3);

            if (userList == null)
            {
                Random r = new Random();
                numOrder = "";

                while (AppConnect.entities.Orders.Where(x => x.order_indification_number == numOrder).Count() > 0 || numOrder == "")
                {
                    numOrder = "UN0";
                    for (int i = 0; i < 10; i++)
                    {
                        int t = r.Next(0, 2);
                        switch (t)
                        {
                            case 0:
                                numOrder += Convert.ToChar(r.Next(65, 90));
                                break;
                            case 1:
                                numOrder += r.Next(0, 10).ToString();
                                break;
                        }
                    }
                    MessageBox.Show("Номер создан", "", MessageBoxButton.OK);

                    if (AppConnect.entities.Orders.Where(x => x.order_indification_number == numOrder).Count() > 0)
                    {
                        MessageBox.Show("Такой номер уже есть.", "lol", MessageBoxButton.OK);
                    }
                }

                try
                {
                    Orders userDir = new Orders()
                    {
                        order_indification_number = numOrder,
                        order_status_id = 1,
                        order_creating_date = DateTime.Now,
                        order_user_id = user.id_user
                    };
                    AppConnect.entities.Orders.Add(userDir);
                    AppConnect.entities.SaveChanges();
                    MessageBox.Show($"Новый номер сгенернирован: {numOrder}", "Тестирование", MessageBoxButton.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при внедрении данных на сервер!\n{ex.Message}", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Товару успешно присвоен номер", "Тестирование", MessageBoxButton.OK);
                numOrder = userList.order_indification_number.ToString();
            }

            var ord = (Products)l_productList.SelectedItem;
            var num = AppConnect.entities.Orders.FirstOrDefault(x => x.order_indification_number == numOrder && (x.order_status_id != 2 || x.order_status_id != 3));
            var goodOrder = AppConnect.entities.Sells.FirstOrDefault(x => x.sell_product_id == ord.id_product && x.sell_order_id == num.id_order);

            if (ord != null && goodOrder == null)
            {
                try
                {
                    Sells userOrder = new Sells()
                    {
                        sell_order_id = num.id_order,
                        sell_product_id = ord.id_product,
                        sell_product_count = 1
                    };
                    AppConnect.entities.Sells.Add(userOrder);
                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Товар успешно добавлен в корзину.", "Тестовое уведомление", MessageBoxButton.OK);

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при внедрении данных:\n{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Этот товар уже есть в вашей корзине!", "Уведомление", MessageBoxButton.OK);
            }
        }
    }
}
