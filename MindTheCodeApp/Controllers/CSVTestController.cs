using AppCore.Models.DTOs;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Asn1;
using System.Globalization;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MindTheCodeApp.Utils;
using AppCore.Models.BookModels;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace MindTheCodeApp.Controllers
{
    [Route("/csv")]
    public class CSVTestController : Controller
    {
        private readonly CsvUtils _csvUtils;
        private readonly ApplicationDbContext _context;

        public CSVTestController(CsvUtils csvUtils, ApplicationDbContext context)
        {
            _csvUtils = csvUtils;
            _context = context;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("/Views/CSVTest/CSVTest.cshtml");
        }

        [HttpPost("")]
        public IActionResult UploadCsv(IFormFile file)
        {
            if (file is null)
                return BadRequest();

            using (var reader = new StreamReader(file.OpenReadStream()))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<BookCsvDTOMap>();
                csv.Context.RegisterClassMap<AuthorCsvDTOMap>();
                csv.Context.RegisterClassMap<CategoryCsvDTOMap>();

                var records = csv.GetRecords<BookCsvDTO>().ToList();

                _csvUtils.ProcessCsv(
                    records,
                    out List<Book> books,
                    out List<BookAuthor> bookAuthors,
                    out List<BookCategory> bookCategories);

                _context.AddRange(bookAuthors);
                _context.AddRange(bookCategories);
                _context.AddRange(books);

                _context.SaveChanges();

                return View("/Views/CSVTest/CSVTest.cshtml");
            }
        }

    }
}
