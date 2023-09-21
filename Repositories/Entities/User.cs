namespace Repositories.Entities;

public partial class User
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    public string Username { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public byte[] ProfilePicture { get; set; }

    public virtual ICollection<Information> Information { get; set; } = new List<Information>();
}
