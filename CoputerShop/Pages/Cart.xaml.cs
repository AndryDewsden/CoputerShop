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
    public partial class Cart : Page
    {
        Users user = new Users();
        private Orders _curCart = new Orders();
        string Num;
        public Cart(Users use)
        {
            InitializeComponent();
            user = use;

            _curCart = AppConnect.entities.Orders.FirstOrDefault(x => x.order_user_id == use.id_user && x.order_status_id != 2 && x.order_status_id != 3);

            if (_curCart != null)
            {
                Num = _curCart.order_indification_number;
                l_cartList.ItemsSource = FillCart();
            }
            else
            {
                //OrderMake.IsEnabled = false;
            }
        }

        public Sells[] FillCart()
        {
            var productsInCart = AppConnect.entities.Sells.ToList();

            if (Num != null)
            {
                int num = AppConnect.entities.Orders.FirstOrDefault(x => x.order_indification_number == Num).id_order;
                productsInCart = AppConnect.entities.Sells.Where(x => x.sell_order_id == num).ToList();

                int CountGood = 0;

                for (int i = 0; i < productsInCart.Count; i++)
                {

                    CountGood += 1;
                }

                if (productsInCart.Count > 0)
                {
                    //Stat.Content = $"В вашей корзине {productsInCart.Count} товаров. Ваш номер: {Num}";
                    //OrderMake.IsEnabled = true;

                    double r = 0;
                    double w = 0;
                    Products p = new Products();

                    for (int i = 0; i < productsInCart.Count(); i++)
                    {
                        Sells c = productsInCart[i];
                        p = AppConnect.entities.Products.FirstOrDefault(x => x.id_product == c.sell_product_id);

                        r += p.product_retail_price * c.sell_product_count;
                        w += p.product_wholesale_price * c.sell_product_count;
                    }

                    l_retail_price.Content = $"Сумма по розничной цене: {r} руб.";
                    l_whole_price.Content = $"Сумма по оптовой цене: {w} руб.";
                }
                else
                {
                    //Stat.Content = "Ваша корзина пуста.";
                    //OrderMake.IsEnabled = false;
                }
            }

            return productsInCart.ToArray();
        }

        private void b_user_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new User(user));
        }

        private void b_shop_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Product(user));
        }

        private void b_del_from_cart_Click(object sender, RoutedEventArgs e)
        {
            if(l_cartList.SelectedItem != null)
            {
                try
                {
                    Sells pro = (Sells)l_cartList.SelectedItem;
                    AppConnect.entities.Sells.Remove(pro);
                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Товар успешно удалён из корзины", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при удалении данных:\n{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            l_cartList.ItemsSource = FillCart();
        }

        private void b_less_Click(object sender, RoutedEventArgs e)
        {
            if (l_cartList.SelectedItem != null)
            {
                Sells pro = (Sells)l_cartList.SelectedItem;
                if (pro.sell_product_count > 1)
                {
                    try
                    {
                        pro.sell_product_count--;

                        AppConnect.entities.SaveChanges();
                        MessageBox.Show("Товар успешно уменьшил своё количество", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при изменении данных (-):\n{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Вы не можете уменьшить минимальное количество этого товара", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            l_cartList.ItemsSource = FillCart();
        }

        private void b_more_Click(object sender, RoutedEventArgs e)
        {
            if (l_cartList.SelectedItem != null)
            {
                Sells pro = (Sells)l_cartList.SelectedItem;
                if (pro.sell_product_count < 99)
                {
                    try
                    {
                        pro.sell_product_count++;

                        AppConnect.entities.SaveChanges();
                        MessageBox.Show("Товар успешно успешно увеличил своё количество", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при изменении данных (+):\n{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Вы не можете увеличить максимальное количество этого товара", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Выберите товар", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            l_cartList.ItemsSource = FillCart();
        }

        private void b_done_Click(object sender, RoutedEventArgs e)
        {
            var r = MessageBox.Show("Вы действительно хотите завершить свой заказ?", "Уведомление", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if(r == MessageBoxResult.Yes)
            {
                try
                {
                    AppConnect.entities.Orders.Remove(_curCart);
                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Заказ успешно завершён.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("", "", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void logs_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Changelogs changelogs = new Changelogs();
                changelogs.changelog_user_id = user.id_user;
                changelogs.changelog_message = "???";
                changelogs.changelog_date = DateTime.Now;
                AppConnect.entities.Changelogs.Add(changelogs);
                AppConnect.entities.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
