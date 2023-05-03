using DataClasses;
using DataModel;
using FunctionLayer;

namespace TestProject
{
    [TestClass]
    public class UnitTest
    {
        Func Func { get; set; } = new(false);

        [TestMethod]
        [ExpectedException(typeof(Exception))] // Assert
        public void TestCustomerIDNumberInUse()
        {
            // Arrange
            Func.SaveCustomer(int.MaxValue, "Test@Email1");

            // Act
            Func.SaveCustomer(int.MaxValue, "Test@Email2");
        }

        [TestMethod]
        public void TestBookRentalPeriode()
        {
            Func.SaveBookRental(DateTime.Today.AddDays(-1), Func.Books[Func.Books.Count - 1], Func.Customers[Func.Customers.Count - 1], 1);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void OverdueBooksCantRentNew()
        {
            // Arrange
            Func.SaveCustomer(int.MaxValue, "Test@Email1");

            Func.SaveBook("Test Author", "Test Title", "Test Publisher", DateTime.Today, 2, long.MaxValue);

            BookRental bookRental = new()
            {
                RentalStart = DateTime.Today.AddMonths(-1),
                Customer = Func.Customers[Func.Customers.Count - 1],
                Book = Func.Books[Func.Books.Count - 1],
                BooksRented = 1,
            };

            //Model.AddBookRental(bookRental);

            Func.SaveBookRental(DateTime.MinValue, Func.Books[Func.Books.Count - 1], Func.Customers[Func.Customers.Count - 1], 1);

            // Act
            Func.SaveBookRental(DateTime.Today, Func.Books[Func.Books.Count - 1], Func.Customers[Func.Customers.Count - 1], 1);
        }
    }
}