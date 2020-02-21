using System;
using System.Collections.Generic;
using Geomancer;
using Geomancer.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;
using System.IO;

namespace Geomancer {
  class EditorLogger : Geomancer.Model.ILogger {
    public void Error(string str) {
      Debug.LogError(str);
    }

    public void Info(string str) {
      Debug.Log(str);
    }

    public void Warning(string str) {
      Debug.LogWarning(str);
    }
  }

  public class EditorPresenter : MonoBehaviour {
    public Instantiator instantiator;
    public GameObject cameraObject;

    CameraController cameraController;

    public LookPanelView lookPanelView;

    public PlayerPanelView playerPanelView;

    public NarrationPanelView messageView;

    Root ss;
    Level level;

    TerrainPresenter terrainPresenter;

    Dictionary<KeyCode, string> memberByKeyCode;

    public void Start() {
      memberByKeyCode = new Dictionary<KeyCode, string>();
      memberByKeyCode.Add(KeyCode.G, "grass");
      memberByKeyCode.Add(KeyCode.D, "dirt");
      memberByKeyCode.Add(KeyCode.R, "rocks");

      // var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();


      //try {
      //  writers.Add(new StreamWriter(logFilenames[i], false));
      //} catch (UnauthorizedAccessException e) {
      //  externalSS.GetRoot().logger.Error(
      //      "Couldn't make a log file: " + e.Message);
      //}

      ss = new Root(new EditorLogger());

      var entries = new Dictionary<string, List<Vivimap.IDescription>>();
      var grassEntries = new List<Vivimap.IDescription>();
      grassEntries.Add(new Vivimap.SideColorDescriptionForIDescription(new Color(.5f, .5f, 0)));
      grassEntries.Add(new Vivimap.TopColorDescriptionForIDescription(new Color(0, 0, .5f)));
      entries.Add("grass", grassEntries);
      var rocksEntries = new List<Vivimap.IDescription>();
      rocksEntries.Add(
        new Vivimap.OverlayDescriptionForIDescription(
          new ExtrudedSymbolDescription(
            RenderPriority.TILE,
            new SymbolDescription("f", new Color(.5f, .5f, .5f), 0, OutlineMode.WithOutline, new Color(0, 0, 0)),
            false,
            new Color(0, 0, 0))));
      entries.Add("rocks", rocksEntries);
      var dirtEntries = new List<Vivimap.IDescription>();
      dirtEntries.Add(new Vivimap.SideColorDescriptionForIDescription(new Color(.6f, .6f, 0)));
      entries.Add("dirt", dirtEntries);
      var vivimap = new Vivimap(entries);


      var pattern = PentagonPattern9.makePentagon9Pattern();
      level = ss.Transact(delegate () {
        var terrain = ss.EffectTerrainCreate(pattern, 0.2f, ss.EffectTerrainTileByLocationMutMapCreate());
        return ss.EffectLevelCreate(terrain);
      });

      using (var fileStream = new FileStream("level.athlev", FileMode.OpenOrCreate)) {
        using (var reader = new StreamReader(fileStream)) {
          while (true) {
            string line = reader.ReadLine();
            if (line == null) {
              break;
            }
            string[] parts = line.Split(' ');
            int groupX = int.Parse(parts[0]);
            int groupY = int.Parse(parts[1]);
            int indexInGroup = int.Parse(parts[2]);
            int elevation = int.Parse(parts[3]);

            ss.Transact(delegate () {
              var tile = ss.EffectTerrainTileCreate(elevation, ss.EffectStrMutListCreate());
              level.terrain.tiles.Add(new Location(groupX, groupY, indexInGroup), tile);

              for (int i = 4; i < parts.Length; i++) {
                tile.members.Add(parts[i]);
              }

              return new Geomancer.Model.Void();
            });
          }
        }
      }

      terrainPresenter = new TerrainPresenter(vivimap, level.terrain, instantiator);
      terrainPresenter.PhantomTileClicked += HandlePhantomTileClicked;
      terrainPresenter.TerrainTileClicked += HandleTerrainTileClicked;

      cameraController = new CameraController(cameraObject, level.terrain.GetTileCenter(new Location(0, 0, 0)).ToUnity());
    }

    public void HandlePhantomTileClicked(Location location) {
      ss.Transact(delegate () {
        var terrainTile = level.root.EffectTerrainTileCreate(1, level.root.EffectStrMutListCreate());
        level.terrain.tiles.Add(location, terrainTile);
        return terrainTile;
      });
    }

    public void HandleTerrainTileClicked(Location location) {
      Debug.LogError("clicked " + location);
      var selection = terrainPresenter.GetSelection();
      if (selection.Contains(location)) {
        selection.Remove(location);
      } else {
        selection.Add(location);
      }
      terrainPresenter.SetSelection(selection);
    }

    public void Update() {
      if (Input.GetKey(KeyCode.RightBracket)) {
        cameraController.MoveIn(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.LeftBracket)) {
        cameraController.MoveOut(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.UpArrow)) {
        cameraController.MoveUp(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.DownArrow)) {
        cameraController.MoveDown(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.RightArrow)) {
        cameraController.MoveRight(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.LeftArrow)) {
        cameraController.MoveLeft(Time.deltaTime);
      }
      if (Input.GetKeyDown(KeyCode.Escape)) {
        terrainPresenter.SetSelection(new SortedSet<Location>());
      }
      if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.Plus)) {
        ss.Transact<Geomancer.Model.Void>(delegate () {
          foreach (var loc in terrainPresenter.GetSelection()) {
            level.terrain.tiles[loc].elevation = level.terrain.tiles[loc].elevation + 1;
          }
          return new Geomancer.Model.Void();
        });
        Save();
      }
      if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.Underscore)) {
        ss.Transact<Geomancer.Model.Void>(delegate () {
          foreach (var loc in terrainPresenter.GetSelection()) {
            level.terrain.tiles[loc].elevation = Math.Max(1, level.terrain.tiles[loc].elevation - 1);
          }
          return new Geomancer.Model.Void();
        });
        Save();
      }
      if (Input.GetKeyDown(KeyCode.Delete)) {
        ss.Transact<Geomancer.Model.Void>(delegate () {
          foreach (var loc in terrainPresenter.GetSelection()) {
            var tile = level.terrain.tiles[loc];
            level.terrain.tiles.Remove(loc);
            tile.Destruct();
          }
          return new Geomancer.Model.Void();
        });
        Save();
      }
      foreach (var keyCodeAndMember in memberByKeyCode) {
        if (Input.GetKeyDown(keyCodeAndMember.Key)) {
          ToggleMember(keyCodeAndMember.Value);
          Save();
        }
      }

      UnityEngine.Ray ray = cameraObject.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
      terrainPresenter.UpdateMouse(ray);
    }

    private void ToggleMember(string member) {
      var selection = terrainPresenter.GetSelection();
      if (!AllLocationsHaveMember(selection, member)) {
        ss.Transact(delegate () {
          foreach (var location in selection) {
            if (!LocationHasMember(location, member)) {
              level.terrain.tiles[location].members.Add(member);
            }
          }
          return new Geomancer.Model.Void();
        });
      } else {
        ss.Transact(delegate () {
          foreach (var location in selection) {
            var tile = level.terrain.tiles[location];
            for (int i = 0; i < tile.members.Count; i++) {
              if (tile.members[i] == member) {
                tile.members.RemoveAt(i);
                break;
              }
            }
          }
          return new Geomancer.Model.Void();
        });
      }
    }

    private bool AllLocationsHaveMember(SortedSet<Location> locations, string member) {
      foreach (var location in locations) {
        if (!LocationHasMember(location, member)) {
          return false;
        }
      }
      return true;
    }

    private bool LocationHasMember(Location location, string member) {
      foreach (var hayMember in level.terrain.tiles[location].members) {
        if (member == hayMember) {
          return true;
        }
      }
      return false;
    }

    private void Save() {
      using (var fileStream = new FileStream("level.athlev", FileMode.OpenOrCreate)) {
        using (var writer = new StreamWriter(fileStream)) {
          Save(writer);
        }
      }

      var timestamp = (int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
      using (var fileStream = new FileStream("level" + timestamp + ".athlev", FileMode.OpenOrCreate)) {
        using (var writer = new StreamWriter(fileStream)) {
          Save(writer);
        }
      }
    }

    private void Save(StreamWriter writer) {
      foreach (var locAndTile in level.terrain.tiles) {
        var loc = locAndTile.Key;
        var tile = locAndTile.Value;
        string line = loc.groupX + " " + loc.groupY + " " + loc.indexInGroup + " " + tile.elevation;
        foreach (var member in tile.members) {
          line += " " + member;
        }
        writer.WriteLine(line);
      }
      writer.Close();
    }
  }
}
