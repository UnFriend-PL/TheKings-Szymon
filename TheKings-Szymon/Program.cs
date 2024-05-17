using Newtonsoft.Json;
using TheKings_Szymon.Models;

namespace TheKings_Szymon
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            string url = "https://gist.githubusercontent.com/christianpanton/10d65ccef9f29de3acd49d97ed423736/raw/";
            List<Monarch> monarchs = new List<Monarch>();
            try
            {
                monarchs = await FetchMonarchsAsync(url);

                foreach (var monarch in monarchs)
                {
                    Console.WriteLine($"ID: {monarch.Id}, Name: {monarch.Name}, Country: {monarch.Country}, House: {monarch.House}, Years: {monarch.Years}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("How many monarchs are there in the list?");
            Console.WriteLine($"There are {CountOfMonarhOnTheList(monarchs)} monarchs in the list.");
            Console.WriteLine(" Which monarch ruled the longest (and for how long)?");
            var longestReigningMonarch = FindLongestReigningMonarch(monarchs);
            Console.WriteLine($"\nThe longest reigning monarch is {longestReigningMonarch.Name} who ruled for {longestReigningMonarch.Duration} years.");
        }

        public static int CountOfMonarhOnTheList(List<Monarch> monarchs)
        {
            return monarchs.Count;
        }

        public static async Task<List<Monarch>> FetchMonarchsAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                string jsonData = await client.GetStringAsync(url);
                return JsonConvert.DeserializeObject<List<Monarch>>(jsonData);
            }
        }

        public static Monarch FindLongestReigningMonarch(List<Monarch> monarchs)
        {
            Monarch longestReigningMonarch = null;
            int maxDuration = 0;

            foreach (var monarch in monarchs)
            {
                int duration = monarch.GetReignDuration();
                if (duration > maxDuration)
                {
                    maxDuration = duration;
                    longestReigningMonarch = monarch;
                }
            }

            if (longestReigningMonarch != null)
            {
                longestReigningMonarch.Duration = maxDuration;
            }

            return longestReigningMonarch;
        }
    }
}

