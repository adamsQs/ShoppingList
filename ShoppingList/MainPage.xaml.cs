using System.Collections.ObjectModel;
using ShoppingList.Models;
using ShoppingList.Services;

namespace ShoppingList
{
    public partial class MainPage : ContentPage
    {
        public List<string> AvailableUnits { get; } = new List<string> { "szt.", "kg", "l" };
        public ObservableCollection<Product> Products { get; set; }
        private readonly ProductService _productService;
        public MainPage()
        {
            InitializeComponent();
            _productService = new ProductService();
            Products = new ObservableCollection<Product>(_productService.GetProducts());
            BindingContext = this;
        }

        private async void OnAddProduct(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(nameEntry.Text) ||
                unitEntry.SelectedItem == null)
                return;

            int quantity = 1;
            if (!string.IsNullOrWhiteSpace(quantityEntry.Text))
            {
                if (!int.TryParse(quantityEntry.Text, out quantity) || quantity < 1)
                {
                    quantity = 1;
                }
            }

            var product = new Product
            {
                Name = nameEntry.Text,
                Unit = unitEntry.SelectedItem.ToString(),
                Quantity = quantity
            };

            await _productService.AddProduct(product);
            Products.Add(product);

            nameEntry.Text = string.Empty;
            unitEntry.SelectedItem = null;
            quantityEntry.Text = string.Empty;
        }
        private async void OnProductDelete(object sender, Product product)
        {
            await _productService.DeleteProduct(product);
            Products.Remove(product);
        }
    }

}
