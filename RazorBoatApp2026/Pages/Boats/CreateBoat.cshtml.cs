using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Boats
{
    public class CreateBoatModel : PageModel
    {
        private IBoatRepository _repo;

        [BindProperty]
        public Boat NewBoat { get; set; }

        public CreateBoatModel(IBoatRepository boatRepository)
        {
            _repo = boatRepository;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _repo.AddBoat(NewBoat);
            return RedirectToPage("Index");
        }
    }
}
