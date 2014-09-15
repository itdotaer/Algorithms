using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BankDetail
{
    public class Head
    {
        public string Id { get; set; }
        public string InCome { get; set; }
        public string OutCome { get; set; }
        public string Remain { get; set; }
    }

    public class MoneyDetail
    {
        public int Id { get; set; }
        public decimal InCome { get; set; }
        public decimal OutCome { get; set; }
        public string Remain { get; set; }
    }

    public class AllDetail
    {
        public Head Head { get; set; }
        public List<MoneyDetail> MoneyDetails { get; set; }

    }

    public class MoneyProccessor
    {
        public static AllDetail formatMoneyInput(string input)
        {
            var moneyDetails = new List<MoneyDetail>();
            Head moneyHead = null;

            var firstSplit = input.Split('\n');

            for (var i = 0; i < firstSplit.Count(); i++)
            {
                var s = firstSplit[i];
                var secondSplit = s.Split(';');

                if (secondSplit.Count() != 4)
                {
                    break;
                }

                if (i == 0)
                {
                    moneyHead = new Head
                    {
                        Id = secondSplit[0],
                        InCome = secondSplit[1],
                        OutCome = secondSplit[2],
                        Remain = secondSplit[3]
                    };
                }
                else
                {
                    moneyDetails.Add(new MoneyDetail
                    {
                        Id = int.Parse(secondSplit[0]),
                        InCome = decimal.Parse(secondSplit[1]),
                        OutCome = decimal.Parse(secondSplit[2]),
                        Remain = secondSplit[3]
                    });
                }
            }
            

            return new AllDetail{Head = moneyHead, MoneyDetails = moneyDetails};
        }

        public static List<MoneyDetail> calculatMoney(List<MoneyDetail> formatDetails )
        {
            for (var i = 0; i < formatDetails.Count(); i++)
            {
                var moneyDetail = formatDetails.ElementAt(i);

                //The first node's remain can't be ?
                if (i == 0 && "?".Equals(moneyDetail.Remain))
                {
                    return null;
                }

                if ("?".Equals(moneyDetail.Remain))
                {
                    var calculatedRemain = decimal.Parse(formatDetails.ElementAt(i - 1).Remain) + formatDetails.ElementAt(i - 1).InCome - formatDetails.ElementAt(i - 1).OutCome;
                    moneyDetail.Remain = calculatedRemain.ToString();

                    //Update the input list of money details
                    formatDetails.RemoveAt(i);
                    formatDetails.Insert(i, moneyDetail);
                }
            }

            return formatDetails;
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            //var moneyDetails = new List<MoneyDetail>();

            //moneyDetails.Add(new MoneyDetail {Id =1,InCome = 10M, OutCome = 1M, Remain = "9"});
            //moneyDetails.Add(new MoneyDetail { Id = 1, InCome = 10M, OutCome = 1M, Remain = "?" });

            //var result = MoneyProccessor.calculatMoney(moneyDetails);

            //var sb = new StringBuilder();
            //var key = Console.ReadKey();

            //while (key.Key != ConsoleKey.Tab)
            //{
            //    if (key.Key == ConsoleKey.Enter)
            //    {
            //        sb.Append("\n");
            //        Console.SetCursorPosition(0, Console.CursorTop + 1);
            //    }
            //    else if (key.Key == ConsoleKey.Backspace)
            //    {
            //        sb.Remove(sb.Length - 1, 1);
            //        Console.WriteLine(sb);
            //    }
            //    else
            //    {
            //        sb.Append(key.KeyChar);
            //    }

            //    key = Console.ReadKey();
            //}

            var sb = new StringBuilder();
            var readLine = Console.ReadLine();
            while (!readLine.StartsWith("\t"))
            {
                sb.Append(readLine + "\n");
                readLine = Console.ReadLine();
            }

            var rs = MoneyProccessor.formatMoneyInput(sb.ToString());

            var calculated = MoneyProccessor.calculatMoney(rs.MoneyDetails);

            Console.WriteLine();
            Console.WriteLine("{0};{1};{2};{3}", rs.Head.Id, rs.Head.InCome, rs.Head.OutCome, rs.Head.Remain);
            foreach (var moneyDetail in calculated)
            {
                Console.WriteLine("{0};{1};{2};{3}", moneyDetail.Id, moneyDetail.InCome, moneyDetail.OutCome, moneyDetail.Remain);
            }

            Console.Read();
        }
    }
}
