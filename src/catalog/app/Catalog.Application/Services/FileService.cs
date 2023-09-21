using System;
using Catalog.Application.Contract;
using Catalog.Domain.Shared;
using Catalog.Domain.Shared.Localization;
using Grpc.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;
using static Catalog.Application.FileService;

namespace Catalog.Application.Services
{
    public class FileService : FileServiceBase, IFileService
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IConfiguration _config;
        private readonly IStringLocalizer<CatalogResource> _localizer;

        public FileService(IGuidGenerator guidGenerator, IConfiguration config, IStringLocalizer<CatalogResource> localizer)
        {
            _guidGenerator = guidGenerator;
            _config = config;
            _localizer = localizer;
        }

        public async Task<string> SaveFileAsync(byte[] content, string? name, string ext)
        {
            var fileRoot = _config.GetValue<string>("file_path") ?? "/files";
            Directory.CreateDirectory(fileRoot);
            string path = Path.Combine(fileRoot, (name ?? _guidGenerator.Create().ToString()) + "." + ext);
            using (var filestream = System.IO.File.Create(path)) {
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
            var savedPath = await SaveFileAsync(request.File.Content.ToByteArray(), request.Metadata.Name, request.Metadata.Type);

            return new FileUploadResponse()
            {
                Name = savedPath
            };
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
                throw new RpcException(new Status(StatusCode.InvalidArgument, _localizer[CatalogErrorCodes.File_Not_Found,request.Metadata.Name]));
            }
        }
    }
}