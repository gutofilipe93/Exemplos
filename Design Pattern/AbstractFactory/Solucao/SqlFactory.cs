using AbstractFactory.Violacao;

namespace AbstractFactory.Solucao
{
    public class SqlFactory : DbFactory
    {
        public override DbCommand CreateCommand()
        {
            return new SqlCommand();
        }

        public override DbConnection CreateConnection()
        {
            return new SqlConnection();
        }
    }
}