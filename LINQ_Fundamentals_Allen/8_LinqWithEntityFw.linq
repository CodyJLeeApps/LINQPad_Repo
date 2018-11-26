<Query Kind="Program" />

void Main()
{
	CreateXml();
	QueryXml();
	
}

private static void CreateXml()
{
	var records = ProcessCarsFile(@"C:\Users\cody\Documents\LINQPad Queries\LINQPad_Repo\LINQ_Fundamentals_Allen\fuel.csv");
	
	var ns = (XNamespace)"http://pluralsight.com/cars/2016";
	var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
	
	var document = new XDocument();
	var cars = new XElement(ns + "Cars");

	var elements = from record in records
				   select new XElement(ex + "Car",
							   new XAttribute("Name", record.Name),
							   new XAttribute("Combined", record.Combined),
							   new XAttribute("Manufacturer", record.Manufacturer));

	cars.Add(new XAttribute(XNamespace.Xmlns + "ex", ex));
	cars.Add(elements);

	document.Add(cars);
	document.Save(@"C:\users\cody\Documents\LINQPad Queries\LINQPad_Repo\LINQ_Fundamentals_Allen\fuel.xml");
}

private static void QueryXml()
{
	var document = XDocument.Load(@"C:\users\cody\Documents\LINQPad Queries\LINQPad_Repo\LINQ_Fundamentals_Allen\fuel.xml");
	var ns = (XNamespace)"http://pluralsight.com/cars/2016";
	var ex = (XNamespace)"http://pluralsight.com/cars/2016/ex";
	
	var query = from element in document.Element(ns + "Cars")?.Elements(ex + "Car") 
					?? Enumerable.Empty<XElement>()
				where element.Attribute("Manufacturer")?.Value == "BMW"
				select element.Attribute("Name").Value;
	foreach (var element in query)
	{
		Console.WriteLine(element);
	}
}

public class CarStatistics
{
	public int Max { get; set; }
	public int Min { get; set; }
	public int Total { get; set; }
	public int Count { get; set; }
	public int Average { get; set; }

	public CarStatistics()
	{

		Max = Int32.MinValue;
		Min = Int32.MaxValue;

	}

	public CarStatistics Accumulate(Car car)
	{
		Count += 1;
		Total += car.Combined;
		Max = Math.Max(Max, car.Combined);
		Min = Math.Min(Min, car.Combined);
		return this;
	}

	public CarStatistics Compute()
	{
		Average = Total / Count;
		return this;
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