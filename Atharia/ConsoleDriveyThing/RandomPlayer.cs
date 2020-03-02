using System;
using System.Diagnostics;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace ConsoleDriveyThing {
  class RandomPlayer {
    static string HandleDirection(Superstructure ss, Game game, Location destination) {
      var unitByLocation = new SortedDictionary<Location, Unit>();
      foreach (var unit in game.level.units) {
        if (unit.location == destination) {
          return ss.RequestAttack(game.id, unit.id);
        }
      }

      return ss.RequestMove(game.id, destination);
    }

    public static void Play() {
      var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

      int seed = (int)timestamp;
      Superstructure ss = new Superstructure(new ConsoleLoggers.ConsoleLogger());
      new ReplayLogger(ss, new string[] { "Latest.sslog", timestamp + ".sslog" });

      Console.WriteLine("Seed: " + seed);

      var random = new Random(seed);

      var root = ss.GetRoot();

      var game = ss.RequestSetupGauntletGame(seed, false);
      var superstate = ss.GetSuperstate(game);
      var player = game.player;

      // todo: differentiate between hunt mode and combat mode.

      for (int i = 0; i < 100; i++) {
        while (game.player.alive &&
            ss.GetSuperstate(game).GetStateType() != MultiverseStateType.kBeforePlayerInput) {
          ss.RequestResume(game.id);
        }

        if (!game.player.alive) {
          Console.WriteLine("Died!");
          break;
        }

        float gameTimeBeforeAction = game.time;

        if (superstate.GetStateType() == MultiverseStateType.kBeforePlayerResume) {
          System.Threading.Thread.Sleep(300);
          ss.RequestFollowDirective(game.id);
          ss.RequestResume(game.id);
        } else {

          // 0: defend
          // 1: time shift back by 1-9 turns
          // 2-X: Go to that nearby location
          List<Location> nearbyLocations =
              game.level.terrain.GetAdjacentExistingLocations(
                  game.player.location, game.level.ConsiderCornersAdjacent());
          int numPossibleActions = 2 + nearbyLocations.Count;

          int action = random.Next() % numPossibleActions;

          if (action == 0) {
            string defendResult = ss.RequestDefend(game.id);
            if (defendResult == "") {
              ss.RequestResume(game.id);
            } else { 
              Console.WriteLine(defendResult);
            }
          } else if (action == 1) {
            int numTurnsBackward = 1 + random.Next() % 9;

            for (int j = 0; j < numTurnsBackward; j++) {
              string timeShiftError = ss.RequestTimeShift(game.id);
              if (timeShiftError.Length > 0) {
                Console.WriteLine(timeShiftError);
                break;
              }
            }
          } else {
            int locationIndex = action - 2;
            Location location = nearbyLocations[locationIndex];

            string result = HandleDirection(ss, game, location);
            if (result.Length == 0) {
              ss.RequestResume(game.id);
            } else {
              Console.WriteLine(result);
              break;
            }
          }
        }
      }
    }
  }
}
