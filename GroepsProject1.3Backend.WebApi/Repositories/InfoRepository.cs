using Dapper;
using Microsoft.Data.SqlClient;

namespace GroepsProject1._3Backend.WebApi
{
    public class InfoRepository : IInfoRepository
    {
        private string _connectionString;
        public InfoRepository(string connectionString) 
        { 
            this._connectionString = connectionString;
        }

        public async Task<Info> GetInfoAsync(Guid UserId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<Info>(
                "SELECT * FROM dbo.Info WHERE userId = @userId",
                new { userId = UserId });
            }
        }

        public async Task CreateInfoAsync(Info info)
        {
            string name = info.Name;
            string nameDocter = info.NameDocter;
            bool route = info.Route;
            DateTime birthDay = info.BirthDay;
            int avatarId = info.AvatarId;
            Guid _userId = info.UserId;
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(
                "INSERT INTO dbo.Info (Name, NameDocter, Route, BirthDay, AvatarId, userId) " +
                "VALUES (@Name, @NameDocter, @Route, @BirthDay, @AvatarId, @userId)",
                new { Name = name, NameDocter = nameDocter, Route = route, BirthDay = birthDay, AvatarId = avatarId, userId = _userId });
            }
        }

        public async Task DeleteInfoAsync(Guid infoId)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM dbo.Info WHERE userId = @userId", new { userId = infoId });
            }
        }

        public async Task UpdateInfoAsync(Info info)
        {
            string name = info.Name;
            string nameDocter = info.NameDocter;
            bool route = info.Route;
            DateTime birthDay = info.BirthDay;
            int avatarId = info.AvatarId;
            Guid _userId = info.UserId;
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                await sqlConnection.ExecuteAsync(
                "UPDATE dbo.Info SET Name = @Name, NameDocter = @NameDocter, " +
                "Route = @Route, BirthDay = @BirthDay, AvatarId = @AvatarId WHERE userId = @userId",
                new { Name = name, NameDocter = nameDocter, Route = route, BirthDay = birthDay, AvatarId = avatarId, userId = _userId });
            }
        }
    }
}
