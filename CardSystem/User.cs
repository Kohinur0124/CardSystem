using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem
{
    public class User
    {
        private Random rnd = new Random();
        public User(string firstname, string lastname, string phoneNumber)
        {
            Id = rnd.Next(100, 999);
            FirstName = firstname;
            LastName = lastname;
            PhoneNumber = phoneNumber;
            CashBack =  0;
            using (StreamWriter writer = new StreamWriter("User.txt", append: true))
            {
                writer.WriteLine($"{Id}%%{FirstName}%%{LastName}%%{PhoneNumber}%%{CashBack}%%{string.Join('#',Cards)}");

            }
        }
        public int Id { get; set; }


        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }

        private double CashBack;

        public Paymet paymet = new Paymet();
        public List<Card> Cards { get; set; } = new List<Card>();

        private bool CheckCard (long id) {

            try
            {
                string readText = File.ReadAllText("Card.txt");
                string[] strings = readText.Split('\n').ToArray();


                

                for (int i = 0; i < strings.Length; i++)
                {
                    string[] strings2 = strings[i].Split("%%");
                    if (strings2[0].Equals(id.ToString()))
                    {
                        return false;
                        
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return true;
                
            }
        }


        public void  AddCard(long id,string expdate)
        {
            if(CheckCard (id))
            {
                var newCard = new Card(Id,id, expdate);
                Cards.Add(newCard);
                Console.WriteLine("Kartangiz  muvofaqqiyatli qoshildi");
            }
        }

    }
}
