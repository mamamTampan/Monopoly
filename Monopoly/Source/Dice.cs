using MonopolyProjectInterface;

namespace MonopolyProjectSource
{
	public class Dice : IDice
	{
		private int diceSide;
		private int diceDoubleCount;

		public bool SetDiceSide(int _diceSide)
		{
			if (diceSide <= 10)
			{
				diceSide = _diceSide;
				return true;
			}
			return false;
		}

		public int Roll()
		{
			Random rand = new ();
			diceSide = rand.Next(1, diceSide + 1);
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
