using MonopolyProjectSource;

namespace MonopolyProjectInterface;

public interface IPiece
{
	bool SetPiece(PieceType pieceType);
	PieceType GetPiece();
}