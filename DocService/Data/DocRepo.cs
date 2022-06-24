using DocService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Data
{
    public class DocRepo : IDocRepo
    {

        private readonly AppDbContext _context;

        public DocRepo(AppDbContext context)
        {
            _context = context;
        }


        public void CreateDoc(int KnowbaseId, Doc doc)
        {
            if (doc == null)
            {
                throw new ArgumentNullException(nameof(doc));
            }

            doc.KnowbaseId = KnowbaseId;
            _context.Docs.Add(doc);
        }

        public void CreateKnowbase(Knowbase kbas)
        {
            if (kbas == null)
            {
                throw new ArgumentNullException(nameof(kbas));
            }
            _context.Knowbases.Add(kbas);
        }

        public bool ExternalKnowbaseExists(int externalKnowbaseId)
        {
            return _context.Knowbases.Any(p => p.ExternalID == externalKnowbaseId);
        }

        public IEnumerable<Knowbase> GetAllKnowbases()
        {
            return _context.Knowbases.ToList();
        }

        public Knowbase GetKnowbaseById(int id)
        {
            return _context.Knowbases.FirstOrDefault(p => p.Id == id);
        }

        public Doc GetDoc(int KnowbaseId, int docId)
        {
            return _context.Docs
                .Where(c => c.KnowbaseId == KnowbaseId && c.Id == docId).FirstOrDefault();
        }

        public IEnumerable<Doc> GetDocsForKnowbase(int KnowbaseId)
        {
            return _context.Docs
                .Where(c => c.KnowbaseId == KnowbaseId)
                .OrderBy(c => c.Knowbase.Name);
        }

        public bool KnowbaseExits(int KnowbaseId)
        {
            return _context.Knowbases.Any(p => p.Id == KnowbaseId);
            
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
