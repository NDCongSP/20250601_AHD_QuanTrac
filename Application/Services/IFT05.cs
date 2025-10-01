namespace Application.Services;

[BasePath(ApiRoutes.FT05.BasePath)]
public interface IFT05 : IRepository<Guid, FT05_ChartHoChua>
{
}
