using System;
using System.Collections.Generic;
using System.Text;

namespace MamNadziejeOstatniaWersjaAlgorytmuGenetycznego
{
    public class Populacja
    {
        public List<Osobnik> listaOsobnikow { get; set; }
        public List<double> poczatkiLista { get; set; }
        public List<double> konceLista { get; set; }
        public List<double> precyzjaLista { get; set; }
        public double calkwoitawartosc { get; set; }
        public List<Osobnik> listaPotomkow { get; set; }
        public List<Osobnik> listaWszystkichPotomkow { get; set; }
        public double sredniaWartosciPokolenia { get; set; }

        public Populacja() { }

        public Populacja(List<double> listaPoczatkow, List<double> listaKoncow, List<double> listaPrecyzji)
        {
            this.listaOsobnikow = new List<Osobnik>();
            this.poczatkiLista = listaPoczatkow;
            this.konceLista = listaKoncow;
            this.precyzjaLista = listaPrecyzji;
        }

        public void SetSredniaZPokolenia(double srednia)
        {
            this.sredniaWartosciPokolenia = srednia;
        }

        public void SetListaOsobnikow(List<Osobnik> listaOsobnikow)
        {
            List<Osobnik> listaDoZastapienia = new List<Osobnik>();

            for (int i = 0; i < listaOsobnikow.Count; i++)
            {
                string imie = "Kroliczek " + i;
                Osobnik Kroliczek = new Osobnik(imie, listaOsobnikow[i]);
                listaDoZastapienia.Add(Kroliczek);
            }

            this.listaOsobnikow = listaDoZastapienia;
        }

        public void SetListaOsobnikowNowaPopulacja(int ileOsobnikow, List<Osobnik> listaOsobnikow, List<double> listaPoczatkow, List<double> listaKoncow, List<double> listaPrecyzji)
        {
            List<Osobnik> listaDoZastapienia = new List<Osobnik>();

            this.poczatkiLista = listaPoczatkow;
            this.konceLista = listaKoncow;
            this.precyzjaLista = listaPrecyzji;

            for (int i = 0; i < ileOsobnikow; i++)
            {
                string imie = "Kroliczek " + i;
                Osobnik Kroliczek = new Osobnik(imie, listaOsobnikow[i]);
                listaDoZastapienia.Add(Kroliczek);
            }

            this.listaOsobnikow = listaDoZastapienia;
        }

        public void policzSredniaWartoscPopulacji()
        {
            double srednia = 0;

            for (int i = 0; i < listaOsobnikow.Count; i++)
            {
                srednia += listaOsobnikow[i].wartoscOsobnika;
            }

            srednia = srednia / listaOsobnikow.Count;

            this.sredniaWartosciPokolenia = srednia;
        }

        public void generujPopulacje(int iloscOsobnikow)
        {
            for (int i = 0; i < iloscOsobnikow; i++)
            {
                string imie = "Krolik " + i;
                Osobnik Krolik = new Osobnik(imie);

                for (int j = 0; j < this.poczatkiLista.Count; j++)
                {
                    PunktGenetyczny Punkt = new PunktGenetyczny(this.poczatkiLista[j], this.konceLista[j], this.precyzjaLista[j]);
                    Punkt.Policz();
                    Punkt.generujLancuchBinarny(Punkt.dlugosclancucha);
                    Punkt.przesuniecieDoPrzedzialu();
                    Krolik.punktyGenetyczne.Add(Punkt);
                }

                Krolik.scalLancuchBinarny();
                Krolik.policzDlugoscCalegoLancucha();
                Krolik.algorytm();
                Krolik.SetIndex(i);
                listaOsobnikow.Add(Krolik);
            }
        }

        // INWERSJA:

        public void generyjZmutowanaPopulacje(List<Osobnik> listaOsobnikowDoNowejPopulacji)
        {
            List<Osobnik> ListaPotomkow = new List<Osobnik>();

            for (int i = 0; i < listaOsobnikowDoNowejPopulacji.Count; i++)
            {
                for (int j = 0; j < listaOsobnikowDoNowejPopulacji[i].punktyGenetyczne.Count; j++)
                {
                    listaOsobnikowDoNowejPopulacji[i].punktyGenetyczne[j].mutacja();
                    listaOsobnikowDoNowejPopulacji[i].punktyGenetyczne[j].SetWartoscPunktu();
                }

                listaOsobnikowDoNowejPopulacji[i].SetWartoscOsobnika();
                listaOsobnikowDoNowejPopulacji[i].SetScalLancuchBinarny();
                ListaPotomkow.Add(listaOsobnikowDoNowejPopulacji[i]);
            }

            this.listaOsobnikow = ListaPotomkow;
        }

        // MUTACJA:

        public void generyjPopulacjePoInwersji(List<Osobnik> listaOsobnikowDoNowejPopulacji)
        {
            List<Osobnik> ListaPotomkow = new List<Osobnik>();

            for (int i = 0; i < listaOsobnikowDoNowejPopulacji.Count; i++)
            {
                listaOsobnikowDoNowejPopulacji[i].inwersjaGenow();
                listaOsobnikowDoNowejPopulacji[i].przydzielNoweLancuchyBinarne();
                listaOsobnikowDoNowejPopulacji[i].SetWartoscOsobnika();
                listaOsobnikowDoNowejPopulacji[i].SetScalLancuchBinarny();
                ListaPotomkow.Add(listaOsobnikowDoNowejPopulacji[i]);
            }
            this.listaOsobnikow = ListaPotomkow;
        }

        // KRZYŻOWANIE:

        // KRZYŻOWANIE JEDNOPUNKTOWE:

        public void krzyzowanieJednopunktowe(string lancuchPierwszegoRodzica, string lancuchDrugiegoRodzica)
        {
            string lancuchPierwszegoPotomka = string.Empty;
            char[] lancuchPierwszegoRodzicaJakoChar = lancuchPierwszegoRodzica.ToCharArray();
            string lancuchDrugiegoPotomka = string.Empty;
            char[] lancuchDrugiegoRodzicaJakoChar = lancuchDrugiegoRodzica.ToCharArray();
            List<string> listaLancuchow = new List<string>();
            List<Osobnik> listaPotomkow = new List<Osobnik>();

            var rng = new Random(Guid.NewGuid().GetHashCode());
            int r;
            int m = (int)listaOsobnikow[0].dlugoscCalegoLancucha - 1;

            r = rng.Next(1, m);

            //Console.WriteLine();
            //Console.WriteLine("Punkt krzyzowania to: " + r);

            for (int i = 0; i <= r; i++)
            {
                lancuchPierwszegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[i]);
                lancuchDrugiegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[i]);
            }

            for (int i = r + 1; i <= m; i++)
            {
                lancuchPierwszegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[i]);
                lancuchDrugiegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[i]);
            }

            listaLancuchow.Add(lancuchPierwszegoPotomka);
            listaLancuchow.Add(lancuchDrugiegoPotomka);

            for (int i = 0; i < listaLancuchow.Count; i++)
            {
                string imie = "Potomek krolika " + i + "" + i;
                Osobnik Potomek = new Osobnik(imie);

                for (int j = 0; j < this.poczatkiLista.Count; j++)
                {
                    PunktGenetyczny Punkt = new PunktGenetyczny(this.poczatkiLista[j], this.konceLista[j], this.precyzjaLista[j]);
                    Punkt.Policz();
                    Punkt.generujLancuchBinarny(Punkt.dlugosclancucha);
                    Punkt.przesuniecieDoPrzedzialu();
                    Potomek.punktyGenetyczne.Add(Punkt);
                }

                Potomek.przydzielNowyLancuchBinarny(listaLancuchow[i]);
                Potomek.SetWartoscOsobnika();
                Potomek.SetScalLancuchBinarny();
                Potomek.policzDlugoscCalegoLancucha();
                listaPotomkow.Add(Potomek);
            }

            this.listaPotomkow = listaPotomkow;
        }

        // KRZYŻOWANIE DWUPUNKTOWE:

        public void krzyzowanieDwupunktowe(string lancuchPierwszegoRodzica, string lancuchDrugiegoRodzica)
        {
            string lancuchPierwszegoPotomka = string.Empty;
            char[] lancuchPierwszegoRodzicaJakoChar = lancuchPierwszegoRodzica.ToCharArray();
            string lancuchDrugiegoPotomka = string.Empty;
            char[] lancuchDrugiegoRodzicaJakoChar = lancuchDrugiegoRodzica.ToCharArray();
            List<string> listaLancuchow = new List<string>();
            List<Osobnik> listaPotomkow = new List<Osobnik>();

            var rng = new Random(Guid.NewGuid().GetHashCode());
            int r;
            int o;
            int m = (int)listaOsobnikow[0].dlugoscCalegoLancucha - 1;

            r = rng.Next(1, m);
            o = rng.Next(r + 1, m);

            //Console.WriteLine();
            //Console.WriteLine("Punkty krzyzowania to: {0}, {1}", r, o);

            for (int i = 0; i <= r; i++)
            {
                lancuchPierwszegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[i]);
                lancuchDrugiegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[i]);
            }

            for (int i = r + 1; i <= o; i++)
            {
                lancuchPierwszegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[i]);
                lancuchDrugiegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[i]);
            }

            for (int i = o + 1; i <= m; i++)
            {
                lancuchPierwszegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[i]);
                lancuchDrugiegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[i]);
            }

            listaLancuchow.Add(lancuchPierwszegoPotomka);
            listaLancuchow.Add(lancuchDrugiegoPotomka);

            for (int i = 0; i < listaLancuchow.Count; i++)
            {
                string imie = "Potomek krolika " + i + "" + i;
                Osobnik Potomek = new Osobnik(imie);

                for (int j = 0; j < this.poczatkiLista.Count; j++)
                {
                    PunktGenetyczny Punkt = new PunktGenetyczny(this.poczatkiLista[j], this.konceLista[j], this.precyzjaLista[j]);
                    Punkt.Policz();
                    Punkt.generujLancuchBinarny(Punkt.dlugosclancucha);
                    Punkt.przesuniecieDoPrzedzialu();
                    Potomek.punktyGenetyczne.Add(Punkt);
                }

                Potomek.przydzielNowyLancuchBinarny(listaLancuchow[i]);
                Potomek.SetWartoscOsobnika();
                Potomek.SetScalLancuchBinarny();
                Potomek.policzDlugoscCalegoLancucha();
                listaPotomkow.Add(Potomek);
            }

            this.listaPotomkow = listaPotomkow;
        }

        // KRZYŻOWANIE WIELOPUNKTOWE:

        public void krzyzowanieWielopunktowe(string lancuchPierwszegoRodzica, string lancuchDrugiegoRodzica)
        {
            string lancuchPierwszegoPotomka = string.Empty;
            char[] lancuchPierwszegoRodzicaJakoChar = lancuchPierwszegoRodzica.ToCharArray();
            string lancuchDrugiegoPotomka = string.Empty;
            char[] lancuchDrugiegoRodzicaJakoChar = lancuchDrugiegoRodzica.ToCharArray();
            List<string> listaLancuchow = new List<string>();
            List<Osobnik> listaPotomkow = new List<Osobnik>();
            List<int> listaPunktowKrzyzowania = new List<int>();

            var rng = new Random(Guid.NewGuid().GetHashCode());
            int r;
            int o = 0;
            int ktoryPunkt;
            int poczatkowyPunktDoLosowania = 0;
            int m = (int)listaOsobnikow[0].dlugoscCalegoLancucha - 1;

            r = rng.Next(1, ((m/2) - 2));

            //Console.WriteLine();
            //Console.WriteLine("Ilosc punktow krzyzowania: " + r);

            for (int i = 0; i < r; i++)
            {
                ktoryPunkt = i + 1;

                if (poczatkowyPunktDoLosowania < m - 2)
                {
                    o = rng.Next(poczatkowyPunktDoLosowania + 1, m);

                    //Console.WriteLine();
                    //Console.WriteLine("{0} punkt to: {1}", i, o);

                    if (ktoryPunkt % 2 != 0)
                    {
                        for (int j = poczatkowyPunktDoLosowania; j < o; j++)
                        {
                            lancuchPierwszegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[j]);
                            lancuchDrugiegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[j]);
                        }
                    }
                    else if (ktoryPunkt % 2 == 0)
                    {
                        for (int j = poczatkowyPunktDoLosowania; j < o; j++)
                        {
                            lancuchPierwszegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[j]);
                            lancuchDrugiegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[j]);
                        }

                    }

                    poczatkowyPunktDoLosowania = o;

                    if (r == ktoryPunkt)
                    {
                        if (r % 2 != 0)
                        {
                            for (int j = poczatkowyPunktDoLosowania; j <= m; j++)
                            {
                                lancuchPierwszegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[j]);
                                lancuchDrugiegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[j]);
                            }
                        }
                        else if (r % 2 == 0)
                        {
                            for (int j = poczatkowyPunktDoLosowania; j <= m; j++)
                            {
                                lancuchPierwszegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[j]);
                                lancuchDrugiegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[j]);
                            }
                        }
                    }
                }
                else if (poczatkowyPunktDoLosowania >= m - 2)
                {
                    if (r == ktoryPunkt)
                    {
                        if (r % 2 != 0)
                        {
                            for (int j = poczatkowyPunktDoLosowania; j <= m; j++)
                            {
                                lancuchPierwszegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[j]);
                                lancuchDrugiegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[j]);
                            }
                        }
                        else if (r % 2 == 0)
                        {
                            for (int j = poczatkowyPunktDoLosowania; j <= m; j++)
                            {
                                lancuchPierwszegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[j]);
                                lancuchDrugiegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[j]);
                            }
                        }
                    }
                    o = 0;
                }
                
            }

            listaLancuchow.Add(lancuchPierwszegoPotomka);
            listaLancuchow.Add(lancuchDrugiegoPotomka);

            for (int i = 0; i < listaLancuchow.Count; i++)
            {
                string imie = "Potomek krolika " + i + "" + i;
                Osobnik Potomek = new Osobnik(imie);

                for (int j = 0; j < this.poczatkiLista.Count; j++)
                {
                    PunktGenetyczny Punkt = new PunktGenetyczny(this.poczatkiLista[j], this.konceLista[j], this.precyzjaLista[j]);
                    Punkt.Policz();
                    Punkt.generujLancuchBinarny(Punkt.dlugosclancucha);
                    Punkt.przesuniecieDoPrzedzialu();
                    Potomek.punktyGenetyczne.Add(Punkt);
                }

                Potomek.przydzielNowyLancuchBinarny(listaLancuchow[i]);
                Potomek.SetWartoscOsobnika();
                Potomek.SetScalLancuchBinarny();
                Potomek.policzDlugoscCalegoLancucha();
                listaPotomkow.Add(Potomek);
            }

            this.listaPotomkow = listaPotomkow;
        }

        // KRZYŻOWANIE RÓWNOMIERNE:

        public void krzyzowanieRownomierne(string lancuchPierwszegoRodzica, string lancuchDrugiegoRodzica)
        {
            string lancuchPierwszegoPotomka = string.Empty;
            char[] lancuchPierwszegoRodzicaJakoChar = lancuchPierwszegoRodzica.ToCharArray();
            string lancuchDrugiegoPotomka = string.Empty;
            char[] lancuchDrugiegoRodzicaJakoChar = lancuchDrugiegoRodzica.ToCharArray();
            List<string> listaLancuchow = new List<string>();
            List<Osobnik> listaPotomkow = new List<Osobnik>();

            var rng = new Random(Guid.NewGuid().GetHashCode());
            int indexWzorca;
            int m = (int)listaOsobnikow[0].dlugoscCalegoLancucha - 1;

            indexWzorca = rng.Next(0, listaOsobnikow.Count);

            Osobnik Wzorzec = listaOsobnikow[indexWzorca];

            //Console.WriteLine("Lancuch osobnika bedacego wzorcem: " + Wzorzec.calyLancuchBinarny);
            //Console.WriteLine();

            char[] lancuchWzorcowy = Wzorzec.calyLancuchBinarny.ToCharArray();

            for (int i = 0; i <= m; i++)
            {
                if (lancuchWzorcowy[i] == '0')
                {
                    lancuchPierwszegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[i]);
                }
                else if (lancuchWzorcowy[i] == '1')
                {
                    lancuchPierwszegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[i]);
                }  
            }

            listaLancuchow.Add(lancuchPierwszegoPotomka);

            for (int i = 0; i <= m; i++)
            {
                if (lancuchWzorcowy[i] == '1')
                {
                    lancuchDrugiegoPotomka += string.Concat(lancuchPierwszegoRodzicaJakoChar[i]);
                }
                else if (lancuchWzorcowy[i] == '0')
                {
                    lancuchDrugiegoPotomka += string.Concat(lancuchDrugiegoRodzicaJakoChar[i]);
                }
            }

            listaLancuchow.Add(lancuchDrugiegoPotomka);

            for (int i = 0; i < listaLancuchow.Count; i++)
            {
                string imie = "Potomek krolika " + i + "" + i;
                Osobnik Potomek = new Osobnik(imie);

                for (int j = 0; j < this.poczatkiLista.Count; j++)
                {
                    PunktGenetyczny Punkt = new PunktGenetyczny(this.poczatkiLista[j], this.konceLista[j], this.precyzjaLista[j]);
                    Punkt.Policz();
                    Punkt.generujLancuchBinarny(Punkt.dlugosclancucha);
                    Punkt.przesuniecieDoPrzedzialu();
                    Potomek.punktyGenetyczne.Add(Punkt);
                }

                Potomek.przydzielNowyLancuchBinarny(listaLancuchow[i]);
                Potomek.SetWartoscOsobnika();
                Potomek.SetScalLancuchBinarny();
                Potomek.policzDlugoscCalegoLancucha();
                listaPotomkow.Add(Potomek);
            }

            this.listaPotomkow = listaPotomkow;
        }
    }
}