using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using magyarposta2.Model;
using magyarposta2.Persistance;
using magyarposta2.ViewModels;
using magyarposta2.Views;

namespace magyarposta2;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        IDataAccess dataAccess = new FileDataAccess();
        MainModel model = new MainModel(dataAccess);
        MainViewModel viewModel = new MainViewModel(model);
        MainView view = new MainView();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = viewModel,
                Content = view
            };
            viewModel.ChangeView += (s, e) =>
            {
                desktop.MainWindow.Content = new PackagesView
                {
                    DataContext = e.package
                };
            };
            viewModel.SaveEvent += async (s, e) =>
            {
                TopLevel topLevel = TopLevel.GetTopLevel(view);
                var file = await topLevel.StorageProvider.SaveFilePickerAsync(new Avalonia.Platform.Storage.FilePickerSaveOptions
                {
                    Title = "Save Packages",
                    SuggestedFileName = "packages.txt",
                    DefaultExtension = "txt"
                });
                if (file is not null)
                {
                    await model.Save(file.Path.AbsolutePath);
                }
            };
            viewModel.LoadEvent += async (s, e) =>
            {
                TopLevel topLevel = TopLevel.GetTopLevel(view);
                var file = await topLevel.StorageProvider.OpenFilePickerAsync(new Avalonia.Platform.Storage.FilePickerOpenOptions
                {
                    Title = "Load Packages",
                    AllowMultiple = false
                });
                if (file is not null)
                {
                    await model.Load(file[0].Path.AbsolutePath);
                }
            };
            viewModel.GoBack += (s, e) =>
            {
                desktop.MainWindow.Content = view;
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = viewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
