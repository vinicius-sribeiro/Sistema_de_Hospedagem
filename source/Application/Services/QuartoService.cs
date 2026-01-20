using SistemaDeHospedagem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeHospedagem.Application.Services
{
    internal class QuartoService
    {
        private List<Quarto> _quartos;

        public List<Quarto> Quartos { get { return _quartos; } }

        public QuartoService()
        {
            _quartos = new List<Quarto>();
        }

        public void Cadastrar(Quarto entidade)
        {
            _quartos.Add(entidade);
        }

        public void Remover(Quarto? entidade)
        {
            bool sucess = false;

            if (entidade == null)
            {
                Console.WriteLine("Quarto Inexistente ou Número inválido. Não foi possível remover.");
                Console.WriteLine("Deseja tentar novamente? (S/N): ");
                return;
            }

            sucess = _quartos.Remove(entidade);

            if (sucess)
            {
                Console.WriteLine("Quarto removido com sucesso!");
                Console.WriteLine("Deseja remover mais Quartos? (S/N): ");
            }
            else
            {
                Console.WriteLine("Quarto Inexistente ou Número inválido. Não foi possível remover.");
                Console.WriteLine("Deseja tentar novamente? (S/N): ");
            }
        }

        public List<Quarto>? Listar()
        {
            if (_quartos.Count == 0)
            {
                Console.WriteLine("Nenhum quarto cadastrado no sistema.");
                return null;
            }

            return Quartos;
        }

        public Quarto? GetQuartoByID(int quartoID)
        {
            return Quartos.Find(quarto => quarto.Id == quartoID)!;
        }

        public bool QuartoExist(int id)
        {
            return Quartos.Exists(quarto => quarto.Id == id);
        }

        // Retorna a lista de suítes disponíveis para reserva
        public List<Quarto>? ListarQuartosDisponiveis()
        {
            if (Quartos.Count == 0) return null;

            return Quartos.FindAll(Quarto => Quarto.GetDisponibilidade());
        }
    }
}
