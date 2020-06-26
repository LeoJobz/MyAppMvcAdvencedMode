using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Services
{
    public class ProdutoService : BaseService, IProdutoService
    {
        public async Task Add(Produto produto)
        {
            if (!ValidationExecute(new ProdutoValidation(), produto)) return;
        }
        public async Task Update(Produto produto)
        {
            if (!ValidationExecute(new ProdutoValidation(), produto)) return;
        }

        public Task Delete(Guid id)
        {
            throw new NotImplementedException();
        }

    }
}
