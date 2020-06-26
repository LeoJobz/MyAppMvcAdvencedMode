using AppMvcEasyMode.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Interfaces
{
    interface IProdutoService
    {
        Task Add(Produto produto);
        Task Update(Produto produto);
        Task Delete(Guid id);
    }
}
