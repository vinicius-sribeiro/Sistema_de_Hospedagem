using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeHospedagem.Models
{
    internal class Hotel
    {

        private static List<Quarto> _quartos { get; set; }

        private static List<Reserva> _reservas { get; set; }

        // ----- Propriedades -----
        public static List<Quarto> Quartos
        {
            get { return _quartos; }
        }

        public static List<Reserva> Reservas
        {
            get { return _reservas; }
        }

        static Hotel()
        {
            _reservas = new List<Reserva>();
            _quartos = new List<Quarto>();
        }

        // ----- Metódos para gerenciar Reservas -----

        public static void AddReserva(Reserva reserva)
        {
            _reservas.Add(reserva);
        }
        public static void RemoverReserva(Reserva reserva)
        {
            reserva.Quarto!.IsDisponivel = true;
            bool sucess = _reservas.Remove(reserva);

            if (sucess)
            {
                Console.WriteLine("Reserva removido com sucesso!");
            }
            else
            {
                Console.WriteLine("Não foi possível remover a reserva.");
            }
        }

        public static List<Reserva>? ListarReservas()
        {
            if (_reservas.Count == 0)
            {
                Console.WriteLine("Nenhum reserva cadastrada no sistema.");
                return null;
            }

            return Reservas;
        }

        public static bool QuartoReservado(int id)
        {
            var wasReserved = Reservas.Find(r => r.Quarto!.Id == id);

            if (wasReserved == null)
            {
                return false;
            }

            return true;
        }

        public static Reserva? GetReservaByQuartoID(int quartoID)
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

        // ----- Metódos para gerenciar Quartos -----

        public static void AddQuarto(Quarto q)
        {            
            _quartos.Add(q);
        }

        public static bool RemoverQuarto(int id)
        {
            bool sucess = false;

            var qRemove = _quartos.FirstOrDefault(q => q.Id == id);

            if (qRemove != null)
            {
                sucess = _quartos.Remove(qRemove);                
            }            

            return sucess;
        }

        public static List<Quarto>? ListarQuartos()
        {
            if (_quartos.Count == 0) 
            {
                Console.WriteLine("Nenhum quarto cadastrado no sistema.");
                return null;
            }

            return Quartos;
        }

        public static bool QuartoExist(int id)
        {
            return Quartos.Exists(quarto => quarto.Id == id);
        }

        public static Quarto GetQuartoById(int id)
        {
            return Quartos.Find(quarto => quarto.Id == id)!;
        }

        // Retorna a lista de suítes disponíveis para reserva
        public static List<Quarto>? ListarQuartosDisponiveis()
        {
            if (_quartos.Count == 0) return null;

            return Quartos.FindAll(Quarto => Quarto.GetDisponibilidade());
        }
    }
}
