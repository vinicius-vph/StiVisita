namespace StiVisita.Common
{
    public class UuidGenerator
    {
        public UuidGenerator()
        {
            Generate();
        }
        public string Generate()
        {
            Guid myuuid = Guid.NewGuid();
            string myuuidAsString = myuuid.ToString();
            Console.WriteLine("Your UUID is: " + myuuidAsString);
            return myuuidAsString;
        }
    }
}
