using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using financasSimples.Infra.Classes;
using financasSimples.Infra.ServicesInterfaces;

namespace financasSimples.Infra.Services;

public class StoregeService : IStoregeService
{

    public async Task<byte[]> DownloadFileS3Async(S3Objecto obj, AwsCredenciais awsCredenciais)
    {
       //string responseBody = "";
            try
            {
                var credenciais = new BasicAWSCredentials(awsCredenciais.AwsKey, awsCredenciais.AwsSecretKey);

                var config = new AmazonS3Config
                {
                    RegionEndpoint = Amazon.RegionEndpoint.USEast1
                };

                //Criando um cliente aws
                using var cliente = new AmazonS3Client(credenciais, config);

                //Criando utilitario de tranferencia para s3
                var transferUtility = new TransferUtility(cliente);
                using (Stream fs = await transferUtility.OpenStreamAsync(obj.BucketNome, obj.Nome, CancellationToken.None))
                {
                   using (MemoryStream reader = new MemoryStream())
                    {
                        await fs.CopyToAsync(reader);
                        return reader.ToArray();
                    }    
                }    
            }
            catch (AmazonS3Exception e)
            {
                // If bucket or object does not exist
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
    }
    




    public async Task<ResquestResponse> UploadFileS3Async(S3Objecto obj, AwsCredenciais awsCredenciais)
    {
        var credenciais = new BasicAWSCredentials(awsCredenciais.AwsKey, awsCredenciais.AwsSecretKey);

        var config = new AmazonS3Config
        {
            RegionEndpoint = Amazon.RegionEndpoint.USEast1
        };

        var response = new ResquestResponse();

        try
        {
            //Criando a requisicão de upload
            var uploadRequest = new TransferUtilityUploadRequest
            {
                InputStream = obj.InputStream,
                BucketName = obj.BucketNome,
                Key = obj.Nome,
                CannedACL = S3CannedACL.NoACL
            };

            //Criando um criente aws
            using var cliente = new AmazonS3Client(credenciais, config);

            //Criando utilitario de tranferencia para s3
            var transferUtility = new TransferUtility(cliente);

            await transferUtility.UploadAsync(uploadRequest);

            response.StatusCode = 200;
            response.Mensagem = $"{obj.Nome} carregado com sucesso!";

        }
        catch (AmazonS3Exception ex)
        {
            response.StatusCode = (int) ex.StatusCode;
            response.Mensagem = ex.Message;
        }
        catch (Exception ex)
        {
            response.StatusCode = 500;
            response.Mensagem = ex.Message;
        }

        return response;
    }

}
