namespace TemplateMethod.Solucao
{
    public class EUVacationPolicy : VacationPolicy
    {
        public EUVacationPolicy()
        {
        }

        protected override void alterForLegalMinimums()
        {
            throw new System.NotImplementedException();
        }
    }
}