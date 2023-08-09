using System.Runtime.Serialization.Json;
using System.Text;
using MonopolyProjectInterface;

namespace MonopolyProjectSource;
public class ChanceCard : ICard
{
    private ChanceCardType _type;
    private string? _description;
    private static Dictionary<ChanceCardType, string> cards = new()
    {
	    { ChanceCardType.Fine, "Pay a Fine: You violated traffic rules. Pay a $15 fine." },
	    { ChanceCardType.Reward, "Get a Reward: You received a performance bonus. Collect $50 from the bank." },
	    { ChanceCardType.Tax, "Pay Tax: Property taxes have increased. Pay $100 to the bank." },
	    { ChanceCardType.GoToJail, "Go to Jail: You have been arrested by the police! Go directly to Jail. Do not pass Start, do not collect $200."},
	    { ChanceCardType.FreeFromJail, "Free from Jail: Get out of Jail free! Keep this card for future use or sell it for a good price."},
	    { ChanceCardType.HeadToLandmark, "Head to Landmark: Advance to the Indonesia. If unowned, you may buy it. If owned, pay rent."},
	    { ChanceCardType.BackToLandmark, "Back to Landmark: Go to the Station. If unowned, you may buy it. If owned, pay rent."},
	    { ChanceCardType.StepForward, "Step Forward: Advance 3 spaces. If you pass Go, collect $200." },
	    { ChanceCardType.StepBack, "Step Back: Move back 3 spaces. If you land on Go, collect $50." },
	    { ChanceCardType.HeadToStart, "Head to Start: Go back to Start. Collect $200 from the bank." }
    };
    public ChanceCard(ChanceCardType type, string? description)
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
        cards = new Dictionary<ChanceCardType, string>();
        FileStream stream = new FileStream("ChanceCards.json", FileMode.Create);
        using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream,Encoding.UTF8,true,true," "))
        {
            var ser = new DataContractJsonSerializer(typeof(Dictionary<ChanceCardType,string>),Settings);
            ser.WriteObject(writer, cards);
            stream.Flush();
        }
    }
}