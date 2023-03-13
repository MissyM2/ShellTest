using System;
using System.Text.Json;

namespace ShellTest.Models
{
    public class FruitService
    {

        public FruitService()
        {
        }

        List<Fruit> fruitList;

        public async Task<List<Fruit>> GetFruits()
        {
            if (fruitList?.Count > 0)
                return fruitList;

            using var stream = await FileSystem.OpenAppPackageFileAsync("fruitdata.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            fruitList = JsonSerializer.Deserialize<List<Fruit>>(contents);

            return fruitList;
        }
    }
}


