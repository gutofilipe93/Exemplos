namespace TemplateMethod.Problema
{
    public class VacationPolicy
    {
        public void AccrueUSDivisionVacation()
        {
            // codigo para calcular as ferias baseado-se nas horas trabalhadas, até a data
            // codigo para garantir que as ferias cumpram o tempo minimo nos EUA
            // codigo para aplicar as ferias ao registro de folha de pagamento
        }

        public void AccrueEUDivisionVacation()
        {
            // codigo para calcular as ferias baseado-se nas horas trabalhadas, até a data
            // codigo para garantir que as ferias cumpram o tempo minimo nos Europa
            // codigo para aplicar as ferias ao registro de folha de pagamento
        }

        // O problema aqui na classe é a quantidade de codigo duplicado, a parte de calcular as horas trabalhadas e registro na folha é o mesmo codigo
        // para os dois metodos, só a parte de cumprir as ferias muda
    }
}