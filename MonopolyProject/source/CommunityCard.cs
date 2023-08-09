using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.Text;
using MonopolyProjectInterface;

namespace MonopolyProjectSource;

public class CommunityCard : ICard	
{
	private CommunityCardType _type;
	private string _description;
	private Dictionary<CommunityCardType, string> cards = new Dictionary<CommunityCardType, string>
	{
		{ CommunityCardType.FineOrTakeChanceCard, "Choose: Pay a $10 fine or Take Chance Card."},
		{ CommunityCardType.Reward, "Get a Reward: You received a performance bonus. Collect $50 from the bank."},
		{ CommunityCardType.Tax, "Pay Tax: Property taxes have increased. Pay $100 to the bank."},
		{ CommunityCardType.GoToJail, "Go to Jail: You have been arrested by the police! Go directly to Jail. Do not pass Start, do not collect $200."},
		{ CommunityCardType.FreeFromJail, "Free from Jail: Get out of Jail free! Keep this card for future use or sell it for a good price."},
		{ CommunityCardType.BackToLandmark, "Back to Landmark: Go Terminal. If unowned, you may buy it. If owned, pay rent."},
		{ CommunityCardType.HeadToStart, "Head to Start: Go back to Start. Collect $200 from the bank."}
	};

	public CommunityCard(CommunityCardType type, string description)
	{
		_type = type;
		_description = description;
	}
		public string? OpenCard()
	{
		if (cards.TryGetValue(_type, out var value))
		{
			_description = value;
		}
		else
		{
			_description = "Card description not found.";
		}
		return _description;
    
	}
	public static DataContractJsonSerializerSettings Settings = new DataContractJsonSerializerSettings
		{ UseSimpleDictionaryFormat = true };
	public void SerializeChance()
	{
        cards = new Dictionary<CommunityCardType, string>();
		FileStream stream = new FileStream("CommunityCards.json", FileMode.Create);
		using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream,Encoding.UTF8,true,true," "))
		{
			var ser = new DataContractJsonSerializer(typeof(Dictionary<ChanceCardType,string>),Settings);
			ser.WriteObject(writer, cards);
			stream.Flush();
		}
	}
}