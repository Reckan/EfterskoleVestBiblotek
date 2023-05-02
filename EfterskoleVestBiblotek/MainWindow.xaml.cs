using DataClasses;
using DataModel;
using FunctionLayer;
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

namespace EfterskoleVestBiblotek
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Func Func { get; set; } = new();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = Func;
        }

        private void BtnSaveBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Func.SaveBook(TbxBookAuthor.Text, TbxBookTitle.Text, TbxBookPublisher.Text, (DateTime)DtPDatePublication.SelectedDate, int.Parse(TbxStock.Text), long.Parse(TbxISBN.Text));

                TbxBookAuthor.Text = "";
                TbxBookTitle.Text = "";
                TbxBookPublisher.Text = "";
                DtPDatePublication.SelectedDate = DateTime.Today;
                TbxStock.Text = "1";
                TbxISBN.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnEdditBook_Click(object sender, RoutedEventArgs e)
        {
            Func.SelectedBook = DgBooks.SelectedItem as Book;
            TbxBookAuthor.Text = (Func.SelectedBook != null) ? Func.SelectedBook.Author : "";
            TbxBookTitle.Text = (Func.SelectedBook != null) ? Func.SelectedBook.Title : "";
            TbxBookPublisher.Text = (Func.SelectedBook != null) ? Func.SelectedBook.Publisher : "";
            DtPDatePublication.SelectedDate = (Func.SelectedBook != null) ? Func.SelectedBook.DateOfPublication : DateTime.Today;
            TbxStock.Text = (Func.SelectedBook != null) ? Func.SelectedBook.Stock.ToString() : "";
            TbxISBN.Text = (Func.SelectedBook != null) ? Func.SelectedBook.ISBN.ToString() : "";
        }

        private void BtnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Func.DeleteBook(DgBooks.SelectedItem as Book);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Func.SaveCustomer(int.Parse(TbxIDNumber.Text), TbxEmail.Text);

                TbxIDNumber.Text = string.Empty;
                TbxEmail.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnEdditCoustomer_Click(object sender, RoutedEventArgs e)
        {
            Func.SelectedCustomer = DgCustomers.SelectedItem as Customer;
            
            TbxIDNumber.Text = (Func.SelectedCustomer != null) ? Func.SelectedCustomer.IDNumber.ToString() : string.Empty;
            TbxEmail.Text = (Func.SelectedCustomer != null) ? Func.SelectedCustomer?.Email : string.Empty;
        }

        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRentBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Func.SaveBookRental(DateTime.Today, CbxBooks.SelectedItem as Book, CbxCustomers.SelectedItem as Customer, int.Parse(TbxBooksRented.Text));

                CbxBooks.SelectedIndex = -1;
                CbxCustomers.SelectedIndex = -1;
                TbxBooksRented.Text = string.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnReturnBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Func.DeleteBookRental(DgBookRentals.SelectedItem as BookRental);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void CbxCustomers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Func.SelectedCustomer = (Customer)CbxCustomers.SelectedItem;
            }
        }

        private void CbxBooks_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                Func.SelectedBook = (Book)CbxBooks.SelectedItem;
            }
        }

        private void CbxRentals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(e.AddedItems.Count > 0)
            {
                Func.SelectedRentalType = (Func.RentalType)e.AddedItems[0];
            }
            else
            {
                Func.SelectedRentalType = Func.RentalType.All;
            }
        }
    }
}
