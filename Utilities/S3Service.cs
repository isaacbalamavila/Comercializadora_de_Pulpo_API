using Amazon.S3;
using Amazon.S3.Model;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.Processing;

namespace comercializadora_de_pulpo_api.Utilities
{
    public class S3Service
    {
        private readonly IAmazonS3 _s3Client;
        private readonly string _bucketName;

        public S3Service(IConfiguration configuration)
        {
            var awsConfig = configuration.GetSection("AWS");
            _bucketName = awsConfig["BucketName"]!;
            _s3Client = new AmazonS3Client(
                awsConfig["AccessKey"],
                awsConfig["SecretKey"],
                Amazon.RegionEndpoint.GetBySystemName(awsConfig["Region"])
            );
        }

        public async Task<string> UploadImageAsWebpAsync(IFormFile file, string fileName)
        {
            using var image = await Image.LoadAsync(file.OpenReadStream());

            // Convierte la imagen a formato WebP (calidad 80%)
            using var memoryStream = new MemoryStream();
            var encoder = new WebpEncoder { Quality = 80 };
            await image.SaveAsWebpAsync(memoryStream, encoder);
            memoryStream.Position = 0;

            string objectKey = $"{fileName}.webp";

            var putRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = objectKey,
                InputStream = memoryStream,
                ContentType = "image/webp",
            };

            await _s3Client.PutObjectAsync(putRequest);

            return $"https://{_bucketName}.s3.amazonaws.com/{objectKey}";
        }
    }
}
