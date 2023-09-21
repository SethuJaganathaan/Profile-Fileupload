namespace Repositories.DTO.Information
{
    public class InformationCreateDTO
    {
        public int Id { get; set; }
        public Guid InformationId { get; set; }
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public byte[] InfoFile { get; set; }
        public string ContentType { get; set; }
        public string Description { get; set; }
    }
}