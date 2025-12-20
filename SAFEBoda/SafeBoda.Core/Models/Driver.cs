namespace SafeBoda.Core.Models;

    public class Driver
    {
        public  Guid Id { get; set; }
        public required string Name { get; set; }
        public required string  PhoneNumber { get; set; }
        public required string MotoPlateNumber { get; set; }

        public Driver() { }
        public Driver(Guid id, string name, string phoneNumber, string motoPlateNumber)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            MotoPlateNumber = motoPlateNumber;
        }
    }