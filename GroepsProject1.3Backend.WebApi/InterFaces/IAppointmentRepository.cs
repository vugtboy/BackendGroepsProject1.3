namespace GroepsProject1._3Backend.WebApi
{
    public interface IAppointmentRepository
    {
        public Task CreateAppointmentAsync(Appointment appointment);
        public Task DeleteAppointmentAsync(Guid appointmentId);
        public Task UpdateAppointmentAsync(Appointment appointment);
        public Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(Guid userId);
        public Task<Appointment> GetAppointmentAsync(Guid appointmentId);
    }
}
