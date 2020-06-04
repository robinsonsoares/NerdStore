using NerdStore.Core.Messages;
using System;
using System.Dynamic;

namespace NerdStore.Vendas.Application.Events
{
   public  class PedidoFinalizadoEvent : Event
    {
        public Guid PedidoId { get; private set; }

        public PedidoFinalizadoEvent( Guid pedidoId)
        {
            PedidoId = pedidoId;
            AggregateId = pedidoId;
        }
    }
}
