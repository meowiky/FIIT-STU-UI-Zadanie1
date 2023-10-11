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
> ```
> No moves needed
> ```
> Vystup je bud postupnost spravnych pohybov alebo vypis, ze po x krokoch (podla toho ako to je v maine nastavene) program cestu nenasiel. Moznost je aj ze zaciatocny a konecny stav sa rovnaju. Pre niektore vstupy neexistuje postupnost spravnych krokov.
> ### Vysvetlenie pohybu
> `Down` znamena posun cisla nad medzerou smerom dole.
> `Up` znamena posun cisla pod medzerou smerom hore.
> `Left` znamena posun cisla napravo od medzery do lava.
> `Right` znamena posun cisla nalavo od medzery do prava.

## Popis a vysvetlenie kodu
### Funkcia Main
Funckia main sa nachadza v [Program.cs](Program.cs), kde sa v try catch spustí statická metóda [Run](#run) statickej classy [AppFlow](AppFlow.cs). Try catch využívam na zachytenie prípadných výnimiek a chýb a ich vypísanie do konzoly.
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

### Run
Tato staticka metoda sa nachadza v [AppFlow](AppFlow.cs) a vola v [Maine](#funkcia-main). Prijma 3 parametre, path k vstupnemu a vystupnemu suboru a počet maximálne povolených iterácií. Pri čísle maxSteps = 10 sa vykoná 20 posunov, 10 spredu a 10 zozadu.
```C#
public static void Run(string inputFile, string outputFile, int maxSteps){}
```
Z metody [GetInputFromTXT](#GetInputFromTXT) ziskam instancie zaciatocnej a finalnej classy [PuzzleNode](#PuzzleNode).
```C#
var (startingNode, finalNode) = GetInputFromTXT(inputFile);
```
V tejto casti kodu volam metodu [IsEqualState](#IsEqualState) classy [PuzzleNode](#PuzzleNode). Ak vrati true to znamena, ze zaciatocny a konecny stav sa rovnaju, takze netreba ziaden posun a program moze skoncit. Teda zisti, ci vystupny subor existuje, ak hej ho vymaze a nasledne vytvori a zapise.
```C#
if (startingNode.IsEqualState(finalNode))
  {
      if (File.Exists(outputFile))
          File.Delete(outputFile);
      using (StreamWriter writer = new StreamWriter(outputFile))
      {
          writer.WriteLine("No moves needed");
      }
  }
```
Vytvorim si 4 variables pre Datovu strukturu [List<data type>](https://learn.microsoft.com/en-us/dotnet/api/system.collections.generic.list-1?view=net-7.0) pre classu [PuzzleNode](#PuzzleNode). Budem prehladavat od zaciatku a od konca. Pre kazde prehladvanie budem mat dva Listy, listy rodicov a listy deti. Na zaciatku si do rodicov pridam zaciatocnu a konecnu [PuzzleNode](#PuzzleNode).
```C#
List<PuzzleNode> parentsFromStart = new List<PuzzleNode>();
List<PuzzleNode> childrenfromStart = new List<PuzzleNode>();

List<PuzzleNode> parentsFromEnd = new List<PuzzleNode>();
List<PuzzleNode> childrenfromEnd = new List<PuzzleNode>();

parentsFromStart.Add(startingNode);
parentsFromEnd.Add(finalNode);
```
Dalsia cela cast sa nachadza v tomto for loope, ktory sluzi nato aby program sa nezacyklil a nebezal do nekonecna, kedze niekedy neexistuje spravna postupnost krokov. 
```C#
for (int i = 0; i < maxSteps; i++)
```
V prvej casti loopu si pre kazdeho rodica nachadzajuceho sa v liste pre rodicov (zo zaciatku aj z konca) zavolam funkciu [GetPossibleMoves](#GetPossibleMoves) ktora mi vrati list class [MoveEnum](#MoveEnum). Toto je vlastne list vsetkych moznych pohybov pre danu [PuzzleNode](#PuzzleNode). Pre kazdy mozny pohyb v tom liste pridam novu [PuzzleNode](#PuzzleNode) dietata do listu deti vdaka funkcii [GetNewState](#GetNewState), ktora sa zavola na aktualnu instanciu parenta a vrati novo vytvorenu instanciu stavu po danom pohybe. Toto sa stane pre rodicov zo zaciatku aj z konca.
```C#
foreach (PuzzleNode node in parentsFromStart)
{
    List<MoveEnum> possibleMoves = node.GetPossibleMoves();
    foreach (MoveEnum move in possibleMoves)
    {
        childrenfromStart.Add(node.GetNewState(move));
    }
}

foreach (PuzzleNode node in parentsFromEnd)
{
    List<MoveEnum> possibleMoves = node.GetPossibleMoves();
    foreach (MoveEnum move in possibleMoves)
    {
        childrenfromEnd.Add(node.GetNewState(move));
    }
}
```
V tejto casti for loopu idem porovnavat [PuzzleNodes](#PuzzleNode) medzi sebou. Pre kazdu Nodu dietata zo zaciatku zavolam kazdu Nodu dietata z konca a pomocou funkcie [IsEqualState](#IsEqualState) zistim, ci sa ich stavy rovnaju. Ak sa ich stavy rovnaju zavolam funkciu [ReturnFoundResult](#ReturnFoundResult) a tuto ukoncim. Ak sa ich stavy nerovnaju tento loop pokracuje a porovnava Nody parentov zo zadu s kazdou Nodou dietata zo zaciatku. Dovod preco musim porovnavat aj deti s parentami je vysvetleny na obrazku pod ukazkou kodu.
```C#
foreach (PuzzleNode nodeA in childrenfromStart)
{
    foreach (PuzzleNode nodeB in childrenfromEnd)
    {
        if (nodeA.IsEqualState(nodeB))
        {
            ReturnFoundResult(nodeA, nodeB, outputFile);
            return;
        }
    }
    foreach (PuzzleNode nodeC in parentsFromEnd)
    {
        if (nodeA.IsEqualState(nodeC))
        {
            ReturnFoundResult(nodeA, nodeC, outputFile);
            return;
        }
    }
}
```
Obrazok pre vysvetlenie preco treba deti porovnavat aj s rodicmi. Ak sa rovna stav dietata a rodica bude neparny pocet posunov.
![UIneparny](https://github.com/meowiky/FIIT-STU-UI-Zadanie1/assets/91073373/ec43e9f1-64ba-4c5e-8e0e-b4760b1c84f6)
Obrazok pre parny pocet posunov, teda pripad, ze rovnaky stav sa najde medzi detmi.
![UIparny](https://github.com/meowiky/FIIT-STU-UI-Zadanie1/assets/91073373/1459f075-411d-45f3-9fb2-65b5746f7677)

### GetInputFromTXT

### PuzzleNode

### MoveEnum

### ReturnFoundResult

### GetPossibleMoves

### GetNewState

### IsEqualState


