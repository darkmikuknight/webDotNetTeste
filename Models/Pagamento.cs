using System;

namespace webDotNet.Models
{
    public class Pagamento
    {
        public int ID { get; set; }
        public double Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public string Descriacao { get; set; }
        public string ObraAssociada { get; set; }

    }
}