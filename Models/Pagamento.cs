using System;
using System.ComponentModel.DataAnnotations;

namespace webDotNet.Models
{
    public class Pagamento
    {
        public int ID { get; set; }
        public double Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        public string Descriacao { get; set; }
        public string ObraAssociada { get; set; }

    }
}