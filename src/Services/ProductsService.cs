using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Thinktecture.AKS.Sample.Entities;
using Thinktecture.AKS.Sample.Models;

namespace Thinktecture.AKS.Sample.Services
{
    public class ProductsService
    {
        private List<Product> _allProducts = new List<Product>
        {
            new Product { Id = Guid.NewGuid(), Name = "Wine - Niagara,vqa Reisling" , Price = 12.3, Category = "Drinks"},
            new Product { Id = Guid.NewGuid(), Name = "Pork Casing" , Price = 5.99, Category = "Food"},
            new Product { Id = Guid.NewGuid(), Name = "Egg Patty Fried" , Price = 2.99, Category = "Food"},
            new Product { Id = Guid.NewGuid(), Name = "Milk 2% 500 Ml" , Price = 1.11, Category = "Drinks"},
            new Product { Id = Guid.NewGuid(), Name = "Yogurt - Blueberry, 175 Gr" , Price = 0.80, Category = "Food"},
            new Product { Id = Guid.NewGuid(), Name = "Spice - Greek 1 Step" , Price = 2.99, Category = "Spices"},
            new Product { Id = Guid.NewGuid(), Name = "Pepper - Yellow Bell" , Price = 1.99, Category = "Spices"},
            new Product { Id = Guid.NewGuid(), Name = "Turkey - Breast, Double" , Price = 4.99, Category = "Food"},
            new Product { Id = Guid.NewGuid(), Name = "Dragon Fruit" , Price = 2.19, Category = "Fruits"},
            new Product { Id = Guid.NewGuid(), Name = "Eggs - Extra Large" , Price = 2.79, Category = "Food"},
        };

        public ProductsService(IMapper mapper)
        {
            Mapper = mapper;
        }

        public IMapper Mapper { get; }

        public async Task<IEnumerable<ProductListItem>> GetAllProductsAsync()
        {
            return await Task.Run(() =>
            {
                return Mapper.Map<IEnumerable<ProductListItem>>(_allProducts);
            });
        }

        public async Task<ProductDetailsItem> GetProductByIdAsync(Guid id)
        {
            return await Task.Run(() =>
            {
                var found = _allProducts.FirstOrDefault(p => p.Id == id);
                if (found == null)
                {
                    return null;
                }
                return Mapper.Map<ProductDetailsItem>(found);
            });
        }

        public async Task<ProductDetailsItem> AddProductAsync(NewProductItem newProductItem)
        {
            if (newProductItem == null)
            {
                throw new ArgumentNullException(nameof(newProductItem));
            }

            return await Task.Run(() =>
            {
                var newProduct = Mapper.Map<Product>(newProductItem);
                newProduct.Id = Guid.NewGuid();
                _allProducts.Add(newProduct);
                return Mapper.Map<ProductDetailsItem>(newProduct);
            });
        }

        public async Task<ProductDetailsItem> UpdateProductAsync(Guid id, UpdateProductItem updateProductItem)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            if (updateProductItem == null)
            {
                throw new ArgumentNullException(nameof(updateProductItem));
            }

            return await Task.Run(() =>
            {
                var found = _allProducts.FirstOrDefault(p => p.Id == id);
                if (found == null)
                {
                    return null;
                }

                found.Name = updateProductItem.Name;
                found.Price = updateProductItem.Price;
                found.Category = updateProductItem.Category;

                return Mapper.Map<ProductDetailsItem>(found);
            });
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(id));
            }

            return await Task.Run(() =>
            {
                var found = _allProducts.FirstOrDefault(p => p.Id == id);
                if (found == null)
                {
                    return false;
                }
                _allProducts.Remove(found);
                return true;
            });
        }
    }
}
