namespace Adapter
{
    public class ExternalGreeter
    {
        public string GreetByName(string name)
        {
            return $"Adaptee says: hi {name}!";
        }
    }
}
