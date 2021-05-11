using CurrencyApi.Models;
using CurrencyApi.Service;
using System;
using System.Collections.Generic;
using Xunit;

namespace CurrencyApiTest
{
    public class CurrencyServiceTest
    {

        private readonly ICurrencyService currencyService;
        public CurrencyServiceTest()
        {
            currencyService = new CurrencyService();
        }

        [Fact]
        public async void GetTodayTest()
        {
            List<Currency> result = currencyService.GetToday();
            Assert.True(condition: result.Count > 0);
        }
    }
}
