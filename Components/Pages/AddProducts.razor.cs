using Microsoft.AspNetCore.Components;
using MYBlazorAPP.Model;
using MYBlazorAPP.Services;
using System.Runtime.CompilerServices;

namespace MYBlazorAPP.Components.Pages
{
    public partial class AddProducts
    {
        [Inject]
        public ProductServices productServices { get; set; }
        public Product product { get; set; } = new Product();
        [Parameter]
        public EventCallback<bool> componentDisabled { get; set; }
        [Parameter]
        public EventCallback OnProductAdded { get; set; }
        [Parameter]
        public int ProductId { get; set; }
        public string saveButtonName => ProductId == 0 ? "Add Product" : "Update Product";

        protected override async Task OnParametersSetAsync()
        {
            if (ProductId != 0)
            {
                product = await productServices.GetProductAsync(ProductId);
            }
            else
            {
                product = new Product();
            }
        }


        public async void SaveProduct()
        {
            if(product != null)
            {
                if(product.Id == 0)
                {
                    await productServices.AddProductAsync(product);
                }
                else
                {
                    await productServices.UpdateProductAsync(product);
                }
                product = new Product();
                await componentDisabled.InvokeAsync(false);
                await OnProductAdded.InvokeAsync();
            }
        }

        public void Cancel()
        {
            product = new Product();
            componentDisabled.InvokeAsync(false);
        }
    }
}
