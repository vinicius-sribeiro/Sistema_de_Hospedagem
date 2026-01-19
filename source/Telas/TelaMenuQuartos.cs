using SistemaDeHospedagem.Helper;
using SistemaDeHospedagem.Models;
using SistemaDeHospedagem.Telas;
using System;
using System.Collections.Generic;
using System.Text;
using static SistemaDeHospedagem.Enums.Enums;

namespace SistemaDeHospedagem.Telas
{
    internal class TelaMenuQuartos : TelaBase
    {
        public TelaMenuQuartos(string? titulo) : base(titulo)
        {
        }

        protected override void ProcessarEscolha(int escolha)
        {
            Sistema.hasOptionSelected = true;
            Console.Clear();

            switch ((eOpcoesPadrao)escolha)
            {
                case eOpcoesPadrao.Cadastrar:
                    CadastrarQuarto();
                    break;
                case eOpcoesPadrao.Remover:
                    RemoverQuarto();
                    break;
                case eOpcoesPadrao.Listar:                    
                    ListarQuartos();
                    break;
                case eOpcoesPadrao.Voltar:
                    VoltarTelaMenuInicial();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;

            }
        }

        private void CadastrarQuarto()
        {            
            Console.WriteLine("------ CADASTRO DE QUARTO ------\n");

            while (Sistema.hasOptionSelected)
            {                

                int id = ConsoleHelper.ReadValue<int>("\nDigite o número do quarto: ");                          

                if (Hotel.QuartoExist(id))
                {
                    Console.WriteLine("\nNúmero do Quarto já existe.");

                    Console.Write("\nDeseja tentar novamente? (S/N): ");
                }
                else
                {
                    eTipoQuarto tipoQuarto = ConsoleHelper.ReadEnum<eTipoQuarto>("\nSelecione o Tipo do Quarto: \n1 - Normal \n2 - Suíte \n3 - Luxo \nEscolha a opção: ");

                    int capacidade = ConsoleHelper.ReadValue<int>("\nDigite a Capacidade do quarto: ");

                    decimal valorDiaria = ConsoleHelper.ReadValue<decimal>("\nDigite o valor diário do quarto: ");

                    Quarto oQuarto = new Quarto(id, tipoQuarto, valorDiaria, capacidade);
                    Hotel.AddQuarto(oQuarto);

                    Console.Write("\nAdicionar mais Quartos? (S/N): ");
                }
                
                string confirmar = Console.ReadLine()!.ToUpper();

                if (confirmar == "S")
                {
                    Console.Clear();
                    Console.WriteLine("------ CADASTRO DE QUARTO ------\n");
                    continue;
                }

                QualquerTeclaParaContinuar();
                Sistema.hasOptionSelected = false;
                break;
            }
        }     
        
        private void RemoverQuarto()
        {
            Console.WriteLine("------ REMOVER QUARTO ------\n");

            while (Sistema.hasOptionSelected)
            {
                int id = ConsoleHelper.ReadValue<int>("Digite o Número do quarto: ");

                bool response = Hotel.RemoverQuarto(id);

                if (response)
                {
                    Console.WriteLine("Quarto removido com sucesso!");
                    Console.WriteLine("Deseja remover mais Quartos? (S/N): ");
                }
                else
                {
                    Console.WriteLine("Quarto Inexistente ou Número inválido. Não foi possível remover.");
                    Console.WriteLine("Deseja tentar novamente? (S/N): ");
                }

                string confirmar = Console.ReadLine()!.ToUpper();

                if (confirmar == "S")
                {
                    Console.Clear();
                    Console.WriteLine("------ REMOVER QUARTO ------\n");
                    continue;
                }

                QualquerTeclaParaContinuar();
                Sistema.hasOptionSelected = false;
                break;
            }
        }

        private void ListarQuartos()
        {
            Console.WriteLine("------ LISTA DE QUARTOS ------\n");

            List<Quarto>? listQuartos = Hotel.ListarQuartos();

            if (listQuartos != null)
            {
                Console.WriteLine("ID | Tipo \t| Vl.Diária \t| Capac. Máx\t| Disponível?");
                foreach (var quarto in listQuartos)
                {
                    quarto.ExibirInformacoes();                  
                }
            }                           

            QualquerTeclaParaContinuar();            
        }
    }
}
