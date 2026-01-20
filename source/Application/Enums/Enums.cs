using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeHospedagem.Application.Enums
{
    internal class Enums
    {
        public enum eEstadoSistema
        {
            Inicializando,
            Executando,
            Encerrado
        }

        public enum eTelas
        {
            MenuInicial,
            MenuReservas,
            MenuHospedes,
            MenuQuartos
        }

        public enum eOpcoesPadrao
        {
            Cadastrar = 1,
            Remover,
            Listar,
            Voltar
        }

        public enum eTipoQuarto
        {
            QuartoNormal = 1,
            Suite,
            SuiteLuxo
        }

        public enum eOpcoesMenuInical
        {
            GerenciarQuartos = 1,
            GerenciarReservas,
            Sair
        }               
    }
}
