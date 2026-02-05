using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IMemberRepository _repo;

        [BindProperty]
        public Member TheMember { get; set; }
        public EditMemberModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
        }
        public IActionResult OnGet(string phoneNumber)
        {
            TheMember = _repo.SearchMember(phoneNumber);
            return Page();
        }

        public IActionResult OnPostEdit()
        {
            _repo.UpdateMember(TheMember);
            return RedirectToPage("Index");
        }
        
        public IActionResult OnPostDelete()
        {
            _repo.RemoveMember(TheMember);
            return RedirectToPage("Index");
        }
    }
}
