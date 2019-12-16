using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flexera.LicenseVerifier.BL.Interface;

namespace Flexera.LicenseVerifier.BL.DTO
{
    /// <summary>
    /// Class for User information which holds information of applications
    /// </summary>
    public class UserInfo:IUserInfo
    {
        public int UserId { get; set; }
        public Dictionary<int, IApplicationInfo> InstalledApplications { get; set; }

        public UserInfo(int userId)
        {
            UserId = userId;
            InstalledApplications = new Dictionary<int, IApplicationInfo>();
        }

        public void AddToCollection(IApplicationInfo applicationInfo)
        {
            if (InstalledApplications == null)
            {
                InstalledApplications = new Dictionary<int, IApplicationInfo>();
            }

            InstalledApplications.Add(applicationInfo.ApplicationId, applicationInfo);
        }

        public bool IsExistingUser(int userId)
        {
            return UserId == userId;
        }

        public IUserInfo Find(int userId)
        {
            return UserId == userId ? this : null;
        }

        public bool IsExistingApplication(int applicationId)
        {
            return InstalledApplications.ContainsKey(applicationId);
        }

        public IApplicationInfo FindApplication(int applicationId)
        {
            InstalledApplications.TryGetValue(applicationId, out var applicationInfo);
            return applicationInfo;
        }
    }
}
