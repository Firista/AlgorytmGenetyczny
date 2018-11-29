using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamNadziejeOstatniaWersjaAlgorytmuGenetycznego
{
    public class SukcesjaZCalkowitymZastepowaniem
    {
        public Populacja populacja { get; set; }
        public Populacja PopulacjaOsobnikowPoMutacji { get; set; }
        public Populacja PopulacjaOsobnikowPoInwersji { get; set; }
        public Populacja PopulacjaPotencjalnychRodzicow { get; set; }
        public Populacja PotomkowiePoKrzyzowaniu { get; set; }
        public List<Osobnik> WszyscyOsobnicy { get; set; }

        public SukcesjaZCalkowitymZastepowaniem(Populacja populacja)
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

        public void SetPopulacjaPoMutacji()
        {
            this.populacja = PopulacjaOsobnikowPoMutacji;
        }

        public void stworzOsobnikowPoInwersji()
        {
            Populacja PopulacjaOsobnikowPoInwersji = new Populacja();
            PopulacjaOsobnikowPoMutacji.SetListaOsobnikowNowaPopulacja(populacja.listaOsobnikow.Count, populacja.listaOsobnikow, populacja.poczatkiLista, populacja.konceLista, populacja.precyzjaLista);
            PopulacjaOsobnikowPoInwersji.generyjPopulacjePoInwersji(populacja.listaOsobnikow);

            this.PopulacjaOsobnikowPoInwersji = PopulacjaOsobnikowPoInwersji;
        }

        public void SetPopulacjaPoInwersji()
        {
            this.PopulacjaOsobnikowPoInwersji = PopulacjaOsobnikowPoMutacji;
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
                //PopulacjaRodzicow.krzyzowanieRownomierne(Parowanie.para[0].calyLancuchBinarny, Parowanie.para[1].calyLancuchBinarny);
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

        public List<Osobnik> wyrzucanieOsobnika(Osobnik doWyrzucenia, List<Osobnik> pozostaliOsobnicy)
        {
            var nowaLista = pozostaliOsobnicy.Select(x => x).Where(x => x.wartoscOsobnika != doWyrzucenia.wartoscOsobnika).Select(x => x).ToList<Osobnik>();

            return nowaLista;
        }

        public void wybierzNastepnePokolenie()
        {
            List<Osobnik> nowePokolenie = new List<Osobnik>();
            List<Osobnik> listaOsobnikow = populacja.listaOsobnikow;
            List<Osobnik> listaRodzicow = PopulacjaPotencjalnychRodzicow.listaOsobnikow;
            List<Osobnik> listaPotomkow = PotomkowiePoKrzyzowaniu.listaOsobnikow;

            Osobnik Wybraniec;
            Osobnik rodzicDoWyrzucenia;
            Osobnik potomekDoWyrzucenia;

            for (int i = 0; i < listaOsobnikow.Count; i++)
            {
                Wybraniec = listaOsobnikow[i];

                for (int j = 0; j < listaRodzicow.Count; j++)
                {
                    rodzicDoWyrzucenia = listaRodzicow[j];
                    potomekDoWyrzucenia = listaPotomkow[j];

                    if (Wybraniec.Equals(rodzicDoWyrzucenia))
                    {
                        nowePokolenie.Add(potomekDoWyrzucenia);
                        listaOsobnikow = wyrzucanieOsobnika(Wybraniec, listaOsobnikow);
                        listaRodzicow = wyrzucanieOsobnika(rodzicDoWyrzucenia, listaRodzicow);
                        listaPotomkow = wyrzucanieOsobnika(potomekDoWyrzucenia, listaPotomkow);
                    }
                }
            }

            for (int i = 0; i < listaOsobnikow.Count; i++)
            {
                nowePokolenie.Add(listaOsobnikow[i]);
            }

            Populacja PopulacjaZPotomkami = new Populacja();
            PopulacjaZPotomkami.SetListaOsobnikowNowaPopulacja(populacja.listaOsobnikow.Count, nowePokolenie, populacja.poczatkiLista, populacja.konceLista, populacja.precyzjaLista);

            this.populacja = PopulacjaZPotomkami;
        }
    }
}