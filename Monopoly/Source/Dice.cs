using MonopolyProjectInterface;

namespace MonopolyProjectSource
{
	public class Dice : IDice
	{
		private int diceSide;
		
		public Dice()
		{
			diceSide = 6;
		}
		
		public bool SetDiceSide(int _diceSide)
		{
			if (diceSide <= 6)
			{
				diceSide = _diceSide;
				return true;
			}
			return false;
		}

		public int Roll()
		{
			Random rand = new ();
			diceSide = rand.Next(1, 7);
			return diceSide;
		}

	}
}
