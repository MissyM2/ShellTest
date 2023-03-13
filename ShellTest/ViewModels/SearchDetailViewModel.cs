using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Web;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShellTest.Models;

namespace ShellTest.ViewModels
{
    [QueryProperty(nameof(Fruit), "Fruit")]
    public partial class SearchDetailViewModel : BaseViewModel
    {
        [ObservableProperty]
        Fruit fruit;

        public SearchDetailViewModel()
        {

        }

    }
        
}

