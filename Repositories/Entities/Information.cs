namespace Repositories.Entities;

public partial class Information
{
    public int Id { get; set; }

    public Guid InformationId { get; set; }

    public Guid? UserId { get; set; }

    public string Name { get; set; }

    public byte[] InfoFile { get; set; }

    public string ContentType { get; set; }

    public string Description { get; set; }

    public virtual User User { get; set; }
}
