using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class BookRental : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public DateTime RentalStart { get; set; }
        public Customer Customer { get; set; }
        public Book Book { get; set; }

        private int booksRented;
        public int BooksRented
        {
            get
            {
                return booksRented;
            }
            set
            {
                booksRented = value;
                RaisePropertyChanged(nameof(BooksRented));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
