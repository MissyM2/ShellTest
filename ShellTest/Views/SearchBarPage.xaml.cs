using System.Collections.ObjectModel;
using ShellTest.Models;
using ShellTest.ViewModels;

namespace ShellTest.Views;

public partial class SearchBarPage : ContentPage
{
    public SearchBarPage(SearchBarViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is SearchBarViewModel vm)
        {
            await vm.InitializeAsync();
        }
    }

    async void searchBar_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        if (e.NewTextValue == "")
        {
            if (BindingContext is SearchBarViewModel vm)
            {

                await vm.PerformSearch("");
            }
        }
        
    }

}