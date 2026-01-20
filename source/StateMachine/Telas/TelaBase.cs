using SistemaDeHospedagem.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;
using static SistemaDeHospedagem.Application.Enums.Enums;

namespace SistemaDeHospedagem.Telas
{
    internal abstract class TelaBase
    {
        protected string? Titulo { get; set; }

        protected TelaBase(string? titulo)
        {
            Titulo = titulo;
        }

        // Permite que classes derivadas sobrescrevam para fornecer opções de menu específicas
        protected virtual Array GetOpcoesMenu()
        {
            return Enum.GetValues<eOpcoesPadrao>();
        }

        internal void Exibir()
        {
            Console.Clear();
            Console.WriteLine($"--- {Titulo ?? "Título não informado"} ---");

            ExibirMenu();

            int escolha = LerOpcao();
            ProcessarEscolha(escolha);
        }

        protected void ExibirMenu()
        {            
            // Percorre as opções do enum e exibe no console
            foreach (var opcao in GetOpcoesMenu())
            {
                Console.WriteLine($"{(int)opcao} - {opcao}");
            }
        }

        protected int LerOpcao()
        {
            Console.Write("\nEscolha uma opção: ");
            string? opcaoStr = Console.ReadLine();

            // Tenta converter a entrada para um número inteiro, caso falhe, retorna -1
            int opcao = int.TryParse(opcaoStr, out int value) ? value : -1;
            return opcao;
        }        

        protected abstract void ProcessarEscolha(int escolha);       

        protected void EncerrarTela()
        {
            Sistema.EstadoAtual = eEstadoSistema.Encerrado; // Encerra o sistema
            Sistema.isTelaRunning = false; // Indica que a tela atual deve ser encerrada
        }

        protected void QualquerTeclaParaContinuar()
        {
            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();
            return;
        }

        protected void VoltarTelaMenuInicial()
        {
            Sistema.TelaAtual = eTelas.MenuInicial;
            Sistema.isTelaRunning = false; // Indica que a tela atual deve ser encerrada
        }
    }
}
