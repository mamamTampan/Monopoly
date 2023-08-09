using MonopolyProjectInterface;
namespace MonopolyProject.source;

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
		var rollValue1 = dices[0].Roll();
		var rollValue2 = dices[1].Roll();
		if (rollValue1 == rollValue2)
		{
			diceDoubleCount++;
			if (diceDoubleCount == 3)
			{
				//SetToJail();
				diceDoubleCount = 0;
			}
		}
		else
		{
			diceDoubleCount = 0;
		}
	}
}
