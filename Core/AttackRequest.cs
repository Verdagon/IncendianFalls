using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class AttackRequest : IComparable<AttackRequest> {
  public static readonly string NAME = "AttackRequest";
  public class EqualityComparer : IEqualityComparer<AttackRequest> {
    public bool Equals(AttackRequest a, AttackRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(AttackRequest a) {
      return a.GetHashCode();
    }
  }
  public class Comparer : IComparer<AttackRequest> {
    public int Compare(AttackRequest a, AttackRequest b) {
      return a.CompareTo(b);
    }
  }
       public readonly int gameId;
  public readonly int targetUnitId;
  public AttackRequest(
      int gameId,
      int targetUnitId) {
    this.gameId = gameId;
    this.targetUnitId = targetUnitId;

  }
  public static bool operator==(AttackRequest a, AttackRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(AttackRequest a, AttackRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is AttackRequest)) {
      return false;
    }
    var that = obj as AttackRequest;
    return true
             && gameId.Equals(that.gameId)
        && targetUnitId.Equals(that.targetUnitId)
        ;
  }
  public override int GetHashCode() {
    int hash = 0;
    hash = hash * 1337;
    hash = hash + gameId.GetHashCode();
    hash = hash * 1337;
    hash = hash + targetUnitId.GetHashCode();
    return hash;
  }
  public int CompareTo(AttackRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (targetUnitId != that.targetUnitId) {
      return targetUnitId.CompareTo(that.targetUnitId);
    }
    return 0;
  }
  public string DStr() {
    return "AttackRequest(" +
        gameId + ", " +
        targetUnitId
        + ")";

    }
    public static AttackRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var targetUnitId = source.ParseInt();
      source.Expect(")");
      return new AttackRequest(gameId, targetUnitId);
  }
}
     
}
