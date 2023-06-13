using confinancia.Models.JsonDTO;
using confinancia.Services.Token;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace confinancia.Controllers
{
	public class GraphController : Controller
	{
        private readonly IGetToken _getToken;

        public GraphController(IGetToken getToken)
        {
            _getToken = getToken;
        }


        [Consumes("application/x-www-form-urlencoded")]
		public async Task<IActionResult> Getoutlook([FromForm] IFormCollection value)
		{
            string code = value.First().Value;
            string accesToken = await _getToken.GetTokenMGraph(code);



            return View();
		}
    }
}
