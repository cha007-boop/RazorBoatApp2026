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
        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        public List<Boat> Boats { get; set; }

        public IndexModel(IBoatRepository boatRepository)
        {
            _repo = boatRepository;
            SortOrder = "asc";
        }

        public void OnGet()
        {
            //if (!string.IsNullOrEmpty(FilterCriteria))
            //    Boats = _repo.FilterBoats(FilterCriteria);
            //else
            //    Boats = _repo.GetAllBoats();
            Boats = _repo.GetBoats(FilterCriteria, SortColumn, SortOrder);
        }

        public string Toggle(string column)
        {
            if (SortColumn == column && SortOrder == "asc")
                return "desc";
            return "asc";
        }
    }
}
