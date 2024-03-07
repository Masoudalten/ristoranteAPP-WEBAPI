namespace apiTest.Classes
{
    public class Piatto
    {
        public string nome { get; set; }
        public string ricetta { get; set; }
        public double prezzo { get; set; }

        //public Piatto(string nome, string ricetta, double prezzo)
        //{
        //    Nome = nome;
        //    Ricetta = ricetta;
        //    Prezzo = prezzo;
        //}

        public string DisplayPrezzo()
        {
            return $"{prezzo:F2}€";
        }
    }
}
