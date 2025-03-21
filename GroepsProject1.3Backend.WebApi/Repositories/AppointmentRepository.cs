using System.Numerics;
using System.Xml.Linq;
using Dapper;
using Microsoft.Data.SqlClient;
using static Azure.Core.HttpHeader;
using static System.Runtime.InteropServices.JavaScript.JSType;
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
            Guid id = appointment.Id;
            string name = appointment.Name;
            //dapper vind een dateonly niet leuk en wil hierom een dateTime hebben, het wordt wel gewoon opgeslagen als dateOnly
            DateTime date = appointment.Date;
            int stickerId = appointment.StickerId;
            Guid UserId = appointment.UserId;
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(
                "INSERT INTO dbo.Appointment (Id, Name, Date, StickerId, UserId) VALUES (@Id, @Name, @Date, @StickerId, @UserId)",
                new { Id = id, Name = name, Date = date, StickerId = stickerId, userId = UserId });
            }
        }

        //Een appointment verwijderen
        public async Task DeleteAppointmentAsync(Guid appointmentId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM dbo.Appointment WHERE Id = @Id", new { Id = appointmentId});
            }
        }

        //Een appointment wijzigen
        public async Task UpdateAppointmentAsync(Appointment appointment)
        {
            Guid id = appointment.Id;
            string name = appointment.Name;
            DateTime date = appointment.Date;
            int stickerId = appointment.StickerId;
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(
                "UPDATE dbo.Appointment SET Name = @Name, Date = @Date, StickerId = @StickerId WHERE Id = @Id",
                new { Id = id, Name = name, Date = date, StickerId = stickerId });
            }
        }

        //Alle appointments van één gebruiker ophalen
        public async Task<IEnumerable<Appointment>> GetAllAppointmentsAsync(Guid userId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return await sqlConnection.QueryAsync<Appointment>(
                "SELECT * FROM dbo.Appointment WHERE userId = @userId",
                new { userId = userId });
            }
        }

        //Een individuele appointment ophalen
        public async Task<Appointment?> GetAppointmentAsync(Guid appointmentId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Appointment>(
                "SELECT * FROM dbo.Appointment WHERE Id = @Id",
                new { Id = appointmentId});
            }
        }

    }
}
