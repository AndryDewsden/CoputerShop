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
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void back_b_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.GoBack();
        }

        private void reg_b_Click(object sender, RoutedEventArgs e)
        {
            if(name_box.Text != null && login_box.Text != null && pass_one_box != null && pass_one_box.Password == pass_two_box.Password)
            {
                if(AppConnect.entities.Users.FirstOrDefault(x => x.user_login == login_box.Text) == null)
                {
                    try
                    {
                        Users use = new Users()
                        {
                            user_login = login_box.Text,
                            user_name = name_box.Text,
                            user_password = pass_two_box.Password,
                            user_role_id = 1
                        };

                        AppConnect.entities.Users.Add(use);
                        AppConnect.entities.SaveChanges();
                        MessageBox.Show($"Вы успешно зарегестрировались!\nПриветствую, пользователь {use.user_name}.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information);

                        Changelogs changelogs = new Changelogs()
                        {
                            changelog_user_id = use.id_user,
                            changelog_message = $"Пользователь {use.id_user}:{use.user_login} успешно зарегестрировался",
                            changelog_date = DateTime.Now
                        };

                        AppConnect.entities.Changelogs.Add(changelogs);
                        
                        AppConnect.entities.SaveChanges();

                        AppFrame.frameMain.Navigate(new Product(use));
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Что-то пошло не так при внедрении данных:\n{ex}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует! Пожалуста, измените свой логин.", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполненны!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
            }    
        }

        private void pass_two_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Checker();
        }

        private void pass_one_box_PasswordChanged(object sender, RoutedEventArgs e)
        {
            Checker();
        }

        private void Checker()
        {
            if(pass_one_box.Password != pass_two_box.Password)
            {
                reg_b.IsEnabled = false;
                t_warm.Content = "Пароли должны совпадать!";
            }
            else
            {
                reg_b.IsEnabled = true;
                t_warm.Content = "";
            }
        }
    }
}
