namespace apiTest.Classes
{
    class Tavolo
    {
        public int NumeroTavolo { get; set; }
        public Ordine Ordine { get; set; }

        public Tavolo(int numeroTavolo)
        {
            NumeroTavolo = numeroTavolo;
            Ordine = new Ordine();
        }

    }
}
