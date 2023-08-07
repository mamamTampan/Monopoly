namespace MonopolyProjectInterface;

public interface IPlayer
{
    bool SetName(string name);
    string? GetName();
    bool SetId(int id);
    int GetId();
}   