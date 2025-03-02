using FGORankChecker.Domain.Entities;

namespace FGORankChecker.Domain.Scraping
{
    public interface IServantScraping
    {
        List<ServantEntity> GetServantList();
    }
}
