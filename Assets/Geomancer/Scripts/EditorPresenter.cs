using System;
using System.Collections.Generic;
using Geomancer;
using Geomancer.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;
using System.IO;
using AthPlayer;

namespace Geomancer {
  public class EditorPresenter : MonoBehaviour {
    public Instantiator instantiator;
    public GameObject cameraObject;
    
    public GameObject panelRootGameObject;

    private OverlayPaneler overlayPaneler;

    CameraController cameraController;

    private SlowableTimerClock clock;

    private LookPanelView lookPanelView;
    private Location maybeLookedLocation;
    
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

      overlayPaneler = new OverlayPaneler(panelRootGameObject, instantiator, clock);
      lookPanelView = new LookPanelView(overlayPaneler, -1, 2);

      var pattern = PentagonPattern9.makePentagon9Pattern();
      //var pattern = SquarePattern.MakeSquarePattern();
      //var pattern = HexPattern.MakeHexPattern();
      var terrain = new Geomancer.Model.Terrain(pattern, 200, new SortedDictionary<Location, TerrainTile>());

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

            var location = new Location(groupX, groupY, indexInGroup);
            var tile = new TerrainTile(location, elevation, new List<string>());
            terrain.tiles.Add(location, tile);

            for (int i = 4; i < parts.Length; i++) {
              tile.members.Add(parts[i]);
            }
          }
        }
      }

      if (terrain.tiles.Count == 0) {
        var tile = new TerrainTile(new Location(0, 0, 0), 1, new List<string>());
        terrain.tiles.Add(new Location(0, 0, 0), tile);
      }

      terrainPresenter = new TerrainPresenter(clock, clock, MemberToViewMap.MakeVivimap(), terrain, instantiator);
      terrainPresenter.PhantomTileClicked += HandlePhantomTileClicked;
      terrainPresenter.TerrainTileClicked += HandleTerrainTileClicked;
      terrainPresenter.TerrainTileHovered += HandleTerrainTileHovered;

      Location startLocation = new Location(0, 0, 0);
      if (!terrain.tiles.ContainsKey(startLocation)) {
        foreach (var locationAndTile in terrain.tiles) {
          startLocation = locationAndTile.Key;
          break;
        }
      }

      cameraController =
        new CameraController(
          clock,
          cameraObject,
          terrain.GetTileCenter(startLocation).ToUnity(),
          new Vector3(0, -10, 5));

      membersView =
        new ListView(
          overlayPaneler.MakePanel(
            0, 0, 40, 16));
    }


    public void HandlePhantomTileClicked(Location location) {
      var terrainTile = new TerrainTile(location, 1, new List<string>());
      terrainPresenter.AddTile(terrainTile);
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
          if (terrainPresenter.terrain.tiles.ContainsKey(location)) {
            message += " elevation " + terrainPresenter.terrain.tiles[location].elevation;
            foreach (var member in terrainPresenter.terrain.tiles[location].members) {
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
        foreach (var locationAndTile in terrainPresenter.terrain.tiles) {
          allLocations.Add(locationAndTile.Key);
        }
        SetSelection(allLocations);
      }
      if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.Mouse2)) {
      
        foreach (var loc in selectedLocations) {
          terrainPresenter.GetTilePresenter(loc).SetElevation(terrainPresenter.terrain.tiles[loc].elevation + 1);
        }
        Save();
        UpdateLookPanelView();
      }
      if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.Underscore) || Input.GetKeyDown(KeyCode.Mouse1)) {
        foreach (var loc in selectedLocations) {
          terrainPresenter.GetTilePresenter(loc).SetElevation(
              Math.Max(1, terrainPresenter.terrain.tiles[loc].elevation - 1));
        }
        Save();
        UpdateLookPanelView();
      }
      if (Input.GetKeyDown(KeyCode.Delete)) {
        foreach (var loc in new SortedSet<Location>(selectedLocations)) {
          selectedLocations.Remove(loc);
          var tile = terrainPresenter.terrain.tiles[loc];
          terrainPresenter.terrain.tiles.Remove(loc);
          tile.Destruct();
        }
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
          foreach (var member in terrainPresenter.terrain.tiles[loc].members) {
            commonMembers.Add(member);
          }
        } else {
          var members = new SortedSet<string>();
          foreach (var member in terrainPresenter.terrain.tiles[loc].members) {
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
        foreach (var location in selectedLocations) {
          if (!LocationHasMember(location, member)) {
            terrainPresenter.GetTilePresenter(location).AddMember(member);
          }
        }
      } else {
        foreach (var location in selectedLocations) {
          var tile = terrainPresenter.terrain.tiles[location];
          for (int i = 0; i < tile.members.Count; i++) {
            if (tile.members[i] == member) {
              terrainPresenter.GetTilePresenter(location).RemoveMember(i);
              break;
            }
          }
        }
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
      foreach (var hayMember in terrainPresenter.terrain.tiles[location].members) {
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
      foreach (var locAndTile in terrainPresenter.terrain.tiles) {
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
