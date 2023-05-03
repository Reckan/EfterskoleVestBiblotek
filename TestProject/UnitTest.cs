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
            //Func.SaveBookRental();
        }

        [TestMethod]
        [ExpectedException (typeof(Exception))]
        public void OverdueBooksCantRentNew()
        {
            // Arrange
            Func.SaveCustomer(int.MaxValue, "Test@Email1");

            Func.SaveBook("Test Author", "Test Title", "Test Publisher", DateTime.Today, 2, long.MaxValue);

            Func.SaveBookRental(DateTime.MinValue, Func.Books[Func.Books.Count -1], Func.Customers[Func.Customers.Count -1], 1);        

            // Act
            Func.SaveBookRental(DateTime.Today, Func.Books[Func.Books.Count - 1], Func.Customers[Func.Customers.Count - 1], 1);
        }
    }
}