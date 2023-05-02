using DataClasses;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;

namespace DataModel
{
    public class Model : DbContext
    {
        private DbSet<Book> Books { get; set; }
        private DbSet<Customer> Customers { get; set; }
        private DbSet<BookRental> BookRentals { get; set; }

        private ObservableCollection<Book> BookList
        {
            get
            {
                if (Books.Local.Count == 0)
                {
                    Books.Load();
                }
                return Books.Local.ToObservableCollection();
            }
        }

        private ObservableCollection<Customer> CustomerList
        {
            get
            {
                if (Customers.Local.Count == 0)
                {
                    Customers.Load();
                }
                return Customers.Local.ToObservableCollection();
            }
        }

        private ObservableCollection<BookRental> BookRentalList
        {
            get
            {
                if (BookRentals.Local.Count == 0)
                {
                    BookRentals.Load();
                }
                return BookRentals.Local.ToObservableCollection();
            }
        }

        private readonly ReadOnlyObservableCollection<Book> _Book;
        public ReadOnlyObservableCollection<Book> Book
        {
            get
            {
                return _Book;
            }
        }

        private readonly ReadOnlyObservableCollection<Customer> _Customer;
        public ReadOnlyObservableCollection<Customer> Customer
        {
            get
            {
                return _Customer;
            }
        }

        private readonly ReadOnlyObservableCollection<BookRental> _BookRental;
        public ReadOnlyObservableCollection<BookRental> BookRental
        {
            get
            {
                return _BookRental;
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=EfterskoleVestBiblotekDB;Trusted_Connection=True;");
        }

        public Model()
        {
            _Book = new ReadOnlyObservableCollection<Book>(BookList);
            _Customer = new ReadOnlyObservableCollection<Customer>(CustomerList);
            _BookRental = new ReadOnlyObservableCollection<BookRental>(BookRentalList);
        }
        private void DoSave()
        {
            SaveChanges();
        }
        public void AddBook(Book book)
        {
            Books.Add(book);
            DoSave();
        }
        public void RemoveBook(Book book)
        {
            Books.Remove(book);
            DoSave();
        }

        public void AddCustomer(Customer customer)
        {
            Customers.Add(customer);
            DoSave();
        }
        public void RemoveCustomer(Customer customer)
        {
            Customers.Remove(customer);
            DoSave();
        }
    }
}