using System.Data;
using System.Linq;
using System.Linq.Expressions;


namespace String_to_Expression
{
	internal class StringToExpression
	{

		static void Main(string[] args)
		{
			//Put down values to be replaced
			int length = 4;
			int width = 2;
			int height = 3;

			//Formula to replace
			string additionFormula = "length * length * height";


			//Create a dictionary containing variables to replace with
			//The foreach loop will check if the formula has the keys, then the keys will be replaced with the value
			Dictionary<string, string> variablesToReplaceWith = new Dictionary<string, string>
			{
				{ "length", length.ToString() },
				{ "width", width.ToString() },
				{ "height", height.ToString() }
			};

			//Place the formula into a string to be used
			string resultingString = additionFormula;
			
			//Converts the new formula string into an actual mathematical expression and returns a result		
			//double result = Convert.ToDouble(new DataTable().Compute(resultingString, null));

			//Prints the result
			//Console.WriteLine(resultingString + " = " + result);

			Dictionary<string, string> sampleFormulaDictionary = new Dictionary<string, string>() 
			{
				{ "Length by Length", "length * length" },
				{ "Width by Width", "width * width" },
				{ "Length by Width by Height", "length * width * height" }
			};
			Dictionary<string, string> output = StringVariableReplacer(variablesToReplaceWith, sampleFormulaDictionary);

			
			Console.WriteLine("After");
			foreach (var keyValuePair in output){
				Console.WriteLine(keyValuePair.Key + " / " + keyValuePair.Value);
			}
		}

		
		public static Dictionary<string,string> StringVariableReplacer(Dictionary<string, string> variablesToReplaceWith, Dictionary<string, string> FormulaDictionary)
		{
			Dictionary<string, string> newFormulaDictionary = new Dictionary<string, string>();
			foreach (var formula in FormulaDictionary)
			{

				foreach (var s in variablesToReplaceWith)
				{
					String newFormula = "";
					//Checks if the variable is in the formula
					if (formula.Value.IndexOf(s.Key) != -1)
					{
						//Replaces the variable with an actual value
						try 
						{
							Console.WriteLine("{0} found in {1}", s.Key, formula.Value);
							String newFormula = formula.Value.Replace(s.Key, variablesToReplaceWith[s.Key]);
							if (newFormulaDictionary.ContainsKey(formula.Key))
							{
								newFormulaDictionary[formula.Key] = newFormula;
							}
							else
							{
								newFormulaDictionary.Add(formula.Key, newFormula);
							}
						}
						catch (IndexOutOfRangeException e) { Console.WriteLine(e.Message + "\n" + formula.Key); }
					}

				}
			}
			return newFormulaDictionary;
		}

		
	}
}