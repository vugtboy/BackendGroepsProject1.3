namespace GroepsProject1._3Backend.WebApi
{
    public interface IAppointmentRepository
    {
        public Task CreateAppointmentAsync(Appointment appointment);
        public Task DeleteAppointmentAsync(string appointmentId);
        public Task UpdateAppointmentAsync(Appointment appointment);
        public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(string userId);
        public Task<Appointment> GetAppointmentAsync(string appointmentId);
    }
}
