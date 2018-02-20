using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculatorGaneSolver
{
	using Button = Program.CalculatorGameSolver.Button;
	using InputBox = Program.CalculatorGameSolver.InputBox;

	class Program
	{
		static void Main(string[] args)
		{
			//Console.Write(CalculatorGameSolver.Portal(null, 964, new object[] { 2, 1 }));
			while(true)	Interface.Run();
		}
		public class Interface
		{
			public static bool ReadInt(out int number)
			{
				return int.TryParse(Console.ReadLine(), out number);
			}
			public static bool ReadInts(out int[] numbers, char split = ' ')
			{
				string[] strings = Console.ReadLine().Split(split);
				numbers = new int[strings.Count()];
				bool isSuccessful = true;
				for (int i = 0; i < strings.Count(); i++)
				{
					isSuccessful &= int.TryParse(strings[i], out numbers[i]);
				}
				return isSuccessful;
			}
			public static void Run()
			{
				Console.WindowHeight = 50;
				Console.Clear();
				int goal, moveTime, origin;
				Console.Write("目标数字：");;
				while (!ReadInt(out goal)) Console.Write("格式错误！");
				Console.Write("移动步数：");
				while (!ReadInt(out moveTime)) Console.Write("格式错误！");
				Console.Write("初始数字：");
				while (!ReadInt(out origin)) Console.Write("格式错误！");
				InputBox inputBox = InputInputBox();
				List<Button> buttons = InputButton();
				CalculatorGameSolver solver = new CalculatorGameSolver(goal, moveTime, origin, buttons, inputBox);
				List<Pair<string, int>> resule = solver.SolvePuzzle();
				Console.WriteLine("\n答案：");
				foreach (var item in resule)
				{
					Console.WriteLine($" {item.Key,-9}{item.Value}");
				}
				Console.ReadKey();
			}

			private static InputBox InputInputBox()
			{
				PrintInputBoxCodeInfo();
				Console.WriteLine("旋转输入框种类：");
				int type;
				ReadInt(out type);
				switch (type)
				{
					case 1:
						{
							Console.Write("参数1，入口：");
							int arg1;
							while (!ReadInt(out arg1)) Console.Write("格式错误！\n参数1，入口：");
							Console.Write("参数2，出口：");
							int arg2;
							while (!ReadInt(out arg2)) Console.Write("格式错误！\n参数2，出口：");
							return new InputBox(InputBox.InputBoxType.Portal, arg1, arg2);
						}
					default:
						return new InputBox(InputBox.InputBoxType.Default);
				}
			}
			private static void PrintInputBoxCodeInfo()
			{
				Console.Write(@"输入框类型：
  Default     0
  Portal      1
");
			}

			private static List<Button> InputButton()
			{
				PrintButtonCodeInfo();
				Console.WriteLine("输入按钮代码，以0撤销，以非数字字符结束：");
				List<Button> buttons = new List<Button>();
				Console.Write("第{0}个按钮：", buttons.Count() + 1);
				int buttonCode;
				while(ReadInt(out buttonCode))
				{
					switch(buttonCode)
					{
						case 0:
							{
								buttons.RemoveAt(buttons.Count() - 1);
								Console.WriteLine("已撤销。");
								break;
							}
						case 1:
							{
								Console.Write("加号。\n参数1，加数：");
								int arg;
								while (!ReadInt(out arg)) Console.Write("格式错误！\n参数1，加数：");
								buttons.Add(new Button(Button.ButtonType.Plus, arg));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 2:
							{
								Console.Write("减号。\n参数1，减数：");
								int arg;
								while (!ReadInt(out arg)) Console.Write("格式错误！\n参数1，减数：");
								buttons.Add(new Button(Button.ButtonType.Minus, arg));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 3:
							{
								Console.Write("乘号。\n参数1，乘数：");
								int arg;
								while (!ReadInt(out arg)) Console.Write("格式错误！\n参数1，乘数：");
								buttons.Add(new Button(Button.ButtonType.Multiply, arg));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 4:
							{
								Console.Write("除号。\n参数1，除数：");
								int arg;
								while (!ReadInt(out arg)) Console.Write("格式错误！\n参数1，除数：");
								buttons.Add(new Button(Button.ButtonType.Division, arg));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 5:
							{
								Console.Write("去尾。\n");
								buttons.Add(new Button(Button.ButtonType.Cut));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 6:
							{
								Console.Write("反转。\n");
								buttons.Add(new Button(Button.ButtonType.Reverse));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 7:
							{
								Console.Write("求和。\n");
								buttons.Add(new Button(Button.ButtonType.Sum));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 8:
							{
								Console.Write("变化。\n参数1，原字符串：");
								string arg1 = Console.ReadLine();
								while (!int.TryParse(arg1, out int tmp))
								{
									Console.Write("格式错误！\n参数1，原字符串：");
									arg1 = Console.ReadLine();
								}
								Console.Write("参数2，新字符串：");
								string arg2 = Console.ReadLine();
								while (!int.TryParse(arg2, out int tmp))
								{
									Console.Write("格式错误！\n参数2，新字符串：");
									arg2 = Console.ReadLine();
								}
								buttons.Add(new Button(Button.ButtonType.Change, arg1, arg2));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 9:
							{
								Console.Write("左移。\n");
								buttons.Add(new Button(Button.ButtonType.LeftShift));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 10:
							{
								Console.Write("右移。\n");
								buttons.Add(new Button(Button.ButtonType.RightShift));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 11:
							{
								Console.Write("镜像。\n");
								buttons.Add(new Button(Button.ButtonType.Mirror));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 12:
							{
								Console.Write("添尾。\n参数1，添加的数：");
								int arg;
								while (!ReadInt(out arg)) Console.Write("格式错误！\n参数1，添加的数：");
								buttons.Add(new Button(Button.ButtonType.Append, arg));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 13:
							{
								Console.Write("储存。\n");
								buttons.Add(new Button(Button.ButtonType.Store));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 14:
							{
								Console.Write("按钮增减。\n参数1，增量：");
								int arg1;
								while (!ReadInt(out arg1)) Console.Write("格式错误！\n参数1，加数：");
								Console.Write("参数2，增减按钮的编号，以非数字字符结束：\n");
								List<Button> arg2=new List<Button>();
								while (ReadInt(out int number))
								{
									if (number <= 0 || number > buttons.Count())
									{
										Console.WriteLine("编号超出范围！");
									}
									else
									{
										arg2.Add(buttons[number - 1]);
									}
								}
								buttons.Add(new Button(Button.ButtonType.Inc, arg1, arg2));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						case 15:
							{
								Console.Write("补十。\n");
								buttons.Add(new Button(Button.ButtonType.Inv10));
								Console.WriteLine(buttons.Last().DictionaryKey);
								break;
							}
						default:
							{
								Console.WriteLine("按钮代码{0}不存在！", buttonCode);
								break;
							}
					}
					Console.Write("第{0}个按钮：", buttons.Count() + 1);
				}
				return buttons;
			}
			private static void PrintButtonCodeInfo()
			{
				Console.WriteLine(@"各按钮于其对应的代码：
  Plus        1
  Minus       2
  Multiply    3
  Division    4
  Cut         5
  Reverse     6
  Sum         7
  Change      8
  LeftShift   9
  RightShift 10
  Mirror     11
  Append     12
  Store      13
  Inc        14
  Inv10      15
");
			}
		}
		public class CalculatorGameSolver
		{
			private int goal;
			private int moveTime;
			private int origin;
			private List<Button> buttons;
			private InputBox inputBox;

			public int Goal
			{
				get { return goal; }
				private set { goal = value; }
			}
			public int MoveTime
			{
				get { return moveTime; }
				private set
				{
					if (value <= 0) throw new ArgumentOutOfRangeException("Move time should be larger than 0.");
					moveTime = value;
				}
			}
			public int Origin
			{
				get { return origin; }
				private set { origin = value; }
			}
			private string ButtonKey
			{
				get
				{
					StringBuilder result = new StringBuilder(buttons.Count * 10);
					foreach (var button in buttons)
					{
						result.Append(button.DictionaryKey);
					}
					return result.ToString();
				}
			}
			public CalculatorGameSolver(int goal, int moveTime, int origin, List<Button> buttons, InputBox inputBox)
			{
				NewPuzzle(goal, moveTime, origin, buttons,inputBox);
			}
			public void NewPuzzle(int goal, int moveTime, int origin, List<Button> buttons,InputBox inputBox)
			{
				Goal = goal;
				MoveTime = moveTime;
				Origin = origin;
				this.buttons = buttons;
				this.inputBox = inputBox;
			}		
			public List<Pair<string, int>> SolvePuzzle()
			{
				Dictionary<string, int> hashTable = new Dictionary<string, int>();
				List<Pair<string, int>> result = new List<Pair<string, int>>();
				RecursiveSolvePuzzle(origin, 0, result, hashTable);
				result.Reverse();
				return RefineResult(result);
			}
			private bool RecursiveSolvePuzzle(int value, int depth, List<Pair<string, int>> result, Dictionary<string, int> hashTable)
			{
				bool hasFind = false;
				if (value == Goal) return true;
				if (depth == MoveTime) return false;
				foreach (var button in buttons)
				{
					foreach (var method in button.methods)
					{
						bool isInHashTable;
						int newValue;
						int newDepth = method.isMoveTimeComsuming ? depth + 1 : depth;
						try
						{
							newValue = method.Function(button, value, method.args);
							newValue = inputBox.Procedure(newValue);
						}
						catch (ArithmeticException)
						{
							continue;
						}
						string dictionaryKey = newValue.ToString() + ButtonKey;
						isInHashTable = hashTable.ContainsKey(dictionaryKey);
						if(isInHashTable)
						{
							if (hashTable[dictionaryKey] > depth || method.isValueImmutable)
							{
								if (hashTable[dictionaryKey] > depth) hashTable[dictionaryKey] = depth;
								if (RecursiveSolvePuzzle(newValue, newDepth, result, hashTable))
								{
									hasFind = true;
									result.Add(new Pair<string, int>(button.ToString(), newValue));
									if (method.isWithdrawable) method.Withdraw(button, value, method.args);
									return hasFind;
								}
							}
						}
						else
						{
							hashTable.Add(dictionaryKey, depth);
							if (RecursiveSolvePuzzle(newValue, newDepth, result, hashTable))
							{
								hasFind = true;
								result.Add(new Pair<string, int>(button.ToString(), newValue));
								if (method.isWithdrawable) method.Withdraw(button, value, method.args);
								return hasFind;
							}
						}
						if (method.isWithdrawable) method.Withdraw(button, value, method.args);
					}
				}
				return hasFind;
			}
			private List<Pair<string, int>> RefineResult(List<Pair<string, int>> result)
			{
				//删除多余Store
				int preIndex = result.FindIndex(x => x.Key.Equals("Store"));
				if (preIndex >= 0)
				{
					result[preIndex].Key += "(S)";
					bool isAppendUsed = false;
					int nextIndex = result.FindIndex(preIndex + 1, x => x.Key.Equals("Store"));
					while (nextIndex != -1)
					{
						if (result[nextIndex].Value != result[nextIndex - 1].Value)  //使用拼接功能
						{
							result[nextIndex].Key += "(A)";
							isAppendUsed = true;
						}
						else  //使用储存功能
						{
							result[nextIndex].Key += "(S)";
							if (isAppendUsed)  //拼接过，不处理	
							{
								isAppendUsed = false;  //新的值未使用过
							}
							else  //未使用过拼接，删除上一个
							{
								result.RemoveAt(preIndex);
								nextIndex--;
							}
						}
						preIndex = nextIndex;
						nextIndex = result.FindIndex(preIndex + 1, x => x.Key.Equals("Store"));
					}
					if (!isAppendUsed) result.RemoveAt(preIndex);
				}
				return result;
			}

			public class InputBox
			{
				public delegate int InputBoxMethodDelegate(InputBox inputBox, int value, object[] args);
				public class Method
				{
					public object[] args;
					public InputBoxMethodDelegate Function;
					public Method(object[] args, InputBoxMethodDelegate Function)
					{
						this.args = args;
						this.Function = Function;
					}
				}
				public Method[] methods;

				public enum InputBoxType
				{
					Default,
					Portal,
				}
				public InputBox(InputBoxType type, params object[] args)
				{
					switch (type)
					{
						case InputBoxType.Default:
							{
								methods = new Method[] { new Method(null, Default) };
								break;
							}
						case InputBoxType.Portal:
							{
								methods = new Method[] { new Method(new object[] { args[0], args[1] }, Portal) };
								break;
							}
						default:
							break;
					}
					
				}
				public int Procedure(int value)
				{
					foreach (var method in methods)
					{
						value = method.Function(this, value, method.args);
					}
					return value;
				}
			}
			public static int Portal(InputBox inputBox, int value, object[] args)
			{
				bool isNegative = value < 0;
				int entrance = (int)args[0] - 1;
				int exit = (int)args[1] - 1;
				value = Math.Abs(value);
				while (value > Math.Pow(10, entrance))
				{
					string strOfValue = value.ToString();
					if (strOfValue.Length == entrance + 1)
					{
						int entranceNumber = int.Parse(strOfValue.Substring(0, 1));
						strOfValue = strOfValue.Substring(1);
						value = int.Parse(strOfValue) + entranceNumber * (int)Math.Pow(10, exit);
					}
					else
					{
						int indexOfEntrance = strOfValue.Length - entrance - 1;
						int entranceNumber = int.Parse(strOfValue.Substring(indexOfEntrance, 1));
						string strOfResult = strOfValue.Substring(0, indexOfEntrance) + strOfValue.Substring(indexOfEntrance+1);
						value = int.Parse(strOfResult) + entranceNumber * (int)Math.Pow(10, exit);
					}
				}
				return (isNegative ? -value : value);
			}
			public static int Default(InputBox inputBox, int value, object[] args)
			{
				return value;
			}

			public class Button
			{
				public delegate int ButtonMethodDelegate(Button button, int value, object[] args);
				public class Method
				{
					public bool isMoveTimeComsuming;
					public bool isWithdrawable;
					public bool isValueImmutable;
					public object[] args;
					public ButtonMethodDelegate Function;
					public ButtonMethodDelegate Withdraw;
					public Method(bool isMoveTimeComsuming, bool isWithdrawable, bool isValueImmutable, object[] args, ButtonMethodDelegate Function, ButtonMethodDelegate Withdraw)
					{
						this.isMoveTimeComsuming = isMoveTimeComsuming;
						this.isWithdrawable = isWithdrawable;
						this.isValueImmutable = isValueImmutable;
						this.args = args;
						this.Function = Function;
						this.Withdraw = Withdraw;
					}
				}
				public ButtonType type;
				public Method[] methods;

				public enum ButtonType
				{
					Plus,
					Minus,
					Multiply,
					Division,
					Append,
					Cut,
					Reverse,
					Sum,
					Change,
					LeftShift,
					RightShift,
					Mirror,
					Store,
					Inc,
					Inv10,
				}
				/// <summary>
				/// 各类型参数个数及说明：
				/// Plus：1个，加数；
				/// Minus：1个，减数；
				/// Multiply：1个，乘数；
				/// Division：1个，除数；
				/// Cut：无；
				/// Reverse：无；
				/// Sum：无；
				/// Change：2个，原字符串和新字符串；
				/// LeftShift：无；
				/// RightShift：无；
				/// Mirror：无；
				/// Append：1个，添加在末尾的数；
				/// Store：无；
				/// Inc：2个，增量和待增按钮的List；
				/// Inv10：无；
				/// </summary>
				/// <param name="type">按钮类型</param>
				/// <param name="args">按钮参数</param>
				public Button(Button.ButtonType type, params object[] args)
				{
					this.type = type;
					switch (type)
					{
						case ButtonType.Plus:
							{
								object[] arg = { args[0] };
								methods = new Method[] { new Method(true, false, false, arg, Plus, null) };
								break;
							}
						case ButtonType.Minus:
							{
								object[] arg = { args[0] };
								methods = new Method[] { new Method(true, false, false, arg, Minus, null) };
								break;
							}
						case ButtonType.Multiply:
							{
								object[] arg = { args[0] };
								methods = new Method[] { new Method(true, false, false, arg, Multiply, null) };
								break;
							}
						case ButtonType.Division:
							{
								object[] arg = { args[0] };
								methods = new Method[] { new Method(true, false, false, arg, Division, null) };
								break;
							}
						case ButtonType.Cut:
							{
								methods = new Method[] { new Method(true, false, false, null, Cut, null) };
								break;
							}
						case ButtonType.Reverse:
							{
								methods = new Method[] { new Method(true, false, false, null, Reverse, null) };
								break;
							}
						case ButtonType.Sum:
							{
								methods = new Method[] { new Method(true, false, false, null, Sum, null) };
								break;
							}
						case ButtonType.Change:
							{
								object[] arg = { args[0], args[1] };
								methods = new Method[] { new Method(true, false, false, arg, Change, null) };
								break;
							}
						case ButtonType.LeftShift:
							{
								methods = new Method[] { new Method(true, false, false, null, LeftShift, null) };
								break;
							}
						case ButtonType.RightShift:
							{
								methods = new Method[] { new Method(true, false, false, null, RightShift, null) };
								break;
							}
						case ButtonType.Mirror:
							{
								methods = new Method[] { new Method(true, false, false, null, Mirror, null) };
								break;
							}
						case ButtonType.Append:
							{
								object[] arg = { args[0] };
								methods = new Method[] { new Method(true, false, false, arg, Append, null) };
								break;
							}
						case ButtonType.Store:
							{
								methods = new Method[] { new Method(false, true, true, new object[] { new List<int>() }, StoreFunction, StoreWithdraw), new Method(true, false, false, new object[] { null }, StoreAppend, null) };
								break;
							}
						case ButtonType.Inc:
							{
								object[] arg = { args[0], args[1] };
								methods = new Method[] { new Method(true, true, true, arg, IncFunction, IncWithdraw) };
								break;
							}
						case ButtonType.Inv10:
							{
								methods = new Method[] { new Method(true, false, false, null, Inv10, null) };
								break;
							}
						default:
							break;
					}
				}
				public override string ToString()
				{
					return GetDictionaryKey(false);
				}
				public string DictionaryKey
				{
					get { return GetDictionaryKey(true); }
				}
				private string GetDictionaryKey(bool isToDictionaryKey = true)
				{
					switch (type)
					{
						case ButtonType.Plus:
							return "+" + (int)methods[0].args[0];
						case ButtonType.Minus:
							return "-" + (int)methods[0].args[0];
						case ButtonType.Multiply:
							return "x" + (int)methods[0].args[0];
						case ButtonType.Division:
							return "/" + (int)methods[0].args[0];
						case ButtonType.Cut:
							return "<<";
						case ButtonType.Reverse:
							return "Reverse";
						case ButtonType.Sum:
							return "Sum";
						case ButtonType.Change:
							return (string)methods[0].args[0] + "=>" + (string)methods[0].args[1];
						case ButtonType.LeftShift:
							return "<Shift";
						case ButtonType.RightShift:
							return "Shift>";
						case ButtonType.Mirror:
							return "Mirror";
						case ButtonType.Append:
							return ((int)methods[0].args[0]).ToString();
						case ButtonType.Store:
							return "Store" + (isToDictionaryKey && ((int?)methods[1].args[0] != null) ? ((int?)methods[1].args[0]).Value.ToString() : string.Empty);
						case ButtonType.Inc:
							return "[+]" + (int)methods[0].args[0];
						case ButtonType.Inv10:
							return "Inv10";
						default:
							return "Error";
					}
				}
			}
			/*加*/
			public static int Plus(Button button, int value, object[] args)
			{
				int result = value + (int)args[0];
				if (result >= 1000000) throw new ArithmeticException();
				return result;
			}
			/*减*/
			public static int Minus(Button button, int value, object[] args)
			{
				int result = value - (int)args[0];
				if (result <= -1000000) throw new ArithmeticException();
				return result;
			}
			/*乘*/
			public static int Multiply(Button button, int value, object[] args)
			{
				int result = value * (int)args[0];
				if (Math.Abs(result) >= 1000000) throw new ArithmeticException();
				return result;
			}
			/*除*/
			public static int Division(Button button, int value, object[] args)
			{
				if (value % (int)args[0] != 0) throw new ArithmeticException();
				return value / (int)args[0];
			}
			/*去尾*/
			public static int Cut(Button button, int value, object[] args)
			{
				if (Math.Abs(value) < 10)
				{
					return 0;
				}
				else
				{
					string strOfValue = value.ToString();
					return int.Parse(strOfValue.Substring(0, strOfValue.Length - 1));
				}
			}
			/*反转*/
			public static int Reverse(Button button, int value, object[] args)
			{
				bool isNegative = value < 0;
				value = Math.Abs(value);
				char[] charArray = value.ToString().ToCharArray();
				Array.Reverse(charArray);
				string strOfResult = new string(charArray);
				return (isNegative ? -int.Parse(strOfResult) : int.Parse(strOfResult));
			}
			/*改变*/
			public static int Change(Button button, int value, object[] args)
			{
				string strOfValue = value.ToString();
				return int.Parse(strOfValue.Replace((string)args[0], (string)args[1]));
			}
			/*左移*/
			public static int LeftShift(Button button, int value, object[] args)
			{
				bool isNegative = value < 0;
				value = Math.Abs(value);
				string strOfValue = value.ToString();
				string strOfResult = strOfValue.Substring(1) + strOfValue[0];
				return (isNegative ? -int.Parse(strOfResult) : int.Parse(strOfResult));
			}
			/*右移*/
			public static int RightShift(Button button, int value, object[] args)
			{
				bool isNegative = value < 0;
				value = Math.Abs(value);
				string strOfValue = value.ToString();
				string strOfResult = strOfValue[strOfValue.Length - 1] + strOfValue.Substring(0, strOfValue.Length - 1);
				return (isNegative ? -int.Parse(strOfResult) : int.Parse(strOfResult));
			}
			/*镜像*/
			public static int Mirror(Button button, int value, object[] args)
			{
				bool isNegative = value < 0;
				value = Math.Abs(value);
				if (Math.Abs(value) >= 1000) throw new ArithmeticException();
				string strOfValue = value.ToString();
				char[] reverse = strOfValue.ToCharArray();
				Array.Reverse(reverse);
				string strOfResult = strOfValue + new string(reverse);
				return (isNegative ? -int.Parse(strOfResult) : int.Parse(strOfResult));
			}
			/*拼接*/
			public static int Append(Button button, int value, object[] args)
			{
				int result = int.Parse(value.ToString() + ((int)args[0]).ToString());
				if (Math.Abs(result) >= 1000000) throw new ArithmeticException();
				return result;
			}
			/*储存*/
			public static int StoreAppend(Button button, int value, object[] args)
			{
				if ((int?)args[0] == null || (int?)args[0] < 0) throw new ArithmeticException();
				int? result = int.Parse(value.ToString() + ((int?)args[0]).ToString());
				if (Math.Abs(result.Value) >= 1000000) throw new ArithmeticException();
				return result.Value;
			}
			public static int StoreFunction(Button button, int value, object[] args)
			{
				if ((int?)button.methods[1].args[0] == value)
				{
					throw new ArithmeticException();
				}
				List<int> list = (List<int>)args[0];
				list.Add(value); 
				button.methods[1].args[0] = value;
				return value;
			}
			public static int StoreWithdraw(Button button, int value, object[] args)
			{
				List<int> list = (List<int>)args[0];
				list.RemoveAt(list.Count - 1);
				if (list.Count() != 0)
				{
					button.methods[1].args[0] = list.Last();
				}
				else button.methods[1].args[0] = null;
				return 0;
			}
			/*按钮增减*/
			public static int IncFunction(Button button, int value, object[] args)
			{
				int inc = (int)args[0];
				List<Button> btns = (List<Button>)args[1];
				foreach (var btn in btns)
				{
					switch (btn.type)
					{
						case Button.ButtonType.Plus:
						case Button.ButtonType.Minus:
						case Button.ButtonType.Multiply:
						case Button.ButtonType.Division:
						case Button.ButtonType.Append:
							btn.methods[0].args[0] = (int)btn.methods[0].args[0] + inc;
							break;
						default:
							throw new NotSupportedException();
					}
				}
				return value;
			}
			public static int IncWithdraw(Button button, int value, object[] args)
			{
				int inc = (int)args[0];
				List<Button> btns = (List<Button>)args[1];
				foreach (var btn in btns)
				{
					switch (btn.type)
					{
						case Button.ButtonType.Plus:
						case Button.ButtonType.Minus:
						case Button.ButtonType.Multiply:
						case Button.ButtonType.Division:
						case Button.ButtonType.Append:
							btn.methods[0].args[0] = (int)btn.methods[0].args[0] - inc;
							break;
						default:
							throw new NotSupportedException();
					}
				}
				return value;
			}
			/*求和*/
			public static int Sum(Button button, int value, object[] args)
			{
				bool isNegative = value < 0;
				value = Math.Abs(value);
				int result = 0;
				string strOfValue = value.ToString();
				foreach (var charater in strOfValue)
				{
					result += int.Parse(charater.ToString());
				}
				return (isNegative ? -result : result);
			}
			/*补十*/
			public static int Inv10(Button button, int value, object[] args)
			{
				bool isNegative = value < 0;
				value = Math.Abs(value);
				char[] charArray = value.ToString().ToCharArray();
				StringBuilder result = new StringBuilder(charArray.Length);
				foreach (var item in charArray)
				{
					int tmp = 10 - int.Parse(new string(item, 1));
					if (tmp == 10) tmp = 0;
					result.Append(tmp.ToString());
				}
				return (isNegative ? -int.Parse(result.ToString()) : int.Parse(result.ToString()));
			}
		}
		public class Pair<TKey,TValue>
		{
			TKey key;
			TValue value;

			public Pair(TKey key, TValue value)
			{
				this.key = key;
				this.value = value;
			}

			public TKey Key { get => key; set => key = value; }
			public TValue Value { get => value; set => this.value = value; }
		}
	}
}