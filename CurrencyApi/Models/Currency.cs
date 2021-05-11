using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyApi.Models
{
    public class Currency
    {
        public int Unit { get; set; }
        public string Isım { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencyCode { get; set; }
        public string CrossRateUsd { get; set; }
        public decimal ForexBuying { get; set; }
        public string ForexSelling { get; set; }
        public string BanknoteBuying { get; set; }
        public string BanknoteSelling { get; set; }

        public static Currency Map(Tarih_DateCurrency x)
        {
            return new Currency
            {
                Unit = x.Unit,
                Isım = x.Isim,
                CurrencyName = x.CurrencyName,
                CurrencyCode = x.CurrencyCode,
                BanknoteSelling = x.BanknoteSelling,
                BanknoteBuying = x.BanknoteBuying,
                ForexBuying = x.ForexBuying,
                ForexSelling = x.ForexSelling,
                CrossRateUsd = x.CrossRateUSD
            };
        }
    }
}
