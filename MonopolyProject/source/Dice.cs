using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class Dice : IDice
{
	private int diceSide;
	private int diceDoubleCount;
	
	public bool SetDiceSide(int _diceSide)
	{
		if (_diceSide < 1 || _diceSide > 6)
		{
			return false;
		}
		diceSide = _diceSide;
		return true;
	}
	public int Roll()
	{
		var random = new Random();
		diceSide = random.Next(1, diceSide + 1);
		return diceSide;
	}
	public virtual void IsDouble()
	{
		var roll1 = Roll();
		var roll2 = Roll();
		if ( roll1 == roll2 )
		{
			diceDoubleCount++;
			if (diceDoubleCount == 3)
			{
				diceDoubleCount = 0;
			}
		}
		else
		{
			diceDoubleCount = 0;
		}	
	}
}
