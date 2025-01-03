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
}