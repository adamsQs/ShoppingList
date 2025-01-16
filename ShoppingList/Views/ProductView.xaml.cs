namespace ShoppingList.Views;
using ShoppingList.Models;
using ShoppingList.Services;

public partial class ProductView : ContentView
{
	private readonly ProductService _productService;

	public ProductView()
	{
		InitializeComponent();
		_productService = new ProductService();
	}

	private async void OnIncrease(object sender, EventArgs e)
	{
		var product = (Product)BindingContext;
		product.Quantity++;
		await _productService.UpdateProduct(product);
	}

	private async void OnDecrease(object sender, EventArgs e)
	{
		var product = (Product)BindingContext;
		if (product.Quantity > 1)
		{
			product.Quantity--;
			await _productService.UpdateProduct(product);
		}
	}

	private async void OnCheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		var product = (Product)BindingContext;
		await _productService.UpdateProduct(product);
	}

	private void OnDelete(object sender, EventArgs e)
	{
		DeleteClicked?.Invoke(this, (Product)BindingContext);
	}

	public event EventHandler<Product> DeleteClicked;
}