using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Lab02_03.Models;

namespace Lab02_03.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private string _firstName;
        private string _lastName;
        private string _email;
        private DateTime? _birthDate;
        private bool _isBusy;
        private string _resultText;

        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(); UpdateProceedCommand(); }
        }

        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(); UpdateProceedCommand(); }
        }

        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); UpdateProceedCommand(); }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set { _birthDate = value; OnPropertyChanged(); UpdateProceedCommand(); }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set { _isBusy = value; OnPropertyChanged(); }
        }

        public string ResultText
        {
            get => _resultText;
            private set { _resultText = value; OnPropertyChanged(); }
        }

        public ICommand ProceedCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        internal MainViewModel()
        {
            ProceedCommand = new RelayCommand(async () => await ProceedAsync(), CanProceed);
        }

        private bool CanProceed()
        {
            return !string.IsNullOrWhiteSpace(FirstName) &&
                   !string.IsNullOrWhiteSpace(LastName) &&
                   !string.IsNullOrWhiteSpace(Email) &&
                   BirthDate.HasValue;
        }

        private async Task ProceedAsync()
        {
            IsBusy = true;
            ResultText = "";
            try
            {

                await Task.Delay(1000);
                Person person = new Person(FirstName, LastName, Email, BirthDate.Value);

                if (person.BirthDate > DateTime.Today)
                {
                    MessageBox.Show("Birth date must be real.");
                    return;
                }

                int age = DateTime.Today.Year - person.BirthDate.Year;
                if (person.BirthDate > DateTime.Today.AddYears(-age)) age--;
                if (age > 135)
                {
                    MessageBox.Show("Age cannot be higher than 135.");
                    return;
                }

                ResultText = $"First Name: {person.FirstName}\n" +
                             $"Last Name: {person.LastName}\n" +
                             $"Email: {person.Email}\n" +
                             $"Birth Date: {person.BirthDate.ToShortDateString()}\n" +
                             $"IsAdult: {person.IsAdult}\n" +
                             $"SunSign: {person.SunSign}\n" +
                             $"ChineseSign: {person.ChineseSign}\n" +
                             $"IsBirthday: {person.IsBirthday}";

                var resultsWindow = new ResultsWindow(ResultText);
                resultsWindow.Show();
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateProceedCommand()
        {
            (ProceedCommand as RelayCommand)?.RaiseCanExecuteChanged();
        }
    }

    internal class RelayCommand : ICommand
    {
        private readonly Func<Task> _execute;
        private readonly Func<bool> _canExecute;

        internal RelayCommand(Func<Task> execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        public async void Execute(object parameter) => await _execute();

        public void RaiseCanExecuteChanged() => CommandManager.InvalidateRequerySuggested();
    }
}