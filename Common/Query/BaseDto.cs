namespace Common.Query;

public class BaseDto<TKey>
{
    public TKey Id { get; set; }
    public DateTime CreationDate { get; set; }
}