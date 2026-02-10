using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class IndexModel : PageModel
    {
        private IBoatRepository _repo;

        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }

        public List<Boat> Boats { get; set; }

        public IndexModel(IBoatRepository boatRepository)
        {
            _repo = boatRepository;
        }

        public void OnGet()
        {
            if (!string.IsNullOrEmpty(FilterCriteria))
                Boats = _repo.FilterBoats(FilterCriteria);
            else
                Boats = _repo.GetAllBoats();
        }
    }
}
