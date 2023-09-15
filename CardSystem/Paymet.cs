using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardSystem
{
    public  class Paymet
    {
        private bool CheckCard(long card,int amount)
        {
            try
            {
                string readText = File.ReadAllText("Card.txt");
                string[] strings = readText.Split('\n').ToArray();

                for (int i = 0; i < strings.Length; i++)
                {
                    string[] string1 = strings[i].Split("%%");
                    if (string1[0].Equals(card.ToString()))
                    {
                        if (Convert.ToInt32(string1[2]) > amount * 1.02) 
                            return true;
                        return false;
                    }

                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        private void UpdateUser(int id,double amount)
        {
            string readText = File.ReadAllText("User.txt");
            string[] strings = readText.Split('\n').ToArray();
            File.Delete("User.txt");
            using (StreamWriter writer = new StreamWriter("User.txt", append: true))
            {


                for (int i = 0; i < strings.Length - 1; i++)
                {
                    string[] string1 = strings[i].Split("%%");
                    if (Convert.ToInt32(string1[0]) == id)
                    {
                        //strings[i][2] = Convert.ToDouble(strings[i][2]); ;
                        var s = Convert.ToDouble(string1[4]);
                        s += amount * 0.01;
                        writer.WriteLine($"{string1[0]}%%{string1[1]}%%{string1[2]}%%{string1[3]}%%{s}%%{string1[5]}");

                    }
                    else
                    {
                        writer.WriteLine($"{string1[0]} %% {string1[1]} %% {string1[2]} %% {string1[3]} %% {strings[i][4]} %% {string1[5]}");
                    }
                }
            }
        }

        private void UpdateCard(long card, int amount, long card2)
        {

            string readText = File.ReadAllText("Card.txt");
            string[] strings = readText.Split('\n').ToArray();
            File.Delete("Card.txt");
            using (StreamWriter writer = new StreamWriter("Card.txt", append: true))
            {


                for (int i = 0; i < strings.Length - 1; i++)
                {
                    string[] string1 = strings[i].Split("%%");
                    if (string1[0].Equals(card.ToString()))
                    {
                        //strings[i][2] = Convert.ToDouble(strings[i][2]); ;
                        var s = Convert.ToDouble(string1[2]);
                        UpdateUser(Convert.ToInt32(string1[3]), s);
                        s -= amount * 1.02;
                        writer.WriteLine($"{string1[0]}%%{string1[1]}%%{s}%%{string1[3]}");
                    }
                    else if (string1[0].Equals(card2.ToString())) 
                    {
                        var s = Convert.ToDouble(string1[2]);
                        s += amount * 1.02;
                        writer.WriteLine($"{string1[0]}%%{string1[1]}%%{s}%%{string1[3]}");
                    }
                    else
                    {
                        writer.WriteLine($"{string1[0]}%%{string1[1]}%%{string1[2]}%%{string1[3]}");
                    }
                }
            }
        }


        private void Report(long card,int amount,long card2) 
        {
            using (StreamWriter writer = new StreamWriter("Report.txt", append: true))
            {
                writer.WriteLine($"{DateTime.UtcNow}%%{card}%%{card2}%%{"Otkazildi"}%%{amount}");
                writer.WriteLine($"{DateTime.UtcNow}%%{card2}%%{card}%%{"Qabul Qilindi"}%%{amount}");

            }
            
        }


        public delegate void CallDel(long card, int amount, long card2);


        public void Pay(long card,int amount,long card2)
        {
            if (CheckCard(card,amount))
            {
                CallDel callDel = UpdateCard;
                callDel += Report;

                callDel.Invoke(card, amount, card2);

                Console.WriteLine("To`lov muvofaqqiyatli amalga oshirildi !!! ");
            }
            else 
            {
                Console.WriteLine("To`lov qilishda Xatolik !!!");
            }

        }
    }
}
