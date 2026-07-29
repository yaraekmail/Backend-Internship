namespace AlMandoobStoneManagement.Models;

public class Order
{
    public int Id { get; set; }

    public int CustomerId { get; set; }

    public int ProjectId { get; set; }

    public decimal TotalPrice { get; set; }
}