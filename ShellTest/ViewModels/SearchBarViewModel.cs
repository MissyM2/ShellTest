using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ShellTest.Models;
using ShellTest.Views;

namespace ShellTest.ViewModels
{
    public partial class SearchBarViewModel : BaseViewModel
    {
        public ObservableCollection<Fruit> SearchResults { get; } = new();


        FruitService fruitService;

        public SearchBarViewModel(FruitService fruitService)
        {
            Title = "Fruit Finder";
            this.fruitService = fruitService;
        }

        public async Task InitializeAsync()
        {
            await GetFruits();
        }

        [RelayCommand]
        async Task GoToDetails(Fruit fruit)
        {
            if (fruit == null)
                return;

            await Shell.Current.GoToAsync(nameof(SearchDetailPage), true, new Dictionary<string, object>
            {
                {"Fruit", fruit }
            });
        }

        //[RelayCommand]
        //async Task PerformSearch(string query)
        //{
        //    SearchResults = await fruitService.GetFruits();
        //}

        [RelayCommand]
        async Task GetFruits()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var fruits = await fruitService.GetFruits();

                if (SearchResults.Count != 0)
                    SearchResults.Clear();

                foreach (var fruit in fruits)
                    SearchResults.Add(fruit);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get fruits: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }
    }
}