using System;
using System.Collections.Generic;

// Product class to manage product details
class Product
{
    // Product attributes
    public string Name { get; set; }
    public int ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // Constructor to initialize product details
    public Product(string name, int productId, decimal price, int quantity)
    {
        Name = name;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }

    // Method to calculate the total price for the product
    public decimal CalculateProductTotal()
    {
        return Price * Quantity;
    }
}

// Address class to manage address details
class Address
{
    // Address attributes
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

    // Constructor to initialize address details
    public Address(string streetAddress, string city, string state, string country)
    {
        StreetAddress = streetAddress;
        City = city;
        State = state;
        Country = country;
    }

    // Method to check if the address is in the USA
    public bool IsAddressInUSA()
    {
        return Country.Equals("USA", StringComparison.OrdinalIgnoreCase);
    }

    // Method to get formatted address string
    public string GetFormattedAddress()
    {
        return $"{StreetAddress}, {City}, {State}, {Country}";
    }
}

// Customer class to manage customer details
class Customer
{
    // Customer attributes
    public string Name { get; set; }
    public Address CustomerAddress { get; set; }

    // Constructor to initialize customer details
    public Customer(string name, Address address)
    {
        Name = name;
        CustomerAddress = address;
    }

    // Method to check if the customer lives in the USA
    public bool IsCustomerInUSA()
    {
        return CustomerAddress.IsAddressInUSA();
    }
}

// Order class to manage product orders
class Order
{
    // Order attributes
    private List<Product> Products { get; set; }
    public Customer CustomerInfo { get; set; }

    // Constructor to initialize the order and create an empty list for products
    public Order()
    {
        Products = new List<Product>();
    }

    // Method to add a product to the order
    public void AddProduct(Product product)
    {
        Products.Add(product);
    }

    // Method to calculate the total cost of the order
    public decimal CalculateTotalCost()
    {
        decimal totalCost = 0;
        foreach (var product in Products)
        {
            totalCost += product.CalculateProductTotal();
        }

        // Shipping cost calculation based on customer location
        decimal shippingCost = CustomerInfo.IsCustomerInUSA() ? 5 : 35;
        return totalCost + shippingCost;
    }

    // Method to generate a packing label
    public string GeneratePackingLabel()
    {
        string packingLabel = "Packing Label:\n";
        foreach (var product in Products)
        {
            packingLabel += $"- {product.Name} (ID: {product.ProductId})\n";
        }
        return packingLabel;
    }

    // Method to generate a shipping label
    public string GenerateShippingLabel()
    {
        string shippingLabel = "Shipping Label:\n";
        shippingLabel += $"Customer: {CustomerInfo.Name}\n";
        shippingLabel += $"Address: {CustomerInfo.CustomerAddress.GetFormattedAddress()}\n";
        return shippingLabel;
    }
}

class Program
{
    static void Main()
    {
        // Creating product instances
        Product product1 = new Product("Product A", 101, 25.99m, 2);
        Product product2 = new Product("Product B", 102, 19.99m, 3);

        // Creating address instance
        Address customerAddress = new Address("123 Main St", "Anytown", "CA", "USA");

        // Creating customer instance
        Customer customer = new Customer("John Doe", customerAddress);

        // Creating order instance and adding products
        Order order = new Order();
        order.CustomerInfo = customer;
        order.AddProduct(product1);
        order.AddProduct(product2);

        // Displaying order details
        Console.WriteLine("Order Details:");
        Console.WriteLine(order.GeneratePackingLabel());
        Console.WriteLine(order.GenerateShippingLabel());
        Console.WriteLine($"Total Cost: ${order.CalculateTotalCost()}");
    }
}
