using DataClasses;
using DataModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;

namespace FunctionLayer
{
    public class Func : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        Model Model { get; set; } = new();
        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        public ReadOnlyObservableCollection<Book> Books
        {
            get
            {
                return Model.Book;
            }
        }

        public ReadOnlyObservableCollection<Customer> Customers
        {
            get
            {
                return Model.Customer;
            }
        }

        public ReadOnlyObservableCollection<BookRental> BookRentals
        {
            get
            {
                return Model.BookRental;
            }
        }

        private Book selectedBook;
        public Book SelectedBook
        {
            get
            {
                return selectedBook;
            }
            set
            {
                selectedBook = value;
                RaisePropertyChanged(nameof(SelectedBook));
            }
        }

        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                RaisePropertyChanged(nameof(SelectedCustomer));
            }
        }

        private void ValidateBook(Book bookInfo, Book existingBook)
        {
            if (bookInfo.Author == "")
            {
                throw new Exception("Book must have an Author");
            }
            if (bookInfo.Title == "")
            {
                throw new Exception("Book must have a titel");
            }
            if (bookInfo.Stock < 1)
            {
                throw new Exception("Book in oure system must have at least 1 in stock");
            }
            foreach (Book book in Books)
            {
                if (book.Title == bookInfo.Title && book != existingBook)
                {
                    throw new Exception("Books cant have the same title");
                }
                if(book.ISBN == bookInfo.ISBN && book != existingBook)
                {
                    throw new Exception("");
                }
            }
        }

        private void NewBook(string author, string titel, string publisher, DateTime dateOfPublication, int stock, int isbn)
        {
            Book book = new()
            {
                Author = author,
                Title = titel,
                Publisher = publisher,
                DateOfPublication = dateOfPublication,
                Stock = stock,
                ISBN = isbn,
            };
            ValidateBook(book, null);
        }

        private void EdditBook(Book book, string author, string titel, string publisher, DateTime dateOfPublication, int stock, int isbn)
        {
            Book tempBook = new()
            {
                Author = author,
                Title = titel,
                Publisher = publisher,
                DateOfPublication = dateOfPublication,
                Stock = stock,
                ISBN = isbn,
            };

            ValidateBook(tempBook, book);

            SelectedBook = null;
        }

        public void SaveBook(string author, string titel, string publisher, DateTime dateOfPublication, int stock, int isbn)
        {
            if (SelectedBook == null)
            {
                NewBook(author, titel, publisher, dateOfPublication, stock, isbn);
            }
            else
            {
                EdditBook(SelectedBook, author, titel, publisher, dateOfPublication, stock, isbn);
            }
        }

        public void DeleteBook(Book book)
        {

        }

        private void NewCustomer(int idNumber, string email)
        {
            Customer customer = new()
            {
                IDNumber = idNumber,
                Email = email,
            };
        }

        private void EdditCustomer(Customer customer, int idNumber, string email)
        {
            Customer temCustomer = new()
            {
                IDNumber = idNumber,
                Email = email,
            };


            SelectedCustomer = null;
        }

        public void SaveCustomer(int idNumber, string email)
        {
            if (SelectedCustomer == null)
            {
                NewCustomer(idNumber, email);
            }
            else
            {
                EdditCustomer(SelectedCustomer, idNumber, email);
            }
        }

        public void DeleteCustomer(Customer customer)
        {

        }

        public void SaveBookRental()
        {

        }

        public void DeleteBookRental()
        {

        }
    }
}