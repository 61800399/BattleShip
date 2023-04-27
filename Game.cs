using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

public class GameBoard
{
	private List<List<int>> _board;

	public List<List<int>> Board 
	{ 
		get { return _board; } 
		set { _board = value; } 
	}

	public GameBoard()
	{
		MakeBoard();
	}
	private void MakeBoard()
	{ // Being Created X, Y
		int num = 0;
		_board = new List<List<int>>();
		for (int x = 0; x < 10; x++)
		{
			List<int> X = new List<int>();
			for (int y = 0; y < 10; y++)
			{ 
				X.Add(num);
				num++;
			}
			Board.Add(X);
        }
		// MessageBox Board TEMP
		/*
		string print = null;
		int _x = 0;
		foreach (var x in Board)
		{
			foreach (var y in x)
			{
                print += $"{y} |";
            }
			print += "\n" ;
			_x++;
		}
		MessageBox.Show(print);
		*/
	}
}
