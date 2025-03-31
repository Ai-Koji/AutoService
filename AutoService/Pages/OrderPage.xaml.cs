using AutoService.Entities;
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

namespace AutoService.Pages
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        List<Product> productList = new List<Product>();
        public OrderPage(List<Product> products, User user)
        {
            InitializeComponent();

            DataContext = this;
            productList = products;
            lViewOrder.ItemsSource = productList;

            cmbPickupPoint.ItemsSource = AutoServiceEntities.GetContext().PickupPoint.ToList();

            if (user != null)
                txtUser.Text = user.UserSurname.ToString() + user.UserName.ToString() + " " + user.UserPatronymic.ToString();
        }

        public string Total
        {
            get
            {
                var total = productList.Sum(p => Convert.ToDouble(p.ProductCost) - Convert.ToDouble(p.ProductCost) * Convert.ToDouble(p.ProductDiscountAmount / 100.00));
                return total.ToString();
            }
        }

        private void btnDeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Вы уверены, что хотите удалить этот элемент?", "Предупреждение", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                productList.Remove(lViewOrder.SelectedItem as Product);
        }

        private void btnOrderSave_Click(object sender, RoutedEventArgs e)
        {
            var productArticle = productList.Select(p => p.ProductArticleNumber).ToArray();
            Random random = new Random();
            var date = DateTime.Now;
            if (productList.Any(p => p.ProductQuantityInStock < 3))
                date = date.AddDays(6);
            else
                date = date.AddDays(3);

            if (cmbPickupPoint.SelectedItem == null)
            {
                MessageBox.Show("Выберите пункт выдачи!", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            try
            {
                Order newOrder = new Order()
                {
                    OrderStatus = "Новый",
                    OrderDate = DateTime.Now,
                    OrderPickupPoint = cmbPickupPoint.SelectedIndex + 1,
                    OrderDeliveryDate = date,
                    ReceiptCode = random.Next(100, 1000),
                    ClientFullName = txtUser.Text
                };
                AutoServiceEntities.GetContext().Order.Add(newOrder);

                for (int i = 0; i < productArticle.Count();i++)
                {
                    OrderProduct newOrderProduct = new OrderProduct()
                    {
                        OrderID = newOrder.OrderID,
                        ProductArticleNumber = productArticle[i],
                        Count = 1
                    };
                    AutoServiceEntities.GetContext().OrderProduct.Add(newOrderProduct);
                }

                AutoServiceEntities.GetContext().SaveChanges();
                MessageBox.Show("Заказ оформлен!", "ИНформация", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.Navigate(new OrderTicketPage(newOrder, productList));

            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
