<Query Kind="Program" />

void Main()
{
	var cars = ProcessFile(@"C:\Users\cody\Documents\LINQPad Queries\LINQPad_Repo\LINQ_Fundamentals_Allen\fuel.csv");
	
	/*
	var query = cars.OrderByDescending(c => c.Combined)
					.ThenBy(c => c.Name);
	*/
	
	var query = from car in cars
				where car.Manufacturer == "BMW" && car.Year == 2016
				orderby car.Combined descending, car.Name ascending
				select new
				{
					Manufacturer = car.Manufacturer,
					car.Name,
					car.Combined
				};
	
	var query2 = 
			cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
				.OrderByDescending(c => c.Combined)
				.ThenBy(c => c.Name)
				.First();

	var result = cars.Any(c => c.Manufacturer == "Ford");
	Console.WriteLine($"{result}");
	
	foreach (var car in query.Take(10))
	{
		Console.WriteLine($"{car.Manufacturer} {car.Name} : {car.Combined}");
	}

	Console.WriteLine("**********");
	Console.WriteLine($"{query2.Name} : {query2.Combined}");
	
	var result2 = cars.SelectMany(c => c.Name);
	foreach (var name in result2)
	{
		Console.WriteLine(name);
	}
}

private static List<Car> ProcessFile(string filePath)
{
	var fileQuery = File.ReadAllLines(filePath)
						.Skip(1)
						.Where(line => line.Length > 1)
						.ToCar();
	
	return fileQuery.ToList();
}

public static class CarExtensions
{
	public static IEnumerable<Car> ToCar(this IEnumerable<string> source)
	{
		foreach(var line in source)
		{
			var columns = line.Split(',');
			yield return new Car
			{
				Year = int.Parse(columns[0]),
				Manufacturer = columns[1],
				Name = columns[2],
				Displacement = double.Parse(columns[3]),
				Cylinders = int.Parse(columns[4]),
				City = int.Parse(columns[5]),
				Highway = int.Parse(columns[6]),
				Combined = int.Parse(columns[7])
			};
		}
	}
}

public class Car
{
	public int Year { get; set; }
	public string Manufacturer { get; set; }
	public string Name { get; set; }
	public double Displacement { get; set; }
	public int Cylinders { get; set; }
	public int City { get; set; }
	public int Highway { get; set; }
	public int Combined { get; set; }
}