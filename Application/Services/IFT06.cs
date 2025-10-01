namespace Application.Services;

[BasePath(ApiRoutes.FT06.BasePath)]
public interface IFT06: IRepository<Guid, FT06_InterpolationTable> 
{
}
