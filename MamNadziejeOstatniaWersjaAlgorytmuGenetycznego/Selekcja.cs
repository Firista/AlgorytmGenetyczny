using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MamNadziejeOstatniaWersjaAlgorytmuGenetycznego
{
    public class Selekcja
    {
        public Populacja popul;
        public List<Osobnik> osobnicyDoSparowania { get; set; }
        public List<Osobnik> para { get; set; }

        public Selekcja(Populacja listaOsobnikow)
        {
            
            this.popul = listaOsobnikow;
        }

        public void obliczWartoscPopulacji()
        {
            double value = 0;
            foreach (var result in popul.listaOsobnikow)
                value += result.wartoscOsobnika;

            popul.calkwoitawartosc = value;
        }

        public void obliczPrawdopodobienstwo()
        {
            for(int i = 0; i < popul.listaOsobnikow.Count; i++)
            {
                popul.listaOsobnikow[i].prawdopodobienstwo = popul.listaOsobnikow[i].wartoscOsobnika / popul.calkwoitawartosc;
            }
        }

        public double policzDystrybuante(int range)
        {
            double value = 0;
            if( range == 0)
                return popul.listaOsobnikow[0].prawdopodobienstwo;
            for (int i = 0; i < range; i++)
            {
                value += popul.listaOsobnikow[i].prawdopodobienstwo;
            }
            return value;

        }
        public void setDystrybuantyDlaOsobnikow()
        {
      
            for (int i = 0; i < popul.listaOsobnikow.Count; i++)
                popul.listaOsobnikow[i].dystrybuanta = policzDystrybuante(i);
     
        }

        // METODA RULETKI:

        public List<Osobnik> obracanieRulatki()
        {
            List<Osobnik> ListaWybranychOsobnikowDoLosowania = posortujWartosciFunkcjiMalejaco();
            List<Osobnik> ListaWybranychOsobnikow = new List<Osobnik>();
            double r = 0;
            var rng = new Random();
            double dystrybuanta = 0;
            Osobnik NowyOsobnik;

            for(int i = 0; i< ListaWybranychOsobnikowDoLosowania.Count; i++)
            {
                r = rng.NextDouble();
                if (r < ListaWybranychOsobnikowDoLosowania[i].dystrybuanta)
                {
                    NowyOsobnik = ListaWybranychOsobnikowDoLosowania[i];
                    NowyOsobnik.SetIndex(i);
                    ListaWybranychOsobnikow.Add(NowyOsobnik);
                }    
                else
                {
                    int a = i;
                    while (r > ListaWybranychOsobnikowDoLosowania[a].dystrybuanta)
                    {
                        dystrybuanta += ListaWybranychOsobnikowDoLosowania[a].prawdopodobienstwo;
                        a++;
                        if (a >= ListaWybranychOsobnikowDoLosowania.Count)
                        {
                            break;
                        }
                    }
                    NowyOsobnik = ListaWybranychOsobnikowDoLosowania[a - 1];
                    NowyOsobnik.SetIndex(i);
                    ListaWybranychOsobnikow.Add(NowyOsobnik);
                }

                dystrybuanta = 0;
            }
            
            return ListaWybranychOsobnikow;
        }

        // METODA RANKINGOWA:

        public List<Osobnik> posortujWartosciFunkcjiRosnaco()
        {
            var sortedList = popul.listaOsobnikow.OrderBy(x => x.wartoscOsobnika).ToList<Osobnik>(); 
            return sortedList;
        }

        public List<Osobnik> posortujWartosciFunkcjiMalejaco()
        {
           
            var sortedList = popul.listaOsobnikow.OrderByDescending(x => x.wartoscOsobnika).ToList<Osobnik>();
            return sortedList;
        }

        public List<Osobnik> wyborOsobnikowMetodaRankingowaMalejaco()
        {
            List<Osobnik> ListaWybranychOsobnikow = posortujWartosciFunkcjiMalejaco();

            List<Osobnik> theCHosenOnes = new List<Osobnik>();

            var rng = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < ListaWybranychOsobnikow.Count; i++)
            {
                theCHosenOnes.Add(ListaWybranychOsobnikow[rng.Next(0, rng.Next(0, ListaWybranychOsobnikow.Count - 1))]);
            }

            return theCHosenOnes;
        }

        public List<Osobnik> wyborOsobnikowMetodaRankingowaRosnaco()
        {
            List<Osobnik> ListaWybranychOsobnikow = posortujWartosciFunkcjiRosnaco();

            List<Osobnik> theCHosenOnes = new List<Osobnik>();

            var rng = new Random(Guid.NewGuid().GetHashCode());

            for (int i = 0; i < ListaWybranychOsobnikow.Count; i++)
            {
                theCHosenOnes.Add(ListaWybranychOsobnikow[rng.Next(0, rng.Next(0, ListaWybranychOsobnikow.Count))]);
            }

            return theCHosenOnes;
        }

        // METODA TURNIEJOWA:

        public List<Osobnik> wyrzucanieOsobnika(Osobnik zwrocony, List<Osobnik> pozostaliOsobnicy)
        {
            var nowaLista = pozostaliOsobnicy.Where(x => x.id != zwrocony.id).Select(x => x).ToList<Osobnik>();

            return nowaLista;
        }

        // ZE ZWRACANIEM:

        public List<Osobnik> ZwracajacaWyborOsobnikowMetodaTurniejowaMalejaco(int iloscOsobWGrupie)
        {
            var rng = new Random(Guid.NewGuid().GetHashCode());
            List<Osobnik> ListaWybranychOsobnikow = new List<Osobnik>();
            List<Osobnik> grupa = new List<Osobnik>();

            List<Osobnik> najsilniejszeOgniwa = popul.listaOsobnikow;
            Osobnik najlepszyOsobnik;

            for (int i = 0; i < popul.listaOsobnikow.Count; i++)
            {
                for (int j = 0; j < iloscOsobWGrupie; j++)
                {
                    if (najsilniejszeOgniwa.Count >= 2)
                    {
                        najlepszyOsobnik = najsilniejszeOgniwa[rng.Next(0, najsilniejszeOgniwa.Count - 1)];
                        grupa.Add(najlepszyOsobnik);

                        najsilniejszeOgniwa = wyrzucanieOsobnika(najlepszyOsobnik, najsilniejszeOgniwa);

                        if (j == iloscOsobWGrupie - 1)
                        {
                            grupa = grupa.OrderByDescending(x => x.wartoscOsobnika).ToList<Osobnik>();

                            ListaWybranychOsobnikow.Add(grupa[0]);

                            grupa = new List<Osobnik>();
                        }
                    }
                    else if (najsilniejszeOgniwa.Count <2 && najsilniejszeOgniwa.Count > 0)
                    {
                        najlepszyOsobnik = najsilniejszeOgniwa[0];
                        grupa.Add(najlepszyOsobnik);

                        najsilniejszeOgniwa = wyrzucanieOsobnika(najlepszyOsobnik, najsilniejszeOgniwa);

                        if (j == iloscOsobWGrupie - 1)
                        {
                            grupa = grupa.OrderByDescending(x => x.wartoscOsobnika).ToList<Osobnik>();

                            ListaWybranychOsobnikow.Add(grupa[0]);

                            grupa = new List<Osobnik>();
                        }
                    }
                }

                najsilniejszeOgniwa = popul.listaOsobnikow;
            }

            return ListaWybranychOsobnikow;
        }

        public List<Osobnik> ZwracajacaWyborOsobnikowMetodaTurniejowaRosnaco(int iloscOsobWGrupie)
        {
            var rng = new Random(Guid.NewGuid().GetHashCode());
            List<Osobnik> ListaWybranychOsobnikow = new List<Osobnik>();
            List<Osobnik> grupa = new List<Osobnik>();

            List<Osobnik> najsilniejszeOgniwa = popul.listaOsobnikow;
            Osobnik najlepszyOsobnik;

            for (int i = 0; i < popul.listaOsobnikow.Count; i++)
            {
                for (int j = 0; j < iloscOsobWGrupie; j++)
                {
                    if (najsilniejszeOgniwa.Count >= 2)
                    {
                        najlepszyOsobnik = najsilniejszeOgniwa[rng.Next(0, najsilniejszeOgniwa.Count - 1)];
                        grupa.Add(najlepszyOsobnik);

                        najsilniejszeOgniwa = wyrzucanieOsobnika(najlepszyOsobnik, najsilniejszeOgniwa);

                        if (j == iloscOsobWGrupie - 1)
                        {
                            grupa = grupa.OrderByDescending(x => x.wartoscOsobnika).ToList<Osobnik>();

                            ListaWybranychOsobnikow.Add(grupa[0]);

                            grupa = new List<Osobnik>();
                        }
                    }
                    else if (najsilniejszeOgniwa.Count < 2 && najsilniejszeOgniwa.Count > 0)
                    {
                        najlepszyOsobnik = najsilniejszeOgniwa[0];
                        grupa.Add(najlepszyOsobnik);

                        najsilniejszeOgniwa = wyrzucanieOsobnika(najlepszyOsobnik, najsilniejszeOgniwa);

                        if (j == iloscOsobWGrupie - 1)
                        {
                            grupa = grupa.OrderBy(x => x.wartoscOsobnika).ToList<Osobnik>();

                            ListaWybranychOsobnikow.Add(grupa[0]);

                            grupa = new List<Osobnik>();
                        }
                    }
                }

                najsilniejszeOgniwa = popul.listaOsobnikow;
            }

            return ListaWybranychOsobnikow;
        }

        // BEZ ZWRACANIA:

        public List<Osobnik> wyborOsobnikowMetodaTurniejowaMalejaco(int iloscOsobWGrupie)
        {
            var rng = new Random(Guid.NewGuid().GetHashCode());
            List<Osobnik> ListaWybranychOsobnikow = new List<Osobnik>();
            List<Osobnik> grupa = new List<Osobnik>();

            for (int i = 0; i < popul.listaOsobnikow.Count; i++)
            {
                for (int j = 0; j < iloscOsobWGrupie; j++)
                {
                    grupa.Add(popul.listaOsobnikow[rng.Next(popul.listaOsobnikow.Count - 1)]);

                    if (j == iloscOsobWGrupie - 1)
                    {
                        grupa = grupa.OrderByDescending(x => x.wartoscOsobnika).ToList<Osobnik>();

                        ListaWybranychOsobnikow.Add(grupa[0]);

                        grupa = new List<Osobnik>();
                    }
                }
            }

            return ListaWybranychOsobnikow;
        }

        public List<Osobnik> wyborOsobnikowMetodaTurniejowaRosnaco(int iloscOsobWGrupie)
        {
            var rng = new Random(Guid.NewGuid().GetHashCode());
            List<Osobnik> ListaWybranychOsobnikow = new List<Osobnik>();
            List<Osobnik> grupa = new List<Osobnik>();

            for (int i = 0; i < popul.listaOsobnikow.Count; i++)
            {
                for (int j = 0; j < iloscOsobWGrupie; j++)
                {
                    grupa.Add(popul.listaOsobnikow[rng.Next(popul.listaOsobnikow.Count - 1)]);

                    if (j == iloscOsobWGrupie - 1)
                    {
                        grupa = grupa.OrderBy(x => x.wartoscOsobnika).ToList<Osobnik>();

                        ListaWybranychOsobnikow.Add(grupa[0]);

                        grupa = new List<Osobnik>();
                    }
                }
            }

            return ListaWybranychOsobnikow;
        }

        // KRZYŻOWANIE:

        public void SetOsobnicyDoSparowania(List<Osobnik> osobnicyDoSparowania)
        {
            this.osobnicyDoSparowania = osobnicyDoSparowania;
        }
        public List<Osobnik> wyborOsobnikowDoKrzyzowania()
        {
            List<Osobnik> listaOsobDoWybrania = popul.listaOsobnikow;

            for (int i = 0; i < listaOsobDoWybrania.Count; i++)
            {
                listaOsobDoWybrania[i].id = i;
            }

            var rng = new Random(Guid.NewGuid().GetHashCode());
            List<Osobnik> listaWybranychOsobnikow = new List<Osobnik>();
            double r = 0;
            double prawdopodobienstwo = 0.5;

            for (int i = 0; i < popul.listaOsobnikow.Count; i++)
            {
                r = rng.NextDouble();

                if (r < prawdopodobienstwo)
                {
                    listaWybranychOsobnikow.Add(listaOsobDoWybrania[i]);
                }

            }

            if (listaWybranychOsobnikow.Count % 2 != 0)
            { 
                int index;
                index = rng.Next(0, listaWybranychOsobnikow.Count - 1);
                listaWybranychOsobnikow.RemoveAt(index);
            }

            if (listaWybranychOsobnikow.Count < 2)
            {
               
                listaWybranychOsobnikow = new List<Osobnik>();
            }

            this.osobnicyDoSparowania = listaWybranychOsobnikow;

            return osobnicyDoSparowania;
        }

        public List<Osobnik> stworzPare()
        {
            
            List<Osobnik> para = new List<Osobnik>();
            Osobnik wybranyOsobnik;

            for (int i = 0; i < 2; i++)
            {
                var rng = new Random(Guid.NewGuid().GetHashCode());
                int j;

                if (osobnicyDoSparowania.Count >= 2)
                {
                    j = rng.Next(0, (osobnicyDoSparowania.Count - 1));
                    wybranyOsobnik = osobnicyDoSparowania[j];
                    para.Add(wybranyOsobnik);
                    osobnicyDoSparowania = wyrzucanieOsobnika(wybranyOsobnik, osobnicyDoSparowania);
                }
                else if (osobnicyDoSparowania.Count < 2 && osobnicyDoSparowania.Count > 0)
                {
                    j = 0;
                    wybranyOsobnik = osobnicyDoSparowania[j];
                    para.Add(wybranyOsobnik);
                    osobnicyDoSparowania = wyrzucanieOsobnika(wybranyOsobnik, osobnicyDoSparowania);
                }
                
            }

            this.osobnicyDoSparowania = osobnicyDoSparowania;

            this.para = para;

            return para;
        }
    }
}