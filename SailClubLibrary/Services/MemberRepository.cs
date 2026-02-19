using SailClubLibrary.Data;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Services
{
    /// <summary>
    /// Class for Constructing and calling Member Repository Objects using the interface
    /// </summary>
    public class MemberRepository : IMemberRepository
    {
        #region Instance Fields
        private Dictionary<string, Member> _members;
        #endregion

        #region Properties
        /// <summary>
        /// Count used for counting members in _members repository
        /// </summary>
        public int Count { get { return _members.Count; } }
        #endregion

        #region Constructor
        /// <summary>
        /// MemberRepository constructor used for making a new member repository called _members with string as key and IMember as value
        /// </summary>
        public MemberRepository()
        {
            //_members = new Dictionary<string, Member>();
            _members = new MockData().MemberData;
        }
        #endregion

        #region Methods
        // Formål:
        // Tilføje Medlem
        // if-statement:
        // Hvis Dictionary _members ikke indeholder Telefonnummer på det Medlem man vil tilføje. Tilføjes Medlemmet
        // Else if:
        //Medlem bliver ikke tilføjet

        /// <summary>
        /// Method for adding members to our repository, which runs a check to tell if the phone number is available
        /// </summary>
        public void AddMember(Member member)
        {
            if (!_members.ContainsKey(member.PhoneNumber))
            {
                _members.Add(member.PhoneNumber, member);
                return;
            }
            throw new MemberPhoneNumberExistsException($"Medlemstelefonnummeret {member.PhoneNumber} findes allerede.");
        }
        // Formål:
        // At få fat på en list med alle medlemmer/objekter
        // Metoden returnere via en indbygget metode som hedder ToList(); som henter liste med _members Values

        /// <summary>
        /// Method for returning a list of members
        /// </summary>
        public List<Member> GetAllMembers()
        {
            return _members.Values.ToList();
        }
        // Formål:
        // Fjerne Medlem
        // Metoden sletter via metoden Remove, og sletter telefonnummeret fra _members

        /// <summary>
        /// Method for removing a member from the dictionary, using their phone number
        /// </summary>
        public void RemoveMember(Member member)
        {
            _members.Remove(member.PhoneNumber);
        }
        // Formål:
        // Opdatere Medlem
        // if-statement:
        // Hvis _members indholder Telefonnummeret argumentet, så overskrider de nye værdier de nuværende med samme telefonnummer.

        /// <summary>
        /// Method to update a member's info, using their phone number to distinguish them
        /// </summary>
        public void UpdateMember(Member updatedMember)
        {
            if (_members.ContainsKey(updatedMember.PhoneNumber))
            {
                Member existingMember = _members[updatedMember.PhoneNumber];

                existingMember.FirstName = updatedMember.FirstName;
                existingMember.SurName = updatedMember.SurName;
                existingMember.Address = updatedMember.Address;
                existingMember.City = updatedMember.City;
                existingMember.Mail = updatedMember.Mail;
                existingMember.TheMemberType = updatedMember.TheMemberType;
                existingMember.TheMemberRole = updatedMember.TheMemberRole;
            }
        }

        /// <summary>
        /// Searches through the member dictionary and returns the member with the given phonenumber. 
        /// </summary>
        public Member? SearchMember(string phoneNumber)
        {
            if (_members.ContainsKey(phoneNumber))
            {
                return _members[phoneNumber];
            }
            return null;
        }

        /// <summary>
        /// Method for printing the info of every member in the dictionary
        /// </summary>
        public void PrintAll()
        {
            foreach (Member member in _members.Values)
            {
                Console.WriteLine(member);
                Console.WriteLine();
            }
        }

        public List<Member> FilterMembers(string filterByProperty, string filterCriteria, MemberType? memberType)
        {
            var filteredList = _members.Values.Where(m => m.TheMemberType == (memberType ?? m.TheMemberType));
            var filter = new FilterByProperty<Member>(filterByProperty, filterCriteria);
            filteredList = filteredList.Where(m => filter.IsMatch(m));

            return filteredList.ToList();
        }

        /* switch case filtering
        public List<Member> FilterMembers(string filterCriteria, string filterByProperty, MemberType memberType)
        {
            List<Member> filteredList = new List<Member>();

            foreach (Member m in _members.Values.Where(m => m.TheMemberType == memberType))
            {
                switch (filterByProperty)
                {
                    case "FirstName":
                        if (m.FirstName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "SurName":
                        if (m.SurName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "PhoneNumber":
                        if (m.PhoneNumber.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "Address":
                        if (m.Address.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "Mail":
                        if (m.Mail.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "City":
                        if (m.City.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    case "Id":
                        if (m.Id.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                            filteredList.Add(m);
                        break;
                    default:
                        if (m.Address.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.City.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.Id.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.Mail.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.FirstName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.SurName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                            m.PhoneNumber.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase))
                        {
                            filteredList.Add(m);
                        }
                        break;
                }
            }
            return filteredList;
        }
        */

        /*
        public List<Member> FilterMembers(string filterCriteria, string filterByProperty, MemberType memberType)
        {
            var filteredList = _members.Values.Where(m => m.TheMemberType == memberType);
            if (!string.IsNullOrEmpty(filterByProperty))
            {
                PropertyInfo propertyInfo = typeof(Member).GetProperty(filterByProperty);
                if (propertyInfo != null)
                {
                    filteredList = filteredList.Where(m =>
                    {
                        var value = propertyInfo.GetValue(m).ToString();
                        return value != null && value.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase);
                    });
                }
            }
            else
            {
                filteredList = filteredList.Where(m =>
                    m.Address.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.City.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.Id.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.Mail.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.FirstName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.SurName.Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase) ||
                    m.PhoneNumber.ToString().Contains(filterCriteria, StringComparison.InvariantCultureIgnoreCase));
            }
            return filteredList.ToList();
        }
        */



        #endregion
    }
}
