using RaAndNubi.Data.DBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RaAndNubi.Data
{
    public static class DBManager
    {
        private static RaAndNubiEntities _context = new RaAndNubiEntities();
        public static RaAndNubiEntities GetContext()
        {
            if (_context == null)
                _context = new RaAndNubiEntities();
            return _context;
        }
        public static bool UpdateDatabase()
        {
            try
            {
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<Person> GetPeople()
        {
            return _context.Person.ToList();
        }

        public static List<Pet> GetPets()
        {
            return _context.Pet.ToList();
        }

        public static List<Content> GetContent()
        {
            return _context.Content.ToList();
        }
    }
}
