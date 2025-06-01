using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

public interface IOsoba //condition that every person has to acccept
{
    void Accept(IOsobaVisitor visitor);
}

public interface IOsobaVisitor //defines what the visitor should do for each person (student, teacher, admin)
{
    void Visit(Uczen uczen);
    void Visit(Nauczyciel nauczyciel);
    void Visit(Administrator administrator);
}

public class Uczen : IOsoba //class that represents the student and data that we need to gather (like name, grade)
{
    public string Imie { get; } 
    public List<int> Oceny { get; }

    public Uczen(string imie, List<int> oceny)
    {
        Imie = imie;
        Oceny = oceny;
    }
    public void Accept(IOsobaVisitor visitor) //accepts the visitor and makes him visit 
    {
        visitor.Visit(this);
    }
}

public class Nauczyciel : IOsoba //class that represents the teacher and data that we need to gather (like name, amount of grades given to students)
{
    public string Imie { get; }
    public int LiczbaWystawionychOcen { get; }

    public Nauczyciel(string imie, int liczbaOcen)
    {
        Imie = imie;
        LiczbaWystawionychOcen = liczbaOcen;
    }

    public void Accept(IOsobaVisitor visitor) //accepts the visitor and makes him visit 
    {
        visitor.Visit(this);
    }
}

public class Administrator : IOsoba //class that represents the admin and data that we need to gather (like name and logs)
{
    public string Imie { get; }
    public List<string> Logi { get; } 

    public Administrator(string imie, List<string> logi)
    {
        Imie = imie;
        Logi = logi;
    }

    public void Accept(IOsobaVisitor visitor) //accepts the visitor and makes him visit
    {
        visitor.Visit(this);
    }
}

public class RaportVisitor : IOsobaVisitor //implementation that generates reports
{
    public void Visit(Uczen uczen)
    {
        if (uczen.Oceny.Count > 0) //checks if student has grades, if yes - it calculates the average, if no - give the output that says "no grades"
        {
            double srednia = uczen.Oceny.Average();
            Console.WriteLine($"Uczeń {uczen.Imie} – Średnia ocen: {srednia:F2}");
        }
        else
        {
            Console.WriteLine($"Uczeń {uczen.Imie} – Brak ocen");
        }
    }

    public void Visit(Nauczyciel nauczyciel)
    {
        Console.WriteLine($"Nauczyciel {nauczyciel.Imie} – Wystawił ocen: {nauczyciel.LiczbaWystawionychOcen}"); //Prints the teacher's name along with the nuber of grades given
    }
    public void Visit(Administrator administrator)
    {
        Console.WriteLine($"Administrator {administrator.Imie} – Logi systemowe:"); //Prints the admin's name 
        if (administrator.Logi.Count > 0) //Checks logs made by the admin and gives it in the output
        {
            foreach (var log in administrator.Logi)
            {
                Console.WriteLine($" - {log}");
            }
        }
        else
        {
            Console.WriteLine(" - Brak logów systemowych."); //if there's no logs - gives the output "no logs"
        }
    }
}
public class Program
{
    public static void Main()
    {
        var osoby = new List<IOsoba> //creates a list of students, teachers and administrators
        {
            new Uczen("Sally Mqueen", new List<int> { 5, 3, 6, 5 }),
            new Uczen("Bert Bertsson", new List<int> { 4, 4, 3, 5 }),
            new Uczen("Bono Svensson", new List<int> { 5, 6, 5, 4 }),
            new Uczen("Marley Fredrovic", new List<int> { 4, 4, 5, 3 }),
            new Nauczyciel("Rodney Bush", 55),
            new Nauczyciel("Frank Franksson", 92),
            new Nauczyciel("Kurt Noodleman", 72),
            new Nauczyciel("Moonie Wiggleton", 82),
            new Administrator("Admin1", new List<string> { "Zalogowano użytkownika", "Zmieniono hasło" }),
            new Administrator("Admin2", new List<string>()) //string is empty because there's no logs from this admin
        };

        var visitor = new RaportVisitor(); //creates a visitor

        foreach (var osoba in osoby) //checks on every person and makes visitor process them
        {
            osoba.Accept(visitor);
        }

        Console.ReadKey();
    }
}