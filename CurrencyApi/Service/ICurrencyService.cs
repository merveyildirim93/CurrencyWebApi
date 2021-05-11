using CurrencyApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyApi.Service
{
    public interface ICurrencyService
    {
        //Task<Currency[]> GetToday();
        List<Currency> GetToday();
        List<Currency> GetByDate(DateTime date);
    }
}
