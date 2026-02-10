using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class IndexModel : PageModel
    {
        private IMemberRepository _repo;
        [BindProperty(SupportsGet = true)]
        public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortColumn { get; set; }
        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }
        public List<Member> Members { get; set; }
        public IndexModel(IMemberRepository memberRepository)
        {
            _repo = memberRepository;
            SortOrder = "asc";
        }

        public void OnGet()
        {
            Members = !string.IsNullOrEmpty(FilterCriteria) 
                ? _repo.FilterMembers(FilterCriteria) 
                : Members = _repo.GetAllMembers();

            Members = SortMembers(Members);
        }

        private List<Member> SortMembers(List<Member> members)
        {
            bool asc = SortOrder == "asc";

            switch (SortColumn)
            {
                case "Id":
                    return asc 
                        ? members.OrderBy(m => m.Id).ToList() 
                        : members.OrderByDescending(m => m.Id).ToList();
                case "FirstName":
                    return asc 
                        ? members.OrderBy(m => m.FirstName).ToList() 
                        : members.OrderByDescending(m => m.FirstName).ToList();
                case "SurName":
                    return asc
                        ? members.OrderBy(m => m.SurName).ToList()
                        : members.OrderByDescending(m => m.SurName).ToList();
                case "PhoneNumber":
                    return asc
                        ? members.OrderBy(m => m.PhoneNumber).ToList()
                        : members.OrderByDescending(m => m.PhoneNumber).ToList();
                case "Address":
                    return asc
                        ? members.OrderBy(m => m.Address).ToList()
                        : members.OrderByDescending(m => m.Address).ToList();
                case "City":
                    return asc
                        ? members.OrderBy(m => m.City).ToList()
                        : members.OrderByDescending(m => m.City).ToList();
                case "Mail":
                    return asc
                        ? members.OrderBy(m => m.Mail).ToList()
                        : members.OrderByDescending(m => m.Mail).ToList();
                case "MemberType":
                    return asc
                        ? members.OrderBy(m => m.TheMemberType).ToList()
                        : members.OrderByDescending(m => m.TheMemberType).ToList();
                case "MemberRole":
                    return asc
                        ? members.OrderBy(m => m.TheMemberRole).ToList()
                        : members.OrderByDescending(m => m.TheMemberRole).ToList();

                default:
                    return members;
            }


        }

        public string Toggle(string column)
        {
            if (SortColumn == column && SortOrder == "asc")
                return "desc";
            return "asc";
        }

    }
}
