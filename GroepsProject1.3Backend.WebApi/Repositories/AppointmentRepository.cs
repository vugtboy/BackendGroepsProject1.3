namespace GroepsProject1._3Backend.WebApi
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private string _connectionString;
        public AppointmentRepository(string connectionString) 
        { 
            this._connectionString = connectionString;
        }

        //Een appointment aanmaken
        public async Task CreateAppointmentAsync(Appointment appointment)
        {
            //todo appointment maken in database
        }

        //Een appointment verwijderen
        public async Task DeleteAppointmentAsync(string appointmentId)
        {
            //todo appointment deleten in database
        }

        //Een appointment wijzigen
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            //todo appointment updaten in database
        }

        //Alle appointments van één gebruiker ophalen
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(string userId)
        {
            //todo appointments ophalen uit database
            return null;
        }

        //Een individuele appointment ophalen
        public Task<Appointment> GetAppointmentAsync(string appointmentId)
        {
            //todo appointment ophalen uit database
            return null;
        }
    }
}
