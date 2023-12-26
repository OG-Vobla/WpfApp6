using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
using WpfApp6.ADO;

namespace WpfApp6
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			if (Login.Text != "" && Password.Password != "")
			{
				var user = DbConectionClass.db.User.Where(x => x.Login == Login.Text).FirstOrDefault();
				if (user != null)
				{
					if (user.Password == Password.Password)
					{
						if (user.Role.Name == "Админ")
						{
							AdminWindow adminWindow = new AdminWindow();
							adminWindow.Show();
						}
						else if (user.Role.Name == "Лейм")
						{
							LeimWindow adminWindow = new LeimWindow();
							adminWindow.Show();
						}
						this.Close();
					}
					else
					{
						MessageBox.Show("Пароль не правильный.");
					}
				}
				else
				{
					MessageBox.Show("Пользователя с таким логином не существует.");
				}
			}
			else
			{
				MessageBox.Show("Не все поля заполнены.");
			}
		}
	}
}
