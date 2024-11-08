using Core.Entity;
using Core.Interface;

namespace Web.ViewModels
{
    public class HomeViewModel
    {
      
        public Owner owner { get; set; }
        public List<ProtfolioItem> protfolioItem { get; set; }
    }
}
