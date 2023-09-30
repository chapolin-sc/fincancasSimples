using financasSimples.Infra.Classes;

namespace financasSimples.Infra.ServicesInterfaces;

public interface IStoregeService
{
    Task<ResquestResponse> UploadFileS3Async(S3Objecto obj, AwsCredenciais credenciais);
    Task<byte[]> DownloadFileS3Async(S3Objecto obj, AwsCredenciais credenciais);
}