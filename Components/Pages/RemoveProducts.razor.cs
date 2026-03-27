using Microsoft.AspNetCore.Components;
using MYBlazorAPP.Model;
using MYBlazorAPP.Services;

namespace MYBlazorAPP.Components.Pages
{
    public partial class RemoveProducts
    {
        [Parameter]
        public int ProductId { get; set; }
        [Inject]
        public ProductServices productServices { get; set; }
        public Product product { get; set; } = new Product();
        [Parameter]
        public EventCallback<bool> componentDisabled { get; set; }
        [Parameter]
        public EventCallback onProductRemoved { get; set; }

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

        async void Cancel()
        {
            await componentDisabled.InvokeAsync(false);
        }

        async Task removeProduct()
        {
             if(ProductId != 0)
             {
                await productServices.DeleteProductAsync(ProductId);
                await componentDisabled.InvokeAsync(false);
                await onProductRemoved.InvokeAsync();
            }
        }

        

    }
}
