using KnowbaseService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KnowbaseService.Data
{
    public interface IKnowbaseRepo
    {
        bool SaveChanges();

        IEnumerable<Knowbase> GetAllKnowbases();

        Knowbase GetKnowbaseById(int id);

        void CreateKnowbase(Knowbase kbas);
    }
}
