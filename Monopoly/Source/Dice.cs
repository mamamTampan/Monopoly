using MonopolyProjectInterface;

namespace MonopolyProjectSource
{
	public class Dice : IDice
	{
		private int diceSide;
		private int diceDoubleCount;

		public Dice()
		{
			diceSide = 6;
			diceDoubleCount = 0;
		}

		public bool SetDiceSide(int _diceSide)
		{
			if (_diceSide == 6)
			{
				diceSide = _diceSide;
				return true;
			}
			return false;
		}

		public int Roll()
		{
			Random rand = new Random();
			diceSide = rand.Next(1, 7);
			return diceSide;
		}

		public void IsDouble()
		{
			var roll1 = Roll();
			var roll2 = Roll();
			if (roll1 == roll2)
			{
				diceDoubleCount++;
			}
		}
	}
}
