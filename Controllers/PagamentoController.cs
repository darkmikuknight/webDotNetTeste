using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.Extensions.DependencyInjection;
using webDotNet.Business;
using webDotNet.Models;

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
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Listar()
        {
            return View();
        }
    }
}