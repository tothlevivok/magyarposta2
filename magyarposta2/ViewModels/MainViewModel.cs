using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using magyarposta2.Model;

namespace magyarposta2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private MainModel _model;

    public int IdInput { get; set; }
    public string NameInput { get; set; }
    private DateTime _sentDate { get; set; }
    private DateOnly _sent;
    public DateOnly Sent
    {
        get { return _sent;}
        set
        {
            if (_sent != value)
            {
                _sent = value;
                OnPropertyChanged(nameof(Sent));
                OnPropertyChanged(nameof(SentDate));
            }
        }
    }

    public DateTime SentDate
    {
        get { return _sentDate; }
        set
        {
            if (_sentDate != value)
            {
                _sentDate = value;
                Sent = DateOnly.FromDateTime(_sentDate);
                OnPropertyChanged(nameof(SentDate));
            }
        }
    }

    public string SentFromInput { get; set; }
    public string DestinationInput { get; set; }
    public string StatusInput { get; set; }
    public int PriceInput { get; set; }
    public int DaysToArriveInput { get; set; }
    public RelayCommand AddPackageCommand { get; set; }
    public RelayCommand LoadCommand { get; set; }
    public RelayCommand SaveCommand { get; set; }
    public ObservableCollection<Package> Packages { get; set; }

    public event EventHandler<PackageEventArgs> ChangeView;
    public event EventHandler GoBack;

    public event EventHandler LoadEvent;
    public event EventHandler SaveEvent;

    public MainViewModel(MainModel model)
    {
        _model = model;
        Packages = new ObservableCollection<Package>();
        _model.PackageAdded += OnAddPackage;
        AddPackageCommand = new RelayCommand(AddPackage);
        LoadCommand = new RelayCommand(() => LoadEvent.Invoke(this, EventArgs.Empty));
        SaveCommand = new RelayCommand(() => { SaveEvent.Invoke(this, EventArgs.Empty); });
    }


    private void OnAddPackage(object? sender, PackageEventArgs e)
    {
        Package package = e.package;

        if (package.Status == "Kiszállítva")
        {
            package.DaysToArrive = 0;
        }
        if (package.Price <= 0)
        {
            return;
        }

        package.MoreCommand = new RelayCommand(() =>
        {
            ChangeView?.Invoke(this, new PackageEventArgs(package));
        });
        package.BackCommand = new RelayCommand(() =>
        {
            GoBack?.Invoke(this, EventArgs.Empty);
        });

        package.DeleteCommand = new RelayCommand(() =>
        {
            _model.DeletePackage(package);
            Packages.Remove(package);
        });

        Packages.Add(package);
    }


    private void AddPackage()
    {
        Package package = new Package(IdInput, NameInput, Sent, SentFromInput, DestinationInput, StatusInput, PriceInput, DaysToArriveInput);
        if (package.Status == "Kiszállítva")
        {
            package.DaysToArrive = 0;
        }
        if (package.Price <= 0)
        {
            return;
        }

        package.MoreCommand = new RelayCommand(() =>
        {
            ChangeView?.Invoke(this, new PackageEventArgs(package));
        });
        package.BackCommand = new RelayCommand(() =>
        {
            GoBack?.Invoke(this, EventArgs.Empty);
        });

        package.DeleteCommand = new RelayCommand(() =>
        {
            _model.DeletePackage(package);
            Packages.Remove(package);
        });

        _model.AddPackage(package);
    }
}
