﻿using System;
using System.Collections.Generic;
using Geomancer;
using Geomancer.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;
using System.IO;
using AthPlayer;

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

    public OverlayPaneler overlayPaneler;

    CameraController cameraController;

    private SlowableTimerClock clock;

    private LookPanelView lookPanelView;
    private Location maybeLookedLocation;

    Root ss;
    Level level;
    EffectBroadcaster broadcaster;

    TerrainPresenter terrainPresenter;

    Dictionary<KeyCode, string> memberByKeyCode;

    SortedSet<Location> selectedLocations = new SortedSet<Location>();

    ListView membersView;

    public void Start() {
      clock = new SlowableTimerClock(1.0f);

      memberByKeyCode = new Dictionary<KeyCode, string>() {
        [KeyCode.B] = "Fire",
        [KeyCode.G] = "Grass",
        [KeyCode.M] = "Mud",
        [KeyCode.D] = "Dirt",
        [KeyCode.R] = "Rocks",
        [KeyCode.O] = "Obsidian",
        [KeyCode.S] = "DarkRocks",
        [KeyCode.X] = "Marker",
        [KeyCode.C] = "Cave",
        [KeyCode.F] = "Floor",
        [KeyCode.T] = "Tree",
        [KeyCode.L] = "Magma",
        [KeyCode.H] = "HealthPotion",
        [KeyCode.P] = "ManaPotion",
        [KeyCode.W] = "CaveWall",
        [KeyCode.Z] = "ObsidianFloor",
        [KeyCode.Hash] = "Wall",
        [KeyCode.BackQuote] = "Water",
      };

      ss = new Root(new EditorLogger());

      lookPanelView = new LookPanelView(overlayPaneler, 0, 1);

      broadcaster = new EffectBroadcaster();
      ss.AddObserver((effect) => broadcaster.Route(effect));

      var pattern = PentagonPattern9.makePentagon9Pattern();
      //var pattern = SquarePattern.MakeSquarePattern();
      //var pattern = HexPattern.MakeHexPattern();
      var (_, newLevel) =
        ss.Transact(delegate () {
          var terrain = ss.EffectTerrainCreate(pattern, 0.2f, ss.EffectTerrainTileByLocationMutMapCreate());
          return ss.EffectLevelCreate(terrain);
        });
      level = newLevel;

      using (var fileStream = new FileStream("level.athlev", FileMode.OpenOrCreate)) {
        using (var reader = new StreamReader(fileStream)) {
          while (true) {
            string line = reader.ReadLine();
            if (line == null) {
              break;
            }
            if (line == "") {
              continue;
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

      if (level.terrain.tiles.Count == 0) {
        ss.Transact(delegate () {
          var tile = ss.EffectTerrainTileCreate(1, ss.EffectStrMutListCreate());
          level.terrain.tiles.Add(new Location(0, 0, 0), tile);
          return new Geomancer.Model.Void();
        });
      }

      terrainPresenter = new TerrainPresenter(clock, clock, broadcaster, MemberToViewMap.MakeVivimap(), level.terrain, instantiator);
      terrainPresenter.PhantomTileClicked += HandlePhantomTileClicked;
      terrainPresenter.TerrainTileClicked += HandleTerrainTileClicked;
      terrainPresenter.TerrainTileHovered += HandleTerrainTileHovered;

      Location startLocation = new Location(0, 0, 0);
      if (!level.terrain.tiles.ContainsKey(startLocation)) {
        foreach (var locationAndTile in level.terrain.tiles) {
          startLocation = locationAndTile.Key;
          break;
        }
      }

      cameraController =
        new CameraController(
          clock,
          cameraObject,
          level.terrain.GetTileCenter(startLocation).ToUnity(),
          new Vector3(0, -10, 5));

      membersView =
        new ListView(
          overlayPaneler.MakePanel(
            0, 0, 40, 16));
    }


    public void HandlePhantomTileClicked(Location location) {
      ss.Transact(delegate () {
        var terrainTile = level.root.EffectTerrainTileCreate(1, level.root.EffectStrMutListCreate());
        level.terrain.tiles.Add(location, terrainTile);
        return terrainTile;
      });
      Save();

      var newSelection = new SortedSet<Location>(selectedLocations);
      newSelection.Add(location);
      SetSelection(newSelection);
    }

    public void HandleTerrainTileClicked(Location location) {
      var newSelection = new SortedSet<Location>(selectedLocations);
      if (newSelection.Contains(location)) {
        newSelection.Remove(location);
      } else {
        newSelection.Add(location);
      }
      SetSelection(newSelection);
    }

    public void HandleTerrainTileHovered(Location location) {
      UpdateLookPanelView();
    }

    private void UpdateLookPanelView() {
      var location = terrainPresenter.GetMaybeMouseHighlightLocation();
      if (location != maybeLookedLocation) {
        maybeLookedLocation = location;
        if (location == null) {
          lookPanelView.SetStuff(false, "", "", new List<KeyValuePair<SymbolDescription, string>>());
        } else {
          var message = "(" + location.groupX + ", " + location.groupY + ", " + location.indexInGroup + ")";

          var symbolsAndDescriptions = new List<KeyValuePair<SymbolDescription, string>>();
          if (level.terrain.tiles.ContainsKey(location)) {
            message += " elevation " + level.terrain.tiles[location].elevation;
            foreach (var member in level.terrain.tiles[location].members) {
              var symbol =
              new SymbolDescription("a",
                              Vector4Animation.Color(1f, 1f, 1f, 0), 180, 1, OutlineMode.WithOutline, Vector4Animation.Color(1, 1, 1));
              symbolsAndDescriptions.Add(new KeyValuePair<SymbolDescription, string>(symbol, member));
            }
          }

          lookPanelView.SetStuff(true, message, "", symbolsAndDescriptions);
        }
      }
    }

    public void Update() {

      clock.Update();

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
        SetSelection(new SortedSet<Location>());
      }
      if (Input.GetKeyDown(KeyCode.Slash)) {
        var allLocations = new SortedSet<Location>();
        foreach (var locationAndTile in level.terrain.tiles) {
          allLocations.Add(locationAndTile.Key);
        }
        SetSelection(allLocations);
      }
      if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Mouse2)) {
        ss.Transact<Geomancer.Model.Void>(delegate () {
          foreach (var loc in selectedLocations) {
            level.terrain.tiles[loc].elevation = level.terrain.tiles[loc].elevation + 1;
          }
          return new Geomancer.Model.Void();
        });
        Save();
        UpdateLookPanelView();
      }
      if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.Underscore) || Input.GetKeyDown(KeyCode.Mouse1)) {
        ss.Transact<Geomancer.Model.Void>(delegate () {
          foreach (var loc in selectedLocations) {
            level.terrain.tiles[loc].elevation = Math.Max(1, level.terrain.tiles[loc].elevation - 1);
          }
          return new Geomancer.Model.Void();
        });
        Save();
        UpdateLookPanelView();
      }
      if (Input.GetKeyDown(KeyCode.Delete)) {
        ss.Transact<Geomancer.Model.Void>(delegate () {
          foreach (var loc in selectedLocations) {
            selectedLocations.Remove(loc);
            var tile = level.terrain.tiles[loc];
            level.terrain.tiles.Remove(loc);
            tile.Destruct();
          }
          return new Geomancer.Model.Void();
        });
        Save();
        UpdateLookPanelView();
      }
      foreach (var keyCodeAndMember in memberByKeyCode) {
        if (Input.GetKeyDown(keyCodeAndMember.Key)) {
          ToggleMember(keyCodeAndMember.Value);
          Save();
        }
        UpdateLookPanelView();
      }

      UnityEngine.Ray ray = cameraObject.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
      terrainPresenter.UpdateMouse(ray);
    }

    private void SetSelection(SortedSet<Location> locations) {
      selectedLocations = locations;
      terrainPresenter.SetHighlightedLocations(selectedLocations);

      SortedSet<string> commonMembers = null;
      foreach (var loc in selectedLocations) {
        if (commonMembers == null) {
          commonMembers = new SortedSet<string>();
          foreach (var member in level.terrain.tiles[loc].members) {
            commonMembers.Add(member);
          }
        } else {
          var members = new SortedSet<string>();
          foreach (var member in level.terrain.tiles[loc].members) {
            members.Add(member);
          }
          foreach (var member in new SortedSet<string>(commonMembers)) {
            if (!members.Contains(member)) {
              commonMembers.Remove(member);
            }
          }
        }
      }

      var entries = new List<ListView.Entry>();
      if (commonMembers != null) {
        foreach (var member in commonMembers) {
          entries.Add(new ListView.Entry("f", member));
        }
      }
      membersView.ShowEntries(entries);
    }

    private void ToggleMember(string member) {
      if (!AllLocationsHaveMember(selectedLocations, member)) {
        ss.Transact(delegate () {
          foreach (var location in selectedLocations) {
            if (!LocationHasMember(location, member)) {
              level.terrain.tiles[location].members.Add(member);
            }
          }
          return new Geomancer.Model.Void();
        });
      } else {
        ss.Transact(delegate () {
          foreach (var location in selectedLocations) {
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
      using (var fileStream = new FileStream("level.athlev", FileMode.Create)) {
        using (var writer = new StreamWriter(fileStream)) {
          Save(writer);
        }
      }

      var timestamp = (int)new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
      using (var fileStream = new FileStream("level" + timestamp + ".athlev", FileMode.Create)) {
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
