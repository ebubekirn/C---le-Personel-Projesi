using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.Data;
using System.Data.SqlClient;

namespace PersonelProje.Controllers
{
    public class UlkeController : BaseController
    {
        public UlkeController(IConfiguration config) : base(config)
        {

        }

        public Ulke UlkeBul(string Id)
        {
            //SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            string qry = $"select * from ulke where Id = '{Id}'";
            return Connect().Query<Ulke>(qry).FirstOrDefault();
            //return secUlke;
        }
        public IActionResult Liste()
        {
            //SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            // Connect metodu oluşturduktan sonra üstteki koda gerek kalmadı. Bir alttaki kodu yazdık.
            // Bunuda sadeleştirirsek con yerine connect metodunu yazdık.
            //var con = Connect();
            string qry = "Select * from ulke order by UlkeAd";
            var ulkeler = Connect().Query<Ulke>(qry).ToList();
            return View(ulkeler);
        }
        public IActionResult Guncel(string Id)
        {
            return View(UlkeBul(Id));
        }
        [HttpPost]
        public IActionResult Guncel(Ulke ulke)
        {
            //SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            // 1. Yol
            string qry = $"update ulke set UlkeAd = @UlkeAd where Id = @Id";
            Connect().ExecuteScalar<int>(qry, ulke);
            // int yapma sebebimiz kaç tane satırı guncellediğimi görmek istersem bunu kullanırız.
            return RedirectToAction("Liste");

            //2. Yol
            /*string qry = $"update ulke set UlkeAd = @UlkeAd where Id = @Id";
            DynamicParameters par = new DynamicParameters();
            par.Add("@UlkeAd", ulke.UlkeAd);
            par.Add("@Id", ulke.Id);
            con.ExecuteScalar<int>(qry, par);
            return RedirectToAction("Liste");*/
        }
        public IActionResult Sil(string Id)
        {
            return View(UlkeBul(Id));
        }
        [HttpPost]
        public IActionResult Sil(Ulke ulke)
        {
            string qry = $"delete from ulke where Id = @Id";
            Connect().ExecuteScalar<int>(qry, ulke);
            return RedirectToAction("Liste");
        }

        //**********************************************************

        /*
        public IActionResult Ekle()
        {
            SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            string qry = $"select * from ulke";
            var secUlke = con.Query<Ulke>(qry).FirstOrDefault();
            return View(secUlke);
        }
        [HttpPost]
        public IActionResult Ekle(Ulke ulke)
        {
            SqlConnection con = new SqlConnection(_config.GetConnectionString("Baglanti"));
            string qry = $"insert into ulke values(@Id, @UlkeAd)";
            con.ExecuteScalar<int>(qry, ulke);
            return RedirectToAction("Liste");
        }
        */

        //******************** Hoca *****************************

        public IActionResult Ekle(Ulke yeniulke, bool d)
        {
            // Ulke yeniulke = new Ulke();
            // Ekle metoduna giriş verirsek üstteki koda gerek yok. Eğer boş olursa new yapmamız gerek.
            // Alttaki ekle metodu ile çakışmaması içinde bool atamamız gerekiyor.
            return View(yeniulke);
        }
        [HttpPost]
        // Post yaptığımız yerlerde oncelikle get komutu yazmalıyız.
        public IActionResult Ekle(Ulke ulke)
        {
            string qry = $"insert into ulke values(@Id, @UlkeAd)";
            Connect().ExecuteScalar<int>(qry, ulke);
            return RedirectToAction("Liste");
        }
    }
}
