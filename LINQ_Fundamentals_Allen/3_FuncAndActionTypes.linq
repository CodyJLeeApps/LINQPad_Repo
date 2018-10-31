<Query Kind="Program" />

void Main()
{

	Func<int, int> square = x => x * x;
	Func<int, int, int> add = (x, y) =>
	{
		int temp = x + y;
		return temp;
	};
	
	Action<int> write = x => Console.WriteLine(x);
	
	Console.WriteLine(square(3));
	Console.WriteLine("*****");
	Console.WriteLine(add(3,5));
	Console.WriteLine("*****");
	write(square(add(3, 5)))
;}

// Typical method definition of Square
private static int Square(int num)
{
	return num * num;
}