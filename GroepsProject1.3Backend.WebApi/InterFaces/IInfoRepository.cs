namespace GroepsProject1._3Backend.WebApi
{
    public interface IInfoRepository
    {
        public Task<Info> GetInfoAsync(Guid UserId);
        public Task CreateInfoAsync(Info info);

        public Task DeleteInfoAsync(Guid infoId);
        public Task UpdateInfoAsync(Info info);
    }
}
