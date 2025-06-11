using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Common;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Model.Auth
{
    public class ApplicationUser : IdentityUser
    {
        [Index("IX_IdentificationNumber", 1, IsUnique = true)]
        public Int64 Identification { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        [Required]
        public bool Enable{ get; set; }

        [Required]
        public Int64 Area { get; set; }
        [Required]
        public Int64 Cargo { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            userIdentity = await CreateUserClaims(userIdentity, manager, userIdentity.GetUserId());

            // Add custom user claims here
            return userIdentity;
        }

        // Create additional parameters to persist on the cookie
        public static async Task<ClaimsIdentity> CreateUserClaims(
            ClaimsIdentity identity,
            UserManager<ApplicationUser> manager,
            string userId
        )
        {
            // Current User
            var currentUser = await manager.FindByIdAsync(userId);

            // Your User Data
            var jUser = JsonConvert.SerializeObject(new CurrentUser
            {
                UserId = currentUser.Id,
                Name = currentUser.Name,
                UserName = currentUser.Email,
                LastName = currentUser.LastName,
                Identificacion = currentUser.Identification,
                Area = currentUser.Area,
                Cargo = currentUser.Cargo
               

            });

            identity.AddClaim(new Claim(ClaimTypes.UserData, jUser));

            return await Task.FromResult(identity);
        }
    }
}
