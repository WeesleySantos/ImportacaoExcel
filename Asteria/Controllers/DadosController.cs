using Asteria_API.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics.Contracts;
using System.IO.Compression;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace Asteria.Controllers
{
    public class DadosController : Controller
    {
        private readonly HttpClient _httpClient;

        public DadosController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = TimeSpan.FromMinutes(10);
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            TempData.Remove("SuccessMessage");
            TempData.Remove("ErrorMessage");

            if (file != null && file.Length > 0)
            {
                try
                {
                    using (var stream = file.OpenReadStream())
                    {
                        var content = new StreamContent(stream);
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                        var formData = new MultipartFormDataContent();
                        formData.Add(content, "file", file.FileName);

                        var response = await _httpClient.PostAsync("https://localhost:7140/api/dados/upload", formData);

                        if (response.IsSuccessStatusCode)
                        {
                            TempData["SuccessMessage"] = "Arquivo enviado e processado com sucesso!";
                        }
                        else
                        {
                            TempData["ErrorMessage"] = "Erro ao processar o arquivo.";
                        }
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Ocorreu um erro ao enviar o arquivo: {ex.Message}";
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Nenhum arquivo foi selecionado.";
            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> BuscarDados(int lastId = 0, int pageSize = 10, string direction = "next", bool isPreviousPage = false)
        {
            var response = await _httpClient.GetAsync($"https://localhost:7140/api/dados/buscar?lastId={lastId}&pageSize={pageSize}&isPreviousPage={isPreviousPage}");

            if (response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return Json(null);
                }
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
                var dados = apiResponse.Data;
                int totalRecords = apiResponse.TotalRecords;

                return Json(new { data = dados, totalRecords });
            }

            TempData["ErrorMessage"] = "Erro ao buscar os dados.";
            return Json(null);
        }

    }
}
