using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.Interface;

namespace Flexera.LicenseVerifier.BL.DTO
{
    /// <summary>
    /// Installation details class for each user
    /// </summary>
    public class InstallationDetailsInfo: IInstallationDetails
    { 
        public Dictionary<int, IUserInfo> UserWiseInstallations { get; set; }

        public InstallationDetailsInfo()
        {
            UserWiseInstallations = new Dictionary<int, IUserInfo>();
        }

        public void AddToCollection(IUserInfo userInfo)
        {
            if (UserWiseInstallations == null)
            {
                UserWiseInstallations = new Dictionary<int, IUserInfo>();
            }

            UserWiseInstallations.Add(userInfo.UserId, userInfo);
        }

        public bool IsExistingUser(int userId)
        {
            return UserWiseInstallations.ContainsKey(userId);//Any(u => u.IsExistingUser(userId));
        }

        public IUserInfo FindUser(int userId)
        {
            UserWiseInstallations.TryGetValue(userId, out var userInfo);
            return userInfo;

        }

        public Dictionary<int,IUserInfo>.ValueCollection GetUsers()
        {
            return UserWiseInstallations.Values;
        }
    }
}
