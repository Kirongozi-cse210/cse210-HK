public class Product // Must be public to be used by Order
{
    private string _name;
    private string _productId;
    private decimal _price;
    private int _quantity;

    public Product(string name, string id, decimal price, int qty)
    {
        _name = name;
        _productId = id;
        _price = price;
        _quantity = qty;
    }

    public decimal GetTotalCost() => _price * _quantity;
    public string GetLabelInfo() => $"{_name} (ID: {_productId})";
}