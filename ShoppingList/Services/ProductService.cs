using ShoppingList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ShoppingList.Services
{
    public class ProductService
    {
        private readonly string _filePath;
        private List<Product> _products;

        public ProductService()
        {
            var appDataPath = FileSystem.AppDataDirectory;
            _filePath = Path.Combine(appDataPath, "products.json");
            _products = LoadProducts();
        }

        private List<Product> LoadProducts()
        {
            if (!File.Exists(_filePath))
                return new List<Product>();

            var json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Product>>(json) ?? new List<Product>();
        }

        private async Task SaveProducts()
        {
            var json = JsonSerializer.Serialize(_products);
            await File.WriteAllTextAsync(_filePath, json);
        }

        public List<Product> GetProducts() => _products;

        public async Task AddProduct(Product product)
        {
            _products.Add(product);
            await SaveProducts();
        }

        public async Task UpdateProduct(Product product)
        {
            var index = _products.FindIndex(p => p.Name == product.Name);
            if (index != -1)
            {
                _products[index] = product;
                await SaveProducts();
            }
        }

        public async Task DeleteProduct(Product product)
        {
            _products.Remove(product);
            await SaveProducts();
        }
    }
}
