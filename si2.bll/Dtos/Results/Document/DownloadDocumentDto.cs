namespace si2.bll.Dtos.Results.Document
{
    public class DownloadDocumentDto
    {
        public string ContentType { get; set; }

        public byte[] FileBytes { get; set; }

        public string OriginalFileName { get; set; }
    }
}
