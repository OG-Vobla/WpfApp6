using QRCoder;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using WpfApp6.ADO;

namespace WpfApp6
{
	/// <summary>
	/// Логика взаимодействия для AdminWindow.xaml
	/// </summary>
	public class ProductQr
	{
		public BitmapImage ProdQrCode { get; set; }
		public string Name { get; set; }
		public int Price { get; set; }
	}
	public partial class AdminWindow : Window
	{
		ObservableCollection<ProductQr> QrProducts { get; set; }
		public AdminWindow()
		{
			InitializeComponent();
			 QrProducts = new ObservableCollection<ProductQr>();
			var a = DbConectionClass.db.Product;
			foreach (var i in a)
			{
				ProductQr prodQr = new ProductQr();
				prodQr.Name = i.Name;
				prodQr.Price = i.Price;

				string soucer_xl = i.ID_Product.ToString();
				QRCoder.QRCodeGenerator qr = new QRCoder.QRCodeGenerator();
				QRCoder.QRCodeData data = qr.CreateQrCode(soucer_xl, QRCoder.QRCodeGenerator.ECCLevel.L);
				QRCoder.QRCode code = new QRCoder.QRCode(data);
				Bitmap bitmap = code.GetGraphic(100);
				using (MemoryStream memory = new MemoryStream())
				{
					bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
					memory.Position = 0;
					BitmapImage bitmapimage = new BitmapImage();
					bitmapimage.BeginInit();
					bitmapimage.StreamSource = memory;
					bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
					bitmapimage.EndInit();
					prodQr.ProdQrCode = bitmapimage;
				}
				QrProducts.Add(prodQr);
			}
			Table.ItemsSource = QrProducts;

		}
	}
}
