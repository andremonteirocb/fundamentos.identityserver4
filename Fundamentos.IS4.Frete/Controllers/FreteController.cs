using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Fundamentos.IS4.Frete.Fretes.Context;
using Fundamentos.IS4.Frete.Fretes.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Frete.Fretes.Controllers
{
    [ApiController]
    [Route("fretes"), Authorize]
    public class FreteController : ControllerBase
    {
        private readonly ILogger<FreteController> _logger;
        private readonly FreteContext _context;

        public FreteController(ILogger<FreteController> logger, FreteContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FreteViewModel>>> ObterModalidades([FromQuery] Model.GeoCoordinate pos)
        {
            _logger.LogInformation("Obtendo Modalidades");
            return Ok(await _context.Fretes.Where(w => w.Ativo).Select(s => s.ToViewModel()).ToListAsync());
        }

        [HttpGet("para/{lat},{lon}/calcular")]
        public async Task<ActionResult<IEnumerable<CalculoFreteViewModel>>> Calcular(double lat, double lon, [FromQuery] EmbalagemViewModel embalagem)
        {
            _logger.LogInformation($"Calculando frete para {lat},{lon}");
            var opcoesFrete = await _context.Fretes.Where(w => w.Ativo).ToListAsync();
            var posicao = new Model.GeoCoordinate(lat, lon);

            return Ok(opcoesFrete.Select(s => s.ToViewModel(s.CalcularFrete(embalagem, posicao))));
        }

        [HttpPost, Authorize(Policy = "Gerente")]
        public async Task<ActionResult<FreteViewModel>> AdicionarModalidade([FromBody] FreteViewModel command)
        {
            command.Check();
            if (!command.IsValid())
                return BadRequest(command.Errors);

            if (await _context.Fretes.AnyAsync(a => a.Modalidade.ToUpper().Equals(command.Modalidade.Trim().ToUpper())))
            {
                command.SetError("Modalidade já existe");
                return BadRequest(command.Errors);
            }

            var frete = command.ToEntity();
            await _context.Fretes.AddAsync(frete);
            await _context.SaveChangesAsync();
            return Created(nameof(ObterModalidades), command);
        }

        [HttpDelete("{modalidade}"), Authorize(Policy = "Gerente")]
        public async Task<ActionResult<FreteViewModel>> RemoverModalidade(string modalidade)
        {
            var frete = await _context.Fretes.FirstOrDefaultAsync(a => a.Modalidade.ToUpper().Equals(modalidade.Trim().ToUpper()));
            frete.Inativar();
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
