using System;
using System.Windows;

public class TagData
{
    public string ID;
	public string Coordinates;

    public TagData(int x, int y)
	{
        Coordinates = $"{x}_{y}";
        ID = $"Sea0#0";
	}
	public void ChangeID(SquareTypes NewType)
	{
		Random IDNum = new Random();
		if (NewType == SquareTypes.Sea)
		{
			ID = "Sea0#0";
        }
		else
		{
			ID = $"Ship{NewType}#{IDNum.Next(1000000)}";
		}
	}
	public enum SquareTypes
	{
		Sea = 0,
		Patrol = 2,
		Cruiser = 3,
		BattleShip = 4,
		Carrier = 5
	}
}
