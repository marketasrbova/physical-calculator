**Projekt PVA – kalkulačka fyzikálních vzorců
Markéta Srbová**

**Popis projektu**

	Cílem tohoto projektu je vytvořit variabilní kalkulačku fyzikálních vzorců. Program se uživatele ptá, jakou fyzikální veličinu chce vypočíst a následně vypíše všechny vzorce pro danou veličinu, které má nahrané v databázi (pokud uživatel předem nespecifikuje, který vzorec chce použít). Poté, co si uživatel vybere jeden ze vzorců, se program zeptá na hodnotu veličin nezbytných pro výpočet a po jejich zadání vypíše výsledek. 

**Realizace**

	Pro začátek je třeba vytvořit databázi vzorců. V programu jsou vzorce uloženy ve struktuře tak, že je definována veličina (př. síla) a její „podveličiny“ (př. vztlaková síla). Každý vzorec je definován pomocí třídy Formula, která obsahuje vzorec k výpočtu veličiny, parametry veličin potřebných pro výpočet a jednotky výsledku. Pro zmenšení chybovosti používá program slovník inputMap, který ukládá různé možnosti názvů veličiny, které může uživatel zadat tak, aby názvy pasovaly na příslušné klíče použité v třídě Formula (př. místo síla odpoví uživatel sílu/F).
	V hlavní části programu se program nejdříve zeptá uživatele na to, jakou jednotku chce vypočítat. Na základě vstupu program vyhledá pasující vzorec. Pokud je nalezených vzorců více, uživatel si může vybrat, který chce použít. Program následně vypíše uživateli žádost pro doplnění hodnot veličin nutných pro výpočet cílové veličiny pomocí vybraného vzorce, hodnotu cílové veličiny spočítá a vypíše. Pro vyhodnocování matematických výrazů program využívá knihovnu NCalc, která umožňuje dynamické vyhodnocení stringů obsahujících matematické operace. To zjednodušuje práci s různými vzorci a jejich parametry.
	Program se snaží co nejvíce omezit chybovost, proto pokud nastane situace, kterou program nezvládne vyřešit, vypíše Nastala chyba a zeptá se, zdali chce uživatel provést nový výpočet. Také ignoruje velká a malá písmena a desetinnou tečku a čárku.

**Zlepšení**

	Zaprvé, program ignoruje velká a malá písmena, což se ve většině částí programu chtěné, ale třeba při zadávání veličiny, kterou chce uživatel vypočítat ve formě její značky (př. pro sílu je to F) to chtěné není. Většinou to nezpůsobí žádné problémy, ale kupříkladu u momentu síly (značka M) a váhy (značka m) by mohlo. To stejné s veličinami, které mají stejné jednotky, kupříkladu teplota a čas (značka t). Tam by bylo potřeba aby se program doptal, kterou přesně veličinu má uživatel na mysli. Další způsob, kterým se dá program zlepšovat je rozšiřování databáze vzorců. Nadále by se z konzolové aplikace dala udělat webová, což by rozhodně bylo vstřícnější k uživateli. Nakonec by nebylo úplně špatné zapojit knihovnu pro řeckou abecedu, díky níž by bylo možné psát značky pro některé veličiny (př. u hustoty se momentálně místo ρ vypisuje ró).
