using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System.Reflection;

namespace RazorBoatApp2026.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepositoryAsync _repo;
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FilterBy { get; set; }
        [BindProperty(SupportsGet = true)]
        public MemberType? SelectedMemberType { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }
        public List<Member> Members { get; set; }
        public IndexModel(IMemberRepositoryAsync memberRepository)
        {
            _repo = memberRepository;
            SortOrder = "asc";
        }

        public async Task OnGet()
        {
            Members = await _repo.GetAllMembers(FilterBy, FilterCriteria, SelectedMemberType, SortColumn, SortOrder);
            //Members = await _repo.FilterMembers(FilterBy, FilterCriteria, SelectedMemberType);

            //Members = SortMembers(Members);
        }

        public string Toggle(string column)
        {
            if (SortColumn == column && SortOrder == "asc")
                return "desc";
            return "asc";
        }

    }
}
