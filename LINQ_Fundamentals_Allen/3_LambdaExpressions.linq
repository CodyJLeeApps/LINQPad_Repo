<Query Kind="Program" />

void Main()
{
	IEnumerable<Employee> developers = new Employee[]
	{
		new Employee { Id = 1, Name = "Cameron" },
		new Employee { Id = 2, Name = "Chris" },
		new Employee { Id = 3, Name = "Sheri" }
	};

	IEnumerable<Employee> sales = new List<Employee>()
	{
		new Employee { Id = 3, Name = "George" }
	};

	// Named Method
	foreach (var employee in developers.Where(NameStartsWithS))
	{
		Console.WriteLine(employee.Name);
	}
	Console.WriteLine("*****");
	
	// Anonymous Method
	foreach(var employee in developers.Where(
		delegate(Employee employee)
		{
			return employee.Name.StartsWith("S");
		}))
	{
		Console.WriteLine(employee.Name);
	}	
	Console.WriteLine("*****");
	
	// Lambda Expression
	foreach(var employee in developers.Where(
					e => e.Name.StartsWith("S")))
	{
		Console.WriteLine(employee.Name);
	}
	
	// LINQ Version
	Console.WriteLine(developers.Count());
	IEnumerator<Employee> enumerator = developers.GetEnumerator();
	while (enumerator.MoveNext())
	{
		Console.WriteLine(enumerator.Current.Name);
	}
}

public static bool NameStartsWithS(Employee employee)
{
	return employee.Name.StartsWith("S");
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
		foreach (var item in sequence)
		{
			count += 1;
		}
		return count;
	}
}