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
    public partial class AddRedact : Page
    {
        private Products _curPro = new Products();
        Users user = new Users();
        public AddRedact(Users us, Products product)
        {
            InitializeComponent();
            user = us;

            c_type.Items.Add("");
            for(int i = 0; i < AppConnect.entities.ProductTypes.Count(); i++)
            {
                c_type.Items.Add(AppConnect.entities.ProductTypes.ToList()[i]);
            }

            c_creator.Items.Add("");
            for (int i = 0; i < AppConnect.entities.ProductCreators.Count(); i++)
            {
                c_creator.Items.Add(AppConnect.entities.ProductCreators.ToList()[i]);
            }

            c_seller.Items.Add("");
            for (int i = 0; i < AppConnect.entities.ProductSellers.Count(); i++)
            {
                c_seller.Items.Add(AppConnect.entities.ProductSellers.ToList()[i]);
            }

            c_status.Items.Add("");
            for (int i = 0; i < AppConnect.entities.ProductStatuses.Count(); i++)
            {
                c_status.Items.Add(AppConnect.entities.ProductStatuses.ToList()[i]);
            }

            if (product != null)
            {
                _curPro = product;
            }

            DataContext = _curPro;
        }

        private void b_user_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new User());
        }

        private void b_shop_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Product(user));
        }

        private void b_add_Click(object sender, RoutedEventArgs e)
        {
            if(t_name.Text != null && t_retail.Text != null && t_whole.Text != null && c_creator.SelectedIndex != 0 && c_seller.SelectedIndex != 0 && c_status.SelectedIndex != 0 && c_status.SelectedIndex != 0)
            {
                try
                {
                    _curPro.product_name = t_name.Text;
                    _curPro.product_description = t_desc.Text;
                    _curPro.product_image = t_image.Text;
                    _curPro.product_retail_price = Convert.ToDouble(t_retail.Text);
                    _curPro.product_wholesale_price = Convert.ToDouble(t_whole.Text);
                    _curPro.product_creator_id = AppConnect.entities.ProductCreators.FirstOrDefault(x => x.product_creator_name == c_creator.Text).id_product_creator;
                    _curPro.product_seller_id = AppConnect.entities.ProductSellers.FirstOrDefault(x => x.product_seller_name == c_seller.Text).id_product_seller;
                    _curPro.product_status_id = AppConnect.entities.ProductStatuses.FirstOrDefault(x => x.product_status_name == c_status.Text).id_product_status;
                    _curPro.product_type_id = AppConnect.entities.ProductTypes.FirstOrDefault(x => x.product_type_name == c_type.Text).id_product_type;

                    AppConnect.entities.Products.Add(_curPro);

                    //Changelogs changelogs = new Changelogs();
                    //changelogs.changelog_user_id = user.id_user;
                    //changelogs.changelog_message = $"Пользователь {user.id_user}:{user.user_login} успешно добавил товар {_curPro.id_product}:{_curPro.product_name} время: {DateTime.Now}";
                    //changelogs.changelog_date = DateTime.Now;
                    
                    //AppConnect.entities.Changelogs.Add(changelogs);

                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Товар успешно добавлен.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при вводе данных на сервер:\n" + ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните всё обязательные поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void b_red_Click(object sender, RoutedEventArgs e)
        {
            if (t_name.Text != null && t_retail.Text != null && t_whole.Text != null && c_creator.SelectedIndex != 0 && c_seller.SelectedIndex != 0 && c_status.SelectedIndex != 0 && c_status.SelectedIndex != 0)
            {
                try
                {
                    //Changelogs changelogs = new Changelogs();
                    //changelogs.changelog_user_id = user.id_user;
                    //changelogs.changelog_message = $"Пользователь {user.id_user}:{user.user_login} успешно редактировал товар {_curPro.product_name} время: {DateTime.Now}";
                    //changelogs.changelog_date = DateTime.Now;

                    //AppConnect.entities.Changelogs.Add(changelogs);

                    AppConnect.entities.SaveChanges();
                    MessageBox.Show("Товар успешно редактирован.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка при редактировании данных на сервере:\n" + ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните всё обязательные поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void t_retail_TextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void t_whole_TextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }
    }
}
