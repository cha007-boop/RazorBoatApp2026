using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Exceptions;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class CreateMemberModel : PageModel
    {
        
        private IMemberRepositoryAsync _repo;
        private IWebHostEnvironment webHostEnvironment;
        [BindProperty]
        public Member NewMember { get; set; }

        [BindProperty]
        public IFormFile Photo { get; set; }

        public CreateMemberModel(IMemberRepositoryAsync memberRepository, IWebHostEnvironment webHost)
        {
            _repo = memberRepository;
            webHostEnvironment = webHost;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (Photo != null)
            {
                if (NewMember.MemberImage != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MemberImages", NewMember.MemberImage);
                    System.IO.File.Delete(filePath);
                }

                NewMember.MemberImage = ProcessUploadedFile();
            }
            else
            {
                NewMember.MemberImage = "default.jpg";
            }

            ModelState.Clear();
            TryValidateModel(NewMember);

            try
            {
                await _repo.AddMember(NewMember);
            }
            catch (MemberPhoneNumberExistsException mex)
            {
                ViewData["ErrorMessage"] = mex.Message;
                ModelState.AddModelError("NewMember.PhoneNumber", mex.Message);
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }

            return RedirectToPage("Index");
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images/MemberImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

    }
}
