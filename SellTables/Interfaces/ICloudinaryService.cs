using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SellTables.Interfaces
{
    public interface ICloudinaryService
    {
        void UploadUserAvatar(byte[] img, string username);
        string UploadImage(byte[] img);
        string UploadCreativeImage(byte[] img);
    }
}
