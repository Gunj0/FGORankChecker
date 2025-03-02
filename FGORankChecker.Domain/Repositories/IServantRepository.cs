using FGORankChecker.Domain.Entities;

namespace FGORankChecker.Domain.Repositories
{
    public interface IServantRepository
    {
        List<ServantEntity> GetServantList();
    }
}
