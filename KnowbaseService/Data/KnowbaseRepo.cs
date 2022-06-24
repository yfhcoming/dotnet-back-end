using KnowbaseService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowbaseService.Data
{
    public class KnowbaseRepo : IKnowbaseRepo
    {

        private readonly AppDbContext _context;

        public KnowbaseRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateKnowbase(Knowbase kbas)
        {
            if (kbas == null)
            {
                throw new ArgumentNullException(nameof(kbas));
            }

            _context.Knowbases.Add(kbas);
        }

        public IEnumerable<Knowbase> GetAllKnowbases()
        {
            return _context.Knowbases.ToList();
        }

        public Knowbase GetKnowbaseById(int id)
        {
            return _context.Knowbases.FirstOrDefault(p => p.Id == id);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
