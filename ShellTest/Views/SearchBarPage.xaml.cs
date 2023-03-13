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
        
}