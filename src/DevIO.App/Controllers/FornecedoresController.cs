using AppMvcEasyMode.Models;
using AutoMapper;
using DevIO.App.ViewModels;
using DevIO.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DevIO.App.Controllers
{
    public class FornecedoresController : BaseController
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public FornecedoresController(IFornecedorRepository fornecedorRepository,
                                      IMapper mapper,
                                      IEnderecoRepository enderecoRepository)
        {
            _fornecedorRepository = fornecedorRepository;
            _mapper = mapper;
            _enderecoRepository = enderecoRepository;
        }

        [Route("lista-de-fornecedores")]
        public async Task<IActionResult> Index()
        {
            //casting to FornecedorViewModel
            return View(_mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ListAll()));
        }

        [Route("dados-do-fornecedor/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var fornecedorViewModel = await GetFornecedorEndereco(id);

            if (fornecedorViewModel == null)
            {
                return NotFound();
            }

            return View(fornecedorViewModel);
        }

        [Route("novo-fornecedor")]
        public IActionResult Create()
        {
            return View();
        }

        [Route("novo-fornecedor")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FornecedorViewModel fornecedorViewModel)
        {
            if (!ModelState.IsValid)
                return RedirectToAction(nameof(Index));

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorRepository.Add(fornecedor);

            return RedirectToAction("Index");
        }

        [Route("editar-fornecedor/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fornecedorViewModel = await GetFornecedorProdutosEndereco(id);

            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        [Route("editar-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, FornecedorViewModel fornecedorViewModel)
        {
            if (id != fornecedorViewModel.Id)
                return NotFound();

            if (!ModelState.IsValid)
                return View(fornecedorViewModel);

            var fornecedor = _mapper.Map<Fornecedor>(fornecedorViewModel);
            await _fornecedorRepository.Update(fornecedor);

            return RedirectToAction("Index");
        }

        [Route("excluir-fornecedor/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fornecedorViewModel = await GetFornecedorEndereco(id);

            if (fornecedorViewModel == null)
                return NotFound();

            return View(fornecedorViewModel);
        }

        [Route("excluir-fornecedor/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fornecedorViewModel = await GetFornecedorEndereco(id);

            if (fornecedorViewModel == null)
                return NotFound();

            await _fornecedorRepository.Remove(id);

            return RedirectToAction("Index");
        }

        [Route("obter-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> GetEndereco(Guid id)
        {
            var fornecedor = await GetFornecedorEndereco(id);

            if (fornecedor == null)
                return NotFound();

            return PartialView("_DetalhesEndereco", fornecedor);
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id)
        {
            var fornecedor = await GetFornecedorEndereco(id);

            if (fornecedor == null)
                return NotFound();

            return PartialView("_AtualizarEndereco", new FornecedorViewModel { Endereco = fornecedor.Endereco });
        }

        [Route("atualizar-endereco-fornecedor/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtualizarEndereco(FornecedorViewModel fornecedorViewModel)
        {
            ModelState.Remove("Nome");
            ModelState.Remove("Documento");

            if (!ModelState.IsValid)
                return PartialView("_AtualizarEndereco", fornecedorViewModel);

            await _enderecoRepository.Update(_mapper.Map<Endereco>(fornecedorViewModel.Endereco));

            var url = Url.Action("GetEndereco", "Fornecedores", new { id = fornecedorViewModel.Endereco.FornecedorId });

            return Json(new { success = true, url });
        }

        private async Task<FornecedorViewModel> GetFornecedorEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.GetFornecedorEndereco(id));
        }

        private async Task<FornecedorViewModel> GetFornecedorProdutosEndereco(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.GetFornecedorProdutosEndereco(id));
        }
    }
}
