namespace GroepsProject1._3Backend.WebApi
{
    public class InfoRepository : IInfoRepository
    {
        private string _connectionString;
        public InfoRepository(string connectionString) 
        { 
            this._connectionString = connectionString;
        }


    }
}
