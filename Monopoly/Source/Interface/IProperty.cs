using MonopolyProjectSource;

namespace MonopolyProjectInterface;

public interface IProperty
{
	bool AddProperty(Dictionary<Tile,KeyValuePair<string,int>> property);		
	bool SellProperty(Tile property);
	Dictionary<Tile,KeyValuePair<string,int>> GetProperty();
}