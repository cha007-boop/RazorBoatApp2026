using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepository _repo;
        public List<Member> Members { get; set; }
        public IndexModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
        }

        public void OnGet()
        {
            Members = _repo.GetAllMembers();
        }
    }
}
