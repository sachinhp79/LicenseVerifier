using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flexera.LicenseVerifier.BL.Interface
{
    public interface IDataFactory
    {
        IInstallationDetails GenerateData(List<string> dataList);
    }
}
