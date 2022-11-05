using Dapper;
using Microsoft.AspNetCore.Mvc;
using PersonelProje.Data;
using PersonelProje.DTO;
using PersonelProje.Models;

namespace PersonelProje.Controllers
{
    public class PersonelController : BaseController
    {
        PersonelModel _model;

        public PersonelController(IConfiguration config, PersonelModel model) : base(config)
        {
            _model = model;
        }

        public List<Ulke> Ulkeler()
        {
            return Connect().Query<Ulke>($"select * from ulke ").ToList();
        }

        public List<Sehir> Sehirler()
        {
            return Connect().Query<Sehir>($"select * from sehir ").ToList();
        }

        public Personel PersonelBul(int Id)
        {
            return Connect().Query<Personel>($"select * from personel where Id = '{Id}'").FirstOrDefault();
        }

        public IActionResult Liste()
        {
            string qry = "select p.Id, Ad + ' ' + Soyad AdSoyad, maas, SehirAd, UlkeAd from Personel p inner join Sehir s on s.Id = p.SehirId inner join Ulke u on u.Id = p.UlkeId";
            var personeller = Connect().Query<PersonelDTO>(qry).ToList();
            return View(personeller);
        }

        public IActionResult Guncel(int Id)
        {
            _model.Personel = PersonelBul(Id);
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Güncelleme İşlemi";
            _model.BtnText = "Güncelle";
            _model.BtnClass = "btn btn-success";
            return View("Genel",_model); // Kendi view çalıştırma Genel view u çalıştır diyoruz.
        }

        [HttpPost]

        public IActionResult Guncel(PersonelModel model)
        {
            Personel personel = model.Personel;
            string qry = $"update personel set Ad = @Ad, Soyad = @Soyad, maas = @Maas, UlkeId = @UlkeId, SehirId = @SehirId where Id = @Id";
            Connect().ExecuteScalar<int>(qry, personel);
            return RedirectToAction("Liste");
        }
        public IActionResult Ekle(int Id)
        {
            _model.Personel = new Personel();
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Ekleme İşlemi";
            _model.BtnText = "Ekle";
            _model.BtnClass = "btn btn-primary";
            return View("Genel", _model); 
        }

        [HttpPost]

        public IActionResult Ekle(PersonelModel model)
        {
            Personel personel = model.Personel;
            string qry = $"insert into personel(Ad, Soyad, maas, UlkeId, SehirId) values(@Ad, @Soyad, @Maas, @UlkeId, @SehirId)";
            Connect().ExecuteScalar<int>(qry, personel);
            return RedirectToAction("Liste");
        }
        public IActionResult Sil(int Id)
        {
            _model.Personel = PersonelBul(Id);
            _model.Ulkeler = Ulkeler();
            _model.Sehirler = Sehirler();
            _model.Baslik = "Silme İşlemi";
            _model.BtnText = "Sil";
            _model.BtnClass = "btn btn-danger";
            return View("Genel", _model);
        }

        [HttpPost]

        public IActionResult Sil(PersonelModel model)
        {
            Personel personel = model.Personel;
            string qry = $"delete from personel where Id = @Id";
            Connect().ExecuteScalar<int>(qry, personel);
            return RedirectToAction("Liste");
        }
    }
}
