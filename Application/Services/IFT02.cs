namespace Application.Services
{
    [BasePath(ApiRoutes.FT02.BasePath)]
    public interface IFT02: IRepository<Guid,FT02>
    {
        [Get(ApiRoutes.FT02.GetRealtimeDisplay)]
        Task<RealtimeDisplayModel> GetFirstOrDefaultRealTimeDisplay();
    }
}
