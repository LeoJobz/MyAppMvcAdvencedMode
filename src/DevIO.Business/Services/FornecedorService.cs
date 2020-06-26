using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class FornecedorService : BaseService, IFornecedorService
    {
        public async Task Add(Fornecedor fornecedor)
        {
            //validar estado da entidade
            if (!ValidationExecute(new FornecedorValidation(), fornecedor)
                && !ValidationExecute(new EnderecoValidation(), fornecedor.Endereco)) return;
        }


        public async Task Update(Fornecedor fornecedor)
        {
            if (!ValidationExecute(new FornecedorValidation(), fornecedor)) return;
        }

        public async Task UpdateEndereco(Endereco endereco)
        {
            if (!ValidationExecute(new EnderecoValidation(), endereco)) return;
        }
        public async Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
