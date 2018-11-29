using System;
using System.Collections.Generic;
using System.Text;

namespace MamNadziejeOstatniaWersjaAlgorytmuGenetycznego
{
    public class PunktGenetyczny
    {
        public double poczatek {get; set;}
        public double koniec { get; set; }
        public double precyzja { get; set; }

        public double dlugosclancucha { get; set; }
        public string lancuchBinarny { get; set; }
        public double wartoscPunktu { get; set; }

        public override string ToString()
        {
            string testOutPut = "\npoczatek: " + poczatek + "\nkoniec: " + koniec + "\nprecyzja: " + precyzja + "\ndlugosc lancucha binarnego: "
                + dlugosclancucha + "\nlancuch binarny: " + lancuchBinarny + "\nwartosc punktu: " + wartoscPunktu + "\n";

            return testOutPut;
        }

        public PunktGenetyczny(double poczatek, double koniec, double precyzja)
        {
            this.poczatek = poczatek;
            this.koniec = koniec;
            this.precyzja = precyzja;
        }

        public void Policz()
        {

            double m;
            m = Math.Round(Math.Log(Math.Pow(10, precyzja) * (koniec - poczatek), 2));

            this.dlugosclancucha = m;
        }

        public string generujLancuchBinarny(double n)
        {
            string lancuch = string.Empty;
            var rng = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < n; i++)
                lancuch += rng.Next(2);

            this.lancuchBinarny = lancuch;

            return lancuchBinarny;
        }

        public void przesuniecieDoPrzedzialu()
        {
            int licznik = 0;
            double wart = 0;
            int dlugosclancuchabinarnego = lancuchBinarny.Length - 1;

            for (int i = ((lancuchBinarny.Length) - 1); i >= 0; i--)
            {
                if (lancuchBinarny[i] == '1')
                {
                    wart += Math.Pow(2, licznik);
                    licznik++;
                }

                else
                {
                    licznik++;
                }
            }

            double dlugoscprzedzialu = koniec - poczatek;
            double pierwszazmienna = dlugoscprzedzialu * wart;
            double potega = Math.Pow(2, dlugosclancucha);
            double drugazmienna = potega - 1;
            this.wartoscPunktu = poczatek + (pierwszazmienna / drugazmienna);
        }


        // MUTACJA GENÓW:


        public void mutacja()
        {
            string tmp = lancuchBinarny;
            string nowyLancuch = string.Empty;

            // tu możemy wprowadzić interesujące nas prawdopodobieństwo mutacji

            double prawdopodobienstwo = 0.1;
            var rng = new Random(Guid.NewGuid().GetHashCode());
            double r;
            for (int i = 0; i < dlugosclancucha; i++)
            {
                r = rng.NextDouble();
                if (r < prawdopodobienstwo)
                {
                    if (lancuchBinarny[i] == '1')
                    {
                        nowyLancuch += '0';
                    }
                    else
                    {
                        nowyLancuch += '1';
                    }
                }
                else nowyLancuch += lancuchBinarny[i];
            }
            this.lancuchBinarny = nowyLancuch;
        }

        public void SetWartoscPunktu()
        {
            int licznik = 0;
            double wart = 0;
            int dlugosclancuchabinarnego = lancuchBinarny.Length - 1;

            for (int i = ((lancuchBinarny.Length) - 1); i >= 0; i--)
            {
                if (lancuchBinarny[i] == '1')
                {
                    wart += Math.Pow(2, licznik);
                    licznik++;
                }

                else
                {
                    licznik++;
                }
            }

            double dlugoscprzedzialu = koniec - poczatek;
            double pierwszazmienna = dlugoscprzedzialu * wart;
            double potega = Math.Pow(2, dlugosclancucha);
            double drugazmienna = potega - 1;
            this.wartoscPunktu = poczatek + (pierwszazmienna / drugazmienna);
        }


        // INWERSJA CHROMOSOMU:

        public void SetLancuchBinarny(string lancuchBinarny)
        {
            this.lancuchBinarny = lancuchBinarny;
        }
    }
}
