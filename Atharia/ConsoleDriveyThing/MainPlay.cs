using System;
using System.Diagnostics;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace ConsoleDriveyThing {
  public class MainPlay {
    static Unit FindUnitAt(Superstructure ss, Game game, Location destination) {
      foreach (var unit in game.level.units) {
        if (unit.location == destination) {
          return unit;
        }
      }
      return Unit.Null;
    }

    static void HandleDirection(Superstructure ss, Game game, Location destination) {
      var unit = FindUnitAt(ss, game, destination);
      if (unit.Exists()) {
        string result = ss.RequestAttack(game.id, unit.id);
        if (result == "") {
          return;
        }
      }

      string moveResult = ss.RequestMove(game.id, destination);
      if (moveResult.Length > 0) {
        Console.WriteLine(moveResult);
      }
    }

    static bool KeyToDirection(ConsoleKey key, out Location direction) {
      switch (key) {
        case ConsoleKey.Q:
          direction = new Location(-1, 1, 0);
          return true;
        case ConsoleKey.W:
          direction = new Location(0, 1, 0);
          return true;
        case ConsoleKey.E:
          direction = new Location(1, 1, 0);
          return true;
        case ConsoleKey.A:
          direction = new Location(-1, 0, 0);
          return true;
        case ConsoleKey.S:
          direction = new Location(0, 0, 0);
          return true;
        case ConsoleKey.D:
          direction = new Location(1, 0, 0);
          return true;
        case ConsoleKey.Z:
          direction = new Location(-1, -1, 0);
          return true;
        case ConsoleKey.X:
          direction = new Location(0, -1, 0);
          return true;
        case ConsoleKey.C:
          direction = new Location(1, -1, 0);
          return true;
      }
      direction = new Location(0, 0, 0);
      return false;
    }

    public static void Play() {
      var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

      //int random = (int)timestamp;
      //int random = 134337; // Stairs right next to you
      int random = 1525224206;
      Superstructure ss = new Superstructure(new ConsoleLoggers.ConsoleLogger());
      new ReplayLogger(ss, new string[] { "Latest.sslog", timestamp + ".sslog" });

      var root = ss.GetRoot();

      //root.Unlock();
      //for (int l = 0; l < GenerationCommon.TOTAL_NUM_LEVELS_BEFORE_BOSS; l++) {
      //  float[] distTotals = new float[7];
      //  for (int r = 0; r < 1000; r++) {
      //    int[] dist =
      //        GenerationCommon.DetermineUnitsForLevel(
      //            root.EffectRandCreate(random), l, GenerationCommon.NUM_UNITS_PER_LEVEL);
      //    for (int i = 0; i < 7; i++) {
      //      distTotals[i] += dist[i];
      //    }
      //  }
      //  for (int i = 0; i < 7; i++) {
      //    distTotals[i] /= 1000;
      //  }

      //  Console.WriteLine(
      //      l + "\t" +
      //      "av: " + Math.Round(distTotals[0], 2) + "\t" +
      //      "ra: " + Math.Round(distTotals[1], 2) + "\t" +
      //      "dr: " + Math.Round(distTotals[2], 2) + "\t" +
      //      "lo: " + Math.Round(distTotals[3], 2) + "\t" +
      //      "yo: " + Math.Round(distTotals[4], 2) + "\t" +
      //      "sp: " + Math.Round(distTotals[5], 2) + "\t" +
      //      "mo: " + Math.Round(distTotals[6], 2));
      //}
      //root.Lock();
      //return;

      //bool squareLevelsOnly = true;
      bool squareLevelsOnly = false;
      var game = ss.RequestSetupGame(random, squareLevelsOnly, false);
      var superstate = ss.GetSuperstate(game);

      game.level.units.AddObserver(new ConsoleLoggers.UnitsLogger());
      Location cursor = game.player.location;

      bool cursorMode = false;
      bool terrainAndFeaturesMode = false;
      bool timeAnchoring = false;

      Displayer.Display(game, cursorMode, cursor, terrainAndFeaturesMode);

      for (bool running = true; running;) {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        for (bool crunching = true; crunching;) {
          if (game.player.Exists() && !game.player.alive) {
            return;
          }

          switch (superstate.GetStateType()) {
            case MultiverseStateType.kAfterUnitAction:
            case MultiverseStateType.kBeforeEnemyAction:
            case MultiverseStateType.kPreActingDetail:
            case MultiverseStateType.kPostActingDetail:
            case MultiverseStateType.kBetweenUnits:
              ss.RequestResume(game.id);
              break;
            default:
              crunching = false;
              break;
          }
        }

        sw.Stop();
        Console.WriteLine("Total elapsed time for turn: " + sw.Elapsed.TotalMilliseconds);

        Displayer.Display(game, cursorMode, cursor, terrainAndFeaturesMode);

        if (game.player.Exists() && !game.player.alive) {
          break;
        }

        float gameTimeBeforeAction = game.time;

        switch (superstate.GetStateType()) {
          case MultiverseStateType.kTimeshiftingBackward:
          case MultiverseStateType.kTimeshiftingCloneMoving:
          case MultiverseStateType.kTimeshiftingAfterCloneMoved:
            System.Threading.Thread.Sleep(300);
            string resultT = ss.RequestTimeShift(game.id);
            if (resultT != "") {
              Console.WriteLine(resultT);
            }
            break;
          case MultiverseStateType.kBeforePlayerResume:
            System.Threading.Thread.Sleep(300);
            string resultF = ss.RequestFollowDirective(game.id);
            if (resultF != "") {
              Console.WriteLine(resultF);
            }
            break;
          case MultiverseStateType.kBeforePlayerInput:
            var key = Console.ReadKey();
            // For some reason, doing this after an input makes the terminal print
            // much smoother on my mac.
            System.Threading.Thread.Sleep(33);

            Console.WriteLine();

            switch (key.Key) {
              case ConsoleKey.Delete:
                running = false;
                break;
              case ConsoleKey.I:
                string result = ss.RequestInteract(game.id);
                if (result != "") {
                  Console.WriteLine(result);
                }
                break;
              case ConsoleKey.M:
                terrainAndFeaturesMode = !terrainAndFeaturesMode;
                break;
              case ConsoleKey.T:
                string timeShiftResult = ss.RequestTimeShift(game.id);
                if (timeShiftResult.Length > 0) {
                  Console.WriteLine(timeShiftResult);
                }
                break;
              case ConsoleKey.B:
                Asserts.Assert(!timeAnchoring);
                timeAnchoring = true;
                break;
              case ConsoleKey.L:
                cursorMode = !cursorMode;
                cursor = game.player.location;
                break;
              case ConsoleKey.N:
                if (cursorMode) {
                  if (timeAnchoring) {
                    string resultTAM = ss.RequestTimeAnchorMove(game.id, cursor);
                    if (resultTAM != "") {
                      Console.WriteLine(resultTAM);
                      timeAnchoring = false;
                    }
                  } else {
                    string resultM = ss.RequestMove(game.id, cursor);
                    if (resultM != "") {
                      Console.WriteLine(resultM);
                    }
                  }
                } else {
                  Console.WriteLine("Not navigating!");
                }
                break;
              case ConsoleKey.F:
                if (cursorMode) {
                  var unitThere = FindUnitAt(ss, game, cursor);
                  if (!unitThere.Exists()) {
                    Console.WriteLine("No unit there!");
                  } else {
                    string fireResult = ss.RequestFire(game.id, unitThere.id);
                    if (fireResult.Length > 0) {
                      Console.WriteLine(fireResult);
                    }
                  }
                } else {
                  Console.WriteLine("Not firing!");
                }
                break;
              case ConsoleKey.S:
                string resultD = ss.RequestDefend(game.id);
                if (resultD != "") {
                  Console.WriteLine(resultD);
                }
                ss.RequestResume(game.id);
                break;
              case ConsoleKey.P:
                var counterResult = ss.RequestCounter(game.id);
                if (counterResult.Length > 0) {
                  Console.WriteLine(counterResult);
                }
                ss.RequestResume(game.id);
                break;
              default:
                Location direction;
                if (KeyToDirection(key.Key, out direction)) {
                  if (cursorMode) {
                    cursor =
                        new Location(
                            cursor.groupX + direction.groupX,
                            cursor.groupY + direction.groupY,
                            cursor.indexInGroup + direction.indexInGroup);
                  } else if (timeAnchoring) {
                    var destination =
                        new Location(
                            game.player.location.groupX + direction.groupX,
                            game.player.location.groupY + direction.groupY,
                            game.player.location.indexInGroup + direction.indexInGroup);
                    string resultA = ss.RequestTimeAnchorMove(game.id, destination);
                    if (resultA != "") {
                      Console.WriteLine(resultA);
                    }
                    timeAnchoring = false;
                  } else {
                    HandleDirection(
                        ss,
                        game,
                        new Location(
                            game.player.location.groupX + direction.groupX,
                            game.player.location.groupY + direction.groupY,
                            game.player.location.indexInGroup + direction.indexInGroup));
                  }
                } else {
                  Console.WriteLine("Unknown key: " + key.Key);
                }
                break;
            }

            break;
        }
      }
    }
  }
}
