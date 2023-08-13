using MonopolyProjectInterface;

namespace MonopolyProjectSource
{
	public class CardMove
	{
		public bool FineCard(IPlayerConfig player)
		{
			int fineAmount = 15;
			player.DecreaseBalance(fineAmount);
			return true;
		}

		public bool RewardCard(IPlayerConfig player)
		{
			int rewardAmount = 50;
			player.IncreaseBalance(rewardAmount);
			return true;
		}

		public bool TaxCard(IPlayerConfig player)
		{
			int taxAmount = 100;
			player.DecreaseBalance(taxAmount);
			return true;
		}

		public bool GoToJailCard(IPlayerConfig player)
		{
			player.SetPositionToNew(11);
			player.IsInJail();
			return true;
		}

public bool FreeFromJailCard(IPlayerConfig player, ICard card)
{
    if (player.IsInJail())
    {
        player.UseCard(card);
        player.SetPositionToNew(11); // Pindahkan ke posisi setelah Jail
        return true;
    }
    return false;
}


		public bool Step(IPlayerConfig player, int steps)
		{
			player.SetPositionToNew(steps);
			return true;
		}

		public bool ToStart(IPlayerConfig player)
		{
			int rewardAmount = 200;
			player.IncreaseBalance(rewardAmount);
			player.SetPositionToNew(1);
			return true;
		}
	}
}
