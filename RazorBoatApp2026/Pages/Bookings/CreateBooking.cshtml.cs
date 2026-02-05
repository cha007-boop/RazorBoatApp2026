using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Bookings
{
    public class CreateBookingModel : PageModel
    {
        private IBookingRepository _bookingRepo;
        private IBoatRepository _boatRepo;
        private IMemberRepository _memberRepo;

        [BindProperty]
        public Booking NewBooking { get; set; }

        [BindProperty]
        public string PhoneNumber { get; set; }
        [BindProperty]
        public string SailNumber { get; set; }

        public IEnumerable<SelectListItem> Boats { get; set; }
        public IEnumerable<SelectListItem> Members { get; set; }
        public CreateBookingModel(IBookingRepository bookingRepository, IBoatRepository boatRepository, IMemberRepository memberRepository)
        {
            _bookingRepo = bookingRepository;
            _boatRepo = boatRepository;
            _memberRepo = memberRepository;
            
        }
        public void OnGet()
        {
            Boats = _boatRepo.GetAllBoats().Select(b => new SelectListItem
            {
                Value = b.SailNumber,
                Text = $"{b.SailNumber} - {b.TheBoatType}"
            });
            Members = _memberRepo.GetAllMembers().Select(m => new SelectListItem
            {
                Value = m.PhoneNumber,
                Text = $"{m.FirstName} {m.SurName} - {m.PhoneNumber}"
            });
        }

        public IActionResult OnPost()
        {
            NewBooking.TheMember = _memberRepo.SearchMember(PhoneNumber);
            NewBooking.TheBoat = _boatRepo.SearchBoat(SailNumber);

            ModelState.Clear();
            TryValidateModel(NewBooking);

            if (!ModelState.IsValid)
            {
                OnGet();
                return Page();
            }

            _bookingRepo.AddBooking(NewBooking);
            return RedirectToPage("Index");
        }
    }
}
