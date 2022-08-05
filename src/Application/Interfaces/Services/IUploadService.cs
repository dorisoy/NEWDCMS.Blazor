using DCMS.Application.Requests;

namespace DCMS.Application.Interfaces.Services
{
    public interface IUploadService
    {
        string UploadAsync(UploadRequest request);
    }
}