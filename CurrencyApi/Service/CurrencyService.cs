using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CurrencyApi.Models;
using CurrencyApi.Serializer;

namespace CurrencyApi.Service
{
    public class CurrencyService : ICurrencyService
    {
        private string xmlUrl = "https://www.tcmb.gov.tr/kurlar/{0}.xml";
        private readonly WebClient webClient;
        private readonly IXmlSerializer serializer;
        public CurrencyService()
        {
            webClient = new WebClient
            {
                Encoding = Encoding.UTF8
            };
            serializer = new XmlSerializer();
        }

        public List<Currency> GetToday()
        {
            var url = new Uri(string.Format(xmlUrl, "today"));
            var jsonData = webClient.DownloadString(url);
            var deserializer = serializer.Deserializer<Tarih_Date>(jsonData);
            var result = deserializer.Currency.Select(Currency.Map).ToList();
            return result;
        }
        public List<Currency> GetByDate(DateTime date)
        {
            var day = date.Day > 0 && date.Day < 10 ? $"0{date.Day}" : date.Day.ToString();
            var month = date.Month > 0 && date.Month < 10 ? $"0{date.Month}" : date.Month.ToString();
            var url = new Uri(string.Format(xmlUrl, $"{date.Year}{month}/{day}{month}{date.Year}"));
            var jsonData = webClient.DownloadString(url);
            var deserializer = serializer.Deserializer<Tarih_Date>(jsonData);
            var result = deserializer.Currency.Select(Currency.Map).ToList();
            return result;
        }

    }
}
