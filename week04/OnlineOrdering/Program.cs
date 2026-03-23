using System;

class Program
{
    static void Main(string[] args)
    {
    // Order 1: USA
        Address addr1 = new Address("123 Apple St", "Rexburg", "ID", "USA");
        Customer cust1 = new Customer("Alice Smith", addr1);
        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Webcam", "W10", 45.00m, 1));
        order1.AddProduct(new Product("USB Cable", "U02", 5.99m, 3));

        // Order 2: International
        Address addr2 = new Address("789 Maple Rd", "London", "London", "UK");
        Customer cust2 = new Customer("Bob Jones", addr2);
        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Gaming Monitor", "G99", 299.99m, 1));
        order2.AddProduct(new Product("Mouse Pad", "P05", 12.50m, 2));
        order2.AddProduct(new Product("Headphones", "H11", 80.00m, 1));

        // Display Order 1
        Console.WriteLine("----- ORDER 1 -----");
        Console.WriteLine("Shipping Label:\n" + order1.GetShippingLabel());
        Console.WriteLine("\nPacking Label:\n" + order1.GetPackingLabel());
        Console.WriteLine($"Total Cost: ${order1.CalculateTotalCost():F2}\n");

        // Display Order 2
        Console.WriteLine("----- ORDER 2 -----");
        Console.WriteLine("Shipping Label:\n" + order2.GetShippingLabel());
        Console.WriteLine("\nPacking Label:\n" + order2.GetPackingLabel());
        Console.WriteLine($"Total Cost: ${order2.CalculateTotalCost():F2}");
    }
}