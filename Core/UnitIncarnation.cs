using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class UnitIncarnation {
  public int events;
  public bool alive;
  public int lifeEndTime;
  public Location location;
  public string classId;
  public int hp;
  public int maxHp;
  public int mp;
  public int maxMp;
  public int inertia;
  public int nextActionTime;
  public int directive;
  public int details;
  public int items;
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
      int directive,
      int details,
      int items) {
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
    this.directive = directive;
    this.details = details;
    this.items = items;
  }
}

}
