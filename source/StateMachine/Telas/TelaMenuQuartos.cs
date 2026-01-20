using SistemaDeHospedagem.Domain.Entities;
using SistemaDeHospedagem.Helper;
using SistemaDeHospedagem.StateMachine;
using SistemaDeHospedagem.Telas;
using System;
using System.Collections.Generic;
using System.Text;
using static SistemaDeHospedagem.Application.Enums.Enums;

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

                if (Sistema.Manager.QuartoExist(id))
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
                    Sistema.Manager.AdicionarQuarto(oQuarto);

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

                Quarto? quarto = Sistema.Manager.GetQuartoByID(id);

                if (quarto != null && !quarto.GetDisponibilidade())
                {
                    Console.WriteLine("Quarto está ocupado. Não é possível remover.");
                    Console.WriteLine("Deseja tentar novamente? (S/N): ");
                    string confirmarOcupado = Console.ReadLine()!.ToUpper();
                    if (confirmarOcupado == "S")
                    {
                        Console.Clear();
                        Console.WriteLine("------ REMOVER QUARTO ------\n");
                        continue;
                    }
                    QualquerTeclaParaContinuar();
                    Sistema.hasOptionSelected = false;
                    break;
                }

                Sistema.Manager.RemoverQuarto(quarto!);                

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

            List<Quarto>? listQuartos = Sistema.Manager.ListarQuartos();

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
