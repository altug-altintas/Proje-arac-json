using Proje_model.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Proje_Dal.Repositories.Interfaces.Concrete
{
    public interface IAppUserRepo
    {

        Task Create(AppUser appUser);

        Task Update(AppUser appUser);

        Task Delete(AppUser appUser);

        Task Active(AppUser appUser);

        bool IsEmailUnique(string email);

        bool IsUserlUnique(string user);

        bool IsUserlUniqueHaric(string user, string currentUSer);

        bool IsEmailUniqueHaric(string email, string currentUserEmail);

        Task <bool> IsPasswordHistoryViolatedAsync(string userId, string newPassword, int passwordHistoryLimit);

        AppUser Authentication(string email, string password);
    }
}
