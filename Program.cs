using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.IO;


namespace ConsultarJson
{
    internal class Program
    {
        static void Main(string[] args)
        
        {
            bool Sair = false;
            while(Sair == false)
            {
                Console.WriteLine("=================================");
                Console.WriteLine("=========CONSULTAR JSON==========");
                Console.WriteLine("=================================");
                Console.WriteLine("");
                Console.WriteLine("============ M E N U ============");
                Console.WriteLine("1- Listar Json\n2- Buscar Json por ID\n3- Sair");
                Console.Write("Digite a opção desejada:  ");
                string opStr = Console.ReadLine();
                int opInt = int.Parse(opStr);

                if (opInt == 1)
                {

                    RequisicaoList();
                    Console.WriteLine("Aperte ENTER para voltar ao menu");
                    Console.ReadLine();
                }
                else if (opInt == 2)
                {
                    Console.Write("Digite o ID: ");
                    string strid = Console.ReadLine();
                    int intid = int.Parse(strid);
                    RequisicaoUnica(intid);
                    
                    Console.WriteLine("Aperte ENTER para voltar ao menu");
                    Console.ReadLine();
                }
                else if (opInt == 3)
                {
                    Sair = true;
                }
                Console.Clear();
            }
            


        }

        static void RequisicaoList()
        {
            var requisicao = WebRequest.Create("https://jsonplaceholder.typicode.com/todos/"); // vai pegar o id 10
            requisicao.Method = "GET";
            var resposta = requisicao.GetResponse();
            using (resposta)
            {
                var stream = resposta.GetResponseStream();
                StreamReader leitor = new StreamReader(stream);
                object dados = leitor.ReadToEnd();

                // Essa primeira parte, vai receber a conversão.
                List<Tarefa> tarefas = JsonConvert.DeserializeObject<List<Tarefa>>(dados.ToString()); // Converte a lista json para uma lista do C#
                // tirei o list
                // tirei o foreach
                foreach (Tarefa tarefa in tarefas)
                {
                    tarefa.Exibir();
                }

                stream.Close();
                resposta.Close();

            }
        }
        public static void RequisicaoUnica(int intid)
        {
            var requisicao = WebRequest.Create($"https://jsonplaceholder.typicode.com/todos/{intid}");
            requisicao.Method = "GET";
            var resposta = requisicao.GetResponse();
            using (resposta)
            {
                var stream = resposta.GetResponseStream();
                StreamReader leitor = new StreamReader(stream);
                object dados = leitor.ReadToEnd();

                Tarefa tarefa = JsonConvert.DeserializeObject<Tarefa>(dados.ToString()); // Converte a lista json para uma lista do C#
                tarefa.Exibir();

                stream.Close();
                resposta.Close();

            }
        }
    }
}
