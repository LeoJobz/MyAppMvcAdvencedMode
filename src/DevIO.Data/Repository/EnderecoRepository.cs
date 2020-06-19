using System;
using System.Threading.Tasks;
using AppMvcEasyMode.Models;
using DevIO.Business.Interfaces;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MyDbContext context) : base(context) { }

        public async Task<Endereco> GetEnderecoByFornecedor(Guid fornecedorId)
        {
            return await Db.Enderecos.AsNoTracking()
                  .FirstOrDefaultAsync(f => f.FornecedorId == fornecedorId);
        }
    }
}
