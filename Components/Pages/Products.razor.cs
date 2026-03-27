using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.HttpOverrides;
using MYBlazorAPP.Model;
using MYBlazorAPP.Services;
using System.Runtime.CompilerServices;

namespace MYBlazorAPP.Components.Pages
{
    public partial class Products
    {
        [Inject]
        public ProductServices productServices { get; set; }

        [Inject]
        public HeaderMessageServices headerMessageServices { get; set; }

        public bool showAddProduct { get; set; } = false;
        public bool showRemoveProduct { get; set; } = false;

        private IQueryable<Product> products = Enumerable.Empty<Product>().AsQueryable();

        public int selectedProductId { get; set; } = 0;
        protected override async Task OnInitializedAsync()
        {
            headerMessageServices.SendMessage("Products");
            //var productData = await productServices.GetProductAsync();
            //products = productData.AsQueryable();
             await LoadData();
        }

        void showAddNewProduct()
        {
            showRemoveProduct = false;
            selectedProductId = 0;
            showAddProduct = true;
        }

        void receivedCancel(bool isShown)
        {
            selectedProductId = 0;
            showAddProduct = isShown;
        }

        private async Task LoadData()
        {
            var productData =await productServices.GetProductAsync();
            products = productData.AsQueryable();
        }

        async Task receivedProductAdd()
        {
            selectedProductId = 0;
            await LoadData();
        }

        void EditProduct(int id)
        {
            showRemoveProduct = false;
            selectedProductId = id;
            showAddProduct = true;
        }

        void DeleteProduct(int id)
        {
            showRemoveProduct = true;
            showAddProduct = false;
            selectedProductId = id;
        }
        void receivedRemoveCancel(bool isShown)
        {
            selectedProductId = 0;
            showRemoveProduct = isShown;
        }

        async Task receivedProductRemoved()
        {
            selectedProductId = 0;
            await LoadData();
        }



    }
}
