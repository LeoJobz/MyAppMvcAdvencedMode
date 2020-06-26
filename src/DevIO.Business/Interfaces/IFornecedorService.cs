using DevIO.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    interface IFornecedorService
    {
        Task Add(Fornecedor fornecedor);
        Task Update(Fornecedor fornecedor);
        Task Delete(Guid id);
        Task UpdateEndereco(Endereco endereco);
    }
}
