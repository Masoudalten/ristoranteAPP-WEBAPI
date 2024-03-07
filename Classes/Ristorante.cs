using System.Collections.Generic;


namespace apiTest.Classes
{
    class Ristorante
    {
        public string Nome { get; set; }
        private List<Tavolo> tavoli;

        public Ristorante(string nome)
        {
            Nome = nome;
            tavoli = new List<Tavolo>();
        }

        public void AddTavolo(Tavolo tavolo)
        {
            tavoli.Add(tavolo);
        }
        public void RemoveTavolo(Tavolo tavolo)
        {
            tavoli.Remove(tavolo);
        }
        public List<Tavolo> GetTavoli()
        {
            return tavoli;

        }

    }
}
