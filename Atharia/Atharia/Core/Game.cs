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
  public void AddObserver(EffectBroadcaster broadcaster, IGameEffectObserver observer) {
    broadcaster.AddGameObserver(id, observer);
  }
  public void RemoveObserver(EffectBroadcaster broadcaster, IGameEffectObserver observer) {
    broadcaster.RemoveGameObserver(id, observer);
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

    if (!root.CommMutListExists(comms.id)) {
      violations.Add("Null constraint violated! Game#" + id + ".comms");
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
    if (root.LevelExists(level.id)) {
      level.FindReachableObjects(foundIds);
    }
    if (root.UnitExists(actingUnit.id)) {
      actingUnit.FindReachableObjects(foundIds);
    }
    if (root.CommMutListExists(comms.id)) {
      comms.FindReachableObjects(foundIds);
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
  public Unit actingUnit {

    get {
      if (root == null) {
        throw new Exception("Tried to get member actingUnit of null!");
      }
      return new Unit(root, incarnation.actingUnit);
    }
                         set { root.EffectGameSetActingUnit(id, value); }
  }
  public bool pauseBeforeNextUnit {
    get { return incarnation.pauseBeforeNextUnit; }
    set { root.EffectGameSetPauseBeforeNextUnit(id, value); }
  }
  public int actionNum {
    get { return incarnation.actionNum; }
    set { root.EffectGameSetActionNum(id, value); }
  }
  public string instructions {
    get { return incarnation.instructions; }
    set { root.EffectGameSetInstructions(id, value); }
  }
  public bool hideInput {
    get { return incarnation.hideInput; }
    set { root.EffectGameSetHideInput(id, value); }
  }
  public IGameEvent evvent {
    get { return incarnation.evvent; }
    set { root.EffectGameSetEvvent(id, value); }
  }
  public CommMutList comms {

    get {
      if (root == null) {
        throw new Exception("Tried to get member comms of null!");
      }
      return new CommMutList(root, incarnation.comms);
    }
                       }
}
}
