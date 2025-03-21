namespace GroepsProject1._3Backend.WebApi
{
    public class Appointment
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public int StickerId { get; set; }
        public Guid UserId { get; set; }
    }
}
