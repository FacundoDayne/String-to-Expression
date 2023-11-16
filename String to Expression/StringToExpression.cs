using System.Collections;
using System.Data;
using System.Linq;
using System.Linq.Expressions;


namespace String_to_Expression
{
	internal class StringToExpression
	{

		static void Main(string[] args)
		{
			StringToExpressionGenerator();
		}

		static void StringToExpressionGenerator()
		{
			ArrayList primedVariables = variablePrimer();
			Dictionary<string, string> variablesToReplaceWith = (Dictionary<string, string>)primedVariables[0];
			Dictionary<string, string> formulaDictionary = (Dictionary<string, string>)primedVariables[1];
			Dictionary<string, string> completedFormulaDictionary = ExpressionConverter(StringVariableReplacer(variablesToReplaceWith, formulaDictionary));
			foreach(var s in completedFormulaDictionary) 
			{
				Console.WriteLine(s.Key + " / " + s.Value);
			}
		}

		static ArrayList variablePrimer()
		{
			Dictionary<string, string> sampleVariablesToReplaceWith = new Dictionary<string, string>
			{
				{ "length", "4" },
				{ "width", "20" },
				{ "height", "6" }
			};
			Dictionary<string, string> sampleFormulaDictionary = new Dictionary<string, string>()
			{
				{ "Length by Length", "length * length" },
				{ "Width by Width", "width * width" },
				{ "Length by Width by Height", "length * width * height" }
			};

			return new ArrayList() { sampleVariablesToReplaceWith, sampleFormulaDictionary };
		}
	
		static Dictionary<string,string> StringVariableReplacer(Dictionary<string, string> variablesToReplaceWith, Dictionary<string, string> FormulaDictionary)
		{
			Dictionary<string, string> newFormulaDictionary = new Dictionary<string, string>();
			foreach (var formula in FormulaDictionary)
			{
				String newFormula = formula.Value;
				foreach (var s in variablesToReplaceWith)
				{
					if (newFormula.IndexOf(s.Key) != -1)
					{
						try 
						{
							newFormula = newFormula.Replace(s.Key, variablesToReplaceWith[s.Key]);	
						}
						catch (IndexOutOfRangeException e) { Console.WriteLine(e.Message + "\n" + formula.Key); }
					}
					if (newFormulaDictionary.ContainsKey(formula.Key)) newFormulaDictionary[formula.Key] = newFormula;
					else newFormulaDictionary.Add(formula.Key, newFormula);

				}
			}
			return newFormulaDictionary;
		}

		static Dictionary<string, string> ExpressionConverter(Dictionary<string, string> newFormulaDictionary)
		{
			Dictionary<string, string> completedValueDictionary = new Dictionary<string, string>();
			foreach(var formula in newFormulaDictionary)
			{
				String result = (Convert.ToDouble(new DataTable().Compute(formula.Value, null))).ToString();
				completedValueDictionary.Add(formula.Key, result);
			}
			return completedValueDictionary;
		}
	}
}