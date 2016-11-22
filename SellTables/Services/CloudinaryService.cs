
using SellTables.Models;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

using System.IO;

namespace SellTables.Services
{
    public class CloudinaryService
    {
        private ApplicationDbContext dataBaseContext;
        private UserService UserService;

        public CloudinaryService(ApplicationDbContext dataBaseContext)
        {
            this.dataBaseContext = dataBaseContext;
            UserService = new UserService(dataBaseContext);
        }

        public void UploadUserAvatar(byte[] img, string username) {
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
                    UserService.UpdateUserAvatar(uploadResult.SecureUri.ToString(), username);
                }
            }
        }

    }
}