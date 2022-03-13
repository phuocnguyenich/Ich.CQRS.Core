namespace Ich.CQRS.Core.Domain
{
    public partial class Traveler
    {
        public string Name
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
