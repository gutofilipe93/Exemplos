namespace TemplateMethod.Solucao
{
    public class USVacationPolicy : VacationPolicy
    {
        public USVacationPolicy()
        {
        }

        protected override void alterForLegalMinimums()
        {
            throw new System.NotImplementedException();
        }
    }
}