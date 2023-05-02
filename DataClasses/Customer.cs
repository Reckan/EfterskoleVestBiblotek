using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataClasses
{
    public class Customer : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private int idNumber;
        public int IDNumber
        {
            get
            {
                return idNumber;
            }
            set
            {
                idNumber = value;
                RaisePropertyChanged(nameof(IDNumber));
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
               _email = value;
                RaisePropertyChanged(nameof(Email));
            }
        }

        public List<BookRental> Rental { get; set; } = new List<BookRental>();

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
