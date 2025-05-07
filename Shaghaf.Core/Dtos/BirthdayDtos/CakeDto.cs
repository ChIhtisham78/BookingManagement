public class CakeDto
{
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public string ServingSize { get; set; } = null!; // تأكد من عدم السماح بقيم NULL
}
