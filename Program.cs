using System;

namespace DIO.Show
{
    class Program
    {
        static ShowRepositorio repositorio = new ShowRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

			while (opcaoUsuario.ToUpper() != "X")
			{
				switch (opcaoUsuario)
				{
					case "1":
						ListarShow();
						break;
					case "2":
						InserirShow();
						break;
					case "3":
						AtualizarShow();
						break;
					case "4":
						ExcluirShow();
						break;
					case "5":
						VisualizarShow();
						break;
					case "C":
						Console.Clear();
						break;

					default:
						throw new ArgumentOutOfRangeException();
				}

				opcaoUsuario = ObterOpcaoUsuario();
			}

			Console.WriteLine("Obrigado por utilizar nossos serviços.");
			Console.ReadLine();
        }

        private static void ExcluirShow()
		{
			Console.Write("Digite o id do show: ");
			int indiceShow = int.Parse(Console.ReadLine());

			repositorio.Exclui(indiceShow);
		}

        private static void VisualizarShow()
		{
			Console.Write("Digite o id do show: ");
			int indiceShow = int.Parse(Console.ReadLine());

			var show = repositorio.RetornaPorId(indiceShow);

			Console.WriteLine(show);
		}

        private static void AtualizarShow()
		{
			Console.Write("Digite o id da show: ");
			int indiceShow = int.Parse(Console.ReadLine());

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Estilo)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Estilo), i));
			}
			Console.Write("Digite o estilo entre as opções acima: ");
			int entradaEstilo = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título d0 Show: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Show: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Show: ");
			string entradaDescricao = Console.ReadLine();

			Show atualizaShow = new Show(id:indiceShow,
										estilo: (Estilo)entradaEstilo,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Atualiza(indiceShow, atualizaShow);
		}
        private static void ListarShow()
		{
			Console.WriteLine("Listar show");

			var lista = repositorio.Lista();

			if (lista.Count == 0)
			{
				Console.WriteLine("Nenhuma show cadastrado.");
				return;
			}

			foreach (var show in lista)
			{
                var excluido = show.retornaExcluido();
                
				Console.WriteLine("#ID {0}: - {1} {2}", show.retornaId(), show.retornaTitulo(), (excluido ? "*Excluído*" : ""));
			}
		}

        private static void InserirShow()
		{
			Console.WriteLine("Inserir nova show");

			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=netcore-3.1
			// https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getname?view=netcore-3.1
			foreach (int i in Enum.GetValues(typeof(Estilo)))
			{
				Console.WriteLine("{0}-{1}", i, Enum.GetName(typeof(Estilo), i));
			}
			Console.Write("Digite o estilo entre as opções acima: ");
			int entradaEstilo= int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Show: ");
			string entradaTitulo = Console.ReadLine();

			Console.Write("Digite o Ano de Início da Show: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Show: ");
			string entradaDescricao = Console.ReadLine();

			Show novaShow = new Show(id: repositorio.ProximoId(),
										estilo: (Estilo)entradaEstilo,
										titulo: entradaTitulo,
										ano: entradaAno,
										descricao: entradaDescricao);

			repositorio.Insere(novaShow);
		}

        private static string ObterOpcaoUsuario()
		{
			Console.WriteLine();
			Console.WriteLine("DIO Show a seu dispor!!!");
			Console.WriteLine("Informe a opção desejada:");

			Console.WriteLine("1- Listar show");
			Console.WriteLine("2- Inserir nova show");
			Console.WriteLine("3- Atualizar show");
			Console.WriteLine("4- Excluir show");
			Console.WriteLine("5- Visualizar show");
			Console.WriteLine("C- Limpar Tela");
			Console.WriteLine("X- Sair");
			Console.WriteLine();

			string opcaoUsuario = Console.ReadLine().ToUpper();
			Console.WriteLine();
			return opcaoUsuario;
		}
    }
}
