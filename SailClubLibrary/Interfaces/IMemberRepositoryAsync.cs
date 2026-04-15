using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Interfaces
{
    public interface IMemberRepositoryAsync
    {
        Task<int> Count();
        Task AddMember(Member member);
        Task RemoveMember(Member member);
        Task UpdateMember(Member member);
        Task<List<Member>> GetAllMembers();
        Task<List<Member>> GetAllMembers(string filterByProperty, string filterCriteria, MemberType? theMemberType, string sortColumn, string sortOrder);
        Task PrintAll();
        Task<Member?> SearchMember(int id);
        Task<List<Member>> FilterMembers(string filterBy, string filterCriteria, MemberType? selectedMemberType);
    }
}
