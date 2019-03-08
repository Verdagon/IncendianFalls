using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class UnitIncarnation {
  public readonly int events;
  public  bool alive;
  public  int lifeEndTime;
  public  Location location;
  public readonly string classId;
  public  int hp;
  public readonly int maxHp;
  public  int mp;
  public readonly int maxMp;
  public readonly int inertia;
  public  int nextActionTime;
  public readonly int components;
  public readonly int items;
  public readonly bool good;
  public readonly int strength;
  public UnitIncarnation(
      int events,
      bool alive,
      int lifeEndTime,
      Location location,
      string classId,
      int hp,
      int maxHp,
      int mp,
      int maxMp,
      int inertia,
      int nextActionTime,
      int components,
      int items,
      bool good,
      int strength) {
    this.events = events;
    this.alive = alive;
    this.lifeEndTime = lifeEndTime;
    this.location = location;
    this.classId = classId;
    this.hp = hp;
    this.maxHp = maxHp;
    this.mp = mp;
    this.maxMp = maxMp;
    this.inertia = inertia;
    this.nextActionTime = nextActionTime;
    this.components = components;
    this.items = items;
    this.good = good;
    this.strength = strength;
  }
}

}
