using System;
using System.Collections.Generic;
using Atharia.Model;

namespace ConsoleDriveyThing {
  public class Displayer {

    static void SetColors(
        out ConsoleColor foregroundColor,
        out ConsoleColor backgroundColor,
        Unit unit) {
      foregroundColor = ConsoleColor.White;
      backgroundColor = ConsoleColor.Black;

      bool isUsingSpecialAbility = false;

      bool isDefending = false;
      foreach (var detail in unit.components) {
        if (detail is ShieldingUCAsIUnitComponent) {
          isDefending = true;
        } else if (detail is CounteringUCAsIUnitComponent) {
          isDefending = true;
        } else if (detail is WanderAICapabilityUCAsIUnitComponent) {
        } else if (detail is AttackAICapabilityUCAsIUnitComponent) {
        } else if (detail is GlaiveAsIUnitComponent) {
        } else if (detail is ArmorAsIUnitComponent) {
        } else if (detail is InertiaRingAsIUnitComponent) {
        } else if (detail is BideAICapabilityUCAsIUnitComponent bideI) {
          if (bideI.obj.charge > 0) {
            isUsingSpecialAbility = true;
          }
        } else if (detail is TimeCloneAICapabilityUCAsIUnitComponent) {
          isUsingSpecialAbility = true;
        } else {
          Asserts.Assert(false, "Unknown: " + detail);
        }
      }

      bool hasArmor = unit.components.GetAllIDefenseItem().Count > 0;
      bool hasGlaive = unit.components.GetAllIOffenseItem().Count > 0;

      bool boostedDefense = isDefending || hasArmor;
      bool boostedOffense = hasGlaive;
      if (isUsingSpecialAbility) {
        backgroundColor = ConsoleColor.Red;
      } else if (boostedDefense && boostedOffense) {
        backgroundColor = ConsoleColor.DarkMagenta;
      } else if (boostedDefense) {
        backgroundColor = ConsoleColor.Cyan;
      } else if (boostedOffense) {
        backgroundColor = ConsoleColor.DarkRed;
      }
    }

    static void GetFloorCharAndColor(
        bool terrainAndFeaturesMode,
        TerrainTile tile,
        out char c,
        out ConsoleColor foregroundColor,
        out ConsoleColor backgroundColor) {

      c = '.';
      foregroundColor = ConsoleColor.DarkGray;
      backgroundColor = ConsoleColor.Black;

      if (!tile.IsWalkable()) {
        c = '#';
        foregroundColor = ConsoleColor.Gray;
        backgroundColor = ConsoleColor.Black;
        return;
      }

      foreach (var tc in tile.components) {
        if (tc is BloodTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            backgroundColor = ConsoleColor.DarkRed;
          }
        } else if (tc is RocksTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            backgroundColor = ConsoleColor.DarkGray;
          }
        } else if (tc is DownstairsTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            c = '>';
          }
        } else if (tc is UpstairsTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            c = '<';
          }
        } else if (tc is CaveTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            c = 'O';
          }
        } else if (tc is HealthPotionAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            c = '!';
            foregroundColor = ConsoleColor.Red;
          }
        } else if (tc is ManaPotionAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            c = '!';
            foregroundColor = ConsoleColor.Blue;
          }
        } else if (tc is TimeAnchorTTCAsITerrainTileComponent) {
          backgroundColor = ConsoleColor.DarkGreen;
        } else if (tc is Atharia.Model.StaircaseTTCAsITerrainTileComponent) {
        } else if (tc is Atharia.Model.GlaiveAsITerrainTileComponent) {
          c = '(';
          foregroundColor = ConsoleColor.Red;
        } else if (tc is Atharia.Model.ArmorAsITerrainTileComponent) {
          c = ']';
          foregroundColor = ConsoleColor.Blue;
        } else if (tc is Atharia.Model.InertiaRingAsITerrainTileComponent) {
          c = '=';
          foregroundColor = ConsoleColor.Yellow;
        } else if (tc is Atharia.Model.WallTTCAsITerrainTileComponent) {
          c = '#';
          foregroundColor = ConsoleColor.Gray;
        } else if (tc is Atharia.Model.StoneTTCAsITerrainTileComponent) {
          c = '.';
          foregroundColor = ConsoleColor.DarkGray;
        } else if (tc is Atharia.Model.CliffLandingTTCAsITerrainTileComponent) {
          c = '.';
          foregroundColor = ConsoleColor.DarkGray;
        } else if (tc is Atharia.Model.MagmaTTCAsITerrainTileComponent) {
          c = '~';
          foregroundColor = ConsoleColor.DarkRed;
        } else if (tc is Atharia.Model.FallsTTCAsITerrainTileComponent) {
          c = '~';
          foregroundColor = ConsoleColor.Blue;
        } else if (tc is Atharia.Model.CliffTTCAsITerrainTileComponent) {
          c = '.';
          foregroundColor = ConsoleColor.DarkYellow;
        } else {
          Asserts.Assert(false, tc.ToString());
        }
      }
      return;
    }

    public static void Display(Game game, bool cursorMode, Location cursor, bool terrainAndFeaturesMode) {
      var liveUnitByLocation = new SortedDictionary<Location, Unit>();
      foreach (var unit in game.level.units) {
        if (unit.alive) {
          liveUnitByLocation[unit.location] = unit;
        }
      }

      Console.WriteLine();
      for (int row = 19; row >= 0; row--) {
        for (int col = 0; col < 80; col++) {
          var loc = new Location(col, row, 0);

          char c = '?';
          ConsoleColor foregroundColor = ConsoleColor.White;
          ConsoleColor backgroundColor = ConsoleColor.Black;

          if (liveUnitByLocation.ContainsKey(loc)) {
            var unit = liveUnitByLocation[loc];
            SetColors(
               out foregroundColor,
               out backgroundColor,
               unit);
            switch (unit.classId) {
              case "chronomancer":
                c = '@';
                break;
              case "avelisk":
                c = 'a';
                break;
              case "draxling":
                c = 'd';
                break;
              case "novafaire":
                c = 'n';
                break;
              case "lornix":
                c = 'l';
                break;
              case "yoten":
                c = 'y';
                break;
              case "vydra":
                c = 'v';
                break;
              case "spiriad":
                c = 's';
                break;
              case "Ravashrike":
                c = 'R';
                break;
            }
          } else {
            if (game.level.terrain.tiles.ContainsKey(loc)) {
              var tile = game.level.terrain.tiles[loc];

              GetFloorCharAndColor(
                  terrainAndFeaturesMode,
                  tile,
                  out c,
                  out foregroundColor,
                  out backgroundColor);
            } else {
              c = ' ';
            }
          }

          if (cursorMode && loc == cursor) {
            backgroundColor = ConsoleColor.DarkYellow;
          }

          Console.BackgroundColor = backgroundColor;
          Console.ForegroundColor = foregroundColor;

          Console.Write(c);

          Console.ResetColor();
        }
        Console.WriteLine();
      }

      string bottomBar = "";
      int hp = 0;
      int maxHp = 0;
      if (game.player.Exists()) {
        hp = game.player.hp;
        maxHp = game.player.maxHp;
      }
      bottomBar += "HP: " + hp + " / " + maxHp;
      while (bottomBar.Length < 19) {
        bottomBar += " ";
      }
      bottomBar += " ";

      int mp = 0;
      int maxMp = 0;
      if (game.player.Exists()) {
        mp = game.player.mp;
        maxMp = game.player.maxMp;
      }
      bottomBar += "MP: " + mp + " / " + maxMp;
      while (bottomBar.Length < 39) {
        bottomBar += " ";
      }
      bottomBar += " ";

      bottomBar += "@ " + game.level.GetName();
      while (bottomBar.Length < 59) {
        bottomBar += " ";
      }
      bottomBar += " ";

      bottomBar += "Time: " + game.time;
      while (bottomBar.Length < 78) {
        bottomBar += " ";
      }
      Console.Write(bottomBar);
    }

  }
}
