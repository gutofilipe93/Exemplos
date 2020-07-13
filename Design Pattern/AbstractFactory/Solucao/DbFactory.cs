using AbstractFactory.Violacao;

namespace AbstractFactory.Solucao    
{
    public abstract class DbFactory
    {
        public abstract DbConnection CreateConnection();
        public abstract DbCommand CreateCommand();
    }
}