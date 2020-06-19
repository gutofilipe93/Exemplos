namespace SOLID.OCP.Solucao_Extension_Methods
{
    // Para poder aplicar o extension Methods a classe e o metodo deve ser 'static' 
    // e o primero parametro do metodo teve conter a palavra reservada 'this'
    // Classe 'CaixaEletronico mostra com esse metodo é acessado
    public static class DebitoContaCorrente
    {
        public static string DebitarContaCorrente(this DebitoConta debitoConta)
        {
            // Logica de negócio para debito em conta corrente.
            return debitoConta.FormatarTransacao();
        }
    }
}