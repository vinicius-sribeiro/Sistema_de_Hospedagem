using SistemaDeHospedagem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeHospedagem.Application.Services
{
    internal class ReservaService
    {
        private List<Reserva> _reservas = new List<Reserva>();

        public List<Reserva> Reservas { get { return _reservas; } }

        public void Cadastrar(Reserva entidade)
        {
            _reservas.Add(entidade);
        }       

        public void Remover(Reserva entidade)
        {
            entidade.Quarto!.IsDisponivel = true;
            bool sucess = _reservas.Remove(entidade);

            if (sucess)
            {
                Console.WriteLine("Reserva removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível remover a reserva.");
            }
        }

        public List<Reserva>? Listar()
        {
            if (_reservas.Count == 0)
            {
                Console.WriteLine("Nenhum reserva cadastrada no sistema.");
                return null;
            }

            return Reservas;
        }

        public Reserva? GetReservaByQuartoID(int quartoID)
        {
            if (_reservas.Count == 0)
            {
                Console.WriteLine("Nenhuma reserva cadastrada no sistema.");
                return null;
            }

            var reserva = Reservas.FirstOrDefault(r => r.Quarto!.Id == quartoID);

            if (reserva == null)
            {
                Console.WriteLine("Não foi encontrada nenhuma reserva cadastrada com esse quarto.");
                return null;
            }

            return reserva;
        }

        public bool IsQuartoReservado(int id)
        {
            var wasReserved = Reservas.Find(r => r.Quarto!.Id == id);

            if (wasReserved == null)
            {
                return false;
            }

            return true;
        }



    }
}
