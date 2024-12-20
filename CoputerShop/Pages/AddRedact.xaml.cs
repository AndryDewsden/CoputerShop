﻿using System;
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
            b_user.Content = user.user_name;

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
                Title = $"Редактирование";
                _curPro = product;
            }
            else
            {
                Title = $"Добавление";
                b_red.IsEnabled = false;
                b_red.Visibility = Visibility.Collapsed;
            }

            DataContext = _curPro;
        }

        public static bool ValidateAddRed(string test_product_name, int test_product_type)
        {
            if(test_product_name == "name" && test_product_type == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void b_user_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new User(user));
        }

        private void b_shop_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Product(user));
        }

        private void b_add_Click(object sender, RoutedEventArgs e)
        {
            if(t_name.Text != "" && t_retail.Text != "" && t_whole.Text != "" && c_creator.SelectedIndex != 0 && c_seller.SelectedIndex != 0 && c_status.SelectedIndex != 0 && c_status.SelectedIndex != 0)
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

                    try
                    {
                        Changelogs changelogs = new Changelogs()
                        {
                            changelog_message = $"Пользователь: {user.id_user}:{user.user_login} добавил продукт {_curPro.id_product}:{_curPro.product_name}",
                            changelog_date = DateTime.Now
                        };
                        
                        AppConnect.entities.Changelogs.Add(changelogs);

                        AppConnect.entities.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

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
            if (t_name.Text != "" && t_retail.Text != "" && t_whole.Text != "" && c_creator.SelectedIndex != 0 && c_seller.SelectedIndex != 0 && c_status.SelectedIndex != 0 && c_status.SelectedIndex != 0)
            {
                try
                {
                    try
                    {
                        Changelogs changelogs = new Changelogs()
                        {
                            changelog_message = $"Пользователь: {user.id_user}:{user.user_login} изменил продукт {_curPro.id_product}:{_curPro.product_name}",
                            changelog_date = DateTime.Now
                        };
                        
                        AppConnect.entities.Changelogs.Add(changelogs);
                        
                        AppConnect.entities.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

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

        private void CheckIsNumeric(TextCompositionEventArgs e)
        {
            int result;

            if (!(int.TryParse(e.Text, out result) || e.Text == "."))
            {
                e.Handled = true;
            }
        }

        private void t_whole_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }

        private void t_retail_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            CheckIsNumeric(e);
        }
    }
}
