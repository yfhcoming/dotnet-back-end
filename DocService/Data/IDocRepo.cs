using DocService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocService.Data
{
    public interface IDocRepo
    {
        bool SaveChanges();


        // Knowbase
        IEnumerable<Knowbase> GetAllKnowbases();

        Knowbase GetKnowbaseById(int id);

        void CreateKnowbase(Knowbase kbas);
        bool KnowbaseExits(int KnowbaseId);

        bool ExternalKnowbaseExists(int externalKnowbaseId);

        // Doc
        IEnumerable<Doc> GetDocsForKnowbase(int KnowbaseId);
        Doc GetDoc(int KnowbaseId, int docId);
        void CreateDoc(int KnowbaseId, Doc doc);
    }
}
