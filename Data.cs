using System;
using System.Windows;

public class TagData
{
    public string ID;
	public string Coordinates;
	public int Length;

    public TagData(int x, int y)
	{
        Coordinates = $"{x}_{y}";
        ID = $"Sea0#0";
	}
	public static string ChangeID(SquareTypes NewType)
	{
		Random IDNum = new Random();
		string id = null;
		if (NewType == SquareTypes.Sea)
		{
			id = "Sea0#0";
        }
		else
		{
			id = $"Ship{NewType}#{IDNum.Next(10000000)}";
		}
		return id;
    }
	public enum SquareTypes
	{
		None = -1,
		Sea = 0,
		Patrol = 2,
		Cruiser = 3,
		BattleShip = 4,
		Carrier = 5
	}
}
