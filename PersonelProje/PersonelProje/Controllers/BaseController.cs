using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class BaseController : Controller
    {
        // IConfiguration configuration = new IConfiguration(); Bu satır diğer üçüne eşdeğer. İnterface new lenemediği için böyle yapıyoruz.

        private readonly IConfiguration _config;
        public BaseController(IConfiguration config)
        {
            _config = config;
        }
        public SqlConnection Connect()
        {
            return new SqlConnection(_config.GetConnectionString("Baglanti"));
        }
    }
}
