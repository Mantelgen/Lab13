namespace Lab13.Domain;

public class ActivePlayer:Entity<long>
{
    public int IDPlayer{set;get;}
    public int IDGame{set;get;}
    public int NrPoints { set; get; }
    public PlayerType PlayerType{set;get;}

    public ActivePlayer(int idPlayer, int idGame, int nrPoints, PlayerType playerType)
    {
        IDPlayer = idPlayer;
        IDGame = idGame;
        NrPoints = nrPoints;
        PlayerType = playerType;
    }
}