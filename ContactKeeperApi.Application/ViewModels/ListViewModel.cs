using ContactKeeperApi.Application.Interfaces;
using System.Collections.Generic;

namespace ContactKeeperApi.Application.ViewModels
{
    public class ListViewModel<T> : IListViewModel<T>
    {
        /// <summary>
        /// Response Content
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Indicador se exiiste mais registros
        /// </summary>
        public bool HasNext { get; set; }

        /// <summary>
        /// Quantidade total de linhas para o filtro informado
        /// </summary>
        public int TotalItemCount { get; set; }
    }
}
