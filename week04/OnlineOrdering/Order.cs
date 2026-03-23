using System.Collections.Generic;

public class Order
{
    private List<Product> _products = new List<Product>();
    private Customer _customer;

    public Order(Customer customer) => _customer = customer;

    public void AddProduct(Product product) => _products.Add(product);

    public decimal CalculateTotalCost()
    {
        decimal total = 0;
        foreach (Product p in _products) total += p.GetTotalCost();

        decimal shipping = _customer.LivesInUSA() ? 5.00m : 35.00m;
        return total + shipping;
    }

    public string GetPackingLabel()
    {
        string label = "";
        foreach (Product p in _products) label += p.GetLabelInfo() + "\n";
        return label;
    }

    public string GetShippingLabel() => $"{_customer.GetName()}\n{_customer.GetAddressString()}";
}