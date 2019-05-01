using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;
using static System.Convert;

namespace egyszamjatek
{
    class Program
    {
        static List<Jatekos> jatekos = new List<Jatekos>();
        static void Main(string[] args)
        {
            Feladat1();
            Feladat2();
            Feladat3();
            Feladat4();
            Feladat5();
            Feladat6();
            Feladat7();
            ReadKey();
        }
        private static void Feladat1()
        {
        }
        private static void Feladat2()
        {
            /*
            2. Olvassa be az egyszamjatek.txt állományban lévő adatokat és tárolja el egy olyan
            adatszerkezetben, ami a további feladatok megoldására alkalmas!           
             */
            FileStream fs = new FileStream("egyszamjatek.txt",FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            /*
            foreach (var i in File.ReadAllLines("egyszamjatek.txt")) 
            {
                t.Add(new Játékos(i.Split()));               
            }
            */
            while (!sr.EndOfStream)
            {
                jatekos.Add(new Jatekos(sr.ReadLine()));
            }
            sr.Close();
            fs.Close();
        }
        private static void Feladat3()
        {
            /* Határozza meg és írja ki a képernyőre a minta szerint, hogy a játékban hány játékos vett
            részt! */
            WriteLine("3. feladat: Játékosok száma: {0}", jatekos.Count);
        }
        private static void Feladat4()
        {
            /* Határozza meg és írja ki a képernyőre a minta szerint, hogy a játékban hány fordulót játszottak a játékosok! 
            Feltételezheti, hogy minden játékos minden fordulóban részt vett.
            */
            WriteLine("4. feladat: Fordulók száma: {0}", jatekos[0].Tippek.Count);
        }
        private static void Feladat5()
        {
            /*
            Döntse el és írja ki a képernyőre a minták szerint, hogy az első fordulóban tippelt-e valaki az 1-es számra!
            */
            WriteLine("5. feladat: Az első fordulóban {0} volt egyes tipp!", jatekos.Count(i => i.Tippek[0] == 1) > 0 ? "" : "nem ");
        }
        private static void Feladat6()
        {
            /*
            Határozza meg és írja ki a minta szerint, hogy a fordulók során melyik volt a legnagyobb tipp!           
            */
            WriteLine("6. feladat: A legnagyobb tipp a fordulók során: {0}", jatekos.Max(i => i.Tippek.Max()));
        }
        private static void Feladat7()
        {
            /*
            Kérje be egy forduló sorszámát! Az adatbevitel előtt jelenjen meg a lehetséges legkisebb 
            és legnagyobb fordulószám értéke! Például: „7. feladat: Kérem a forduló
            sorszámát [1−10]:” (Ebben az esetben 10 db forduló volt az egyszamjatek.txt
            állományban.) Ha a beadott sorszám nem felel meg a lehetséges értékeknek, akkor az
            1. fordulóval dolgozzon a következő feladatokban!
            */
            Write("7. feladat: Kérem a forduló sorszámát [1-{0}]: ", jatekos[0].Tippek.Count);
            int fordulóSorszáma = int.Parse(ReadLine());
            if (fordulóSorszáma < 1 || fordulóSorszáma > jatekos[0].Tippek.Count)
            {
                fordulóSorszáma = 1;
            }
            var egyediTipp = jatekos.GroupBy(g => g.Tippek[fordulóSorszáma - 1]).Where(i => i.Count() == 1).OrderBy(i => i.Key).FirstOrDefault();
            /*
            Az előző feladatban bekért fordulóban határozza meg és írja ki a minta szerint a nyertes
            tipp értékét! Ha nem volt nyertes tipp a vizsgált fordulóban, akkor a „Nem volt egyedi tipp
            a megadott fordulóban!” szöveget jelenítse meg!           
            */
            if (egyediTipp != null)
            {
                WriteLine("8. feladat: A nyertes tipp a megadott fordulóban: {0}", egyediTipp.Key);
            }
            else
            {
                WriteLine("8. feladat: Nem volt egyedi tipp a megadott fordulóban!");
            }
            string fordulóNyertese = " ";
            if (egyediTipp != null)
            {
                fordulóNyertese = jatekos.Where(i => i.Tippek[fordulóSorszáma - 1] == egyediTipp.Key).First().Nev;
                WriteLine("A megadott forduló nyertese: {0}", fordulóNyertese);
            }
            else
            {
                WriteLine("Nem volt nyertes a megadott fordulóban!");
            }
            if (egyediTipp != null)
            {
                FileStream fs = new FileStream("nyertes.txt", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs,Encoding.UTF8);
                // List<string> ki = new List<string>();
                // ki.Add("Forduló sorszáma: {0}.", fordulóSorszáma);
                // ki.Add("Nyertes tipp: {0}", egyediTipp.Key);
                // ki.Add("Nyertes játékos: {0}", fordulóNyertese);
                sw.WriteLine("Nyertes tipp: {0}", egyediTipp.Key);
                sw.WriteLine("Nyertes játékos: {0}", fordulóNyertese);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
        }
    }          
}

/*
Egyszámjáték
40 pont
Az egyszámjáték Mérő László matematikus találmánya. A játék nagyon egyszerű.
Mindenkinek, aki a játék egy fordulójában részt kíván venni, tippelnie kell egy számra 1 és
99 között. A játékot az nyeri, aki a legkisebb olyan számra tippelt, amelyre csak ő tippelt
egyedül, ha nincs ilyen szám, akkor a fordulónak nincs nyertese.
Ebben a feladatban egy többfordulós egyszámjátékkal kapcsolatban kell feladatokat
megoldania.

A megoldás során vegye figyelembe a következőket:

A képernyőre írást igénylő részfeladatok eredményének megjelenítése előtt írja a
képernyőre a feladat sorszámát (például: 3. feladat: )!
Az egyes feladatokban a kiírásokat a minta szerint készítse el!
Az ékezetmentes kiírás is elfogadott.
A program megírásakor a fájlban lévő adatok helyes szerkezetét nem kell
ellenőriznie, feltételezheti, hogy a rendelkezésre álló adatok a leírtaknak
megfelelnek.
A megoldását úgy készítse el, hogy az azonos szerkezetű, de tetszőleges bemeneti
adatok mellett is helyes eredményt adjon!
Az egyszamjatek.txt állomány soronként tartalmazza a játékban részt vevő
játékosok nevét és a fordulónként leadott tippjeit. A játékosok és a fordulók száma 5−10
közötti lehet. A tippek 1−99 közötti egész számok lehetnek. Az adatokat a szóköz karakter
választja el egymástól. Az állományban nincs két egyforma nevű játékos.
Például: 3 12 1 8 5 8 1 2 1 4 Marci
A példában látható, hogy Marci tippjei a játék 10 fordulójában rendre
3 12 1 8 5 8 1 2 1 4 voltak.

1. Készítsen programot a következő feladatok megoldására, amelynek a forráskódját
egyszamjatek néven mentse el!
2. Olvassa be az egyszamjatek.txt állományban lévő adatokat és tárolja el egy olyan
adatszerkezetben, ami a további feladatok megoldására alkalmas!
3. Határozza meg és írja ki a képernyőre a minta szerint, hogy a játékban hány játékos vett
részt!
4. Határozza meg és írja ki a képernyőre a minta szerint, hogy a játékban hány fordulót
játszottak a játékosok! Feltételezheti, hogy minden játékos minden fordulóban részt vett.
5. Döntse el és írja ki a képernyőre a minták szerint, hogy az első fordulóban tippelt-e valaki
az 1-es számra!
6. Határozza meg és írja ki a minta szerint, hogy a fordulók során melyik volt a legnagyobb
tipp!
7. Kérje be egy forduló sorszámát! Az adatbevitel előtt jelenjen meg a lehetséges legkisebb
és legnagyobb fordulószám értéke! Például: „7. feladat: Kérem a forduló
sorszámát [1−10]:” (Ebben az esetben 10 db forduló volt az egyszamjatek.txt
állományban.) Ha a beadott sorszám nem felel meg a lehetséges értékeknek, akkor az
1. fordulóval dolgozzon a következő feladatokban!
8. Az előző feladatban bekért fordulóban határozza meg és írja ki a minta szerint a nyertes
tipp értékét! Ha nem volt nyertes tipp a vizsgált fordulóban, akkor a „Nem volt egyedi tipp
a megadott fordulóban!” szöveget jelenítse meg!
*/
