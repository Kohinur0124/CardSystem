using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem
{
    public class Card
    {
        public Card(int UserId,long id,string expirationDate,int sum = 50000) 
        {
            Id = id;
            ExpirationDate = expirationDate;
            Amount = sum;
            var path = "Card.txt";
            using (StreamWriter writer = new StreamWriter("Card.txt", append: true))
            {
                writer.WriteLine($"{id}%%{expirationDate}%%{Amount}%%{UserId}");

            }
        }
        

        public long Id { get; set; }
        public double Amount ;
        public string ExpirationDate { get; set; }

    }
}
