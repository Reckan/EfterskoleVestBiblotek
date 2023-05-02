using System.ComponentModel;

namespace DataClasses
{
    public class Book : INotifyPropertyChanged
    {
        public int Id { get; set; }

        private string author;
        public string Author
        {
            get
            {
                return author;
            }
            set
            {
                author = value;
                RaisePropertyChanged(nameof(Author));
            }
        }

        private string title;
        public string Title
        {
            get
            {
                return title;
            }
            set
            {
                title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }
        private string? publisher;
        public string? Publisher
        {
            get
            {
                return publisher;
            }
            set
            {
                publisher = value;
                RaisePropertyChanged(nameof(Publisher));
            }
        }
        public DateTime DateOfPublication { get; set; }

        private int stock;
        public int Stock
        {
            get
            {
                return stock;
            }
            set
            {
                stock = value;
                RaisePropertyChanged(nameof(Stock));
            }
        }
        private int? isbn;
        public int? ISBN
        {
            get
            {
                return isbn;
            }
            set
            {
                isbn = value;
                RaisePropertyChanged(nameof(ISBN));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void RaisePropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}