using System.Collections.Generic;
using System.Linq;

namespace apiTest.Classes
{
    class Ordine
    {
        private List<PiattoOrdine> piatti;
        public Ordine()
        {
            piatti = new List<PiattoOrdine>();
        }
        public void AddPiattoOrdine(PiattoOrdine piatto)
        {
            piatti.Add(piatto);
        }
        public void RemovePiattoOrdine(PiattoOrdine piatto)
        {
            piatti.Remove(piatto);
        }
        public List<PiattoOrdine> GetPiattoOrdine()
        {
            return piatti;
        }
        public double GetTotale()
        {
            return piatti.Sum(piatto => piatto.Piatto.prezzo * piatto.Quantità);
        }
    }
}
