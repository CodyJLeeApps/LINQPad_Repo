<Query Kind="Program" />

void Main()
{
	
	var numbers = MyLinq.Random().Where(nameof => nameof > 0.5).Take(10);
	foreach(var number in numbers)
	{
		Console.WriteLine(number);
	}
	
	var movies = new List<Movie>
	{
		new Movie { Title = "The Dark Night Rises", Rating = 8.9f, Year = 2008},
		new Movie { Title = "The Kings Speech",     Rating = 8.0f, Year = 2010},
		new Movie { Title = "Casablanca",           Rating = 8.5f, Year = 1942},
		new Movie { Title = "Star Wars V",          Rating = 8.7f, Year = 1980}
	};

	/*var query = movies.Filter(m => m.Year > 2000)
						.OrderByDescending(m => m.Rating);*/
	var query = from movie in movies
				where movie.Year > 2000
				orderby movie.Rating descending
				select movie;

	Console.WriteLine(query.Count());
	var enumerator = query.GetEnumerator();
	while (enumerator.MoveNext())
	{
		Console.WriteLine(enumerator.Current.Title);
	}
}

public class Movie
{
	public string Title { get; set; }
	public float Rating { get; set; }

	int _year;
	public int Year
	{
		get
		{
			Console.WriteLine($"Returning {_year} for: {Title}");
			return _year;
		}

		set
		{
			_year = value;
		}
	}
}

public static class MyLinq
{
	public static IEnumerable<T> Filter<T>(this IEnumerable<T> source,
											Func<T, bool> predicate)
	{
		var result = new List<T>();

		foreach (var item in source)
		{
			if (predicate(item))
			{
				yield return item;
			}
		}
	}
	
	public static IEnumerable<double> Random()
	{
		var random = new Random();
		while(true)
		{
			yield return random.NextDouble();
		}
	}
}