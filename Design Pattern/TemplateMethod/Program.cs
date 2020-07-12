using System;
using TemplateMethod.Solucao;

namespace TemplateMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            USVacationPolicy uSVacationPolicy = new USVacationPolicy();
            uSVacationPolicy.accrueVacation(); 
            // vai usar todos os metodos que tem na classe VacationPolicy, mas o metodo que foi sobre escrito pela classe 'USVacationPolicy'
        }
    }
}
