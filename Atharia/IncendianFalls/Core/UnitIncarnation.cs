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
}

}
