using System;

class Program
{
    static void Main(string[] args)
    {
        // Order 1 (USA customer)
        Address addr1 = new Address("123 Main St", "New York", "NY", "USA");
        Customer cust1 = new Customer("John Smith", addr1);

        Order order1 = new Order(cust1);
        order1.AddProduct(new Product("Laptop", "P100", 800, 1));
        order1.AddProduct(new Product("Mouse", "P200", 20, 2));

        // Order 2 (International customer)
        Address addr2 = new Address("456 Avenue", "Paris", "N/A", "France");
        Customer cust2 = new Customer("Marie Dupont", addr2);

        Order order2 = new Order(cust2);
        order2.AddProduct(new Product("Phone", "P300", 600, 1));
        order2.AddProduct(new Product("Headphones", "P400", 50, 2));

        // Display Order 1
        Console.WriteLine("=== Order 1 ===");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order1.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order1.GetShippingLabel());

        Console.WriteLine($"Total Cost: ${order1.GetTotalCost()}");
        Console.WriteLine();

        // Display Order 2
        Console.WriteLine("=== Order 2 ===");
        Console.WriteLine("Packing Label:");
        Console.WriteLine(order2.GetPackingLabel());

        Console.WriteLine("Shipping Label:");
        Console.WriteLine(order2.GetShippingLabel());

        Console.WriteLine($"Total Cost: ${order2.GetTotalCost()}");
    }
}