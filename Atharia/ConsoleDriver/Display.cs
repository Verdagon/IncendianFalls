using System;
using System.Collections.Generic;
using Atharia.Model;

namespace ConsoleDriveyThing {
  public class Displayer {
    const int DISPLAY_WIDTH = 80;
    const int DISPLAY_HEIGHT = 20;

    static void GetUnitColors(
        out ConsoleColor foregroundColor,
        out ConsoleColor backgroundColor,
        Unit unit) {
      foregroundColor = ConsoleColor.White;
      backgroundColor = ConsoleColor.Black;

      bool isUsingSpecialAbility = false;

      bool isDefying = false;
      foreach (var detail in unit.components) {
        if (detail is DefyingUCAsIUnitComponent) {
          isDefying = true;
        } else if (detail is CounteringUCAsIUnitComponent) {
          isDefying = true;
        } else if (detail is WanderAICapabilityUCAsIUnitComponent) {
        } else if (detail is AttackAICapabilityUCAsIUnitComponent) {
        } else if (detail is GlaiveAsIUnitComponent) {
        } else if (detail is ArmorAsIUnitComponent) {
        } else if (detail is SorcerousUCAsIUnitComponent) {
        } else if (detail is BaseCombatTimeUCAsIUnitComponent) {
        } else if (detail is BaseMovementTimeUCAsIUnitComponent) {
        } else if (detail is BaseOffenseUCAsIUnitComponent) {
        } else if (detail is BaseDefenseUCAsIUnitComponent) {
        } else if (detail is SpeedRingAsIUnitComponent) {
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

      bool hasArmor = unit.components.GetAllArmor().Count > 0;
      bool hasGlaive = unit.components.GetAllGlaive().Count > 0;

      bool boostedDefense = isDefying || hasArmor;
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

    static char GetUnitChar(Unit unit) {
      switch (unit.classId) {
        case "Chronomancer":
          return '@';
        case "avelisk":
          return 'a';
        case "draxling":
          return 'd';
        case "novafaire":
          return 'n';
        case "lornix":
          return 'l';
        case "yoten":
          return 'y';
        case "vydra":
          return 'v';
        case "spiriad":
          return 's';
        case "Ravashrike":
          return 'R';
        default:
          return '?';
      }
    }

    static void PaintUnit(Unit unit, Cell cell) {
      char c = GetUnitChar(unit);
      GetUnitColors(out var foregroundColor, out var backgroundColor, unit);
      if (backgroundColor == ConsoleColor.Black) {
        cell.Paint(c, foregroundColor);
      } else {
        cell.Paint(c, foregroundColor, backgroundColor);
      }
    }

    delegate void Painter();

    static void PaintTerrain(
        bool terrainAndFeaturesMode,
        TerrainTile tile,
        Cell cell) {

      if (!tile.IsWalkable()) {
        cell.Paint('#', ConsoleColor.Gray, ConsoleColor.Black);
        return;
      }

      var terrainPainters = new List<Painter>();
      var featurePainters = new List<Painter>();
      var itemPainters = new List<Painter>();

      foreach (var tc in tile.components) {
        bool recognized = false;
        if (tc is WallTTCAsITerrainTileComponent) {
          terrainPainters.Add(() => cell.Paint('#', ConsoleColor.Gray));
          recognized = true;
        }
        if (tc is StoneTTCAsITerrainTileComponent) {
          terrainPainters.Add(() => cell.Paint('.', ConsoleColor.DarkGray));
          recognized = true;
        }
        if (tc is MudTTCAsITerrainTileComponent) {
          terrainPainters.Add(() => cell.Paint('.', ConsoleColor.DarkGray));
          recognized = true;
        }
        if (tc is CliffLandingTTCAsITerrainTileComponent) {
          terrainPainters.Add(() => cell.Paint('.', ConsoleColor.DarkGray));
          recognized = true;
        }
        if (tc is MagmaTTCAsITerrainTileComponent) {
          terrainPainters.Add(() => cell.Paint('~', ConsoleColor.DarkRed));
          recognized = true;
        }
        if (tc is FallsTTCAsITerrainTileComponent) {
          terrainPainters.Add(() => cell.Paint('~', ConsoleColor.Blue));
          recognized = true;
        }
        if (tc is CliffTTCAsITerrainTileComponent) {
          terrainPainters.Add(() => cell.Paint('.', ConsoleColor.DarkYellow));
          recognized = true;
        }
        if (tc is RavaNestTTCAsITerrainTileComponent) {
          terrainPainters.Add(() => cell.Paint('.', ConsoleColor.DarkMagenta));
          recognized = true;
        }
        if (tc is BloodTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            featurePainters.Add(() => cell.Paint(ConsoleColor.DarkRed));
          }
          recognized = true;
        }
        if (tc is RocksTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            featurePainters.Add(() => cell.Paint(ConsoleColor.DarkGray));
          }
          recognized = true;
        }
        if (tc is ObsidianTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            featurePainters.Add(() => cell.Paint(ConsoleColor.Black));
          }
          recognized = true;
        }
        if (tc is DownStairsTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            featurePainters.Add(() => cell.Paint('>', ConsoleColor.Gray));
          }
          recognized = true;
        }
        if (tc is UpStairsTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            featurePainters.Add(() => cell.Paint('<', ConsoleColor.Gray));
          }
          recognized = true;
        }
        if (tc is CaveTTCAsITerrainTileComponent) {
          if (!terrainAndFeaturesMode) {
            terrainPainters.Add(() => cell.Paint('O', ConsoleColor.DarkGray));
          }
          recognized = true;
        }
        if (tc is ItemTTCAsITerrainTileComponent itemTTCAsITTC) {
          if (!terrainAndFeaturesMode) {
            var item = itemTTCAsITTC.obj.item;
            if (item is HealthPotionAsIItem) {
              itemPainters.Add(() => cell.Paint('!', ConsoleColor.Red));
              recognized = true;
            } else if (item is ManaPotionAsIItem) {
              itemPainters.Add(() => cell.Paint('!', ConsoleColor.Blue));
              recognized = true;
            } else if (item is GlaiveAsIItem) {
              itemPainters.Add(() => cell.Paint('(', ConsoleColor.Red));
              recognized = true;
            } else if (item is ArmorAsIItem) {
              itemPainters.Add(() => cell.Paint(']', ConsoleColor.Blue));
              recognized = true;
            } else if (item is SpeedRingAsIItem) {
              itemPainters.Add(() => cell.Paint('=', ConsoleColor.Yellow));
              recognized = true;
            }
          }
        }
        if (tc is TimeAnchorTTCAsITerrainTileComponent) {
          featurePainters.Add(() => cell.Paint(ConsoleColor.DarkGreen));
          recognized = true;
        }
        if (tc is GrassTTCAsITerrainTileComponent) {
          featurePainters.Add(() => cell.Paint(ConsoleColor.Green));
          recognized = true;
        }
        if (tc is FireTTCAsITerrainTileComponent) {
          featurePainters.Add(() => cell.Paint(ConsoleColor.Red));
          recognized = true;
        }
        if (tc is LevelLinkTTCAsITerrainTileComponent) {
          recognized = true;
        }
        if (tc is EmberDeepLevelLinkerTTCAsITerrainTileComponent) {
          recognized = true;
        }
        Asserts.Assert(recognized, tc.ToString());
      }

      foreach (var painter in terrainPainters) {
        painter();
      }
      foreach (var painter in featurePainters) {
        painter();
      }
      foreach (var painter in itemPainters) {
        painter();
      }
    }

    class Cell {
      char c;
      ConsoleColor foregroundColor;
      ConsoleColor backgroundColor;

      public Cell(
          char c,
          ConsoleColor foregroundColor,
          ConsoleColor backgroundColor) {
        this.c = c;
        this.foregroundColor = foregroundColor;
        this.backgroundColor = backgroundColor;
      }

      public void Paint(char c, ConsoleColor foregroundColor) {
        this.c = c;
        this.foregroundColor = foregroundColor;
      }
      public void Paint(ConsoleColor backgroundColor) {
        this.backgroundColor = backgroundColor;
      }
      public void Paint(char c, ConsoleColor foregroundColor, ConsoleColor backgroundColor) {
        this.c = c;
        this.foregroundColor = foregroundColor;
        this.backgroundColor = backgroundColor;
      }

      public void Display() {
        Console.BackgroundColor = backgroundColor;
        Console.ForegroundColor = foregroundColor;

        Console.Write(c);

        Console.ResetColor();
      }
    }

    public static void Display(Game game, bool cursorMode, Location cursor, Vec2 cameraCenter, bool terrainAndFeaturesMode) {
      Cell[,] map = new Cell[DISPLAY_WIDTH, 20]; // x then y
      for (int x = 0; x < DISPLAY_WIDTH; x++) {
        for (int y = 0; y < DISPLAY_HEIGHT; y++) {
          map[x, y] = new Cell(' ', ConsoleColor.Black, ConsoleColor.Black);
        }
      }
      foreach (var locationAndTile in game.level.terrain.tiles) {
        var location = locationAndTile.Key;

        var tile = locationAndTile.Value;
        var tileCenter = game.level.terrain.pattern.GetTileCenter(location);
        var positionOnScreen = tileCenter.minus(cameraCenter).plus(new Vec2(DISPLAY_WIDTH / 2 - 1, DISPLAY_HEIGHT / 2 - 1));
        if (positionOnScreen.x < 0 || positionOnScreen.y < 0 || positionOnScreen.x >= map.GetLength(0) || positionOnScreen.y >= map.GetLength(1)) {
          continue;
        }
        var cell = map[(int)positionOnScreen.x, (int)positionOnScreen.y];

        PaintTerrain(terrainAndFeaturesMode, tile, cell);
      }
      foreach (var unit in game.level.units) {
        if (unit.Alive()) {
          var location = unit.location;
          var tileCenter = game.level.terrain.pattern.GetTileCenter(location);
          var positionOnScreen = tileCenter.minus(cameraCenter).plus(new Vec2(DISPLAY_WIDTH / 2 - 1, DISPLAY_HEIGHT / 2 - 1));
          if (positionOnScreen.x < 0 || positionOnScreen.y < 0 || positionOnScreen.x >= map.GetLength(0) || positionOnScreen.y >= map.GetLength(1)) {
            continue;
          }
          var cell = map[(int)positionOnScreen.x, (int)positionOnScreen.y];

          PaintUnit(unit, cell);
        }
      }

      {
        var location = cursor;
        var tileCenter = game.level.terrain.pattern.GetTileCenter(location);
        var positionOnScreen = tileCenter.minus(cameraCenter).plus(new Vec2(DISPLAY_WIDTH / 2 - 1, DISPLAY_HEIGHT / 2 - 1));
        if (positionOnScreen.x < 0 || positionOnScreen.y < 0 || positionOnScreen.x >= map.GetLength(0) || positionOnScreen.y >= map.GetLength(1)) {
          // do nothin
        } else {
          var cell = map[(int)positionOnScreen.x, (int)positionOnScreen.y];
          cell.Paint(ConsoleColor.DarkYellow);
        }
      }

      Console.WriteLine();
      for (int row = DISPLAY_HEIGHT - 1; row >= 0; row--) {
        for (int col = 0; col < DISPLAY_WIDTH; col++) {
          var cell = map[col, row];
          cell.Display();
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
      while (bottomBar.Length < DISPLAY_HEIGHT - 1) {
        bottomBar += " ";
      }
      bottomBar += " ";

      int mp = 0;
      int maxMp = 0;
      if (game.player.Exists()) {
        var sorcerous = game.player.components.GetOnlySorcerousUCOrNull();
        if (sorcerous != null) {
          mp = sorcerous.mp;
          maxMp = sorcerous.maxMp;
        }
      }
      bottomBar += "MP: " + mp + " / " + maxMp;
      while (bottomBar.Length < DISPLAY_WIDTH / 2 - 1) {
        bottomBar += " ";
      }
      bottomBar += " ";

      bottomBar += "@ " + game.level.GetName();
      while (bottomBar.Length < DISPLAY_WIDTH * 3 / 4 - 1) {
        bottomBar += " ";
      }
      bottomBar += " ";

      bottomBar += "Time: " + game.time;
      while (bottomBar.Length < DISPLAY_WIDTH - 2) {
        bottomBar += " ";
      }
      Console.Write(bottomBar);
    }

  }
}
