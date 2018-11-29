using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamNadziejeOstatniaWersjaAlgorytmuGenetycznego
{
    public class SukcesjaZCzesciowaWymiana
    {
        public Populacja populacja { get; set; }
        public Populacja PopulacjaOsobnikowPoMutacji { get; set; }
        public Populacja PopulacjaOsobnikowPoInwersji { get; set; }
        public Populacja PopulacjaPotencjalnychRodzicow { get; set; }
        public Populacja PotomkowiePoKrzyzowaniu { get; set; }
        public List<Osobnik> WszyscyNowiOsobnicy { get; set; }

        public SukcesjaZCzesciowaWymiana(Populacja populacja)
        {
            this.populacja = populacja;
        }

        public void zmutujOsobnikow()
        {
            Populacja PopulacjaOsobnikowPoMutacji = new Populacja();
            PopulacjaOsobnikowPoMutacji.SetListaOsobnikowNowaPopulacja(populacja.listaOsobnikow.Count, populacja.listaOsobnikow, populacja.poczatkiLista, populacja.konceLista, populacja.precyzjaLista);
            PopulacjaOsobnikowPoMutacji.generyjZmutowanaPopulacje(populacja.listaOsobnikow);

            this.PopulacjaOsobnikowPoMutacji = PopulacjaOsobnikowPoMutacji;
        }

        public void stworzOsobnikowPoInwersji()
        {
            Populacja PopulacjaOsobnikowPoInwersji = new Populacja();
            PopulacjaOsobnikowPoMutacji.SetListaOsobnikowNowaPopulacja(populacja.listaOsobnikow.Count, populacja.listaOsobnikow, populacja.poczatkiLista, populacja.konceLista, populacja.precyzjaLista);
            PopulacjaOsobnikowPoInwersji.generyjPopulacjePoInwersji(populacja.listaOsobnikow);

            this.PopulacjaOsobnikowPoInwersji = PopulacjaOsobnikowPoInwersji;
        }

        public void wylosujPotencjalnychRodzicow()
        {
            Selekcja Selekcja = new Selekcja(populacja);
            Selekcja.obliczWartoscPopulacji();
            Selekcja.obliczPrawdopodobienstwo();
            Selekcja.setDystrybuantyDlaOsobnikow();

            // tu można wybrać interesującą nas selekcje:

            //List<Osobnik> WylosowaniOsobnicySukcesji = Selekcja.obracanieRulatki();
            List<Osobnik> WylosowaniOsobnicySukcesji = Selekcja.wyborOsobnikowMetodaRankingowaMalejaco();
            //List<Osobnik> WylosowaniOsobnicySukcesji = Selekcja.wyborOsobnikowMetodaRankingowaRosnaco();
            //List<Osobnik> WylosowaniOsobnicySukcesji = Selekcja.ZwracajacaWyborOsobnikowMetodaTurniejowaMalejaco(2);  //w nawiasie ilość osób w grupie
            //List<Osobnik> WylosowaniOsobnicySukcesji = Selekcja.ZwracajacaWyborOsobnikowMetodaTurniejowaRosnaco(2);
            //List<Osobnik> WylosowaniOsobnicySukcesji = Selekcja.wyborOsobnikowMetodaTurniejowaMalejaco(2);
            //List<Osobnik> WylosowaniOsobnicySukcesji = Selekcja.wyborOsobnikowMetodaTurniejowaRosnaco(2);
            List<Osobnik> potencjalniRodzice = new List<Osobnik>();

            for (int i = 0; i < WylosowaniOsobnicySukcesji.Count; i++)
            {
                string imie = "Potencjalny rodzic " + i;
                Osobnik potencjalnyRodzic = new Osobnik(imie, WylosowaniOsobnicySukcesji[i]);
                potencjalnyRodzic.SetIndex(i);
                potencjalniRodzice.Add(potencjalnyRodzic);
            }

            Populacja PopulacjaPotencjalnychRodzicow = new Populacja();
            PopulacjaPotencjalnychRodzicow.SetListaOsobnikowNowaPopulacja(WylosowaniOsobnicySukcesji.Count, potencjalniRodzice, populacja.poczatkiLista, populacja.konceLista, populacja.precyzjaLista);

            this.PopulacjaPotencjalnychRodzicow = PopulacjaPotencjalnychRodzicow;
        }

        public void krzyzujOsobnikow()
        {
            Selekcja Krzyzowanie = new Selekcja(PopulacjaPotencjalnychRodzicow);
            Krzyzowanie.wyborOsobnikowDoKrzyzowania();

            List<Osobnik> przyszliRodzice = new List<Osobnik>();

            for (int i = 0; i < Krzyzowanie.osobnicyDoSparowania.Count; i++)
            {
                string imie = "Rodzic " + i;
                Osobnik Rodzic = new Osobnik(imie, Krzyzowanie.osobnicyDoSparowania[i]);
                Rodzic.SetIndex(i);
                przyszliRodzice.Add(Rodzic);
            }

            Populacja PopulacjaRodzicow = new Populacja();
            PopulacjaRodzicow = PopulacjaPotencjalnychRodzicow;
            PopulacjaRodzicow.SetListaOsobnikow(przyszliRodzice);

            Selekcja Parowanie = new Selekcja(PopulacjaRodzicow);
            Parowanie.SetOsobnicyDoSparowania(przyszliRodzice);

            List<Osobnik> PotomkowieKrzyzowania = new List<Osobnik>();

            for (int i = 0; i < (Krzyzowanie.osobnicyDoSparowania.Count / 2); i++)
            {
                Parowanie.stworzPare();

                // tu można wybrać interesujący nas rodzaj krzyżowania:

                //PopulacjaRodzicow.krzyzowanieJednopunktowe(Parowanie.para[0].calyLancuchBinarny, Parowanie.para[1].calyLancuchBinarny);
                //PopulacjaRodzicow.krzyzowanieDwupunktowe(Parowanie.para[0].calyLancuchBinarny, Parowanie.para[1].calyLancuchBinarny);
                //PopulacjaRodzicow.krzyzowanieWielopunktowe(Parowanie.para[0].calyLancuchBinarny, Parowanie.para[1].calyLancuchBinarny);
                PopulacjaRodzicow.krzyzowanieRownomierne(Parowanie.para[0].calyLancuchBinarny, Parowanie.para[1].calyLancuchBinarny);

                for (int j = 0; j < PopulacjaRodzicow.listaPotomkow.Count; j++)
                {
                    PotomkowieKrzyzowania.Add(PopulacjaRodzicow.listaPotomkow[j]);
                }
            }

            List<Osobnik> Potomkowie = new List<Osobnik>();

            for (int i = 0; i < PotomkowieKrzyzowania.Count; i++)
            {
                string imie = "Potomek " + i;
                Osobnik Potomek = new Osobnik(imie, PotomkowieKrzyzowania[i]);
                Potomek.SetIndex(i);
                Potomkowie.Add(Potomek);
            }

            Populacja PotomkowiePoKrzyzowaniu = PopulacjaRodzicow;
            PotomkowiePoKrzyzowaniu.SetListaOsobnikow(Potomkowie);

            this.PotomkowiePoKrzyzowaniu = PotomkowiePoKrzyzowaniu;
        }

        public void zbierzWszystkichOsobnikow()
        {
            List<Osobnik> WszyscyNowiOsobnicy = new List<Osobnik>();

            for (int i = 0; i < PopulacjaOsobnikowPoMutacji.listaOsobnikow.Count; i++)
            {
                WszyscyNowiOsobnicy.Add(PopulacjaOsobnikowPoMutacji.listaOsobnikow[i]);
            }

            for (int i = 0; i < PopulacjaOsobnikowPoInwersji.listaOsobnikow.Count; i++)
            {
                WszyscyNowiOsobnicy.Add(PopulacjaOsobnikowPoInwersji.listaOsobnikow[i]);
            }

            for (int i = 0; i < PotomkowiePoKrzyzowaniu.listaOsobnikow.Count; i++)
            {
                WszyscyNowiOsobnicy.Add(PotomkowiePoKrzyzowaniu.listaOsobnikow[i]);
            }

            this.WszyscyNowiOsobnicy = WszyscyNowiOsobnicy;
        }

        public List<Osobnik> wyrzucanieOsobnika(Osobnik doWyrzucenia, List<Osobnik> pozostaliOsobnicy)
        {
            var nowaLista = pozostaliOsobnicy.Select(x => x).Where(x => x.wartoscOsobnika != doWyrzucenia.wartoscOsobnika).Select(x => x).ToList<Osobnik>();

            return nowaLista;
        }

        public void posortujWszystkichOsobnikowMalejaco()
        {
            var posortowanaLista = WszyscyNowiOsobnicy.Select(x => x).OrderByDescending(x => x.wartoscOsobnika).ToList<Osobnik>();

            this.WszyscyNowiOsobnicy = posortowanaLista;
        }

        public void posortujWszystkichOsobnikowRosnaco()
        {
            var posortowanaLista = WszyscyNowiOsobnicy.Select(x => x).OrderBy(x => x.wartoscOsobnika).ToList<Osobnik>();

            this.WszyscyNowiOsobnicy = posortowanaLista;
        }

        public void posortujOsobnikowPoczatkowychMalejaco()
        {
            var posortowanaLista = populacja.listaOsobnikow.Select(x => x).OrderByDescending(x => x.wartoscOsobnika).ToList<Osobnik>();

            this.populacja.listaOsobnikow = posortowanaLista;
        }

        public void posortujOsobnikowPoczatkowychRosnaco()
        {
            var posortowanaLista = populacja.listaOsobnikow.Select(x => x).OrderBy(x => x.wartoscOsobnika).ToList<Osobnik>();

            this.populacja.listaOsobnikow = posortowanaLista;
        }

        public List<Osobnik> posortujOsobnikowMalejaco(List<Osobnik> listaDoPosortowania)
        {
            var posortowanaLista = listaDoPosortowania.Select(x => x).OrderByDescending(x => x.wartoscOsobnika).ToList<Osobnik>();

            listaDoPosortowania = posortowanaLista;

            return posortowanaLista;
        }

        public List<Osobnik> posortujOsobnikowRosnaco(List<Osobnik> listaDoPosortowania)
        {
            var posortowanaLista = listaDoPosortowania.Select(x => x).OrderBy(x => x.wartoscOsobnika).ToList<Osobnik>();

            listaDoPosortowania = posortowanaLista;

            return posortowanaLista;
        }

        public void usunTakichSamychOsobnikow()
        {
            Osobnik doWyrzucenia;

            for (int i = 0; i < WszyscyNowiOsobnicy.Count; i++)
            {
                for (int j = i; j < WszyscyNowiOsobnicy.Count; j++)
                {
                    if (WszyscyNowiOsobnicy[i].Equals(WszyscyNowiOsobnicy[j]))
                    {
                        doWyrzucenia = WszyscyNowiOsobnicy[j];
                        WszyscyNowiOsobnicy = wyrzucanieOsobnika(doWyrzucenia, WszyscyNowiOsobnicy);
                        WszyscyNowiOsobnicy.Add(doWyrzucenia);
                    }
                }
            }

            this.WszyscyNowiOsobnicy = WszyscyNowiOsobnicy;
        }

        public void wybierzNastepnePokolenie()
        {
            List<Osobnik> nowePokolenie = new List<Osobnik>();
            List<Osobnik> WszyscyOsobnicyBezTychZPierwotnejPopulacji = WszyscyNowiOsobnicy;

            Osobnik doWyrzucenia;
            Osobnik Wybraniec;

            for (int i = 0; i < populacja.listaOsobnikow.Count; i++)
            {
                Wybraniec = populacja.listaOsobnikow[i];

                for (int j = 0; j < WszyscyOsobnicyBezTychZPierwotnejPopulacji.Count; j++)
                {
                    doWyrzucenia = WszyscyOsobnicyBezTychZPierwotnejPopulacji[j];

                    if (Wybraniec.Equals(doWyrzucenia))
                    {
                        WszyscyOsobnicyBezTychZPierwotnejPopulacji = wyrzucanieOsobnika(doWyrzucenia, WszyscyOsobnicyBezTychZPierwotnejPopulacji);
                    }
                }
            }

            // tu można wybrać rodzaj sortowania osobników:

            //WszyscyOsobnicyBezTychZPierwotnejPopulacji = posortujOsobnikowRosnaco(WszyscyOsobnicyBezTychZPierwotnejPopulacji);
            WszyscyOsobnicyBezTychZPierwotnejPopulacji = posortujOsobnikowMalejaco(WszyscyOsobnicyBezTychZPierwotnejPopulacji);

            var rng = new Random(Guid.NewGuid().GetHashCode());
 
            int iloscOsobnikowZNastepnejPopulacji = rng.Next(0, (populacja.listaOsobnikow.Count - 1));
            int iloscOsobnikowZPierwotnejPopulacji = populacja.listaOsobnikow.Count - iloscOsobnikowZNastepnejPopulacji;
            int index = 0;

            for (int i = 0; i < iloscOsobnikowZPierwotnejPopulacji; i++)
            {
                string imie = "Krolik " + i;
                Osobnik Potomek = new Osobnik(imie, populacja.listaOsobnikow[i]);
                Potomek.SetIndex(i);
                nowePokolenie.Add(Potomek);
            }

            for (int i = 0; i < iloscOsobnikowZNastepnejPopulacji; i++)
            {
                index = rng.Next(0,(WszyscyOsobnicyBezTychZPierwotnejPopulacji.Count - 1));
                string imie = "Krolik " + i;
                Osobnik Potomek = new Osobnik(imie, WszyscyOsobnicyBezTychZPierwotnejPopulacji[index]);
                Potomek.SetIndex(i);
                nowePokolenie.Add(Potomek);
            }

            List<Osobnik> Potomkowie = new List<Osobnik>();

            for (int i = 0; i < nowePokolenie.Count; i++)
            {
                string imie = "Krolik " + i;
                Osobnik Potomek = new Osobnik(imie, nowePokolenie[i]);
                Potomek.SetIndex(i);
                Potomkowie.Add(Potomek);
            }

            Populacja NowePokolenie = new Populacja();
            NowePokolenie.SetListaOsobnikowNowaPopulacja(populacja.listaOsobnikow.Count, Potomkowie, populacja.poczatkiLista, populacja.konceLista, populacja.precyzjaLista);

            this.populacja = NowePokolenie;
        }

    }
}