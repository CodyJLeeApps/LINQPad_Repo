<Query Kind="Program" />

void Main()
{
	var cars = ProcessCarsFile(@"C:\Users\cody\Documents\LINQPad Queries\LINQPad_Repo\LINQ_Fundamentals_Allen\fuel.csv");
	var manufacturers = ProcessManufacturersFile(@"C:\Users\cody\Documents\LINQPad Queries\LINQPad_Repo\LINQ_Fundamentals_Allen\manufacturers.csv");

	var query = from manufacturer in manufacturers
				join car in cars on manufacturer.Name equals car.Manufacturer
					into carGroup
				orderby manufacturer.Name
				select new
				{
					Manufacturer = manufacturer,
					Cars = carGroup
				} into result
				group result by result.Manufacturer.Headquarters;


	foreach (var group in query)
	{
		Console.WriteLine($"{group.Key}");
		foreach(var car in group.SelectMany(g => g.Cars)
								.OrderByDescending(c => c.Combined)
								.Take(3))
		{
			Console.WriteLine($"\t\t\t{car.Manufacturer} - {car.Name} : {car.Combined}");
		}
	}

	var query2 = manufacturers.GroupJoin(cars, 
										m => m.Name, 
										c => c.Manufacturer, 
										(m, g) => 
											new 
											{ 
												Manufacturer = m,
												Cars = g
											})
								.GroupBy(m => m.Manufacturer.Headquarters);

	Console.WriteLine("\n**********\n");
	foreach (var group in query2)
	{
		Console.WriteLine($"{group.Key}");
		foreach (var car in group.SelectMany(g => g.Cars)
								.OrderByDescending(c => c.Combined)
								.Take(3))
		{
			Console.WriteLine($"\t\t\t{car.Manufacturer} - {car.Name} : {car.Combined}");
		}
	}
}

private static List<Manufacturer> ProcessManufacturersFile(string filePath)
{
	var query = File.ReadAllLines(filePath)
					.Where(line => line.Length > 1)
					.Select(line =>
					{
						var columns = line.Split(',');
						return new Manufacturer
						{
							Name = columns[0],
							Headquarters = columns[1],
							Year = int.Parse(columns[2])
						};
					});
	return query.ToList();
}

private static List<Car> ProcessCarsFile(string filePath)
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
		foreach (var line in source)
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

public class Manufacturer
{
	public string Name { get; set; }
	public string Headquarters { get; set; }
	public int Year { get; set; }
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