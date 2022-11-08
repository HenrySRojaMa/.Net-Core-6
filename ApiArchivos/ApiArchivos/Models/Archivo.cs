namespace ApiArchivos.Models
{
    public class Archivo
    {
        public int Id { get; set; }
        public string Length { get; set; }
        public string FilePath { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] FileBytes { get; set; }
    }
}
