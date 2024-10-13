using System.Security.Principal;

namespace Asteria_API.Model
{
    public class DataModel
    {
        public int Id { get; set; }
        public int CodigoCliente { get; set; }
        public string CategoriaProduto  { get; set; }
        public string SkuProduto  { get; set; }
        public DateTime Data { get; set; }
        public int Quantidade { get; set; }
        public decimal ValorFaturamento { get; set; }
    }
}
