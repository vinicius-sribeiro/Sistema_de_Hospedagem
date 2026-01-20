using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeHospedagem.Domain.Entities
{
    internal class Hospede
    {
        public string? Nome { get; init; }
        public string? Sobrenome { get; init; }
        public string? Cpf { get; init; }

        public Hospede(string? nome, string? sobrenome, string? cpf)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            Cpf = cpf;
        }
    }
}
