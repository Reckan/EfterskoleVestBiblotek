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
        private readonly Model Model;


        public Func(bool mustSave = true)
        {
            Model = new Model(mustSave);
        }

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

        public enum RentalType { All, Active, Overdue };
        public List<RentalType> RentalTypeList { get; set; } = new() { RentalType.All, RentalType.Active, RentalType.Overdue };
        RentalType selectedRentalType;
        public RentalType SelectedRentalType
        {
            private get
            {
                return selectedRentalType;
            }
            set
            {
                selectedRentalType = value;
                RaisePropertyChanged(nameof(Rentals));
            }
        }

        public ReadOnlyObservableCollection<BookRental> Rentals
        {
            get
            {
                // This works too
                //switch (SelectedRentalType)
                //{
                //    case RentalType.Active:
                //        return ActiveRentals;
                //    case RentalType.Overdue:
                //        return OverdueRentals;
                //    default:
                //        return BookRentals;
                //}
                return SelectedRentalType switch
                {
                    RentalType.Active => ActiveRentals,
                    RentalType.Overdue => OverdueRentals,
                    _ => BookRentals,

                };
            }
        }

        public ReadOnlyObservableCollection<BookRental> ActiveRentals
        {
            get
            {
                IEnumerable<BookRental> list = from bookRental in BookRentals where DateTime.Today <= bookRental.RentalStart.AddDays(30) select bookRental;

                ObservableCollection<BookRental> list1 = new(list);
                ReadOnlyObservableCollection<BookRental> list2 = new(list1);
                return list2;
            }
        }


        //public IEnumerable<BookRental> OverdueRentals
        //{
        //    get
        //    {
        //        return from bookRental in BookRentals where bookRental.RentalStart.AddDays(30) < DateTime.Today select bookRental;
        //    }
        //}

        public ReadOnlyObservableCollection<BookRental> OverdueRentals
        {
            get
            {
                IEnumerable<BookRental> list = from bookRental in BookRentals where bookRental.RentalStart.AddDays(30) < DateTime.Today select bookRental;

                ObservableCollection<BookRental> list1 = new(list);
                ReadOnlyObservableCollection<BookRental> list2 = new(list1);
                return list2;
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
                if (book.ISBN == bookInfo.ISBN && book != existingBook)
                {
                    throw new Exception("This ISBN don't belong to this book");
                }
            }
        }

        private void ValidateCustomer(Customer customerInfo, Customer existingCustomer)
        {
            if (customerInfo.IDNumber < 1)
            {
                throw new Exception("ID Number can't be negative");
            }
            if (customerInfo.Email == "")
            {
                throw new Exception("Email is required");
            }
            foreach (Customer customer in Customers)
            {
                if (customer.IDNumber == customerInfo.IDNumber && customer != existingCustomer)
                {
                    throw new Exception("Two Customers can't have the same ID Number");
                }
                if (customer.Email == customerInfo.Email && customer != existingCustomer)
                {
                    throw new Exception("This Email is already in use");
                }
            }
        }

        private void NewBook(string author, string titel, string publisher, DateTime dateOfPublication, int stock, long isbn)
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

            Model.AddBook(book);
        }

        private void EdditBook(Book book, string author, string titel, string publisher, DateTime dateOfPublication, int stock, long isbn)
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

            book.Author = author;
            book.Title = titel;
            book.Publisher = publisher;
            book.DateOfPublication = dateOfPublication;
            book.Stock = stock;
            book.ISBN = isbn;

            Model.Update();

            PropertyChanged?.Invoke(book, new PropertyChangedEventArgs(nameof(book.DateOfPublication)));
            PropertyChanged?.Invoke(book, new PropertyChangedEventArgs(nameof(book.BooksAvailable)));

            SelectedBook = null;
        }

        public void SaveBook(string author, string title, string publisher, DateTime dateOfPublication, int stock, long isbn)
        {
            if (SelectedBook == null)
            {
                NewBook(author, title, publisher, dateOfPublication, stock, isbn);
            }
            else
            {
                EdditBook(SelectedBook, author, title, publisher, dateOfPublication, stock, isbn);
            }
        }

        public void DeleteBook(Book book)
        {
            if(book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            if(book.Stock != book.BooksAvailable)
            {
                throw new Exception("Can't Delete a book if there any are rented out");
            }

            Model.RemoveBook(book);
        }

        private void NewCustomer(int idNumber, string email)
        {
            Customer customer = new()
            {
                IDNumber = idNumber,
                Email = email,
            };

            ValidateCustomer(customer, null);

            Model.AddCustomer(customer);
        }

        private void EdditCustomer(Customer customer, int idNumber, string email)
        {
            Customer temCustomer = new()
            {
                IDNumber = idNumber,
                Email = email,
            };

            ValidateCustomer(temCustomer, customer);

            customer.IDNumber = idNumber;
            customer.Email = email;

            Model.Update();

            //PropertyChanged?.Invoke(customer, new PropertyChangedEventArgs(nameof(BookRental.Customer.IDNumber)));
            //PropertyChanged?.Invoke(customer, new PropertyChangedEventArgs(nameof(BookRental.Customer.Email)));

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
            if(customer == null)
            {
                throw new Exception();
            }
            if(customer.Rental.Count > 0)
            {
                throw new Exception("Can't Delete if they have not yet returend all books");
            }

            Model.RemoveCustomer(customer);

        }

        public void SaveBookRental(DateTime dateTime, Book book, Customer customer, int booksRented)
        {          
            if (book == null)
            {
                throw new ArgumentNullException(nameof(book));
            }
            if (customer == null)
            {
                throw new ArgumentNullException(nameof(customer));
            }
            if (book.BooksAvailable < booksRented)
            {
                throw new Exception("Cant rent more books than we have in stock");
            }

            // Wasen't part of the Eksamen
            foreach(BookRental bookRental1 in OverdueRentals)
            {
                if (bookRental1.Customer == customer)
                {
                    throw new Exception("Can't Rent new books if you havent returnde your overdue ones first");
                }
            }




            BookRental bookRental = new()
            {
                RentalStart = dateTime,
                Customer = customer,
                Book = book,
                BooksRented = booksRented,
            };

            Model.AddBookRental(bookRental);

            RaisePropertyChanged(nameof(customer));

            SelectedBook = null;
            SelectedCustomer = null;
        }

        public void DeleteBookRental(BookRental bookRental)
        {
            Model.RemoveBookRental(bookRental);
        }
    }
}