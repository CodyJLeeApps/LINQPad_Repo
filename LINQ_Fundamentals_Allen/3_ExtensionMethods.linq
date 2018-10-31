<Query Kind="Program" />

void Main()
{
	IEnumerable<Employee> developers = new Employee[]
	{
		new Employee { Id = 1, Name = "Cameron" },
		new Employee { Id = 2, Name = "Chris" }
	};

	IEnumerable<Employee> sales = new List<Employee>()
	{
		new Employee { Id = 3, Name = "George" }
	};

	// C# Version
	foreach (var person in developers)
	{
		Console.WriteLine(person.Name);
	}

	Console.WriteLine("*****");

	// LINQ Version
	Console.WriteLine(developers.Count());
	IEnumerator<Employee> enumerator = developers.GetEnumerator();
	while (enumerator.MoveNext())
	{
		Console.WriteLine(enumerator.Current.Name);
	}
}

public class Employee
{
	public int Id { get; set; }
	public string Name { get; set; }
}

public static class MyLinq
{
	public static int Count<T>(this IEnumerable<T> sequence)
	{
		int count = 0;
		foreach(var item in sequence)
		{
			count+=1;
		}
		return count;
	}
}