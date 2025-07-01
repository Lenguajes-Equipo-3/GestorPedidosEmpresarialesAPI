using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GestorPedidosEmpresarialesBackend.Data;
using GestorPedidosEmpresarialesBackend.Domain;

namespace GestorPedidosEmpresarialesBackend.Business
{
    public class CategoriaBusiness
    {
        private readonly CategoriaData categoriaData;

        public CategoriaBusiness(CategoriaData categoriaData)
        {
            this.categoriaData = categoriaData;
        }

        public List<Categoria> GetAllCategorias() => categoriaData.GetAllCategorias();

        public Categoria? GetCategoriaById(int id) => categoriaData.GetCategoriaById(id);

        public void AddCategoria(Categoria categoria) => categoriaData.InsertCategoria(categoria);

        public void DeleteCategoria(int id) => categoriaData.DeleteCategoria(id);
    }
}
