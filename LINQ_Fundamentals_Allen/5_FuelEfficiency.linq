<Query Kind="Program" />

void Main()
{
	var cars = ProcessFile("fuel.csv");
}

private static List<Car> ProcessFile(string filePath)
{
	throw new NotImplementedException();
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