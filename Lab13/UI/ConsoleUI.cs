using Lab13.Domain;
using Lab13.Service;

namespace Lab13.UI;

public class ConsoleUI
{
    GeneralService service;

    public ConsoleUI(GeneralService service)
    {
        this.service = service;
    }
    private long readId(String name)
    {
        Console.WriteLine($"Id-ul pentru {name} :");
        return long.Parse(Console.ReadLine());
    }

    private DateTime readDate()
    {
        Console.WriteLine("Data (format: yyyy-mm-dd hh:mm:ss) :");
        return DateTime.Parse(Console.ReadLine());
    }

    public void run()
    {
        while (true)
        {
            Console.Write(">>>");
            String cmd = Console.ReadLine();
            switch (cmd)
            {
                case "help":
                    Console.WriteLine();
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("Comenzile sunt :");
                    Console.WriteLine("1 - Afisam toti jucatori unei echipe date");
                    Console.WriteLine("2 - Afisam toti jucatorii activi ai unei echipe de la un anumit meci");
                    Console.WriteLine("3 - Sa se afiseze toate meciurile dintr-o anumita perioada calendaristica");
                    Console.WriteLine("4 - Sa se afiseze scorul de la un anumit meci");
                    Console.WriteLine("exit - exit");
                    Console.WriteLine("help - help");
                    Console.WriteLine("--------------------------------------------------");
                    Console.WriteLine("\n");
                    break;
                case "exit":
                    return;
                case "1":
                    long id = readId("echipa");
                    foreach (var player in service.GetAllPlayersFromTeam(id))
                    {
                        Console.WriteLine(player.Name+", "+player.School);
                    }
                    break;
                case "2":
                    long teamId = readId("echipa");
                    long gameId = readId("meci");
                    foreach (var player in service.GetACtivePlayersFromTeamAndGame(teamId, gameId))
                    {
                        Console.WriteLine(player.Name + ", " + player.School);
                    }
                    break;
                case "3":
                    DateTime start = readDate();
                    DateTime end = readDate();
                    foreach (var game in service.GetAllGamesInPeriod(start, end))
                    {
                        Team team1 = service.GetTeamFromID(game.Team1);
                        Team team2 = service.GetTeamFromID(game.Team2);
                        Console.WriteLine(team1.Name + " vs " + team2.Name + " din " + game.StartDate);
                    }
                    break;
                case "4":
                    long gameId2= readId("meci");
                    Game game2 = service.GetGameFromID(gameId2);
                    Team team12 = service.GetTeamFromID(game2.Team1);
                    Team team22 = service.GetTeamFromID(game2.Team2);
                    Console.WriteLine($"Scorul meciului dintre {team12.Name} si {team22.Name} din {game2.StartDate} este {service.CalculateGameScore(gameId2)}");
                    break;
                default:
                    Console.WriteLine("Opps! Wrong command! Try again!");
                    break;
            }
        }
        
    }
}