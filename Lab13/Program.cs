// See https://aka.ms/new-console-template for more information
using Lab13;
using Lab13.Domain;
using System;
using System.Data;
using System.Data.Common;
using Lab13.Repository.DataBaseRepository;
using Lab13.Service;
using Lab13.UI;
using Npgsql;
    
String host = "localhost";
String user = "postgres";
String password = "cosmin2004"; 
String database = "NBALeagueRomania";
int port = 5433;

ActivePlayerRepo activePlayersRepo = new ActivePlayerRepo(null,host,user,password,database,port,"activeplayer");
GamesRepo gamesRepo = new GamesRepo(null,host,user,password,database,port,"games");
PlayerRepo playersRepo = new PlayerRepo(null,host,user,password,database,port,"players");
StudentRepo studentRepo = new StudentRepo(null,host,user,password,database,port,"students");
TeamRepo teamRepo = new TeamRepo(null,host,user,password,database,port,"team");
GeneralService service = new GeneralService(gamesRepo,activePlayersRepo,playersRepo,teamRepo,studentRepo);
ConsoleUI ui = new ConsoleUI(service);
ui.run();



