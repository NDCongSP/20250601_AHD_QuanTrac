namespace Application.Services;

[BasePath(ApiRoutes.FT07.BasePath)]
public interface IFT07 : IRepository<Guid, FT07_ChartMucNuoc>
{
}
