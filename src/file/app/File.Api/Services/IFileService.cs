namespace File.Api.Services
{
    public interface IFileService
    {
		public Task<string> SaveFileAsync(byte[] content,string? name,string ext);

        public Task<string> SaveFileAsync(Stream content, string? name, string ext);

        public Task<byte[]> DownloadFileAsBytesAsync(string? path);

        public Task<Stream> DownloadFileAsStreamAsync(string? path);
    }
}