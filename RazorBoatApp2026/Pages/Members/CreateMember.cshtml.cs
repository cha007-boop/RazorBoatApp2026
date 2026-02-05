using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        private IMemberRepository _repo;
        [BindProperty]
        public Member NewMember { get; set; }
        public CreateMemberModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _repo.AddMember(NewMember);
            return RedirectToPage("Index");
        }
    }
}
