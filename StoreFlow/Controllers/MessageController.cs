using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoreFlow.Context;

namespace StoreFlow.Controllers
{
    public class MessageController : Controller
    {
        private readonly StoreContext _context;
        public MessageController(StoreContext context)
        {
            _context = context;
        }
        public IActionResult MessageList()
        {
            var values = _context.Messages.AsNoTracking().ToList();
            return View(values);
            //sadece okuma işlemleri için idealdir
        }


        /*
         * Sadece bir kaç alan güncellencekse Attach()+IsModified
         * Tüm alanlar değişecekse Update()
         * Foreign key üzerinden ilişki kurulcaksa Attach()
         * AsNoTracking() sınrası tekrar ilişki kuracaksan Attach()
         */
    }
}