using System;
using System.Diagnostics;
using System.Collections.Generic;
using Atharia.Model;
using IncendianFalls;

namespace ConsoleDriveyThing {
  public class MainPlay {
    static Unit FindUnitAt(Game game, Location destination) {
      foreach (var unit in game.level.units) {
        if (unit.location == destination) {
          return unit;
        }
      }
      return Unit.Null;
    }

    static List<IEffect> HandleDirection(Superstructure serverSS, Game game, Location destination) {
      var unit = FindUnitAt(game, destination);
      if (unit.Exists()) {
        var (effects, result) = serverSS.RequestAttack(game.id, unit.id);
        if (result == "") {
          return effects;
        }
      }

      var (moveEffects, moveResult) = serverSS.RequestMove(game.id, destination);
      if (moveResult.Length > 0) {
        Console.WriteLine(moveResult);
      }
      return moveEffects;
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

    private static List<IEffect> FilterRelevantEffects(List<IEffect> sourceEffects) {
      var result = new List<IEffect>();
      foreach (var sourceEffect in sourceEffects) {
        if (sourceEffect is RandSetRandEffect)
          continue;
        result.Add(sourceEffect);
      }
      return result;
    }

    private delegate void IDisplay();
    private static void ApplyEffectsAndDisplay(Game game, List<IEffect> effects, IDisplay display) {
      int currentActionNum = game.actionNum;

      game.root.Transact(delegate () {
        bool anyEffectsSinceLastDisplay = false;
        var applier = new EffectApplier(game.root);
        for (int i = 0; i < effects.Count; i++) {
          var effect = effects[i];
          if (game.actionNum != currentActionNum) {
            display();
            System.Threading.Thread.Sleep(100);
            currentActionNum = game.actionNum;
          }
          if (effect is GameSetEvventEffect gsee) {
            if (gsee.newValue is RevertedEventAsIGameEvent) {
              display();
              System.Threading.Thread.Sleep(1000);
            }
          }
          applier.Apply(effect);
          anyEffectsSinceLastDisplay = true;
        }

        if (anyEffectsSinceLastDisplay) {
          display();
        }
        return 0;
      });
    }

    public static void Play() {
      var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();

      //int random = (int)timestamp;
      //int random = 134337; // Stairs right next to you
      int random = 1533924206;
      Superstructure serverSS = new Superstructure(new ConsoleLoggers.ConsoleLogger());
      Root clientRoot = new Root(new ConsoleLoggers.ConsoleLogger());
      using (new ReplayLogger(serverSS, new string[] { "Latest.sslog", timestamp + ".sslog" })) {

        bool squareLevelsOnly = true;
        //bool squareLevelsOnly = false;
        var (setupEffects, serverGame) = serverSS.RequestSetupRavaArcanaGame(random, 0, squareLevelsOnly);
        clientRoot.Transact(delegate () {
          foreach (var effect in FilterRelevantEffects(setupEffects)) {
            // Console.WriteLine("Applying " + effect);
            new EffectApplier(clientRoot).Apply(effect);
          }
          return 0;
        });
        var game = clientRoot.GetGame(serverGame.id);

        Location cursor = game.player.location;

        bool cursorMode = false;
        bool terrainAndFeaturesMode = false;
        bool timeAnchoring = false;

        IDisplay display = () => Displayer.Display(game, cursorMode, cursor, game.level.terrain.pattern.GetTileCenter(cursor), terrainAndFeaturesMode);
        display();

        for (bool running = true; running;) {

          while (!game.WaitingOnPlayerInput()) {
            if (!game.player.Exists()) {
              Console.WriteLine("Press any key to continue.");
              Console.ReadKey();
            }
            var (resumeEffects, resumeStatus) = serverSS.RequestResume(game.id);
            Asserts.Assert(resumeStatus == "");
            ApplyEffectsAndDisplay(game, FilterRelevantEffects(resumeEffects), display);
          }
          if (game.player.Exists() && !game.player.Alive()) {
            break;
          }

          float gameTimeBeforeAction = game.time;

          var key = Console.ReadKey();
          //// For some reason, doing this after an input makes the terminal print
          //// much smoother on my mac.
          //System.Threading.Thread.Sleep(33);

          Console.WriteLine();

          switch (key.Key) {
            case ConsoleKey.Delete:
              running = false;
              break;
            case ConsoleKey.I:
              var (interactEffects, result) = serverSS.RequestInteract(game.id);
              ApplyEffectsAndDisplay(game, FilterRelevantEffects(interactEffects), display);
              if (result != "") {
                Console.WriteLine(result);
              }
              break;
            case ConsoleKey.Backspace:
              terrainAndFeaturesMode = !terrainAndFeaturesMode;
              break;
            case ConsoleKey.T:
              var (timeShiftEffects, timeShiftResult) = serverSS.RequestTimeShift(game.id);
              ApplyEffectsAndDisplay(game, FilterRelevantEffects(timeShiftEffects), display);
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
                  var (tamEffects, resultTAM) = serverSS.RequestTimeAnchorMove(game.id, cursor);
                  ApplyEffectsAndDisplay(game, FilterRelevantEffects(tamEffects), display);
                  if (resultTAM != "") {
                    Console.WriteLine(resultTAM);
                    timeAnchoring = false;
                  }
                } else {
                  var (moveEffects, resultM) = serverSS.RequestMove(game.id, cursor);
                  ApplyEffectsAndDisplay(game, FilterRelevantEffects(moveEffects), display);
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
                var unitThere = FindUnitAt(game, cursor);
                if (!unitThere.Exists()) {
                  Console.WriteLine("No unit there!");
                } else {
                  var (fireEffects, fireResult) = serverSS.RequestFire(game.id, unitThere.id);
                  ApplyEffectsAndDisplay(game, FilterRelevantEffects(fireEffects), display);
                  if (fireResult.Length > 0) {
                    Console.WriteLine(fireResult);
                  }
                }
              } else {
                Console.WriteLine("Not firing!");
              }
              break;
            case ConsoleKey.M:
              if (cursorMode) {
                var unitThere = FindUnitAt(game, cursor);
                if (!unitThere.Exists()) {
                  Console.WriteLine("No unit there!");
                } else {
                  var (mireEffects, mireResult) = serverSS.RequestMire(game.id, unitThere.id);
                  ApplyEffectsAndDisplay(game, FilterRelevantEffects(mireEffects), display);
                  if (mireResult.Length > 0) {
                    Console.WriteLine(mireResult);
                  }
                }
              } else {
                Console.WriteLine("Not firing!");
              }
              break;
            case ConsoleKey.S:
              var (defyEffects, resultD) = serverSS.RequestDefy(game.id);
              ApplyEffectsAndDisplay(game, FilterRelevantEffects(defyEffects), display);
              if (resultD != "") {
                Console.WriteLine(resultD);
              }
              break;
            case ConsoleKey.P:
              var (counterEffects, counterResult) = serverSS.RequestCounter(game.id);
              ApplyEffectsAndDisplay(game, FilterRelevantEffects(counterEffects), display);
              if (counterResult.Length > 0) {
                Console.WriteLine(counterResult);
              }
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
                  display();
                } else if (timeAnchoring) {
                  var destination =
                      new Location(
                          game.player.location.groupX + direction.groupX,
                          game.player.location.groupY + direction.groupY,
                          game.player.location.indexInGroup + direction.indexInGroup);
                  var (tamEffects, resultA) = serverSS.RequestTimeAnchorMove(game.id, destination);
                  ApplyEffectsAndDisplay(game, FilterRelevantEffects(tamEffects), display);
                  if (resultA != "") {
                    Console.WriteLine(resultA);
                  }
                  timeAnchoring = false;
                } else {
                  var directionEffects =
                  HandleDirection(
                      serverSS,
                      game,
                      new Location(
                          game.player.location.groupX + direction.groupX,
                          game.player.location.groupY + direction.groupY,
                          game.player.location.indexInGroup + direction.indexInGroup));
                  ApplyEffectsAndDisplay(game, FilterRelevantEffects(directionEffects), display);
                }
              } else {
                Console.WriteLine("Unknown key: " + key.Key);
              }
              break;
          }
        }
      }
    }
  }
}
