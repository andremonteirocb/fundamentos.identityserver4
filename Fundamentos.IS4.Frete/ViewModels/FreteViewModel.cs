using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace Fundamentos.IS4.Frete.Fretes.ViewModels
{
    public class FreteViewModel
    {
        public bool Ativo { get; set; }
        public string Modalidade { get; set; }
        public string Descricao { get; set; }
        public decimal ValorMinimo { get; set; }
        public decimal Multiplicador { get; set; }

        [JsonIgnore]
        public ValidationProblemDetails Errors { get; private set; }
        public Model.Frete ToEntity()
        {
            return new Model.Frete()
            {
                Ativo = true,
                Descricao = Descricao.Trim(),
                Modalidade = Modalidade.Trim(),
                ValorMinimo = ValorMinimo,
                Multiplicador = Multiplicador
            };
        }

        public bool IsValid()
        {
            if (Errors?.Errors == null) return true;
            return !Errors.Errors.Any();
        }

        public void Check()
        {
            var erros = new List<string>();

            if (string.IsNullOrEmpty(Modalidade))
                erros.Add("Modalidade invalida");

            if (ValorMinimo <= 0)
                erros.Add("Valor não pode ser negativo");

            if (Multiplicador <= 0)
                erros.Add("Multiplicador não pode ser negativo");

            if (!erros.Any()) return;

            Errors = new ValidationProblemDetails(new Dictionary<string, string[]> { { "Frete", erros.ToArray() } })
            {
                Detail = "Erros na validação do frete"
            };
        }

        public void SetError(string erro)
        {
            var erros = new List<string>() { erro };
            Errors = new ValidationProblemDetails(new Dictionary<string, string[]> { { "Frete", erros.ToArray() } })
            {
                Detail = "Erros na validação do frete"
            };
        }
    }
}