﻿using System;
using System.Threading.Tasks;
using NerdStore.Catalogo.Domain.Events;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;

namespace NerdStore.Catalogo.Domain
{
    public class EstoqueService : IEstoqueService
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IMediatorHandler _mediatorHandler;

        public EstoqueService(IProdutoRepository produtoRepository,
                              IMediatorHandler mediatorHandler)
        {
            _produtoRepository = produtoRepository;
            _mediatorHandler = mediatorHandler;
        }
        public async Task<bool> DebitarEstoque(Guid produtoId, int quantidade)
        {
            if (!await DebitarItemEstoque(produtoId, quantidade)) return false;

            return await _produtoRepository.UnitOfWork.Commit();
        }

        private async Task<bool> DebitarItemEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            if (!produto.PossuiEstoque(quantidade))
            {
                await _mediatorHandler.PublicarNotificacao(new DomainNotification("Estoque", $"Produto - {produto.Nome} sem estoque"));
                return false;
            }

            produto.DebitarEstoque(quantidade);

            // TODO: 10 pode ser parametrizavel em arquivo de configuração
            if (produto.QuantidadeEstoque < 10)
            {
                await _mediatorHandler.PublicarEvento(new ProdutoAbaixoEstoqueEvent(produto.Id, produto.QuantidadeEstoque));
            }

            _produtoRepository.Atualizar(produto);
            return true;
        }

        public  async Task<bool> ReporEstoque(Guid produtoId, int quantidade)
        {
            var produto = await _produtoRepository.ObterPorId(produtoId);

            if (produto == null) return false;

            produto.ReporEstoque(quantidade);

            _produtoRepository.Atualizar(produto);

            return await _produtoRepository.UnitOfWork.Commit();
        }
        public void Dispose()
        {
            _produtoRepository.Dispose();
        }
    }
}
