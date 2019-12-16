using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.DTO;

namespace Flexera.LicenseVerifier.BL.Interface
{
    public interface IInstallationDetails
    {
        void AddToCollection(IUserInfo userInfo);

        bool IsExistingUser(int userId);
        IUserInfo FindUser(int userId);
        Dictionary<int, IUserInfo>.ValueCollection GetUsers();
    }
}
