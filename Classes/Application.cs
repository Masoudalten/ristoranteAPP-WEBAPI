using System;
using apiTest.Model;

namespace apiTest.Classes
{
    public class Application
    {

        private Ristorante ristorante;
        private Menu menu;
        private Piatto[] piatto;
        int NumeroTavolo;
        public Application()
        {
            ristorante = new Ristorante("Pizzeria");
            menu = new Menu();
            piatto = menu.MenuList;
        }

        public void Run()
        {
            DisplayMenu();
            OrderManagement();
            DisplayOrders();
        }

        private void DisplayMenu()
        {
            Console.WriteLine("Menu:");
            try
            {
                for (int i = 0; i < piatto.Length; i++)
                {
                    //Console.WriteLine($"{i + 1} - {piatto[i].nome} - {piatto[i].DisplayPrezzo()}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying the menu: {ex.Message}");
            }
        }

        private void OrderManagement()
        {
            try
            {
                do
                {
                    Console.Write("Choose a table (1 - 8): ");
                    string input = Console.ReadLine();

                    if (int.TryParse(input, out NumeroTavolo) && NumeroTavolo >= 1 && NumeroTavolo <= 8)
                    {
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Choose a table between 1 - 8");
                    }
                } while (true);

                var table = new Tavolo(NumeroTavolo);
                ristorante.AddTavolo(table);

                do
                {
                    Console.Write("Choose dish: ");
                    int ChoosedPiatto = Convert.ToInt32(Console.ReadLine());

                    Console.Write("Choose Quantity: ");
                    int ChoosedQuantity = Convert.ToInt32(Console.ReadLine());

                    var ordineTable1 = table.Ordine;
                    ordineTable1.AddPiattoOrdine(new PiattoOrdine(piatto[ChoosedPiatto - 1], ChoosedQuantity));

                } while (AddMore() == Choice.Yes);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter a valid integer.");
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Invalid choice. Please choose a valid dish.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private Choice AddMore()
        {
            while (true)
            {
                Console.Write("Do you want to add more? (Y/N): ");
                string ChoosedAnswer = Console.ReadLine().ToLower();

                if (ChoosedAnswer == "n")
                {
                    return Choice.No;
                }
                else if (ChoosedAnswer == "y")
                {
                    return Choice.Yes;
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter 'Y' or 'N'.");
                }
            }
        }

        private void DisplayOrders()
        {
            foreach (var tavolo in ristorante.GetTavoli())
            {
                Console.WriteLine($"Ordine tavolo:{tavolo.NumeroTavolo}");
                foreach (var piattoOrdine in tavolo.Ordine.GetPiattoOrdine())
                {
                    Console.WriteLine($"{piattoOrdine.Piatto.nome}, Quantità: {piattoOrdine.Quantità}");
                }
                Console.WriteLine($"Totale: {tavolo.Ordine.GetTotale()}");
                Console.WriteLine();
            }
        }
    }
}
