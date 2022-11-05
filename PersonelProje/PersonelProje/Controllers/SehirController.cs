using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.Data;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class SehirController : BaseController
    {
        public SehirController(IConfiguration config) : base(config)
        {
        }

        public Sehir SehirBul(int Id)
        {
            string qry = $"select * from sehir where Id = '{Id}'";
            return Connect().Query<Sehir>(qry).FirstOrDefault();
        }

        public IActionResult Liste()
        {
            string qry = $"select * from sehir order by Id";
            var sehirler = Connect().Query<Sehir>(qry).ToList();
            return View(sehirler);
        }
        public IActionResult Guncel(int Id)
        {
            return View(SehirBul(Id));
        }
        [HttpPost]
        public IActionResult Guncel(Sehir sehir)
        {
            string qry = $"update sehir set SehirAd = @SehirAd where Id = @Id";
            Connect().ExecuteScalar<int>(qry, sehir);
            return RedirectToAction("Liste");
        }
        public IActionResult Sil(int Id)
        {
            return View(SehirBul(Id));
        }
        [HttpPost]
        public IActionResult Sil(Sehir sehir)
        {
            string qry = $"delete from sehir where Id = @Id";
            Connect().ExecuteScalar<int>(qry, sehir);
            return RedirectToAction("Liste");
        }

        public IActionResult Ekle(Sehir yenisehir, bool d)
        {
            return View(yenisehir);
        }
        [HttpPost]
        public IActionResult Ekle(Sehir sehir)
        {
            string qry = $"insert into sehir values(@Id, @SehirAd)";
            Connect().ExecuteScalar<int>(qry, sehir);
            return RedirectToAction("Liste");
        }
    }
}
