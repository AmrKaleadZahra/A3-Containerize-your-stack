namespace A2_Connecting_to_the_database.DTOs
{
    public class ApiDetails
    {
        public string Name { get; set; }
        public string Version { get; set; }
        public List<string> Endpoint { get; set; } = new List<string>();
    }
}
