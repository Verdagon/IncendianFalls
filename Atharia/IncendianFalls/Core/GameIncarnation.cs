using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class GameIncarnation : IGameEffectVisitor {
  public readonly int rand;
  public readonly bool squareLevelsOnly;
  public readonly int levels;
  public  int player;
  public  int level;
  public  int time;
  public  int actingUnit;
  public  bool pauseBeforeNextUnit;
  public  int actionNum;
  public  string instructions;
  public  bool hideInput;
  public  IGameEvent evvent;
  public GameIncarnation(
      int rand,
      bool squareLevelsOnly,
      int levels,
      int player,
      int level,
      int time,
      int actingUnit,
      bool pauseBeforeNextUnit,
      int actionNum,
      string instructions,
      bool hideInput,
      IGameEvent evvent) {
    this.rand = rand;
    this.squareLevelsOnly = squareLevelsOnly;
    this.levels = levels;
    this.player = player;
    this.level = level;
    this.time = time;
    this.actingUnit = actingUnit;
    this.pauseBeforeNextUnit = pauseBeforeNextUnit;
    this.actionNum = actionNum;
    this.instructions = instructions;
    this.hideInput = hideInput;
    this.evvent = evvent;
  }
  public GameIncarnation Copy() {
    return new GameIncarnation(
rand,
squareLevelsOnly,
levels,
player,
level,
time,
actingUnit,
pauseBeforeNextUnit,
actionNum,
instructions,
hideInput,
evvent    );
  }

  public void visitGameCreateEffect(GameCreateEffect e) {}
  public void visitGameDeleteEffect(GameDeleteEffect e) {}



public void visitGameSetPlayerEffect(GameSetPlayerEffect e) { this.player = e.newValue; }
public void visitGameSetLevelEffect(GameSetLevelEffect e) { this.level = e.newValue; }
public void visitGameSetTimeEffect(GameSetTimeEffect e) { this.time = e.newValue; }
public void visitGameSetActingUnitEffect(GameSetActingUnitEffect e) { this.actingUnit = e.newValue; }
public void visitGameSetPauseBeforeNextUnitEffect(GameSetPauseBeforeNextUnitEffect e) { this.pauseBeforeNextUnit = e.newValue; }
public void visitGameSetActionNumEffect(GameSetActionNumEffect e) { this.actionNum = e.newValue; }
public void visitGameSetInstructionsEffect(GameSetInstructionsEffect e) { this.instructions = e.newValue; }
public void visitGameSetHideInputEffect(GameSetHideInputEffect e) { this.hideInput = e.newValue; }
public void visitGameSetEvventEffect(GameSetEvventEffect e) { this.evvent = e.newValue; }
  public void ApplyEffect(IGameEffect effect) { effect.visitIGameEffect(this); }
}

}
