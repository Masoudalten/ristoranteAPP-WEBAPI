using apiTest.Model;

namespace apiTest.Classes
{
    class PiattoOrdine
    {
        public Piatto Piatto { get; set; }
        public int Quantità { get; set; }

        public PiattoOrdine(Piatto piatto, int quantit)
        {
            Piatto = piatto;
            Quantità = quantit;
        }
    }
}
