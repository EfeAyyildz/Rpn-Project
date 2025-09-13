using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;

namespace RpnCalculator
{
    // Operand sınıfı - sayıları tutmak için
    public class Operand
    {
        public double Value { get; set; }

        public Operand(double value)
        {
            Value = value;
        }
    }

    // Stack sınıfı - yığın işlemleri için
    public class MyStack
    {
        private List<Operand> items;

        public MyStack()
        {
            items = new List<Operand>();
        }

        public void Push(Operand operand)
        {
            items.Add(operand);
        }

        public Operand Pop()
        {
            if (items.Count == 0)
                throw new InvalidOperationException("Yığın boş - operand bulunamadı!");

            Operand item = items[items.Count - 1];
            items.RemoveAt(items.Count - 1);
            return item;
        }

        public bool Bosmu()
        {
            return items.Count == 0;
        }

        public int Say()
        {
            return items.Count;
        }
    }

    // Soyut Operator sınıfı
    public abstract class Operator
    {
        public abstract double Calculate(double operand1, double operand2);
        public abstract string Sembol { get; }
    }

    // Toplama operatörü
    public class ToplaOperator : Operator
    {
        public override string Sembol => "+";

        public override double Calculate(double operand1, double operand2)
        {
            return operand1 + operand2;
        }
    }

    // Çıkarma operatörü
    public class CikarOperator : Operator
    {
        public override string Sembol => "-";

        public override double Calculate(double operand1, double operand2)
        {
            return operand1 - operand2;
        }
    }

    // Çarpma operatörü
    public class CarpOperator : Operator
    {
        public override string Sembol => "*";

        public override double Calculate(double operand1, double operand2)
        {
            return operand1 * operand2;
        }
    }

    // Bölme operatörü
    public class BolOperator : Operator
    {
        public override string Sembol => "/";

        public override double Calculate(double operand1, double operand2)
        {
            if (operand2 == 0)
                throw new DivideByZeroException("Sıfıra bölme hatası!");
            return operand1 / operand2;
        }
    }

    // Kullanıcı tanımlı hatalar
    public class UndefinedOperatorException : Exception
    {
        public UndefinedOperatorException(string message) : base(message) { }
    }

    public class InsufficientOperandsException : Exception
    {
        public InsufficientOperandsException(string message) : base(message) { }
    }

    // GUI sınıfı - konsol arayüzü
    public class HesapMakArayuz
    {
        public string Girisal()
        {
            Console.Write("RPN ifadesi girin (çıkmak için 'cikis'): ");
            return Console.ReadLine();
        }

        public void ShowResult(double result)
        {
            Console.WriteLine($"Sonuç: {result}");
            Console.WriteLine();
        }

        public void ShowError(string errorMessage)
        {
            Console.WriteLine($"HATA: {errorMessage}");
            Console.WriteLine();
        }

        public void ShowWelcome()
        {
            Console.WriteLine("=== RPN Hesap Makinesi ===");
            Console.WriteLine("Örnek kullanım: '3 4 +' -> 7");
            Console.WriteLine("              : '3 4 2 + -' -> -3");
            Console.WriteLine("Desteklenen operatörler: +, -, *, /");
            Console.WriteLine();
        }
    }

    // Ana Calculator sınıfı
    public class HesapMak
    {
        private MyStack stack;
        private HesapMakArayuz gui;
        private Dictionary<string, Operator> operators;

        public HesapMak()
        {
            stack = new MyStack();
            gui = new HesapMakArayuz();

            // Operatörleri hazırla
            operators = new Dictionary<string, Operator>();
            operators["+"] = new ToplaOperator();
            operators["-"] = new CikarOperator();
            operators["*"] = new CarpOperator();
            operators["/"] = new BolOperator();
        }

        public double Calculate(string expression)
        {
            // Yeni hesaplama için yığını temizle
            stack = new MyStack();

            string[] tokens = expression.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (tokens.Length == 0)
                throw new ArgumentException("Boş ifade girildi!");

            foreach (string token in tokens)
            {
                if (IsNumber(token))
                {
                    // Sayı ise yığına ekle
                    double number = double.Parse(token);
                    stack.Push(new Operand(number));
                }
                else if (IsOperator(token))
                {
                    // Operatör ise işlemi yap
                    ProcessOperator(token);
                }
                else
                {
                    throw new UndefinedOperatorException($"Tanımlanmamış operatör: {token}");
                }
            }

            // Son durumu kontrol et
            if (stack.Say() == 0)
                throw new InsufficientOperandsException("Hesaplama sonucu bulunamadı!");
            else if (stack.Say() > 1)
                throw new InsufficientOperandsException("Eksik operatör - fazla operand kaldı!");

            return stack.Pop().Value;
        }

        private bool IsNumber(string token)
        {
            double result;
            return double.TryParse(token, out result);
        }

        private bool IsOperator(string token)
        {
            return operators.ContainsKey(token);
        }

        private void ProcessOperator(string operatorSymbol)
        {
            if (stack.Say() < 2)
                throw new InsufficientOperandsException($"'{operatorSymbol}' operatörü için yeterli operand yok!");

            // Yığından iki operand al (sıra önemli!)
            // İkinci operand önce çıkar, birinci operand sonra çıkar
            Operand operand2 = stack.Pop(); // İkinci operand (sağdaki)
            Operand operand1 = stack.Pop(); // Birinci operand (soldaki)

            // İşlemi yap: operand1 operator operand2
            Operator op = operators[operatorSymbol];
            double result = op.Calculate(operand1.Value, operand2.Value);

            // Sonucu yığına geri koy
            stack.Push(new Operand(result));
        }

        private void LogError(string error)
        {
            try
            {
                string logFile = "calculator_errors.log";
                string logEntry = $"[{DateTime.Now}] {error}{Environment.NewLine}";
                File.AppendAllText(logFile, logEntry);
            }
            catch
            {
                // Log yazma hatası durumunda sessizce devam et
            }
        }

        public void Run()
        {
            gui.ShowWelcome();

            while (true)
            {
                try
                {
                    string input = gui.Girisal();

                    if (input?.ToLower() == "cikis")
                        break;

                    if (string.IsNullOrWhiteSpace(input))
                        continue;

                    double result = Calculate(input);
                    gui.ShowResult(result);
                }
                catch (Exception ex)
                {
                    string errorMessage = ex.Message;
                    gui.ShowError(errorMessage);
                    LogError($"Girdi: '{gui}' - Hata: {errorMessage}");
                }
            }

            Console.WriteLine("Hesap makinesi kapatıldı.");
        }
    }

    // Ana program
    public class Program
    {
        public static void Main(string[] args)
        {
            HesapMak calculator = new HesapMak();
            calculator.Run();
        }
    }
}