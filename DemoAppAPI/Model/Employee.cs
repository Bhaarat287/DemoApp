namespace DemoAppAPI.Models
{
    public class Address
    {
        public int ID { get; set; }
        public string Line1 { get; set; } = String.Empty;
        public string Line2 { get; set; } = String.Empty;
        public string City { get; set; } = String.Empty;
        public string PostCode { get; set; } = String.Empty;

    }
    public class ApiKey
    {
        public string Id { get; set; } = String.Empty;
    }
}