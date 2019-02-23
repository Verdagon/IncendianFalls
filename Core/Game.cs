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
  public Level level {

    get {
      if (root == null) {
        throw new Exception("Tried to get member level of null!");
      }
      return new Level(root, incarnation.level);
    }
                         set { root.EffectGameSetLevel(id, value); }
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
