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
using AutoService.Entities;
using Microsoft.Win32;

namespace AutoService.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddEditProductPage.xaml
    /// </summary>
    public partial class AddEditProductPage : Page
    {
        Product product = new Product();
        public AddEditProductPage(Product currentProduct)
        {
            InitializeComponent();

            if (currentProduct != null)
            {
                product = currentProduct;
                btnDeleteProduct.Visibility = Visibility.Visible;
                txtArticle.IsEnabled = false;
            }
            DataContext = product;
            cmbCategory.ItemsSource = CategoryList;
        }

        public string[] CategoryList =
        {
            "Аксессуары",
            "Автозапчасти",
            "Автосервис",
            "Съемники подшипников",
            "Ручные инструменты",
            "Зарядные устройства"
        };

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AutoServiceEntities.GetContext().Product.Remove(product);
                AutoServiceEntities.GetContext().SaveChanges();
                MessageBox.Show("Запись удалена!", "Уведомление",
                              MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                              MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnSaveProduct_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder errors = new StringBuilder();

            if (product.ProductCost < 0)
                errors.AppendLine("Стоимость не может быть отрицательной!");
            if (product.MinCount < 0)
                errors.AppendLine("Минимальное количество не может быть отрицательным!");
            if (product.ProductDiscountAmount > product.MaxDiscountAmount)
                errors.AppendLine("Действующая скидка на товар не может быть больше максимальной скидки!");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (product.ProductArticleNumber == null)
                AutoServiceEntities.GetContext().Product.Add(product);

            try
            {
                AutoServiceEntities.GetContext().SaveChanges();
                MessageBox.Show("Информация сохранена!", "Успех",
                               MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка",
                               MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
        private void btnEnterImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog GetImageDialog = new OpenFileDialog();


            GetImageDialog.Filter = "Файлы изображений: (*.png, *.jpg, *.jpeg)|*.png;*.jpg;*.jpeg";
            GetImageDialog.InitialDirectory = @"C:\Users\koji\Desktop\AutoService\AutoService\Resources\";

            if (GetImageDialog.ShowDialog() == true)
            {
                product.ProductImage = GetImageDialog.SafeFileName; // Сохраняем имя файла в объект продукта
            }
        }
    }
}
