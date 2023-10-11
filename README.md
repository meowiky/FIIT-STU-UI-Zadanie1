# UI Zadanie 1 c
## Prehľadávanie stavového priestoru 8-Hlavolam, Obojstranny Algoritmus
[Link na Originalne Zadanie](http://www2.fiit.stuba.sk/~kapustik/z2d.html#C)

> ## How To: Spustenie a Pouzivanie programu
> Tento program bol napisany v programovaciom jazyku C#. Da sa spustit jeho [.exe](bin/Debug/net7.0/UI_Zadanie1_c_Bukovska.exe) Pozor, ak sa spusta cez .exe musi sa nachadzat v directori aj s ostatnymi subormi z [Debug/net7.0](bin/Debug/net7.0/) alebo spustit vo [Visual Studio 2022](https://visualstudio.microsoft.com/), ktore je dostupne aj pre Mac. Ak chcete otvorit tento projekt vo VS2022, staci mat nainstalovany VS2022 a stlacit na subor [.sln](UI_Zadanie1_c_Bukovska.sln) a VS2022 sa automaticky otvori nastavene s tymto projektom.
> V [Program.cs](Program.cs) sa nachadza main funkcia, kde sa da zmenit path k vstupnemu a vystupnemu suboru a maximalny pocet iteracii programu.
> Automaticky su obe paths nastavene na `input.txt` a `output.txt`. Tieto dva subory sa na ukazku tiez nachadzaju v [Debug/net7.0](bin/Debug/net7.0/),
>  teda [input file](bin/Debug/net7.0/input.txt) a [output file](bin/Debug/net7.0/output.txt).
> Pre spravne fungovanie programu subor `input.txt` alebo subor definovany v path v main musi existovat a mat spravnu syntax.
> ### Ukazka `Input.txt`
>```
>3*4
>1 2 3 4 5 6 7 8 9 10 11 0
>1 2 3 4 5 10 6 7 9 0 11 8
>```
> Na prvom riadku sa musia nachadzat dve cisla oddelene `*`. Prve cislo je pocet riadkov a druhe pocet stlpcov.
> Na druhom riadku sa nachadza po riadkoch obsah zaciatocneho stavu hlavolamu. Cisla musia byt oddelene ` ` teda medzerou. Pre spravne fungovanie programu sa tu musi nachadzat spravny pocet cisiel.
> Na tretom riadku su cisla finalneho stavu hlavolamu, ku ktoremu sa chcete dostat zo zaciatocneho stavu. Cislo `0` znazornuje medzeru.
> ### Ukazka `Output.txt`
> ```
> Down, Right, Right, Up, Left
> ```
> ```
> After 20 steps, the program still hasn't found the right steps
> ```
> Vystup je bud postupnost spravnych pohybov alebo vypis, ze po x krokoch (podla toho ako to je v maine nastavene) program cestu nenasiel. Pre niektore vstupy neexistuje postupnost spravnych krokov.
> ### Vysvetlenie pohybu
> `Down` znamena posun cisla nad medzerou smerom dole.
> `Up` znamena posun cisla pod medzerou smerom hore.
> `Left` znamena posun cisla napravo od medzery do lava.
> `Right` znamena posun cisla nalavo od medzery do prava.

## Popis a vysvetlenie kodu
### Funkcia Main
Funckia main sa nachadza v [Program.cs](Program.cs), kde sa v try catch spustí statická metóda [Run](#funkcia-run) statickej classy [AppFlow](AppFlow.cs). Try catch využívam na zachytenie prípadných výnimiek a chýb a ich vypísanie do konzoly.
```C#
static void Main(string[] args)
  {
      try
      {
          int maxSteps = 10;
          AppFlow.Run("input.txt", "output.txt", maxSteps);
      }
      catch (Exception e)
      {
          Console.WriteLine("An error occurred;  " + e.Message);
      }
      
  }
```
`int maxSteps` je počet maximálne povolených iterácií. Pri čísle maxSteps = 10 sa vykoná 20 posunov, 10 spredu a 10 zozadu.
### Funkcia Run()
