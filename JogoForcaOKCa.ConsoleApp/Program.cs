namespace JogoForcaOKCa.ConsoleApp
{
    internal class Program
    {
    static string[] palavras = { "ABACATE", "ABACAXI", "ACEROLA", "AÇAI", "ARAÇA",
    "BACABA", "BACURI", "BANANA", "CAJA", "CAJU", "CARAMBOLA", "CUPUAÇU","UVAIA",
    "GRAVIOLA", "GOIABA", "JABUTICABA", "JENIPAPO", "MAÇÃ", "MANGABA", "MANGA",
    "MARACUJÁ", "MURICI", "PEQUI", "PITANGA", "PITAYA", "SAPOTI", "TANGERINA",
    "UMBU", "UVA"
    };

    static void Main(string[] args)
        {
            string palavraSecreta, letrasChutadas;
            char[] letrasPalavraSecreta;
            bool acertou, perdeu;
            int tentativasIncorretas;

            DeclaracaoDasVariaveis(out palavraSecreta, out letrasPalavraSecreta, out letrasChutadas, out tentativasIncorretas);

            do
            {
                EscreverTituloDoJogo();

                ImprimirLetrasJaChutadas(letrasChutadas);

                DesenharForca(tentativasIncorretas);

                ImprimirOsUnderlines(letrasPalavraSecreta);

                char letraChutada = PerguntarQualOChute(ref letrasChutadas);

                tentativasIncorretas = CompararChutesAPalavraSecreta(palavraSecreta, letrasPalavraSecreta, tentativasIncorretas, letraChutada);

                string palavraEncontrada = new string(letrasPalavraSecreta);

                ImpressãoMensagemFinalAcertoOuErro(palavraSecreta, out acertou, out perdeu, tentativasIncorretas, palavraEncontrada);

                Console.Clear();
            } while (acertou == false && perdeu == false);

            static void DesenharForca(int quantidadeErros)
            {
                string cabecaDifunto = quantidadeErros >= 1 ? " o " : " ";
                string peitoralDifunto = quantidadeErros >= 2 ? "x" : " ";
                string bracoEsquerdoDifunto = quantidadeErros >= 3 ? "/" : " ";
                string bracoDireitoDifunto = quantidadeErros >= 3 ? @"\" : " ";
                string pernasDifunto = quantidadeErros >= 4 ? "/ \\" : " ";

                Console.WriteLine(" ___________        ");
                Console.WriteLine(" |/        |        ");
                Console.WriteLine(" |        {0}       ", cabecaDifunto);
                Console.WriteLine(" |        {0}{1}{2} ", bracoEsquerdoDifunto, peitoralDifunto, bracoDireitoDifunto);
                Console.WriteLine(" |        {0}       ", pernasDifunto);
                Console.WriteLine(" | ");
                Console.WriteLine(" | ");
                Console.WriteLine("_|________________________________________________________________");

            }
        }

        private static int CompararChutesAPalavraSecreta(string palavraSecreta, char[] letrasPalavraSecreta, int tentativasIncorretas, char letraChutada)
        {
            bool letraPertenceAPalavra = false;

            for (int i = 0; i < palavraSecreta.Length; i++)
                if (letraChutada == palavraSecreta[i])
                {
                    letrasPalavraSecreta[i] = letraChutada;
                    letraPertenceAPalavra = true;
                }

            if (letraPertenceAPalavra == false)
                tentativasIncorretas++;
            return tentativasIncorretas;
        }

        private static char PerguntarQualOChute(ref string letrasChutadas)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.Write("\nQual o seu chute? ");
            char letraChutada = Convert.ToChar(Console.ReadLine().ToUpper());
            letrasChutadas += letraChutada;
            Console.ForegroundColor = ConsoleColor.Magenta;
            return letraChutada;
        }

        private static void ImprimirOsUnderlines(char[] letrasPalavraSecreta)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine(letrasPalavraSecreta);
            Console.ResetColor();
        }

        private static void DeclaracaoDasVariaveis(out string palavraSecreta, out char[] letrasPalavraSecreta, out string letrasChutadas, out int tentativasIncorretas)
        {
            Random rand = new Random();
            int indiceAleatorio = rand.Next(palavras.Length);
            palavraSecreta = palavras[indiceAleatorio];
            letrasPalavraSecreta = new char[palavraSecreta.Length];
            for (int letraImputada = 0; letraImputada < letrasPalavraSecreta.Length; letraImputada++)
                letrasPalavraSecreta[letraImputada] = '_';

            letrasChutadas = "";
            tentativasIncorretas = 0;
        }

        private static void ImprimirLetrasJaChutadas(string letrasChutadas)
        {
            Console.Write($"                                    Letras já chutadas: {letrasChutadas} ");
            Console.WriteLine();
        }

        private static void ImpressãoMensagemFinalAcertoOuErro(string palavraSecreta, out bool acertou, out bool perdeu,
            int tentativasIncorretas, string palavraEncontrada)
        {
            acertou = palavraEncontrada == palavraSecreta;
            perdeu = tentativasIncorretas == 5;

            if (acertou)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine();
                Console.WriteLine($"Você acertou! A palavra era {palavraSecreta}, parabéns!");
                Console.ResetColor();
                Console.ReadLine();
            }
            else if (perdeu)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine();
                Console.WriteLine($"A palavra sorteada era {palavraSecreta}. Você perdeu." +
                    $"\nMas não desanime. Tente novamente!");
                Console.ResetColor();
                Console.ReadLine();
            }
        }

        static void EscreverTituloDoJogo()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine();
            Console.WriteLine("                    Jogo de Forca Da Camile ");
            Console.WriteLine("__________________________________________________________________");
            Console.WriteLine();
        }
    }
}