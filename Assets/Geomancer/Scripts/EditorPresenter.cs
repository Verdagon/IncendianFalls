using System;
using System.Collections.Generic;
using Geomancer;
using Geomancer.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;

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

    public void Start() {
      // var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();

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
        {
          var tile = ss.EffectTerrainTileCreate(1, ss.EffectStrMutListCreate());
          tile.members.Add("grass");
          terrain.tiles.Add(new Location(0, 0, 0), tile);
        }
        {
          var tile = ss.EffectTerrainTileCreate(1, ss.EffectStrMutListCreate());
          //tile.members.Add("dirt");
          tile.members.Add("rocks");
          terrain.tiles.Add(new Location(0, 0, 1), tile);
        }
        {
          var tile = ss.EffectTerrainTileCreate(2, ss.EffectStrMutListCreate());
          tile.members.Add("grass");
          terrain.tiles.Add(new Location(0, 0, 2), tile);
        }
        return ss.EffectLevelCreate(terrain);
      });

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
      if (Input.GetKey(KeyCode.Plus) || Input.GetKey(KeyCode.Equals)) {
        cameraController.MoveIn(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.Minus) || Input.GetKey(KeyCode.Underscore)) {
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
      if (Input.GetKeyDown(KeyCode.C)) {
        terrainPresenter.SetSelection(new SortedSet<Location>());
      }
      if (Input.GetKeyDown(KeyCode.T)) {
        ss.Transact<Geomancer.Model.Void>(delegate () {
          foreach (var loc in terrainPresenter.GetSelection()) {
            level.terrain.tiles[loc].elevation = level.terrain.tiles[loc].elevation + 1;
          }
          return new Geomancer.Model.Void();
        });
      }
      if (Input.GetKeyDown(KeyCode.G)) {
        ss.Transact<Geomancer.Model.Void>(delegate () {
          foreach (var loc in terrainPresenter.GetSelection()) {
            level.terrain.tiles[loc].elevation = Math.Max(1, level.terrain.tiles[loc].elevation - 1);
          }
          return new Geomancer.Model.Void();
        });
      }

      UnityEngine.Ray ray = cameraObject.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);
      terrainPresenter.UpdateMouse(ray);
    }
  }
}
