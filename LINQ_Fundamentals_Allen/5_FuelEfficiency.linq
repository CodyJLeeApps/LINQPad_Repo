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
				select car;
	
	var query2 = 
			cars.Where(c => c.Manufacturer == "BMW" && c.Year == 2016)
				.OrderByDescending(c => c.Combined)
				.ThenBy(c => c.Name)
				.First();
	
	foreach (var car in query.Take(10))
	{
		Console.WriteLine($"{car.Name} : {car.Combined}");
	}

	Console.WriteLine("**********");
	Console.WriteLine($"{query2.Name} : {query2.Combined}");
}

private static List<Car> ProcessFile(string filePath)
{
	return File.ReadAllLines(filePath)
				.Skip(1)
				.Where(line => line.Length > 1)
				.Select(Car.ParseFromCsv)
				.ToList();
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

	internal static Car ParseFromCsv(string line)
	{
		var columns = line.Split(',');
		return new Car
		{
			Year 			= int.Parse(columns[0]),
			Manufacturer	= columns[1],
			Name			= columns[2],
			Displacement	= double.Parse(columns[3]),
			Cylinders		= int.Parse(columns[4]),
			City			= int.Parse(columns[5]),
			Highway			= int.Parse(columns[6]),
			Combined		= int.Parse(columns[7])
		};
	}
}