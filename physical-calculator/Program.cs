using System.Globalization;
using System.Linq.Expressions;
using NCalc;

class Program
{
    static void Main(string[] args)
    {
        var formulas = new Dictionary<string, Dictionary<string, Formula>>
        {
            ["rychlost"] = new Dictionary<string, Formula> {
                {"okamžitá rychlost", new Formula("s / t", new[]{"s [m]", "t [s]"}, "m/s")}
            },
            ["síla"] = new Dictionary<string, Formula> {
                {"základní vzorec", new Formula("m * a", new[]{"m [kg]", "a [m/s^2]"}, "N")},
                {"gravitační síla", new Formula("m * g", new[]{"m [kg]", "g [9.81]"}, "N")},
                {"vztlaková síla", new Formula("ró * g * V", new[]{"ró [kg*m^3]", "g [9.81]", "V [m^3]"}, "N")},
                {"síla momentu síly", new Formula("M / r", new[]{"M [Nm]", "r [m]"}, "N")},
                {"třecí síla smyková", new Formula("M / r", new[]{"M [Nm]", "r [m]"}, "N")},
                {"tlaková síla 1", new Formula("S * p", new[]{ "S [m^2]", "p [Pa]"}, "N")},
                {"tlaková síla 2", new Formula("S * h * ró * g", new[]{ "S [m^2]", "h [m]", "ró [kg*m^3]", "g [9.81]"}, "N")},
                {"síla elektrického pole", new Formula("E * Q", new[]{ "E [N/C]", "Q [C]"}, "N")},
            },
            ["čas"] = new Dictionary<string, Formula> {
                {"základní vzorec", new Formula("s / v", new[]{"s [m]", "v [m/s]"}, "s")}
            },
            ["rychlost"] = new Dictionary<string, Formula> {
                {"základní vzorec", new Formula("s / t", new[]{"s [m]", "t [s]"}, "m/s")}
            },
            ["tlak"] = new Dictionary<string, Formula> {
                {"základní vzorec", new Formula("F / S", new[]{"F [N]", "S [m^2]"}, "Pa")},
                {"hydrostatický/atmosférický tlak", new Formula("h * ró * g", new[]{"h [m]", "ró [kg*m^3]", "g [9.81]" }, "Pa")},
            },
        };

        var inputMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
                {"rychlost", "rychlost"},
                {"v", "rychlost"},
                {"okamžitá rychlost", "rychlost|okamžitá rychlost"},
                {"okamžitou rychlost", "rychlost|okamžitá rychlost"},
                {"síla", "síla"},
                {"sílu", "síla"},
                {"F", "síla"},
                {"gravitační síla", "síla|gravitační síla"},
                {"gravitační sílu", "síla|gravitační síla"},
                {"vztlaková síla", "síla|vztlaková síla"},
                {"vztlakovou sílu", "síla|vztlaková síla"},
                {"síla momentu síly", "síla|síla momentu síly"},
                {"sílu momentu síly", "síla|síla momentu síly"},
                {"třecí síla smyková", "síla|třecí síla smyková"},
                {"tlaková síla 1", "síla|tlaková síla 1"},
                {"tlakovou sílu 1", "síla|tlaková síla 1"},
                {"tlaková síla 2", "síla|tlaková síla 2"},
                {"tlakovou sílu 2", "síla|tlaková síla 2"},
                {"síla elektrického pole", "síla|síla elektrického pole"},
                {"sílu elektrického pole", "síla|síla elektrického pole"},
                {"čas", "čas"},
                {"tlak", "tlak"},
                {"hydrostatický tlak", "tlak|hydrostatický/atmosférický tlak"},
                {"atmosférický tlak", "tlak|hydrostatický/atmosférický tlak"},
        };

        bool continueCalculating = true;
        while (continueCalculating)
        {
            try
            {
                Console.WriteLine("Jakou fyzikální veličinu chcete vypočítat?");
                string choice = Console.ReadLine().ToLower();

                if (inputMap.TryGetValue(choice, out string formulaPath))
                {
                    var parts = formulaPath.Split('|');
                    var category = parts[0];
                    if (parts.Length == 1) // If no specific formula is indicated, list all available
                    {
                        if (formulas.TryGetValue(category, out var categoryFormulas))
                        {
                            Console.WriteLine("Dostupné vzorce:");
                            int index = 1;
                            foreach (var formula in categoryFormulas)
                            {
                                Console.WriteLine($"{index++}: {formula.Key} - {formula.Value.Expression} ({formula.Value.Unit})");
                            }
                            Console.WriteLine("Který vzorec chcete použít?");
                            int formulaChoice = Convert.ToInt32(Console.ReadLine()) - 1;
                            var selectedFormula = categoryFormulas.Values.ElementAt(formulaChoice);
                            ComputeAndDisplayResult(selectedFormula);
                        }
                    }
                    else // If a specific formula is indicated
                    {
                        var formulaName = parts[1];
                        if (formulas[category].TryGetValue(formulaName, out Formula formula))
                        {
                            ComputeAndDisplayResult(formula);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Pro tuto jednotku v databázi není vzorec.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Nastala chyba: " + ex.Message);
            }

            Console.WriteLine("Chcete vypočítat další vzorec? (ano/ne)");
            continueCalculating = Console.ReadLine().ToLower() == "ano";
        }
    }

    private static void ComputeAndDisplayResult(Formula formula)
    {
        try
        {
            var expression = new NCalc.Expression(formula.Expression);
            Console.WriteLine($"Using the formula: {formula.Expression}");

            foreach (var param in formula.Parameters)
            {
                Console.WriteLine($"Napište hodnotu {param}:");
                string input = Console.ReadLine().Replace(',', '.'); 
                double value = double.Parse(input, CultureInfo.InvariantCulture);
                string paramName = param.Split(' ')[0];
                expression.Parameters[paramName] = value;
            }

            var result = expression.Evaluate();
            Console.WriteLine($"Výsledek je: {result} {formula.Unit}");
        }
        catch (Exception)
        {
            Console.WriteLine("Nastala chyba při výpočtu.");
            throw; 
        }
    }
}

public class Formula
{
    public string Expression { get; }
    public string[] Parameters { get; }
    public string Unit { get; }

    public Formula(string expression, string[] parameters, string unit)
    {
        Expression = expression;
        Parameters = parameters;
        Unit = unit;
    }
}