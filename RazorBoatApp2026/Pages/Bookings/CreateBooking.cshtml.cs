using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SailClubLibrary.Exceptions;
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

            try
            {
                _bookingRepo.AddBooking(NewBooking);
            }
            catch (NullReferenceException nex)
            {
                ViewData["ErrorMessage"] = nex.Message;
                OnGet();
                return Page();
            }
            catch (InvalidDateException iex)
            {
                //ViewData["ErrorMessage"] = iex.Message;
                ModelState.AddModelError("NewBooking.EndDate",
                    iex.Message);
                OnGet();
                return Page();
            }
            catch (OverlappingDateException oex)
            {
                //ViewData["ErrorMessage"] = oex.Message;
                ModelState.AddModelError("NewBooking.StartDate", oex.Message);
                OnGet();
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                OnGet();
                return Page();
            }
            return RedirectToPage("Index");
        }
    }
}
