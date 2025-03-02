using FGORankChecker.Domain.Entities;
using FGORankChecker.Infrastructure.AngleSharp;

namespace FGORankChecker.UI.ViewModels
{
    public class ViewModel
    {
        List<ServantEntity> ServantEntities = new List<ServantEntity>();

        public ViewModel()
        {
            var angleSharpScraping = new AngleSharpScraping();
            ServantEntities = angleSharpScraping.GetServantList();
        }

        public List<ServantEntity> GetServantEntities()
        {
            return ServantEntities;
        }
    }
}