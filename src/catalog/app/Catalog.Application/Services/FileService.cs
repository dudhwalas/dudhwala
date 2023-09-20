using System;
using Catalog.Application.Contract;
using Grpc.Core;
using Volo.Abp.Application.Services;
using Volo.Abp.Guids;
using static Catalog.Application.FileService;

namespace Catalog.Application.Services
{
	public class FileService : FileServiceBase,IFileService
	{
        private readonly IGuidGenerator _guidGenerator;
        public FileService(IGuidGenerator guidGenerator)
		{
            _guidGenerator = guidGenerator;
		}

        public async Task<string> SaveFileAsync(byte[] content, string? name, string ext)
        {
            var fileRoot = $"files";
            Directory.CreateDirectory(fileRoot);
            string path = Path.Combine(fileRoot, (name ?? _guidGenerator.Create().ToString())+"."+ext);
            using (var filestream = System.IO.File.Create(path)) {
               await filestream.WriteAsync(content);
            }
            return path;
        }

        public Task<string> SaveFileAsync(Stream content, string? name, string ext)
        {
            throw new NotImplementedException();
        }

        public override async Task<FileUploadResponse> Upload(FileUploadRequest request, ServerCallContext context)
        {
            var savedPath = await SaveFileAsync(request.File.Content.ToByteArray(), request.Metadata.Name, request.Metadata.Type);
            
            return new FileUploadResponse()
            {
                Name = savedPath
            };
        }
    }
}