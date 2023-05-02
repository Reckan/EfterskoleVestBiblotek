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
                Func.SaveBook(TbxBookAuthor.Text, TbxBookTitle.Text, TbxBookPublisher.Text, (DateTime)DtPDatePublication.SelectedDate, int.Parse(TbxStock.Text), int.Parse(TbxISBN.Text));
               
                TbxBookAuthor.Text = "";
                TbxBookTitle.Text = "";
                TbxBookPublisher.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void BtnEdditBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteBook_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnSaveCustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEdditCoustomer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
