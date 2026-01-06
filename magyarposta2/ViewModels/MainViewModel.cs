using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using magyarposta2.Model;

namespace magyarposta2.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    private MainModel _model;

    public int IdInput { get; set; }
    public string NameInput { get; set; }
    public DateOnly SentDateInput { get; set; }
    public string SentFromInput { get; set; }
    public string DestinationInput { get; set; }
    public string StatusInput { get; set; }
    public int PriceInput { get; set; }
    public int DaysToArriveInput { get; set; }

    public RelayCommand AddPackageCommand { get; set; }
    public RelayCommand LoadCommand { get; set; }
    public RelayCommand SaveCommand { get; set; }
    public ObservableCollection<Package> Packages { get; set; }

    public event EventHandler LoadEvent;
    public event EventHandler SaveEvent;

    public MainViewModel(MainModel model)
    {
        _model = model;
        Packages = new ObservableCollection<Package>();
        _model.PackageAdded += OnAddPackage;
        AddPackageCommand = new RelayCommand(AddPackage);
        LoadCommand = new RelayCommand(() => LoadEvent.Invoke(this, EventArgs.Empty));
        SaveCommand = new RelayCommand(() => { loadPackage(); SaveEvent.Invoke(this, EventArgs.Empty); });
    }



    private void loadPackage()
    {
        _model.package = new Package(IdInput, NameInput, SentDateInput, SentFromInput, DestinationInput, StatusInput, PriceInput, DaysToArriveInput);
    }

    private void OnAddPackage(object? sender, PackageEventArgs e)
    {
        Package package = new Package(e.package.Id, e.package.Name, e.package.SentDate, e.package.SentFrom, e.package.Destination, e.package.Status, e.package.Price, e.package.DaysToArrive);
        Packages.Add(package);
    }

    private void AddPackage()
    {
        Package package = new Package(IdInput, NameInput, SentDateInput, SentFromInput, DestinationInput, StatusInput, PriceInput, DaysToArriveInput);
        _model.AddPackage(package);
    }
}
