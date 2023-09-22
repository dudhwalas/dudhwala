using File.Application;
using Grpc.Core;
using static File.Application.FileService;

namespace File.Api.Services
{
    public class FileService : FileServiceBase, IFileService
    {
        private readonly IConfiguration _config;

        public FileService(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string> SaveFileAsync(byte[] content, string? name, string ext)
        {
            var fileRoot = _config.GetValue<string>("file_path") ?? "/files";
            Directory.CreateDirectory(fileRoot);
            string path = Path.Combine(fileRoot, (name ?? Guid.NewGuid().ToString()) + "." + ext);
            using (var filestream = System.IO.File.Create(path))
            {
                await filestream.WriteAsync(content);
            }
            return path;
        }

        public Task<string> SaveFileAsync(Stream content, string? name, string ext)
        {
            throw new NotImplementedException();
        }

        public Task<byte[]> DownloadFileAsBytesAsync(string? path)
        {
            return System.IO.File.ReadAllBytesAsync(path ?? string.Empty);
        }

        public Task<Stream> DownloadFileAsStreamAsync(string? path)
        {
            return Task.FromResult(System.IO.File.OpenRead(path ?? string.Empty) as Stream);
        }

        public override async Task<FileUploadResponse> Upload(FileUploadRequest request, ServerCallContext context)
        {

            if (request.File == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, nameof(request.File)));
            if (request.File.Content == null || request.File.Content.Length <= 0)
                throw new RpcException(new Status(StatusCode.InvalidArgument, nameof(request.File.Content)));
            if (request.Metadata == null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, nameof(request.Metadata)));
            if (string.IsNullOrWhiteSpace(request.Metadata.Name))
                throw new RpcException(new Status(StatusCode.InvalidArgument, nameof(request.Metadata.Name)));
            if (string.IsNullOrWhiteSpace(request.Metadata.Type))
                throw new RpcException(new Status(StatusCode.InvalidArgument, nameof(request.Metadata.Type)));
            try
            {
                var savedPath = await SaveFileAsync(request.File.Content.ToByteArray(), request.Metadata.Name, request.Metadata.Type);

                return new FileUploadResponse()
                {
                    Name = savedPath
                };
            }
            catch (DirectoryNotFoundException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, request.Metadata.Name));
            }
        }

        public override async Task<FileDownloadResponse> Download(FileDownloadRequest request, ServerCallContext context)
        {
            try
            {
                return new()
                {
                    File = new()
                    {
                        Content = await Google.Protobuf.ByteString.FromStreamAsync(await DownloadFileAsStreamAsync(request.Metadata.Name))
                    }
                };
            }
            catch (FileNotFoundException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, request.Metadata.Name));
            }
            catch (DirectoryNotFoundException)
            {
                throw new RpcException(new Status(StatusCode.InvalidArgument, request.Metadata.Name));
            }
        }
    }
}