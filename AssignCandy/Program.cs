using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;

namespace AssignCandy
{
    public class Candy
    {
        public int Num { get; set; }
        public decimal Rating { get; set; }
        public int CandyNum { get; set; }
    }

    public class AssignCandyCore
    {
        public static List<Candy> preProcessor(List<Candy> preCandies)
        {
            for (int i = 0; i < preCandies.Count; i++)
            {
                var candy = preCandies.ElementAt(i);
                if (i == 0)
                {
                    candy.CandyNum = 1;
                    preCandies.RemoveAt(i);
                    preCandies.Insert(i, candy);
                }
                else
                {
                    if (candy.Rating > preCandies.ElementAt(i - 1).Rating)
                    {
                        candy.CandyNum = preCandies.ElementAt(i - 1).CandyNum + 1;
                        preCandies.RemoveAt(i);
                        preCandies.Insert(i, candy);
                    }
                    
                }
            }

            return preCandies;
        }

        public static List<Candy> postProcessor(List<Candy> postCandies)
        {
            for (int i = postCandies.Count - 2; i >= 0; i--)
            {
                var candy = postCandies.ElementAt(i);
                if (candy.Rating > postCandies.ElementAt(i + 1).Rating)
                {
                    candy.CandyNum = postCandies.ElementAt(i + 1).CandyNum + 1;
                    postCandies.RemoveAt(i);
                    postCandies.Insert(i, candy);
                }
            }

            return postCandies;
        }

        public static int TotalValues(List<Candy> postCandies)
        {
            return postCandies.Sum(c => c.CandyNum);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var candyList = new List<Candy>();

            Console.WriteLine("Please input your data");
            var nStr = Console.ReadLine();
            var n = -1;
            try
            {
                n = int.Parse(nStr);
            }
            catch (Exception e)
            {                
                throw new Exception(e.Message);
            }

            if (n != -1 && n <= 100000)
            {
                for (var i = 0; i < n; i++)
                {
                    var ratingStr = Console.ReadLine();
                    decimal rating;
                    try
                    {
                        rating = decimal.Parse(ratingStr);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }

                    candyList.Add(new Candy {Num = i,Rating = rating, CandyNum = 1});
                }
            }

            var result = AssignCandyCore.postProcessor(AssignCandyCore.preProcessor(candyList));

            while (result != AssignCandyCore.postProcessor(AssignCandyCore.preProcessor(result)))
            {
                result = AssignCandyCore.postProcessor(AssignCandyCore.preProcessor(result));
            }

            //foreach (var candy in result)
            //{
            //    Console.WriteLine(candy.CandyNum);
            //}

            Console.WriteLine(AssignCandyCore.TotalValues(result));
        }
    }
}
