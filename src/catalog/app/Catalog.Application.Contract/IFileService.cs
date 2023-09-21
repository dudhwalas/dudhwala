using System;
using Volo.Abp.Application.Services;

namespace Catalog.Application.Contract
{
	public interface IFileService : IApplicationService
    {
		public Task<string> SaveFileAsync(byte[] content,string? name,string ext);

        public Task<string> SaveFileAsync(Stream content, string? name, string ext);

        public Task<byte[]> DownloadFileAsBytesAsync(string? path);

        public Task<Stream> DownloadFileAsStreamAsync(string? path);
    }
}