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
    public partial class Autorisation : Page
    {
        public Autorisation()
        {
            InitializeComponent();
        }

        private void Enter_b_Click(object sender, RoutedEventArgs e)
        {
            if(login_box != null && pass_box != null)
            {
                try
                {
                    Users us = AppConnect.entities.Users.FirstOrDefault(x => x.user_login == login_box.Text && x.user_password == pass_box.Password);
                    if (us != null)
                    {
                        switch (us.user_role_id)
                        {
                            case 1:
                                MessageBox.Show("", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                                AppFrame.frameMain.Navigate(new Product(us));
                                break;
                            case 2:
                                MessageBox.Show("", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                                AppFrame.frameMain.Navigate(new Product(us));
                                break;
                            case 3:
                                MessageBox.Show("", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                                AppFrame.frameMain.Navigate(new Product(us));
                                break;
                            default:
                                MessageBox.Show("", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                                break;
                        }
                    }
                    else
                            {
                                MessageBox.Show("Такого пользователя нет!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("\n" + ex, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Reg_b_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new Registration());
        }

        private void Exit_b_Click(object sender, RoutedEventArgs e)
        {
            //App.Close();
        }
    }
}
