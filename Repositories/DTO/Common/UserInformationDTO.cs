namespace Repositories.DTO.Common
{
    public class UserInformationDTO
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public Guid InformationId { get; set; }
        public string Name { get; set; }
        public byte[] InfoFile { get; set; }
        public string Description { get; set; }
    }
}