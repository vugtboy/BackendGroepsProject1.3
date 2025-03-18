namespace GroepsProject1._3Backend.WebApi
{
    public class Appointment
    {
        public string Id { get; set; }
        public DateOnly Date { get; set; }
        public string Name { get; set; }
        public int StickerId { get; set; }
        public string UserId { get; set; }
    }
}
