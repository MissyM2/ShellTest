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
        public ObservableCollection<Fruit> SourceItems { get; set; } = new();
        public ObservableCollection<Fruit> SearchResults { get; set; } = new();


        FruitService fruitService;

        public SearchBarViewModel(FruitService fruitService)
        {
            Title = "Fruit Finder";
            this.fruitService = fruitService;
        }

        public async Task InitializeAsync()
        {
            await GetAllFruits();
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

        [RelayCommand]
        public async Task PerformSearch(string searchTerm)
        {

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                searchTerm = string.Empty;
            }

            searchTerm = searchTerm.ToLower();
            var filteredItems = SourceItems.Where(value => value.Name.ToLower().Contains(searchTerm)).ToList();

            foreach (var value in SourceItems)
            {
                if (!filteredItems.Contains(value))
                {
                    SearchResults.Remove(value);
                }
                else if (!SearchResults.Contains(value))
                {
                    SearchResults.Add(value);
                }
            }

           
        }

        [RelayCommand]
        async Task GetAllFruits()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var fruits = await fruitService.GetFruits();

                if (SourceItems.Count != 0)
                    SourceItems.Clear();

                if (SearchResults.Count != 0)
                    SearchResults.Clear();

                foreach (var fruit in fruits)
                {
                    SourceItems.Add(fruit);
                    SearchResults.Add(fruit);
                }

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