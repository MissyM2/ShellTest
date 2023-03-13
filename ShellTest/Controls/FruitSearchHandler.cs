using System;
using ShellTest.Models;

namespace ShellTest.Controls
{
	public class FruitListearchHandler :SearchHandler
    {
        public IList<Fruit> FruitList { get; set; }
        public Type SelectedItemNavigationTarget { get; set; }

        protected override void OnQueryChanged(string oldValue, string newValue)
        {
            base.OnQueryChanged(oldValue, newValue);

            if (string.IsNullOrWhiteSpace(newValue))
            {
                ItemsSource = null;
            }
            else
            {
                ItemsSource = FruitList
                    .Where(Fruit => Fruit.Name.ToLower().Contains(newValue.ToLower()))
                    .ToList<Fruit>();
            }
        }

        protected override async void OnItemSelected(object item)
        {
            base.OnItemSelected(item);

            Fruit Fruit = item as Fruit;
            string navigationTarget = GetNavigationTarget();

            
                string lowerCasePropertyName = navigationTarget.Replace("details", string.Empty);
                // Capitalise the property name
                string propertyName = char.ToUpper(lowerCasePropertyName[0]) + lowerCasePropertyName.Substring(1);

                var navigationParameters = new Dictionary<string, object>
                {
                    { propertyName, Fruit }
                };

                // Navigate, passing an object
                await Shell.Current.GoToAsync($"{navigationTarget}", navigationParameters);
        }

        string GetNavigationTarget()
        {
            return (Shell.Current as AppShell).Routes.FirstOrDefault(route => route.Value.Equals(SelectedItemNavigationTarget)).Key;
        }
    }
}

