using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace WpfApp6
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Person> People { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }
        public ICommand OpenCommand { get; set; }

        public MainWindowViewModel()
        {
            People = new ObservableCollection<Person>();
            AddCommand = new RelayCommand(AddPerson);
            DeleteCommand = new RelayCommand(DeletePerson, CanDeletePerson);
            SaveCommand = new RelayCommand(SaveData);
            OpenCommand = new RelayCommand(OpenData);
        }

        private void AddPerson(object obj)
        {
            var person = new Person { FullName = "Новый человек", Address = "Адрес", PhoneNumber = "Телефон" };
            People.Add(person);
            SaveData(null);
        }

        private void DeletePerson(object obj)
        {
            if (People.Any())
            {
                var selectedPerson = People.First(); // Логика для удаления выбранного человека
                People.Remove(selectedPerson);
                SaveData(null);
            }
        }

        private bool CanDeletePerson(object obj)
        {
            return People.Any(); 
        }

        private void SaveData(object obj)
        {
            File.WriteAllLines("notebook.txt", People.Select(p => $"{p.FullName};{p.Address};{p.PhoneNumber}"));
        }

        private void OpenData(object obj)
        {
            // Логика для открытия файла
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
