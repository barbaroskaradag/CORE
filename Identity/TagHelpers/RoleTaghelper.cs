using Identity.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.TagHelpers
{
    [HtmlTargetElement("RolGoster")]
    public class RoleTaghelper : TagHelper
    {
        private readonly UserManager<AppUser> _userManager;
        public RoleTaghelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public int UserId { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user =  _userManager.Users.FirstOrDefault(x => x.Id == UserId);
            var roles = await _userManager.GetRolesAsync(user);

            var builder = new StringBuilder();
            builder.Append($"<strong> {String.Join(",", roles)} </strong>");
            output.Content.SetHtmlContent(builder.ToString());
        }
    }
}
