using CloudinaryDotNet;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IConfiguration _configuration)
    {
        
    }
}
