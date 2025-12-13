namespace AoC2025.Day01;

public interface IDayOneSolution
{
	Task<int> SolvePartOneAsync();
	Task<int> SolvePartTwoAsync();
}

public class DayOneSolution : IDayOneSolution
{
	private const int MaxNumber = 100;
	private const int MinNumber = 0;
	private const int StartingPoint = 50;
	private const string LeftRotation = "L";
	private const string RightRotation = "R";

	public async Task<int> SolvePartOneAsync()
	{
		var position = StartingPoint;
		var count = 0;

		Console.WriteLine($"The dial starts by pointing at {position}");

		await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "input.txt")))
		{

			var moves = line.StartsWith(LeftRotation)
					? -int.Parse(line[LeftRotation.Length..])
					: int.Parse(line[RightRotation.Length..]);

			var newPosition = position + moves;

			position = newPosition;
			position %= MaxNumber;

			if (position < MinNumber)
			{
				position = MaxNumber + position;
			}

			if (position == MinNumber) count++;

			Console.WriteLine($"The dial is rotated {line} to point at {position}");
		}

		return count;
	}

	public async Task<int> SolvePartTwoAsync()
	{
		var position = StartingPoint;
		var count = 0;

		Console.WriteLine($"The dial starts by pointing at {position}");

		await foreach (var line in File.ReadLinesAsync(Path.Combine(Directory.GetCurrentDirectory(), "input.txt")))
		{
			var moves = line.StartsWith(LeftRotation)
					? -int.Parse(line[LeftRotation.Length..])
					: int.Parse(line[RightRotation.Length..]);

			var newPosition = position + moves;

			var passes = (int)Math.Floor((double)newPosition / MaxNumber);

			if (newPosition < 0)
			{
				passes = position == 0 ? -(newPosition / MaxNumber) : 1 + -(newPosition / MaxNumber);
			}
			else if (newPosition > 0)
			{
				passes = newPosition / MaxNumber;
			}
			else
			{
				count++;
			}

			position = newPosition % MaxNumber;

			if (position < MinNumber)
			{
				position += MaxNumber;
			}

			count += passes;

			if (passes > 0)
			{
				Console.WriteLine(
						$"The dial is rotated {line} to point at {position}; during this rotation, it points at {MinNumber} {passes} times.");
			}
			else
			{
				Console.WriteLine($"The dial is rotated {line} to point at {position}");
			}
		}

		return count;
	}
}
