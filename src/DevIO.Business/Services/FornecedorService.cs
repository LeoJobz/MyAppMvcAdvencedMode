using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _EnderecoRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository, 
                                 IEnderecoRepository enderecoRepository,
                                 INotifier notifier) : base(notifier)
        {
            _fornecedorRepository = fornecedorRepository;
            _EnderecoRepository = enderecoRepository;
        }

        public async Task Add(Fornecedor fornecedor)
        {
            //validar estado da entidade
            if (!ValidationExecute(new FornecedorValidation(), fornecedor)
                || !ValidationExecute(new EnderecoValidation(), fornecedor.Endereco));

            if (_fornecedorRepository.Search(f => f.Documento == fornecedor.Documento).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _fornecedorRepository.Add(fornecedor);

        }

        public async Task Update(Fornecedor fornecedor)
        {
            if (!ValidationExecute(new FornecedorValidation(), fornecedor)) return;

            if (_fornecedorRepository.Search(f => f.Documento == fornecedor.Documento && f.Id != fornecedor.Id).Result.Any())
            {
                Notify("Já existe um fornecedor com este documento informado.");
                return;
            }

            await _fornecedorRepository.Update(fornecedor);
        }

        public async Task UpdateEndereco(Endereco endereco)
        {
            if (!ValidationExecute(new EnderecoValidation(), endereco)) return;

            await _EnderecoRepository.Update(endereco);
        }
        public async Task Delete(Guid id)
        {
            if (_fornecedorRepository.GetFornecedorProdutosEndereco(id).Result.Produtos.Any())
            {
                Notify("O fornecedor possui produtos cadastrados.");
                return;
            }

            await _fornecedorRepository.Remove(id);
        }

        public void Dispose()
        {
            _fornecedorRepository?.Dispose();
            _EnderecoRepository?.Dispose();
        }
    }
}
