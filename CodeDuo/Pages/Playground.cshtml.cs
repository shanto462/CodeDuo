using CodeDuo.Areas.Identity.Data;
using CodeDuo.DI.Access;
using CodeDuo.DI.Memory;
using CodeDuo.DI.Providers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CodeDuo.Pages
{
    public class PlaygroundModel : PageModel
    {
        public string CodeId { get; set; }

        public InMemoryCodeData TempCodeData { get; set; }

        private readonly IAccessDB _accessDB;
        private readonly ICodeIdProvider _codeIdProvider;
        private readonly UserManager<CodeDuoUser> _userManager;

        public PlaygroundModel(IAccessDB accessDB, ICodeIdProvider codeIdProvider, UserManager<CodeDuoUser> userManager)
        {
            _accessDB = accessDB;
            _codeIdProvider = codeIdProvider;
            _userManager = userManager;
        }

        public IActionResult OnGet()
        {
            CodeId = Request.RouteValues["CodeId"]?.ToString();

            if (string.IsNullOrWhiteSpace(CodeId))
            {
                var guid = _codeIdProvider.GetCodeId();
                CodeId = guid.ToString();
                TempCodeData = _accessDB.CreateCodedata(guid, _userManager.GetUserName(User));
                return RedirectToPage($"/Playground", new { CodeId = CodeId });
            }
            else
            {
                if (_accessDB.IsInvalidGuid(CodeId))
                    return RedirectToPage($"/Errors/Invalid", new { ErrorType = "InvalidLink" });
                if(!_accessDB.AddSharingToUserId(Guid.Parse(CodeId), _userManager.GetUserName(User)))
                    return RedirectToPage($"/Errors/Invalid", new { ErrorType = "InvalidLink" });

                TempCodeData = _accessDB.GetCodedata(Guid.Parse(CodeId));
            }
            return Page();
        }
    }
}
