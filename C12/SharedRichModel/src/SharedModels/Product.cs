using System;

namespace SharedModels
{
    public class Product
    {
        public Product(string name, int quantityInStock)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            QuantityInStock = quantityInStock;
        }

        public Product(int id, string name, int quantityInStock)
            : this(name, quantityInStock)
        {
            Id = id;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public int QuantityInStock { get; private set; }

        public void AddStock(int amount)
        {
            QuantityInStock += amount;
        }

        public void RemoveStock(int amount)
        {
            if (amount > QuantityInStock)
            {
                throw new NotEnoughStockException(QuantityInStock, amount);
            }
            QuantityInStock -= amount;
        }
    }
}
