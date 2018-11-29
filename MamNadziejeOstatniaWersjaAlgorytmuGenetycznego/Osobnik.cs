using System;
using System.Collections.Generic;
using System.Text;

namespace MamNadziejeOstatniaWersjaAlgorytmuGenetycznego
{
    public class Osobnik
    {
        public string imie { get; set; }
        public List<PunktGenetyczny> punktyGenetyczne;
        public double wartoscOsobnika { get; set; }
        public double prawdopodobienstwo { get; set; }
        public double dystrybuanta { get; set; }

        public string calyLancuchBinarny = string.Empty;
        public double dlugoscCalegoLancucha { get; set; }
        public int id { get; set; }

        public override string ToString()
        {
            string testOutPut = "_____________________________________\n" + imie + "\nMa wartosc funkcji: " + wartoscOsobnika
                + "\nJego lancuch binarny to: " + calyLancuchBinarny + "\nMa on laczna dlugosc:" + dlugoscCalegoLancucha + "\nIndex:" + id;
          /*
          testOutPut += "\n\nSzczegolowe informacje o Osobniku:\n";
            
            for (int i = 0; i < punktyGenetyczne.Count; i++)
            {
                testOutPut += "\nPunkt" + (i + 1) +":";
                testOutPut += punktyGenetyczne[i];
            }
            */
            return testOutPut;
        }

        public Osobnik(string imie)
        {
            this.imie = imie;
            this.punktyGenetyczne = new List<PunktGenetyczny>();          
        }

        public Osobnik(string nazwa, Osobnik osobnikDoZduplikowania)
        {
            this.imie = nazwa;
            this.punktyGenetyczne = osobnikDoZduplikowania.punktyGenetyczne;
            this.wartoscOsobnika = osobnikDoZduplikowania.wartoscOsobnika;
            this.prawdopodobienstwo = osobnikDoZduplikowania.prawdopodobienstwo;
            this.dystrybuanta = osobnikDoZduplikowania.dystrybuanta;
            this.calyLancuchBinarny = osobnikDoZduplikowania.calyLancuchBinarny;
            this.dlugoscCalegoLancucha = osobnikDoZduplikowania.dlugoscCalegoLancucha;

        }

        public Osobnik(Osobnik osobnikDoZduplikowania)
        {
            this.imie = osobnikDoZduplikowania.imie;
            this.punktyGenetyczne = osobnikDoZduplikowania.punktyGenetyczne;
            this.wartoscOsobnika = osobnikDoZduplikowania.wartoscOsobnika;
            this.prawdopodobienstwo = osobnikDoZduplikowania.prawdopodobienstwo;
            this.dystrybuanta = osobnikDoZduplikowania.dystrybuanta;
            this.calyLancuchBinarny = osobnikDoZduplikowania.calyLancuchBinarny;
            this.dlugoscCalegoLancucha = osobnikDoZduplikowania.dlugoscCalegoLancucha;

        }

        public Osobnik(double wartoscFunkcji)
        {
            this.wartoscOsobnika = wartoscFunkcji;
        }

        public void SetIndex(int index)
        {
            this.id = index;
        }

        public double algorytm()
        {
            int A = 10;
            double wynik = 0;
            double w = 20 * Math.PI;


            for (int i = 0; i < punktyGenetyczne.Count; i++)
            {

                wynik += (Math.Pow(punktyGenetyczne[i].wartoscPunktu, 2)) - (A * (Math.Cos(w * punktyGenetyczne[i].wartoscPunktu)));

            }

            wynik += A * punktyGenetyczne.Count;
            this.wartoscOsobnika = wynik;
            return wartoscOsobnika;
        }

        public double policzDlugoscCalegoLancucha()
        {
            double dlugoscCalegoLancucha = 0;

            for (int i = 0; i < punktyGenetyczne.Count; i++)
            {
                dlugoscCalegoLancucha += punktyGenetyczne[i].dlugosclancucha;
            }

            this.dlugoscCalegoLancucha = dlugoscCalegoLancucha;

            return dlugoscCalegoLancucha;
        }

        public string scalLancuchBinarny()
        {
            for (int i = 0; i < punktyGenetyczne.Count; i++)
            {
                calyLancuchBinarny += string.Concat(punktyGenetyczne[i].lancuchBinarny);
            }

            return calyLancuchBinarny;
        }

        public override bool Equals(object obj)
        {
            if (obj is Osobnik)
            {
                Osobnik tmp = obj as Osobnik;
                if (this.calyLancuchBinarny.Equals(tmp.calyLancuchBinarny))
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        // MUTACJA GENÓW:


        public void SetWartoscOsobnika()
        {
            int A = 10;
            double wynik = 0;
            double w = 20 * Math.PI;


            for (int i = 0; i < punktyGenetyczne.Count; i++)
            {

                wynik += (Math.Pow(punktyGenetyczne[i].wartoscPunktu, 2)) - (A * (Math.Cos(w * punktyGenetyczne[i].wartoscPunktu)));

            }

            wynik += A * punktyGenetyczne.Count;
            this.wartoscOsobnika = wynik;
        }


        public void SetScalLancuchBinarny()
        {
            string nowyCalyLancuchBinarny = string.Empty;

            for (int i = 0; i < punktyGenetyczne.Count; i++)
            {
                nowyCalyLancuchBinarny += string.Concat(punktyGenetyczne[i].lancuchBinarny);
            }

            this.calyLancuchBinarny = nowyCalyLancuchBinarny;
        }


        // INWERSJA CHROMOSOMU:


        public void inwersjaGenow()
        {
            string chromosomPoInwersji = string.Empty;
            char[] lancuchJakoChar = this.calyLancuchBinarny.ToCharArray();
            string nowycalyLancuchBinarny = string.Empty;

            // tu możemy wprowadzić interesujące nas prawdopodobieństwo inwersji

            double prawdopodobienstwo = 0.5;
            var rng = new Random(Guid.NewGuid().GetHashCode());
            double r;
            int m = (int)dlugoscCalegoLancucha - 1;

            r = rng.NextDouble();

            if (r < prawdopodobienstwo)
            {
                int poczatkowyPunktInwersji;
                int koncowyPunktInwersji;
                poczatkowyPunktInwersji = rng.Next(0, m);
                koncowyPunktInwersji = rng.Next((poczatkowyPunktInwersji + 1), m);

                for (int i = 0; i < poczatkowyPunktInwersji; i++)
                {
                    nowycalyLancuchBinarny += string.Concat(lancuchJakoChar[i]);
                }

                string tmp = string.Empty;

                for (int i = koncowyPunktInwersji; i >= poczatkowyPunktInwersji; i--)
                {
                    tmp += string.Concat(lancuchJakoChar[i]);
                }

                nowycalyLancuchBinarny += string.Concat(tmp);

                for (int i = koncowyPunktInwersji + 1; i <=m; i++)
                {
                    nowycalyLancuchBinarny += string.Concat(lancuchJakoChar[i]);
                }
            }
            else
            {
                nowycalyLancuchBinarny = string.Concat(calyLancuchBinarny);
            }

            this.calyLancuchBinarny = nowycalyLancuchBinarny;
        }

        public void przydzielNoweLancuchyBinarne()
        {
            string binarnyLancuch = string.Empty;
            char[] lancuchJakoChar = this.calyLancuchBinarny.ToCharArray();

            int poczatekLancucha = 0;
            int dlugoscLancucha = 0;

            for (int i = 0; i < punktyGenetyczne.Count; i++)
            {
                dlugoscLancucha = (int)punktyGenetyczne[i].dlugosclancucha;

                binarnyLancuch = string.Concat(calyLancuchBinarny.Substring(poczatekLancucha, dlugoscLancucha));
                punktyGenetyczne[i].SetLancuchBinarny(binarnyLancuch);
                punktyGenetyczne[i].SetWartoscPunktu();

                binarnyLancuch = string.Empty;
            
                poczatekLancucha += (int)punktyGenetyczne[i].dlugosclancucha;
                dlugoscLancucha = 0;
            }           
        }
        public void przydzielNowyLancuchBinarny(string lancuchBinarny)
        {
            string binarnyLancuch = string.Empty;
            char[] lancuchJakoChar = lancuchBinarny.ToCharArray();

            int poczatekLancucha = 0;
            int dlugoscLancucha = 0;

            for (int i = 0; i < punktyGenetyczne.Count; i++)
            {
                dlugoscLancucha = (int)punktyGenetyczne[i].dlugosclancucha;

                binarnyLancuch = string.Concat(lancuchBinarny.Substring(poczatekLancucha, dlugoscLancucha));
                punktyGenetyczne[i].SetLancuchBinarny(binarnyLancuch);
                punktyGenetyczne[i].SetWartoscPunktu();

                binarnyLancuch = string.Empty;

                poczatekLancucha += (int)punktyGenetyczne[i].dlugosclancucha;
                dlugoscLancucha = 0;
            }
        }
    }
}