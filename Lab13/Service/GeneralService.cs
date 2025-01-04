using Lab13.Domain;
using Lab13.Repository;

namespace Lab13.Service;

public class GeneralService
{
    private IRepository<long,Game> gamesRepository;
    private IRepository<long,ActivePlayer> activePlayersRepository;
    private IRepository<long,Player> playersRepository;
    private IRepository<long,Team> teamsRepository;
    private IRepository<long,Student> studentsRepository;

    public GeneralService(IRepository<long, Game> gamesRepository, IRepository<long, ActivePlayer> activePlayersRepository, IRepository<long, Player> playersRepository, IRepository<long, Team> teamsRepository, IRepository<long, Student> studentsRepository)
    {
        this.gamesRepository = gamesRepository;
        this.activePlayersRepository = activePlayersRepository;
        this.playersRepository = playersRepository;
        this.teamsRepository = teamsRepository;
        this.studentsRepository = studentsRepository;
    }

    public IEnumerable<Player> GetAllPlayersFromTeam(long teamID)
    {
        //Team team = teamsRepository.FindOne(teamID);
        return
            from player in playersRepository.FindAll()
            where player.Team == teamID
                select player;
    }

    public IEnumerable<Player> GetACtivePlayersFromTeamAndGame(long teamID, long gameID)
    {
      
        return 
            from activePlayer in activePlayersRepository.FindAll()
            join player in playersRepository.FindAll() on activePlayer.IDPlayer equals player.ID
            where activePlayer.IDGame == gameID && player.Team == teamID
            select player;
    }

    public IEnumerable<Game> GetAllGamesInPeriod(DateTime start, DateTime end)
    {
        return 
            from game in gamesRepository.FindAll()
            where game.StartDate >= start && game.StartDate <= end
            select game;
    }

    public int CalculateScore(Team team,Game game)
    {
        return 
            (from activePlayer in activePlayersRepository.FindAll() 
            join player in playersRepository.FindAll() on activePlayer.IDPlayer equals player.ID
            join team2 in teamsRepository.FindAll() on player.Team equals team2.ID 
            where activePlayer.IDGame==game.ID && team2.ID == team.ID 
                select activePlayer.NrPoints).Sum();
    }

    public String CalculateGameScore(long gameID)
    {
        Game game = gamesRepository.FindOne(gameID);
        Team team1 = teamsRepository.FindOne(game.Team1);
        Team team2 = teamsRepository.FindOne(game.Team2);
        
        int scoreTeam1 = CalculateScore(team1, game);
        int scoreTeam2 = CalculateScore(team2, game);
        return scoreTeam1.ToString()+" - " + scoreTeam2.ToString();
    }

    public Team GetTeamFromID(long teamID)
    {
        return teamsRepository.FindOne(teamID);
    }

    public Game GetGameFromID(long gameID)
    {
        return gamesRepository.FindOne(gameID);
    }
}