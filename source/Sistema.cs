using SistemaDeHospedagem.Telas;
using System;
using System.Collections.Generic;
using System.Text;
using static SistemaDeHospedagem.Enums.Enums;

namespace SistemaDeHospedagem
{
    internal class Sistema
    {
        internal static bool IsRunning { get; set; }
        internal static bool isTelaRunning { get; set; }
        internal static bool hasOptionSelected { get; set; }
        internal static eEstadoSistema EstadoAtual { get; set; }
        internal static eTelas TelaAtual { get; set; }

        static Sistema()
        {
            IsRunning = true;
            EstadoAtual = eEstadoSistema.Inicializando;     
            Console.WriteLine("Sistema inicializado.");
        }

        internal static void GerenciarEstados()
        {
            Console.WriteLine("Gerenciar estados.");
            while (IsRunning)
            {
                switch (EstadoAtual)
                {
                    case eEstadoSistema.Inicializando:
                        // Lógica de inicialização
                        TelaAtual = eTelas.MenuInicial;
                        EstadoAtual = eEstadoSistema.Executando;
                        break;
                    case eEstadoSistema.Executando:
                        GerenciarTelas();
                        break;
                    case eEstadoSistema.Encerrado:
                        IsRunning = false;
                        break;
                }
            }
        }

        internal static void GerenciarTelas()
        {
            isTelaRunning = true;
            TelaBase? oTela = null;            

            switch (TelaAtual)
            {
                case eTelas.MenuInicial:                    
                    oTela = new TelaMenuInical("Menu Inicial");
                    break;
                case eTelas.MenuReservas:
                    oTela = new TelaMenuReserva("Menu de Reservas");
                    break;                
                case eTelas.MenuQuartos:
                    oTela = new TelaMenuQuartos("Menu Quartos");
                    break;
            }

            while (isTelaRunning)
            {               
                oTela?.Exibir();
            }
        }
    }
}


