using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitIncarnation : IUnitEffectVisitor {
  public readonly int events;
  public  bool alive;
  public  int lifeEndTime;
  public  Location location;
  public readonly string classId;
  public  int nextActionTime;
  public  int hp;
  public  int maxHp;
  public readonly int components;
  public readonly bool good;
  public UnitIncarnation(
      int events,
      bool alive,
      int lifeEndTime,
      Location location,
      string classId,
      int nextActionTime,
      int hp,
      int maxHp,
      int components,
      bool good) {
    this.events = events;
    this.alive = alive;
    this.lifeEndTime = lifeEndTime;
    this.location = location;
    this.classId = classId;
    this.nextActionTime = nextActionTime;
    this.hp = hp;
    this.maxHp = maxHp;
    this.components = components;
    this.good = good;
  }
  public UnitIncarnation Copy() {
    return new UnitIncarnation(
events,
alive,
lifeEndTime,
location,
classId,
nextActionTime,
hp,
maxHp,
components,
good    );
  }

  public void visitUnitCreateEffect(UnitCreateEffect e) {}
  public void visitUnitDeleteEffect(UnitDeleteEffect e) {}

public void visitUnitSetAliveEffect(UnitSetAliveEffect e) { this.alive = e.newValue; }
public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect e) { this.lifeEndTime = e.newValue; }
public void visitUnitSetLocationEffect(UnitSetLocationEffect e) { this.location = e.newValue; }

public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect e) { this.nextActionTime = e.newValue; }
public void visitUnitSetHpEffect(UnitSetHpEffect e) { this.hp = e.newValue; }
public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect e) { this.maxHp = e.newValue; }


  public void ApplyEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
}

}
