<Query Kind="Program">
  <Namespace>LINQPad.FSharpExtensions</Namespace>
</Query>

void Main()
{
	Rover rover = new Rover(0, 0, Direction.North);
	rover.Direction.Dump();
}

public enum Direction
{
	North,
	South,
	East,
	West
}

public class Rover
{
	public int X { get; private set; }
	public int Y { get; private set; }
	public Direction Direction { get; private set; }
	
	public Rover(int x, int y, Direction direction)
	{
		
	}
}



