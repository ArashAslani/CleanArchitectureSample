namespace Common.Domain;

public class BaseDomainEvent
{
    public DateTime CreationDate { get; protected set; }

    public BaseDomainEvent()
    {
        CreationDate = DateTime.Now;
    }
}