using ClosedXML.Excel;
using CurrencyApi.Models;
using CurrencyApi.Serializer;
using CurrencyApi.Service;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace CurrencyApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        ICurrencyService _currencyService;
        public CurrenciesController(ICurrencyService currencyService)
        {
            _currencyService = currencyService;
        }

        [HttpGet("gettoday")]
        public IActionResult GetToday()
        {
            var result = _currencyService.GetToday();
            return Ok(result);
        }

        [HttpGet("ConvertToCsv")]
        public IActionResult ConvertToCsv()
        {
            var result = _currencyService.GetToday();
            try
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("CurrencyName");
                stringBuilder.Append(" - ");
                stringBuilder.Append("ForexBuying");
                stringBuilder.AppendLine();
                foreach (var cr in result.ToList())
                {
                    stringBuilder.Append(cr.CurrencyName);
                    stringBuilder.Append(" - ");
                    stringBuilder.Append(cr.ForexBuying);
                    stringBuilder.AppendLine();
                }
                return File(Encoding.UTF8.GetBytes
                (stringBuilder.ToString()), "text/csv", "dovizkur.csv");
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("ConvertToExcel")]
        public IActionResult DownloadExcelDocument()
        {
            var result = _currencyService.GetToday();
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "TcmbDovizKurlari.xlsx";
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Döviz Kurları");
                    worksheet.Cell(1, 1).Value = "Unit";
                    worksheet.Cell(1, 2).Value = "CurrencyName";
                    worksheet.Cell(1, 3).Value = "ForexBuying";
                    worksheet.Cell(1, 4).Value = "ForexSelling";
                    worksheet.Cell(1, 5).Value = "BanknoteBuying";
                    worksheet.Cell(1, 6).Value = "BanknoteSelling";
                    worksheet.Cell(1, 7).Value = "CrossRateUSD";
                    for (int index = 1; index <= result.Count(); index++)
                    {
                        worksheet.Cell(index + 1, 1).Value = result[index - 1].Unit;
                        worksheet.Cell(index + 1, 2).Value = result[index - 1].CurrencyName;
                        worksheet.Cell(index + 1, 3).Value = result[index - 1].ForexBuying;
                        worksheet.Cell(index + 1, 4).Value = result[index - 1].ForexSelling;
                        worksheet.Cell(index + 1, 5).Value = result[index - 1].BanknoteBuying;
                        worksheet.Cell(index + 1, 6).Value = result[index - 1].BanknoteSelling;
                        worksheet.Cell(index + 1, 7).Value = result[index - 1].CrossRateUsd;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return File(content, contentType, fileName);
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
