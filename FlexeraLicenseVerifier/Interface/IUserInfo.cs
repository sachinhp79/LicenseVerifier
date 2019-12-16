using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexera.LicenseVerifier.BL.Interface
{
    public interface IUserInfo
    {
        int UserId { get; set; }
        Dictionary<int, IApplicationInfo> InstalledApplications { get; set; }
        void AddToCollection(IApplicationInfo applicationInfo);
        bool IsExistingUser(int userId);
        IUserInfo Find(int userId);
        bool IsExistingApplication(int applicationId);

        IApplicationInfo FindApplication(int applicationId);
    }
}
