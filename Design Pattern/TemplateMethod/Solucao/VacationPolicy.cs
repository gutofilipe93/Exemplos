namespace TemplateMethod.Solucao
{
    abstract public class VacationPolicy
    {
        public void accrueVacation()
        {
            calculateBaseVacationHours();
            alterForLegalMinimums();
            applyToPayroll();
        }

        private void calculateBaseVacationHours()
        {

        }

        abstract protected void alterForLegalMinimums();
        private void applyToPayroll(){}
    }
}