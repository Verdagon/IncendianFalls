using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class Game {
  public readonly Root root;
  public readonly int id;
  public Game(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public GameIncarnation incarnation { get { return root.GetGameIncarnation(id); } }
  public void AddObserver(IGameEffectObserver observer) {
    root.AddGameObserver(id, observer);
  }
  public void RemoveObserver(IGameEffectObserver observer) {
    root.RemoveGameObserver(id, observer);
  }
  public void Delete() {
    root.EffectGameDelete(id);
  }
  public static Game Null = new Game(null, 0);
  public bool Exists() { return root != null && root.GameExists(id); }
  public bool NullableIs(Game that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {

    if (!root.RandExists(rand.id)) {
      violations.Add("Null constraint violated! Game#" + id + ".rand");
    }

    if (!root.LevelMutSetExists(levels.id)) {
      violations.Add("Null constraint violated! Game#" + id + ".levels");
    }

    if (!root.IGameEventMutListExists(events.id)) {
      violations.Add("Null constraint violated! Game#" + id + ".events");
    }

    if (!root.UnitWeakMutSetExists(eventedUnits.id)) {
      violations.Add("Null constraint violated! Game#" + id + ".eventedUnits");
    }

    if (!root.TerrainTileWeakMutSetExists(eventedTerrainTiles.id)) {
      violations.Add("Null constraint violated! Game#" + id + ".eventedTerrainTiles");
    }

    if (!root.ExecutionStateExists(executionState.id)) {
      violations.Add("Null constraint violated! Game#" + id + ".executionState");
    }
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.RandExists(rand.id)) {
      rand.FindReachableObjects(foundIds);
    }
    if (root.LevelMutSetExists(levels.id)) {
      levels.FindReachableObjects(foundIds);
    }
    if (root.UnitExists(player.id)) {
      player.FindReachableObjects(foundIds);
    }
    if (root.IGameEventMutListExists(events.id)) {
      events.FindReachableObjects(foundIds);
    }
    if (root.UnitWeakMutSetExists(eventedUnits.id)) {
      eventedUnits.FindReachableObjects(foundIds);
    }
    if (root.TerrainTileWeakMutSetExists(eventedTerrainTiles.id)) {
      eventedTerrainTiles.FindReachableObjects(foundIds);
    }
    if (root.LevelExists(level.id)) {
      level.FindReachableObjects(foundIds);
    }
    if (root.ExecutionStateExists(executionState.id)) {
      executionState.FindReachableObjects(foundIds);
    }
  }
  public bool Is(Game that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public Rand rand {

    get {
      if (root == null) {
        throw new Exception("Tried to get member rand of null!");
      }
      return new Rand(root, incarnation.rand);
    }
                       }
  public bool squareLevelsOnly {
    get { return incarnation.squareLevelsOnly; }
  }
  public LevelMutSet levels {

    get {
      if (root == null) {
        throw new Exception("Tried to get member levels of null!");
      }
      return new LevelMutSet(root, incarnation.levels);
    }
                       }
  public Unit player {

    get {
      if (root == null) {
        throw new Exception("Tried to get member player of null!");
      }
      return new Unit(root, incarnation.player);
    }
                         set { root.EffectGameSetPlayer(id, value); }
  }
  public IGameEventMutList events {

    get {
      if (root == null) {
        throw new Exception("Tried to get member events of null!");
      }
      return new IGameEventMutList(root, incarnation.events);
    }
                       }
  public UnitWeakMutSet eventedUnits {

    get {
      if (root == null) {
        throw new Exception("Tried to get member eventedUnits of null!");
      }
      return new UnitWeakMutSet(root, incarnation.eventedUnits);
    }
                       }
  public TerrainTileWeakMutSet eventedTerrainTiles {

    get {
      if (root == null) {
        throw new Exception("Tried to get member eventedTerrainTiles of null!");
      }
      return new TerrainTileWeakMutSet(root, incarnation.eventedTerrainTiles);
    }
                       }
  public Level level {

    get {
      if (root == null) {
        throw new Exception("Tried to get member level of null!");
      }
      return new Level(root, incarnation.level);
    }
                         set { root.EffectGameSetLevel(id, value); }
  }
  public string instructions {
    get { return incarnation.instructions; }
    set { root.EffectGameSetInstructions(id, value); }
  }
  public int time {
    get { return incarnation.time; }
    set { root.EffectGameSetTime(id, value); }
  }
  public ExecutionState executionState {

    get {
      if (root == null) {
        throw new Exception("Tried to get member executionState of null!");
      }
      return new ExecutionState(root, incarnation.executionState);
    }
                       }
}
}
