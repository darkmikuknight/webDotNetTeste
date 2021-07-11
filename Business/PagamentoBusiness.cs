using dotnetcoreWebapp.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using webDotNet.Models;

namespace webDotNet.Business
{
    public class PagamentoBusiness
    {
        private AplicationDB _db;

        public PagamentoBusiness(AplicationDB db)
        {
            _db = db;
        }

        public bool ValidateCadastro(Pagamento model, out string errorMsg, ModelValidationError results)
        {
            bool isOk = true;
            errorMsg = "";

            if (String.IsNullOrEmpty(model.Descriacao))
            {
                results.Add(nameof(Pagamento.Descriacao), "A descrição é obrigatória.");
                errorMsg = "A descrição é obrigatória.";
                isOk = false;
            }

            if (String.IsNullOrEmpty(model.ObraAssociada))
            {
                results.Add(nameof(Pagamento.ObraAssociada), "A obra associada é obrigatória.");
                errorMsg = "A obra associada é obrigatória";
                isOk = false;
            }

            DateTime dateValue = Convert.ToDateTime(model.DataPagamento);
            if (dateValue.Equals(null)) // TODO Pensar em outra maneira de verificar isso sem usar viewModel
            {
                results.Add(nameof(Pagamento.DataPagamento), "A data informada é inválida.");
                errorMsg = "A data informada é inválida.";
                isOk = false;
            }
            else if (model.DataPagamento.Date > DateTime.Now.Date)
            {
                results.Add(nameof(Pagamento.DataPagamento), "A data informada não pode ser posterior a data de hoje.");
                errorMsg = "A data informada não pode ser posterior a data de hoje.";
                isOk = false;
            }

            if (model.Valor <= 0)
            {
                results.Add(nameof(Pagamento.Valor), "O valor informado tem que ser maior que zero.");
                errorMsg = "O valor informado tem que ser maior que zero.";
                isOk = false;
            }

            return isOk;
        }

        public void SavePagamento (Pagamento model)
        {
            try 
            {
                _db.Pagamentos.Add(model);
                _db.SaveChanges();
            } 
            catch (Exception e)
            {
                throw new Exception("Não foi possível salvar o registro de pagamento", e);
            }
        }

        public List<Pagamento> ListAll()
        {
            return _db.Pagamentos.OrderByDescending(x => x.DataPagamento).ToList();
        }

        public List<Pagamento> ListFilteredPagamento(string srchText)
        {
            return _db.Pagamentos.OrderByDescending(x => x.DataPagamento)
                    .Where(x => x.Descriacao.Contains(srchText) || x.ObraAssociada.Contains(srchText))
                    .ToList();
        }
    }
}