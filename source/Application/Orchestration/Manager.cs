using SistemaDeHospedagem.Application.Services;
using SistemaDeHospedagem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeHospedagem.Application.Orchestration
{
    internal class Manager
    {
        private readonly QuartoService _quartoService;
        private readonly ReservaService _reservaService;

        public Manager(ReservaService reserva, QuartoService quarto)
        {
            _reservaService = reserva;
            _quartoService = quarto;           
        }

        public void AdicionarReserva(Reserva reserva) => _reservaService.Cadastrar(reserva);
        public void RemoverReserva(Reserva reserva) => _reservaService.Remover(reserva);
        public List<Reserva>? ListarReservas() => _reservaService.Listar();
        public bool IsQuartoReservado(int quartoID) => _reservaService.IsQuartoReservado(quartoID);
        public Reserva? GetReservaByQuartoID(int id) => _reservaService.GetReservaByQuartoID(id);       

        public void AdicionarQuarto(Quarto quarto) => _quartoService.Cadastrar(quarto);
        public void RemoverQuarto(Quarto? quarto) => _quartoService.Remover(quarto);
        public List<Quarto>? ListarQuartos() => _quartoService.Listar();
        public Quarto? GetQuartoByID(int id) => _quartoService.GetQuartoByID(id);
        public List<Quarto>? ListarQuartosDisponiveis() => _quartoService.ListarQuartosDisponiveis();
        public bool QuartoExist(int id) => _quartoService.QuartoExist(id);


    }
}
