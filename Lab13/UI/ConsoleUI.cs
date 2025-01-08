using System.Security.Cryptography;
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
        String input = Console.ReadLine();
        if(!long.TryParse(input, out long result))
            return 0;
        else
            return long.Parse(input);
    }

    private DateTime readDate()
    {
        Console.WriteLine("Data (format: yyyy-mm-dd hh:mm:ss) :");
        String input = Console.ReadLine();
        if(!DateTime.TryParse(input, out DateTime result))
            return DateTime.MinValue;
        else
           return DateTime.Parse(input);
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
                    if (id == 0)
                        Console.WriteLine("Eroare la id!");
                    else
                    {
                        Team team = service.GetTeamFromID(id);
                        foreach (var player in service.GetAllPlayersFromTeam(team))
                        {
                            Console.WriteLine(player.Name + ", " + player.School);
                        }
                    }
                    break;
            case "2":
                    long teamId = readId("echipa");
                    long gameId = readId("meci");
                    if (teamId == 0 || gameId == 0)
                        Console.WriteLine("Eroare la id-uri!");
                    else
                    {
                        Team team = service.GetTeamFromID(teamId);
                        Game game = service.GetGameFromID(gameId);
                        foreach (var player in service.GetACtivePlayersFromTeamAndGame(team, game))
                        {
                            Console.WriteLine(player.Name + ", " + player.School);
                        }
                    }
                    break;
                case "3":
                    DateTime start = readDate();
                    DateTime end = readDate();
                    if(start>end || start == DateTime.MinValue || end == DateTime.MinValue)
                        Console.WriteLine("Eroare la date!");
                    else
                    {
                        foreach (var game in service.GetAllGamesInPeriod(start, end))
                        {
                            Team team1 = service.GetTeamFromID(game.Team1);
                            Team team2 = service.GetTeamFromID(game.Team2);
                            Console.WriteLine(team1.Name + " vs " + team2.Name + " din " + game.StartDate);
                        }
                    }
                    break;
                case "4":
                    long gameId2= readId("meci");
                    if (gameId2 == 0)
                        Console.WriteLine("Eroare la id!");
                    Game game2 = service.GetGameFromID(gameId2);
                    if (game2 == null)
                        Console.WriteLine("Nu exista meciul cerut!");
                    else
                    {
                        Team team12 = service.GetTeamFromID(game2.Team1);
                        Team team22 = service.GetTeamFromID(game2.Team2);
                        Console.WriteLine($"Scorul meciului dintre {team12.Name} si {team22.Name} din {game2.StartDate} este {service.CalculateGameScore(game2)}");
                    }
                    
                    break;
                default:
                    Console.WriteLine("Opps! Wrong command! Try again!");
                    break;
            }
        }
        
    }
}