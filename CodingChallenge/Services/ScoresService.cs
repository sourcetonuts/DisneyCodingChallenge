using CodingChallenge.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace CodingChallenge.Services
{
    public class ScoresService
    {
        public List<ScoreViewModel> LoadScoresAsync( int year, int month, int day )
        {
            using (var myWebClient = new WebClient())
            {
                var urlscores = $"http://gdx.mlb.com/components/game/mlb/year_{year}/month_{month:00}/day_{day}/master_scoreboard.json";
                var data = myWebClient.DownloadData(urlscores);
                if (data == null)
                    throw new Exception("scores download failed");
                var json = Encoding.UTF8.GetString(data);
                dynamic obj = JsonConvert.DeserializeObject(json);
                var numGames = obj.data.games.game.Count;
                var scores = new List<ScoreViewModel>( numGames );
                for ( int iGame = 0; iGame < numGames ; ++ iGame )
                {
                    var game = obj.data.games.game[iGame];
                    if ( game != null )
                    {
                        int homescore = 0;
                        int awayscore = 0;
                        var innings = game.linescore.inning;
                        var numInnings = innings.Count;
                        for ( int iInning = 0; iInning < innings.Count; ++iInning )
                        {
                            var inningscore = innings[iInning];
                            var home = inningscore.home.Value.Length > 0 ? int.Parse(inningscore.home.Value) : 0;
                            var away = inningscore.away.Value.Length > 0 ? int.Parse(inningscore.away.Value) : 0;
                            homescore += home;
                            awayscore += away;
                        }

                        var score = new ScoreViewModel
                        {
                            Title = $"{game.away_team_name} v {game.home_team_name}",
                            Subtitle = $"{awayscore} : {homescore}",
                            ImageUrl = game.video_thumbnail
                        };
                        scores.Add(score);
                    }
                }
                return scores;
            }
        }
    }
}
