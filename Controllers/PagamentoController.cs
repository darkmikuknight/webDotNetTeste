using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.Extensions.DependencyInjection;
using webDotNet.Business;
using webDotNet.Models;
using webDotNet.Models.Support;
using System.Collections.Generic;

namespace webDotNet.Controllers
{
    public class PagamentoController : Controller
    {
        private PagamentoBusiness _pagamento { get; set; }

        public PagamentoController(PagamentoBusiness pagamento)
        {
            _pagamento = pagamento;
        }
        public IActionResult Cadastrar()
        {
            return View();
        }

        public IActionResult Salvar(Pagamento model)
        {
            ModelValidationError results = new ModelValidationError();

            if(!_pagamento.ValidateCadastro(model, out string errorMsg, results))
            {
                ViewBag.ErrorMessage = errorMsg;
                results.UpdateModelState(ModelState);
                return View("Cadastrar", model);
            }
            else
            {
                _pagamento.SavePagamento(model);
                return RedirectToAction("Listar");
            }
        }

        public IActionResult Listar(List<Pagamento> pagList = null)
        {
            List<Pagamento> pagamentos = pagList.Count == 0 ? _pagamento.ListAll() : pagList;
            return View(pagamentos);
        }

        [HttpPost]
        public void FilterPagamento(string srchText)
        {
            List<Pagamento> pagamentos = _pagamento.ListFilteredPagamento(srchText);
            Listar(pagamentos);
        }
    }
}