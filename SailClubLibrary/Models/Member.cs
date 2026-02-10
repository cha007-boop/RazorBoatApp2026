using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SailClubLibrary.Models
{
    public class Member
    {
        #region Instance Fields
        #endregion

        #region Properties
        [Display(Name = "First name")]
        [Required(ErrorMessage ="First name required")]
        public string FirstName { get; set; }
        [Display(Name = "Surname")]
        public string SurName { get; set; }
        [Display(Name = "Phone number")]
        [Required(ErrorMessage = "Phone number required")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Mail { get; set; }
        [Display(Name = "Member type")]
        public MemberType TheMemberType { get; set; }
        [Display(Name = "Member role")]
        public MemberRole TheMemberRole { get; set; }
        [Required(ErrorMessage = "Id required")]
        public int Id { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor used for creating new member objects
        /// </summary>
        /// 

        public Member()
        {

        }
        public Member(int id, string name, string surName, string phoneNumber, string address, string city, string mail, MemberType theMemberType, MemberRole theMemberRole)
        {
            FirstName = name;
            SurName = surName;
            PhoneNumber = phoneNumber;
            Address = address;
            City = city;
            Mail = mail;
            TheMemberType = theMemberType;
            TheMemberRole = theMemberRole;
            Id = id;
        }

        #endregion
        #region Methods
        /// <summary>
        /// ToString method used for printing out member information
        /// </summary>
        public override string ToString()
        {
            return $"Medlemsnummer: {Id}\nFornavn: {FirstName}\nEfternavn: {SurName}\nTelefonnummer: {PhoneNumber}\n" +
                $"Adresse: {Address}\nBy: {City}\nEmail: {Mail}\nType: {TheMemberType}\n" +
                $"Rolle: {TheMemberRole}";
        }
        #endregion 
    }
}
