using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeHospedagem.Models
{
    internal class Reserva
    {        
        private Guid Id { get; set; }        
        public int DiasReservados { get; set; }
        private List<Hospede> Hospedes { get; set; }
        public Quarto? Quarto { get; set; }

        public Reserva()
        {
            Id = Guid.NewGuid();
            Hospedes = new List<Hospede>();
        }

        public void AddHospedes(List<Hospede> hospedes)
        {
            Hospedes = hospedes;
        }   

        public int ObterQuantidadeHospedes()
        {            
            return Hospedes?.Count ?? 0;
        }

        public decimal CalcularValorDiaria()
        {
            return DiasReservados * Quarto!.ValorDiaria;
        }

        public void ExibirInformacoes()
        {
            Console.WriteLine($"\t{DiasReservados} \t| " +
                $"\t{ObterQuantidadeHospedes()} \t| " +
                $"\t{Quarto!.Id} \t| " +
                $" R$ {CalcularValorDiaria()}");
        }
        public void ExibirInformacoes_NoQuantHospedes(int quantHospedes)
        {
            Console.WriteLine($"\t{DiasReservados} \t| " +
                $"\t{quantHospedes} \t| " +
                $"\t{Quarto!.Id} \t| " +
                $" R$ {CalcularValorDiaria()}");
        }

    }
}
