using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.WriteLine("ORGANIZADOR DE ARQUIVOS");
        Console.WriteLine("------------------------");

        string? caminho;

        // 🔁 Loop principal que força o usuário a digitar um caminho válido ou sair
        while (true)
        {
            Console.Write("Digite o caminho da pasta para organizar (ou digite 'sair'): ");
            caminho = Console.ReadLine();

            // ⚠️ Valida se a entrada está vazia
            if (string.IsNullOrWhiteSpace(caminho))
            {
                Console.WriteLine("❌ Entrada vazia. Tente novamente.");
                continue; // Volta para o início do loop
            }

            // 🚪 Permite o usuário encerrar o programa com "sair"
            if (caminho.Trim().ToLower() == "sair")
            {
                Console.WriteLine("👋 Encerrando o programa...");
                break; // Sai do loop e vai para o final
            }

            // ❌ Valida se o caminho realmente existe no sistema
            if (!Directory.Exists(caminho))
            {
                Console.WriteLine("❌ Caminho inválido. Tente novamente.");
                continue; // Volta para o início do loop
            }

            // ✅ Se o caminho é válido, prossegue com a organização
            string[] arquivos = Directory.GetFiles(caminho);

            Dictionary<string, int> contadorExtensoes = new Dictionary<string, int>();

            foreach (string arquivo in arquivos)
            {
                string extensao = Path.GetExtension(arquivo).ToLower().TrimStart('.');

                if (string.IsNullOrEmpty(extensao)) continue;

                string pastaDestino = Path.Combine(caminho, extensao);

                if (!Directory.Exists(pastaDestino))
                    Directory.CreateDirectory(pastaDestino);

                string nomeArquivo = Path.GetFileName(arquivo);
                string destinoFinal = Path.Combine(pastaDestino, nomeArquivo);

                if (!File.Exists(destinoFinal))
                {
                    File.Move(arquivo, destinoFinal);
                    Console.WriteLine($"✅ {nomeArquivo} movido para: /{extensao}");

                    // 2️⃣ Atualiza contador
                    if (contadorExtensoes.ContainsKey(extensao))
                        contadorExtensoes[extensao]++;
                    else
                        contadorExtensoes[extensao] = 1;
                }
                else
                {
                    Console.WriteLine($"⚠️ {nomeArquivo} já existe em: /{extensao}. Pulado.");
                }
            }

            // 3️⃣ Exibir resumo
            Console.WriteLine("\n📊 Resumo da organização:");
            foreach (var item in contadorExtensoes)
            {
                Console.WriteLine($"📁 {item.Key}: {item.Value} arquivo(s)");
            }

            // ✅ Pausa para que o usuário veja o resultado antes de reiniciar o loop
            Console.WriteLine("\nPressione Enter para organizar outra pasta ou digite 'sair'...");
            Console.ReadLine();
            Console.Clear(); // 🧼 Limpa o terminal para a próxima execução
        }
    }
}