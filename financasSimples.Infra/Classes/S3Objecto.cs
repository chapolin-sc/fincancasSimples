namespace financasSimples.Infra.Classes;

public class S3Objecto
{
    public string Nome { get; set; } = null!;
    public MemoryStream InputStream { get; set; } = null!;
    public string BucketNome { get; set; } = null!;
    
}