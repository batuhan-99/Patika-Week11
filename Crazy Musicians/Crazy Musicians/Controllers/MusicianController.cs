using Microsoft.AspNetCore.Mvc;
using Crazy_Musicians.Models;
namespace Crazy_Musicians.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MusicianController : ControllerBase
    {
        public static List<Musician> _musician = new List<Musician>()
        {
            new Musician{Id=1, Name="Ahmet Çalgı", Job="Ünlü Çalgı Çalar", Skill = "Her zaman yanlış nota çalar ama çok eğlenceli"},
            new Musician{Id=2, Name="Zeynep Melodi", Job="Popüler Melodi Yazarı", Skill = "Şarkıları yanlış anlaşılır ama çok popüler"},
            new Musician{Id=3, Name="Cemil Akor", Job="Çılgın Akorist", Skill = "Akorları sık değiştirir, ama şaşırtıcı derece yetenekli"},
            new Musician{Id=4, Name="Fatma Nota", Job="Sürpriz Nota Üreticisi", Skill="Nota üretirken sürekli sürpriz hazırlar"}
        };
        
        [HttpGet]
        public IEnumerable<Musician> Get()
        {
            return _musician;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var musician = _musician.FirstOrDefault(x => x.Id == id);
            if (musician == null)
            {
                return NotFound();
            }
            return Ok(musician);
        }
        [HttpGet("search")]
        public IActionResult SearchMusician([FromQuery] string job)
        {
            var filteredMusicians = _musician.Where(m => m.Job.Contains(job, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filteredMusicians.Any())
                return NotFound();
            return Ok(filteredMusicians);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Musician musician)
        {
            var id = _musician.Max(x => x.Id) + 1;
            musician.Id = id;
            _musician.Add(musician);
            return CreatedAtAction(nameof(Get), new { musician.Id }, id);
        }

        [HttpPatch("{id}")]
        public IActionResult UpdateFunFact(int id, [FromBody] string newSkill)
        {
            var musician = _musician.FirstOrDefault(m => m.Id == id);
            if (musician == null)
            {
                return NotFound("Müzisyen bulunamadı.");
            }

            // Sadece 'FunFact' alanını güncelliyoruz
            musician.Skill = newSkill;
            return Ok(musician);
        }

        [HttpPut]

        public IActionResult Put(int id, [FromBody]  Musician musician)
        {
            if (musician == null || id != musician.Id)
            {
                return BadRequest();
            }
            var existingMusician = _musician.FirstOrDefault(x => x.Id == id);

            if (existingMusician != null)
            {
                return NotFound();
            }

            existingMusician.Name = musician.Name;
            
            return Ok(existingMusician);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var existingMusician = _musician.FirstOrDefault(y => y.Id == id);
            if (existingMusician is null)
            {
                return NotFound();
            }

            _musician.Remove(existingMusician);

            return NoContent();
        }

    }
}
