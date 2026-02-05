using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class EditBoatModel : PageModel
    {
        private IBoatRepository _repo;

        [BindProperty]
        public Boat TheBoat { get; set; }

        public EditBoatModel(IBoatRepository boatRepository)
        {
            _repo = boatRepository;
        }

        public IActionResult OnGet(string sailNumber)
        {
            TheBoat = _repo.SearchBoat(sailNumber);
            return Page();
        }

        public IActionResult OnPostEdit()
        {
            _repo.UpdateBoat(TheBoat);
            return RedirectToPage("Index");
        }

        public IActionResult OnPostDelete()
        {
            _repo.RemoveBoat(TheBoat.SailNumber);
            return RedirectToPage("Index");
        }
    }
}
