using SellTables.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using System.IO;
using SellTables.Interfaces;

namespace SellTables.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private ApplicationDbContext dataBaseContext;
        private UserService UserService;

        public CloudinaryService(ApplicationDbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
            UserService = new UserService(dataBaseContext);
        }

        public void UploadUserAvatar(byte[] img, string username)
        {
            string path = UploadImage(img);
            if (path == null) path = "https://res.cloudinary.com/festak/image/upload/v1479038549/defaultUser_fofp7w.png";
            UserService.UpdateUserAvatar(path, username);
        }

        public string UploadImage(byte[] img)
        {
            Account account = new Account(
            "qwe123",
            "361919682238885",
            "rxw9_ETqk63uignEfF1R9TCcZ6I");
            Cloudinary cloudinary = new Cloudinary(account);
            if (img != null)
            {
                using (Stream str = new MemoryStream(img))
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription("name", str)
                    };
                    var uploadResult = cloudinary.Upload(uploadParams);
                    return uploadResult.SecureUri.ToString();
                }
            }
            return null;
        }

        public string UploadCreativeImage(byte[] img)
        {
            string path = null;
            if (img != null) path = UploadImage(img);
            if (path == null) path = "https://res.cloudinary.com/qwe123/image/upload/v1479815852/default-placeholder-1024x1024-570x321_y1j7pd.png";
            return path;
        }

    }
}