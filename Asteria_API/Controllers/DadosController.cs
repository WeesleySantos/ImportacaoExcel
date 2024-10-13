using Asteria_API.Data;
using Asteria_API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;

namespace Asteria_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DadosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DadosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadExcel([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido.");

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    using (var package = new ExcelPackage(memoryStream))
                    {
                        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                        var workbook = package.Workbook;
                        var worksheet = workbook.Worksheets[0]; 
                        var rowCount = worksheet.Dimension.Rows;

                        for (int row = 2; row <= rowCount; row++) 
                        {
                            if (string.IsNullOrWhiteSpace(worksheet.Cells[row, 1].Text))
                            {
                                break; 
                            }
                            var codigoCliente = worksheet.Cells[row, 1].Text;
                            var categoriaProduto = worksheet.Cells[row, 2].Text;
                            var skuProduto = worksheet.Cells[row, 3].Text;
                            var data = worksheet.Cells[row, 4].Text;
                            var quantidade = worksheet.Cells[row, 5].Text;
                            var valorFaturamento = worksheet.Cells[row, 6].Text;
                            var dataModel = new DataModel
                            {
                                CodigoCliente = int.Parse(codigoCliente),
                                CategoriaProduto = categoriaProduto,
                                SkuProduto = skuProduto,
                                Data = int.TryParse(data, out int excelDate) ? DateTime.FromOADate(excelDate) : Convert.ToDateTime(data),
                                Quantidade = int.Parse(quantidade),
                                ValorFaturamento = decimal.Parse(valorFaturamento)
                            };

                            _context.Planilha.Add(dataModel);
                        }

                        await _context.SaveChangesAsync(); 
                    }
                }

                return Ok("Dados salvos com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro ao processar o arquivo: {ex.Message}");
            }
        }

        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarDados(int? lastId = null, int pageSize = 15, bool isPreviousPage = false)
        {
            var query = _context.Planilha.AsQueryable();

            if (isPreviousPage)
            {
                if (lastId.HasValue && lastId.Value > 0)
                {
                    var startId = lastId.Value - 14; 
                    query = query.Where(p => p.Id < startId).OrderByDescending(p => p.Id);
                }

                var dados = await query.Take(pageSize).ToListAsync();

                dados = dados.OrderBy(p => p.Id).ToList();

                if (!dados.Any())
                {
                    return NoContent(); 
                }

                var response = new
                {
                    data = dados, 
                    firstId = dados.First().Id,
                    totalRecords = _context.Planilha.Count() 
                };

                return Ok(response);
            }
            else
            {
                if (lastId.HasValue && lastId.Value > 0)
                {
                    query = query.Where(p => p.Id > lastId.Value).OrderBy(p => p.Id);
                }

                var dados = await query.OrderBy(d => d.Id).Take(pageSize).ToListAsync();

                if (!dados.Any())
                {
                    return NoContent();
                }

                var response = new
                {
                    data = dados,
                    lastId = dados.Last().Id, 
                    totalRecords = _context.Planilha.Count()
                };

                return Ok(response);
            }
        }
    }
}
