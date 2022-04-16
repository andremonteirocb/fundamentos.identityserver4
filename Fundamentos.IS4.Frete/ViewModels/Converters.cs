namespace Fundamentos.IS4.Frete.Fretes.ViewModels
{
    public static class Converters
    {

        public static CalculoFreteViewModel ToViewModel(this Model.Frete frete, decimal valor)
        {
            return new CalculoFreteViewModel()
            {
                Modalidade = frete.Modalidade,
                Descricao = frete.Descricao,
                Valor = valor
            };
        }
        public static FreteViewModel ToViewModel(this Model.Frete frete)
        {
            return new FreteViewModel()
            {
                Modalidade = frete.Modalidade,
                Descricao = frete.Descricao,
                Multiplicador = frete.Multiplicador,
                Ativo = frete.Ativo,
                ValorMinimo = frete.ValorMinimo
            };
        }
    }
}