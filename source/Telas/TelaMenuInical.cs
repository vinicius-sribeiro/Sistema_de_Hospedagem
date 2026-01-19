using System;
using System.Collections.Generic;
using System.Text;
using static SistemaDeHospedagem.Enums.Enums;

namespace SistemaDeHospedagem.Telas
{
    internal class TelaMenuInical : TelaBase
    {       
        public TelaMenuInical(string? titulo) : base(titulo)
        {          
        }

        protected override Array GetOpcoesMenu()
        {
            return Enum.GetValues<eOpcoesMenuInical>();
        }

        protected override void ProcessarEscolha(int escolha)
        {      
            switch ((eOpcoesMenuInical)escolha)
            {
                case eOpcoesMenuInical.GerenciarQuartos:
                    Sistema.TelaAtual = eTelas.MenuQuartos;
                    Sistema.isTelaRunning = false;
                    break;
                case eOpcoesMenuInical.GerenciarReservas:                                    
                    Sistema.TelaAtual = eTelas.MenuReservas;
                    Sistema.isTelaRunning = false; // Sair da tela atual para ir para a tela de reservas
                    break;
                case eOpcoesMenuInical.Sair:
                    // Encrerrar o aplicativo
                    EncerrarTela();                    
                    break;
                default:
                    Console.WriteLine("Opção inválida. Pressione qualquer tecla para tentar novamente...");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
