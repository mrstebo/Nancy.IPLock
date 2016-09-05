namespace Nancy.IPLock
{
    public interface IIPValidator
    {
        bool IsValid(string remoteAddress);
    }
}
