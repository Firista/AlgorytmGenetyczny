using System;
using System.Collections.Generic;

namespace MamNadziejeOstatniaWersjaAlgorytmuGenetycznego
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> a, b, d;
            a = new List<double>
            {
                -4,-3,-2
            };
            b = new List<double>
            {
                4,3,2
            };
            d = new List<double>
            {
                3,5,7
            };

            Populacja test = new Populacja(a, b, d);

            test.generujPopulacje(10);
            test.policzSredniaWartoscPopulacji();

            foreach (var result in test.listaOsobnikow)
                Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine("Srednia wartosc pierwotnej populacji to: " + test.sredniaWartosciPokolenia);


            Console.WriteLine();
            Console.WriteLine("Wybierz selekcje: \n 1. Sukcesja elitarna. \n 2. Sukcesja losowa. \n 3. Sukcesja z usuwaniem podobnych. \n 4. Sukcesja z częściowym zastępowaniem. \n 5. Sukcesja z całkowitym zastępowaniem.");
            string numer = Console.ReadLine();
            int numerSukcesji = 0;
            if (Int32.TryParse(numer, out numerSukcesji)) ;

            switch (numerSukcesji)
            {
                case 1:
                    {
                        // ________SUKCESJA ELITARNA:________  //

                        SukcesjaElitarna Elita = new SukcesjaElitarna(test);

                        for (int i = 0; i < 50; i++)
                        {

                            Elita.zmutujOsobnikow();
                            Elita.stworzOsobnikowPoInwersji();
                            Elita.wylosujPotencjalnychRodzicow();
                            Elita.krzyzujOsobnikow();
                            Elita.zbierzWszystkichOsobnikow();
                            Elita.usunTakichSamychOsobnikow();

                            //tu można wybrać sposób sortowania osobników:

                            //Elita.posortujWszystkichOsobnikowRosnaco();
                            Elita.posortujWszystkichOsobnikowMalejaco();
                            Elita.usunTakichSamychOsobnikow();

                            //tu można wybrać sposób sortowania osobników:

                            //Elita.posortujWszystkichOsobnikowRosnaco();
                            Elita.posortujWszystkichOsobnikowMalejaco();
                            Elita.wybierzNastepnePokolenie();
                            Elita.populacja.policzSredniaWartoscPopulacji();

                            if (((i + 1) % 10) == 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Srednia wartosc populacji po {0} epokach to: {1}", (i + 1), Elita.populacja.sredniaWartosciPokolenia);
                            }
                            
                        }
                        /*
                        Console.WriteLine();
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine("Populacja po przeprowadzeniu sukcesji elitarnej:");
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine();

                        for (int j = 0; j < Elita.populacja.listaOsobnikow.Count; j++)
                        {
                            Console.WriteLine(Elita.populacja.listaOsobnikow[j]);
                        }*/
                    }
                    break;
                case 2:
                    {
                        // ________SUKCESJA LOSOWA:________  //

                        SukcesjaLosowa Losowa = new SukcesjaLosowa(test);

                        for (int i = 0; i < 50; i++)
                        {

                            Losowa.zmutujOsobnikow();
                            Losowa.stworzOsobnikowPoInwersji();
                            Losowa.wylosujPotencjalnychRodzicow();
                            Losowa.krzyzujOsobnikow();
                            Losowa.zbierzWszystkichOsobnikow();
                            Losowa.usunTakichSamychOsobnikow();

                            //tu można wybrać sposób sortowania osobników:

                            //Losowa.posortujWszystkichOsobnikowRosnaco();
                            Losowa.posortujWszystkichOsobnikowMalejaco();
                            Losowa.usunTakichSamychOsobnikow();

                            //tu można wybrać sposób sortowania osobników:

                            //Losowa.posortujWszystkichOsobnikowRosnaco();
                            Losowa.posortujWszystkichOsobnikowMalejaco();
                            Losowa.usunTakichSamychOsobnikow();
                            Losowa.posortujWszystkichOsobnikowMalejaco();
                            Losowa.wybierzNastepnePokolenie();
                            Losowa.populacja.policzSredniaWartoscPopulacji();

                            if (((i + 1) % 10) == 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Srednia wartosc populacji po {0} epokach to: {1}", (i + 1), Losowa.populacja.sredniaWartosciPokolenia);
                            }
                            
                        }
                        /*
                        Console.WriteLine();
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine("Populacja po przeprowadzeniu sukcesji losowej:");
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine();

                        for (int j = 0; j < Losowa.populacja.listaOsobnikow.Count; j++)
                        {
                            Console.WriteLine(Losowa.populacja.listaOsobnikow[j]);
                        }*/
                    }
                    break;
                case 3:
                    {
                        // ________SUKCESJA Z USUWANIEM PODOBNYCH:________  //

                        SukcesjaUsuwaniePodobnych Niepodobni = new SukcesjaUsuwaniePodobnych(test);

                        for (int i = 0; i < 50; i++)
                        {

                            Niepodobni.zmutujOsobnikow();
                            Niepodobni.stworzOsobnikowPoInwersji();
                            Niepodobni.wylosujPotencjalnychRodzicow();
                            Niepodobni.krzyzujOsobnikow();
                            Niepodobni.zbierzWszystkichOsobnikow();
                            Niepodobni.usunTakichSamychOsobnikow();

                            //tu można wybrać sposób sortowania osobników:

                            //Niepodobni.posortujWszystkichOsobnikowRosnaco();
                            Niepodobni.posortujWszystkichOsobnikowMalejaco();
                            Niepodobni.usunTakichSamychOsobnikow();

                            //tu można wybrać sposób sortowania osobników:

                            //Niepodobni.posortujWszystkichOsobnikowRosnaco();
                            Niepodobni.posortujWszystkichOsobnikowMalejaco();
                            Niepodobni.wybierzNastepnePokolenie();
                            Niepodobni.populacja.policzSredniaWartoscPopulacji();

                            if (((i + 1) % 10) == 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Srednia wartosc populacji po {0} epokach to: {1}", (i + 1), Niepodobni.populacja.sredniaWartosciPokolenia);
                            }
                            
                        }
                        /*
                        Console.WriteLine();
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine("Populacja po przeprowadzeniu sukcesji z usuwaniem podobnych osobnikow:");
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine();

                        for (int j = 0; j < Niepodobni.populacja.listaOsobnikow.Count; j++)
                        {
                            Console.WriteLine(Niepodobni.populacja.listaOsobnikow[j]);
                        }*/
                    }
                    break;
                case 4:
                    {
                        // ________SUKCESJA Z CZĘŚCIOWĄ WYMINĄ:________  //

                        SukcesjaZCzesciowaWymiana Czesc = new SukcesjaZCzesciowaWymiana(test);

                        for (int i = 0; i < 50; i++)
                        {

                            Czesc.zmutujOsobnikow();
                            Czesc.stworzOsobnikowPoInwersji();
                            Czesc.wylosujPotencjalnychRodzicow();
                            Czesc.krzyzujOsobnikow();
                            Czesc.zbierzWszystkichOsobnikow();
                            Czesc.usunTakichSamychOsobnikow();

                            //tu można wybrać sposób sortowania osobników:

                            //Czesc.posortujWszystkichOsobnikowRosnaco();
                            Czesc.posortujWszystkichOsobnikowMalejaco();
                            Czesc.usunTakichSamychOsobnikow();

                            //tu można wybrać sposób sortowania osobników:

                            //Czesc.posortujWszystkichOsobnikowRosnaco();
                            //Czesc.posortujWszystkichOsobnikowRosnaco();
                            Czesc.posortujWszystkichOsobnikowMalejaco();

                            //tu można wybrać sposób sortowania osobników:

                            //Czesc.posortujOsobnikowPoczatkowychRosnaco();
                            Czesc.posortujOsobnikowPoczatkowychMalejaco();
                            Czesc.wybierzNastepnePokolenie();
                            Czesc.populacja.policzSredniaWartoscPopulacji();

                            if (((i + 1) % 10) == 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Srednia wartosc populacji po {0} epokach to: {1}", (i + 1), Czesc.populacja.sredniaWartosciPokolenia);
                            }
                            
                        }
                        /*
                        Console.WriteLine();
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine("Populacja po przeprowadzeniu sukcesji z czesciowa wymiana:");
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine();

                        for (int i = 0; i < Czesc.populacja.listaOsobnikow.Count; i++)
                        {
                            Console.WriteLine(Czesc.populacja.listaOsobnikow[i]);
                        }*/
                    }
                    break;
                case 5:
                    {
                        // ________SUKCESJA Z CAŁKOWITYM ZASTĘPOWANIEM:________  //

                        SukcesjaZCalkowitymZastepowaniem Calosc = new SukcesjaZCalkowitymZastepowaniem(test);

                        for (int i = 0; i < 50; i++)
                        {

                            Calosc.zmutujOsobnikow();
                            Calosc.stworzOsobnikowPoInwersji();
                            Calosc.wylosujPotencjalnychRodzicow();
                            Calosc.krzyzujOsobnikow();
                            Calosc.wybierzNastepnePokolenie();
                            Calosc.populacja.policzSredniaWartoscPopulacji();

                            if (((i + 1) % 10) == 0)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Srednia wartosc populacji po {0} epokach to: {1}", (i + 1), Calosc.populacja.sredniaWartosciPokolenia);
                            }
                            
                        }
                        /*
                        Console.WriteLine();
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine("Populacja po przeprowadzeniu sukcesji z całkowitym zastępowaniem:");
                        Console.WriteLine("---------*--------*--------*--------*---------*--------*--------*--------*---------");
                        Console.WriteLine();

                        for (int i = 0; i < Calosc.populacja.listaOsobnikow.Count; i++)
                        {
                            Console.WriteLine(Calosc.populacja.listaOsobnikow[i]);
                        }*/
                    }
                    break;
                default:
                    Console.WriteLine("Nie wybrano żadnej opcji z listy.");
                    break;
            }

            Console.ReadKey();
        }
    }
}