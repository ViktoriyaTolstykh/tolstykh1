using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace tolstykh1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class humanController : ControllerBase
    {
       
        private readonly DataContext _context;

        public humanController(DataContext context) 
        {
            _context = context;
        }

        [HttpGet] //атрибут
        public async Task<ActionResult<List<human>>> Get() // асинхронная задача, с помощью гет вернем список людей

        {

            return Ok(await _context.humen.ToListAsync());// возвращаем ожидание а затем контекст и получаем доступ к людям
        }
        [HttpGet("{id}")] //атрибут
        public async Task<ActionResult<human>> Get(int id)

        {
            var student = await _context.humen.FindAsync(id);// добавляем идентификатор чтоб он искал первичный ключ
            if (student == null)
                return BadRequest("Student not found.");
            return Ok(student);
        }

        [HttpPost]
        public async Task<ActionResult<List<human>>> AddHuman(human student)
        {
            _context.humen.Add(student);
            await _context.SaveChangesAsync();
            return Ok(await _context.humen.ToListAsync());// ждем пока контекстные люди будут перечислены асинхронно 
        }

        //изменим героя
        [HttpPut]
        public async Task<ActionResult<List<human>>> UpdateHuman(human request)
        {
            var dbstudent = await _context.humen.FindAsync(request.id);
            if (dbstudent == null)
                return BadRequest("Student not found.");

            dbstudent.Name = request.Name;
            dbstudent.FirstName = request.FirstName;
            dbstudent.LastName = request.LastName;
            dbstudent.Group = request.Group;

            await _context.SaveChangesAsync();
            return Ok(await _context.humen.ToListAsync());
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<List<human>>> Delete(int id)

        {
            var dbstudent = await _context.humen.FindAsync(id);
            if (dbstudent == null)
                return BadRequest("Student not found.");
           

            _context.humen.Remove(dbstudent);

        await _context.SaveChangesAsync();
            return Ok(await _context.humen.ToListAsync());
        }
    }
}
