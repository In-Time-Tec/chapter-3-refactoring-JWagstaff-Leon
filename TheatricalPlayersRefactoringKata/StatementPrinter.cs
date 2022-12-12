using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        /* REVIEW This is how I'd like it, but then the tests have to change
        StatementPrinter(Invoice invoice, Dictionary<string, Play> plays)
        {
            cultureInfo_ = new CultureInfo("en-US");
            invoice_ = invoice;
            plays_   = plays;
            
            StartNewStatement();
        }
        */
        public StatementPrinter()
        {
            cultureInfo_ = new CultureInfo("en-US");
        }


        
        private void StartNewStatement()
        {
            statement_ = "";
        }



        private void PrintCustomerInfo()
        {
            statement_ += string.Format("Statement for {0}\n", invoice_.Customer);
        }



        private int CalculatePerformanceCost(Performance performance)
        {
            var play         = plays_[performance.PlayID];
            var audienceSize = performance.AudienceSize;
            var cost         = 0;
            // TODO figure out how to move this to its own function/class
            switch (play.Type)
            {
                case "tragedy":
                    cost = 40000;
                    if (audienceSize > 30) {
                        cost += 1000 * (audienceSize - 30);
                    }
                    break;
                case "comedy":
                    cost = 30000;
                    if (audienceSize > 20) {
                        cost += 10000 + 500 * (audienceSize - 20);
                    }
                    cost += 300 * audienceSize;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }
            return cost;
        }



        private int CalculateCreditsEarned(Performance performance)
        {
            Play play = plays_[performance.PlayID];
            int creditsEarned = Math.Max(performance.AudienceSize - 30, 0);
            if ("comedy" == play.Type)
            {
                creditsEarned += (int)Math.Floor((decimal)performance.AudienceSize / 5);
            }
            return creditsEarned;
        }



        private void PrintPerformanceList()
        {
            foreach(var performance in invoice_.Performances) 
            {
                var play          = plays_[performance.PlayID];
                var cost          = CalculatePerformanceCost(performance);
                var creditsEarned = CalculateCreditsEarned(performance);

                statement_ += String.Format(cultureInfo_, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(cost / 100), performance.AudienceSize);
            }
        }



        int CalculateTotalCost()
        {
            int cost = 0;
            foreach(var performance in invoice_.Performances) 
            {
                cost += CalculatePerformanceCost(performance);
            }
            return cost;
        }



        private void PrintTotalCost()
        {
            int cost = CalculateTotalCost();
            statement_ += String.Format(cultureInfo_, "Amount owed is {0:C}\n", Convert.ToDecimal(cost / 100));
        }



        int CalculateTotalCreditsEarned()
        {
            int credits = 0;
            foreach(var performance in invoice_.Performances) 
            {
                credits += CalculateCreditsEarned(performance);
            }
            return credits;

        }



        private void PrintTotalCreditsEarned()
        {
            int creditsEarned = CalculateTotalCreditsEarned();
            statement_ += String.Format("You earned {0} credits\n", creditsEarned);
        }



        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            // REVIEW move these sets to the constructor
            invoice_ = invoice;
            plays_   = plays;

            StartNewStatement();
            PrintCustomerInfo();
            PrintPerformanceList();
            PrintTotalCost();
            PrintTotalCreditsEarned();

            return statement_;
        }



        private CultureInfo              cultureInfo_;
        private Invoice                  invoice_;    
        private Dictionary<string, Play> plays_; 
        private string                   statement_;
    }
}
