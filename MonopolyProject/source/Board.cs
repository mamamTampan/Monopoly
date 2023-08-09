using System.Runtime.Serialization.Json;
using System.Text;

namespace MonopolyProjectSource;

public class Board
{
	private List<Tile> tiles;
	public Board()
	{
		tiles = new List<Tile>();
		CreatingBoard();
	}
	public List<Tile> CreatingBoard()
	{
		tiles.Add(new StartTile("GO !!!", 1,"Collect $200 for salary"));
		tiles.Add(new LandmarkTile("Malaysia", 2,"Price $60"));
		tiles.Add(new CommunityTile("Community Chest", 3,"Take a Community Card"));
		tiles.Add(new LandmarkTile("Thailand", 4,"Price $60"));
		tiles.Add(new TaxTile("Income Tax", 5,"Pay $200"));
		tiles.Add(new PublicPlaceTile("Terminal", 6,"Price $200"));
		tiles.Add(new LandmarkTile("Singapore", 7,"Price $100"));
		tiles.Add(new ChanceTile("Chance", 8,"Take a Chance Card"));
		tiles.Add(new LandmarkTile("Hong Kong", 9,"Price $100"));
		tiles.Add(new LandmarkTile("India", 10,"Price $120"));
		tiles.Add(new InJailTile("Jail", 11,"Just Visited"));
		tiles.Add(new LandmarkTile("Saudi Arabia", 12,"Price $140"));
		tiles.Add(new PublicPlaceTile("Electric Company", 13,"Price $150"));
		tiles.Add(new LandmarkTile("Egypt", 14,"Price $140"));
		tiles.Add(new LandmarkTile("Africa", 15,"Price $160"));
		tiles.Add(new PublicPlaceTile("Station", 16,"Price $200"));
		tiles.Add(new LandmarkTile("China", 17,"Price $180"));
		tiles.Add(new CommunityTile("Community Chest", 18,"Take a Community Card"));
		tiles.Add(new LandmarkTile("Korea", 19,"Price $180"));
		tiles.Add(new LandmarkTile("Japan", 20,"Price $200"));
		tiles.Add(new FreeParkingTile("Free Parking", 21,"U can enjoy until ur turn"));
		tiles.Add(new LandmarkTile("Finland", 22,"Price $220"));
		tiles.Add(new ChanceTile("Chance", 23,"Take a Chance Card"));
		tiles.Add(new LandmarkTile("New Zealand", 24,"Price $220"));
		tiles.Add(new LandmarkTile("Australia", 25,"Price $240"));
		tiles.Add(new PublicPlaceTile("Port", 26,"Price $200"));
		tiles.Add(new LandmarkTile("Italy", 27,"Price $260"));
		tiles.Add(new LandmarkTile("England", 28,"Price $260"));
		tiles.Add(new PublicPlaceTile("Water Works", 29,"Price $150"));
		tiles.Add(new LandmarkTile("France", 30,"Price $280"));
		tiles.Add(new GoToJailTile("Go To Jail", 31,"Move to Jail"));
		tiles.Add(new LandmarkTile("Mexico", 32,"Price $300"));
		tiles.Add(new LandmarkTile("Brazil", 33,"Price $300"));
		tiles.Add(new CommunityTile("Community Chest", 34,"Take a Community Card"));
		tiles.Add(new LandmarkTile("America", 35,"Price $320"));
		tiles.Add(new PublicPlaceTile("Airport", 36,"Price $200"));
		tiles.Add(new ChanceTile("Chance", 37,"Take a Chance Card"));
		tiles.Add(new LandmarkTile("Dubai", 38,"Price $350"));
		tiles.Add(new TaxTile("Luxury Tax", 39,"Pay $100"));
		tiles.Add(new LandmarkTile("Indonesia ", 40,"Price $400"));
		return tiles;
		
	}
		
	public static DataContractJsonSerializerSettings Settings = new DataContractJsonSerializerSettings
		{ UseSimpleDictionaryFormat = true };
	public void SerializeChance()
	{
		tiles = new List<Tile>();
		FileStream stream = new FileStream("TilesOnBoard.json", FileMode.Create);
		using (var writer = JsonReaderWriterFactory.CreateJsonWriter(stream,Encoding.UTF8,true,true," "))
		{
			var ser = new DataContractJsonSerializer(typeof(Dictionary<ChanceCardType,string>),Settings);
			ser.WriteObject(writer, tiles);
			stream.Flush();
		}
	}
}
