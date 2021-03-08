using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitIncarnation : IUnitEffectVisitor {
  public  IUnitEvent evvent;
  public  int lifeEndTime;
  public  Location location;
  public readonly string classId;
  public  int nextActionTime;
  public  int hp;
  public  int maxHp;
  public readonly int components;
  public readonly bool good;
  public UnitIncarnation(
      IUnitEvent evvent,
      int lifeEndTime,
      Location location,
      string classId,
      int nextActionTime,
      int hp,
      int maxHp,
      int components,
      bool good) {
    this.evvent = evvent;
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
evvent,
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
public void visitUnitSetEvventEffect(UnitSetEvventEffect e) { this.evvent = e.newValue; }
public void visitUnitSetLifeEndTimeEffect(UnitSetLifeEndTimeEffect e) { this.lifeEndTime = e.newValue; }
public void visitUnitSetLocationEffect(UnitSetLocationEffect e) { this.location = e.newValue; }

public void visitUnitSetNextActionTimeEffect(UnitSetNextActionTimeEffect e) { this.nextActionTime = e.newValue; }
public void visitUnitSetHpEffect(UnitSetHpEffect e) { this.hp = e.newValue; }
public void visitUnitSetMaxHpEffect(UnitSetMaxHpEffect e) { this.maxHp = e.newValue; }


  public void ApplyEffect(IUnitEffect effect) { effect.visitIUnitEffect(this); }
}

}
