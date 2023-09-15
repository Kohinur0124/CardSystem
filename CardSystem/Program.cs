
namespace CardSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User("Sevinch", "Xayriddinova", "+998939613663");
            user.AddCard(1234123412341234, "11/26");
            User user1 = new User("Sadaf", "Karimova", "+998939613663");
            user1.AddCard(4321432143214321, "11/26");
            user.paymet.Pay(1234123412341234, 25000, 4321432143214321);

        }
    }
}