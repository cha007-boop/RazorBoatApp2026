using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SailClubLibrary.Interfaces;
using SailClubLibrary.Models;

namespace RazorBoatApp2026.Pages.Members
{
    public class EditMemberModel : PageModel
    {
        private IWebHostEnvironment webHostEnvironment;
        private IMemberRepositoryAsync _repo;

        [BindProperty]
        public Member TheMember { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public EditMemberModel(IMemberRepositoryAsync memberRepository, IWebHostEnvironment webHost)
        {
            _repo = memberRepository;
            webHostEnvironment = webHost;
        }
        public async Task<IActionResult> OnGet(int id)
        {
            TheMember = await _repo.SearchMember(id);
            return Page();
        }

        public IActionResult OnPostEdit()
        {
            if (Photo != null)
            {
                if (TheMember.MemberImage != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MemberImages", TheMember.MemberImage);
                    System.IO.File.Delete(filePath);
                }

                TheMember.MemberImage = ProcessUploadedFile();
            }

            _repo.UpdateMember(TheMember);
            return RedirectToPage("Index");
        }
        
        public IActionResult OnPostDelete()
        {
            _repo.RemoveMember(TheMember);
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
