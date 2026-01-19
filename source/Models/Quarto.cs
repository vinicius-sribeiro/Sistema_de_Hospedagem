using System;
using System.Collections.Generic;
using System.Text;
using static SistemaDeHospedagem.Enums.Enums;

namespace SistemaDeHospedagem.Models
{
    internal class Quarto
    {        
        public int Id { get; init; }        
        public eTipoQuarto TipoQuarto { get; init; }
        public decimal ValorDiaria { get; init; }
        public int CapacidadeMaxima { get; init; }
        public bool IsDisponivel { get; set; }

        public Quarto(int id, eTipoQuarto tipoQuato, decimal valorDiaria, int capacidadeMaxima)
        {
            Id = id;
            TipoQuarto = tipoQuato;
            ValorDiaria = valorDiaria;
            CapacidadeMaxima = capacidadeMaxima;
            IsDisponivel = true;
        }

        public bool GetDisponibilidade()
        {
            return IsDisponivel;
        }   

        public void ExibirInformacoes()
        {
            string disponivel = IsDisponivel ? "Sim" : "Não";
            string tipoStr = "";

            switch (TipoQuarto)
            {
                case eTipoQuarto.QuartoNormal:
                    tipoStr = $"{TipoQuarto}|";
                    break;

                case eTipoQuarto.Suite:
                case eTipoQuarto.SuiteLuxo:
                    tipoStr = $"{TipoQuarto} \t|";
                    break;                
            }
            
            Console.WriteLine($"{Id} | " +
                tipoStr +
                $"\t{ValorDiaria} \t| " +
                $"\t{CapacidadeMaxima} \t| " +
                $"{disponivel}");
        }
    }
}
