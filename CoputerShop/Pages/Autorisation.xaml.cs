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

        public static bool ValidateAuto(string login, string password)
        {
            if (login == "1" && password == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void Enter_b_Click(object sender, RoutedEventArgs e)
        {
            if (login_box.Text != "" && pass_box.Password != "")
            {
                Auto(login_box.Text, pass_box.Password);
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
            var exitBi = MessageBox.Show("Вы уверенны, что хотите закрыть приложение?", "Выход из приложения", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (exitBi == MessageBoxResult.Yes)
            {
                Application.Current.Shutdown();
            }
        }

        private void Auto(string login, string password)
        {
            try
            {
                Users us = AppConnect.entities.Users.FirstOrDefault(x => x.user_login == login && x.user_password == password);

                if (us != null)
                {
                    switch (us.user_role_id)
                    {
                        case 1:
                            MessageBox.Show($"Приветствую, пользователь {us.user_name}!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                            Changelogs changelogs = new Changelogs();

                            changelogs = new Changelogs()
                            {
                                changelog_message = $"Пользователь {us.id_user}:{us.user_login} успешно вошёл в сеть",
                                changelog_date = DateTime.Now
                            };

                            AppConnect.entities.Changelogs.Add(changelogs);
                            AppConnect.entities.SaveChanges();

                            AppFrame.frameMain.Navigate(new Product(us));
                            break;
                        case 2:
                            MessageBox.Show($"Приветствую, менеджер {us.user_name}!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                            changelogs = new Changelogs()
                            {
                                changelog_message = $"Менеджер {us.id_user}:{us.user_login} успешно вошёл в сеть",
                                changelog_date = DateTime.Now
                            };

                            AppConnect.entities.Changelogs.Add(changelogs);
                            AppConnect.entities.SaveChanges();

                            AppFrame.frameMain.Navigate(new Product(us));
                            break;
                        case 3:
                            MessageBox.Show($"Приветствую, администратор {us.user_name}!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                            changelogs = new Changelogs()
                            {
                                changelog_message = $"Администратор {us.id_user}:{us.user_login} успешно вошёл в сеть",
                                changelog_date = DateTime.Now
                            };

                            AppConnect.entities.Changelogs.Add(changelogs);
                            AppConnect.entities.SaveChanges();

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
    }
}
