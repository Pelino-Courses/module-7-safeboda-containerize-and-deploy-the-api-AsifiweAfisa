namespace SafeBoda.Core.Models;

public class Rider
{
    public  Guid Id { get; set; }
    public required string PhoneNumber { get; set; }
    public required string Name { get; set; }

    public Rider() { }
    public Rider(Guid id, string name, string phoneNumber)
    {
        Id = id;
        Name = name;
        PhoneNumber = phoneNumber;
    }
}