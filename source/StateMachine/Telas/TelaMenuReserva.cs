using SistemaDeHospedagem.Domain.Entities;
using SistemaDeHospedagem.Helper;
using SistemaDeHospedagem.StateMachine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using static SistemaDeHospedagem.Application.Enums.Enums;

namespace SistemaDeHospedagem.Telas
{
    internal class TelaMenuReserva : TelaBase
    {        
        private Reserva Reserva;

        public TelaMenuReserva(string? titulo) : base(titulo)
        {
            Reserva = new Reserva();
        }

        protected override void ProcessarEscolha(int escolha)
        {
            Sistema.hasOptionSelected = true;
            Console.Clear();

            switch ((eOpcoesPadrao)escolha)
            {
                case eOpcoesPadrao.Cadastrar:
                    CadastrarReserva();
                    break;
                case eOpcoesPadrao.Remover:
                    RemoverReserva();
                    break;
                case eOpcoesPadrao.Listar:
                    ListarReservas();
                    break;
                case eOpcoesPadrao.Voltar:
                    VoltarTelaMenuInicial();
                    break;
                default:
                    Console.WriteLine("Opção inválida. Tente novamente.");
                    break;

            }
        }


        private void CadastrarReserva()
        {
            bool etapaConscluida = false;

            while (Sistema.hasOptionSelected)
            {
                // ---------------- Seleção do Quarto ----------------
                Console.Clear();
                Console.WriteLine("------ CADASTRO DA RESERVA ------\n");
                Console.WriteLine("------ Quartos ------\n");
                Console.WriteLine("Lista de quartos disponiveis: \n");

                List<Quarto>? quartos = Sistema.Manager.ListarQuartosDisponiveis();

                if (quartos == null)
                {
                    Console.WriteLine("Nenhuma suíte disponível no momento.");
                    QualquerTeclaParaContinuar();
                    Sistema.hasOptionSelected = false;
                    break;
                }

                Console.WriteLine("ID | Tipo \t| Vl.Diária \t| Capac. Máx\t| Disponível?");

                foreach (Quarto s in quartos)
                {
                    s.ExibirInformacoes();
                }

                int quartoID = ConsoleHelper.ReadValue<int>("\nDigite o Número do quarto desejado: ");

                if (!Sistema.Manager.QuartoExist(quartoID) || Sistema.Manager.IsQuartoReservado(quartoID))
                {
                    Console.WriteLine("\nQuarto não encontrado ou já reservado. Tente novamente.");
                    QualquerTeclaParaContinuar();
                    continue;
                }

                Quarto? newQuarto = Sistema.Manager.GetQuartoByID(quartoID);
                Reserva.Quarto = newQuarto;

                // ---------------- Cadastro dos Hóspedes ----------------
                Console.Clear();
                Console.WriteLine("------ CADASTRO DA RESERVA ------\n");
                Console.WriteLine("------ Hóspedes ------");
                etapaConscluida = false;
                int quantHospedes = 0;
                List<Hospede> listHospedeTemp = new List<Hospede>();

                while (!etapaConscluida)
                {
                    quantHospedes++;

                    if (quantHospedes > newQuarto?.CapacidadeMaxima)
                    {
                        Console.WriteLine("Número máximo de hóspedes atingido para este quarto.");
                        QualquerTeclaParaContinuar();
                        etapaConscluida = true;
                        break;
                    }

                    Console.Write("\nDigite o nome do hóspede: ");
                    string nomeHospede = Console.ReadLine()!;

                    Console.Write("Digite a sobrenome do hóspede: ");
                    string sobrenome = Console.ReadLine()!;

                    Console.Write("Digite o CPF do hóspede: ");
                    string cpf = Console.ReadLine()!;

                    Hospede newHospede = new Hospede(nomeHospede, sobrenome, cpf);
                    listHospedeTemp.Add(newHospede);
                    
                    Console.WriteLine("\nDeseja adicionar outro hóspede? (S/N): ");
                    string resposta = Console.ReadLine()!.ToUpper();
                    if (resposta == "S")
                    {
                        continue;
                    }

                    etapaConscluida = true;
                }

                // ---------------- Cadastro da quantidade de dias ----------------
                Console.Clear();
                Console.WriteLine("------ CADASTRO DA RESERVA ------\n");                
                int diasReservados = ConsoleHelper.ReadValue<int>("\nQuantidade de dias reservados: ");
                Reserva.DiasReservados = diasReservados;

                Console.Clear();
                Console.WriteLine("------ CADASTRO DA RESERVA ------\n");
                Console.WriteLine("------ Resumo da Reserva ------\n");

                Console.WriteLine("Dias Reservados\t| Qt. Hóspedes \t| N° Quarto \t| Vl.Total");
                Reserva.ExibirInformacoes_NoQuantHospedes(listHospedeTemp.Count);

                Console.Write("\nConfirmar reserva? (S/N): ");
                string confirmar = Console.ReadLine()!.ToUpper();
                if (confirmar == "S")
                {
                    Reserva.Quarto!.IsDisponivel = false;
                    Reserva.AddHospedes(listHospedeTemp);
                    Sistema.Manager.AdicionarReserva(Reserva);
                    Console.WriteLine("\nReserva confirmada com sucesso!");
                }
                else
                {
                    Console.WriteLine("\nReserva cancelada.");
                }

                QualquerTeclaParaContinuar();
                Sistema.hasOptionSelected = false;
                break;
            }
        }

        private void ListarReservas()
        {
            Console.WriteLine("------ LISTA DE RESERVAS ------\n");

            List<Reserva>? listReserva = Sistema.Manager.ListarReservas();

            if (listReserva != null)
            {
                Console.WriteLine("Dias Reservados\t| Qt. Hóspedes \t| N° Quarto \t| Vl.Total");

                foreach (var reserva in listReserva)
                {
                    reserva.ExibirInformacoes();
                }
            }

            QualquerTeclaParaContinuar();
        }

        private void RemoverReserva()
        {
            while (Sistema.hasOptionSelected)
            {
                Console.WriteLine("-------- REMOVER RESERVA --------");

                Console.WriteLine("\n-------- Lista das Reservas --------");

                List<Reserva>? listReserva = Sistema.Manager.ListarReservas();

                if (listReserva == null)
                {
                    QualquerTeclaParaContinuar();
                    Sistema.hasOptionSelected = false;
                    break;
                }

                Console.WriteLine("Dias Reservados\t| Qt. Hóspedes \t| N° Quarto \t| Vl.Total");

                foreach (var r in listReserva!)
                {
                    r.ExibirInformacoes();
                }

                int quartoID = ConsoleHelper.ReadValue<int>("\nDigite o N° do quarto reservado: ");

                var reserva = Sistema.Manager.GetReservaByQuartoID(quartoID);

                if (reserva == null)
                {
                    Console.Write("Deseja tentar novamente? (S/N): ");
                }
                else
                {
                    Sistema.Manager.RemoverReserva(reserva);
                    Console.Write("\nDeseja remover outra reserva? (S/N): ");
                }

                string confirmar = Console.ReadLine()!.ToUpper();
                QualquerTeclaParaContinuar();
                Sistema.hasOptionSelected = false;
            }
        }
    }
}
