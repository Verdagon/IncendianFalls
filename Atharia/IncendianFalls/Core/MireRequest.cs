using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class MireRequest : IComparable<MireRequest> {
  public static readonly string NAME = "MireRequest";
  public class EqualityComparer : IEqualityComparer<MireRequest> {
    public bool Equals(MireRequest a, MireRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(MireRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<MireRequest> {
    public int Compare(MireRequest a, MireRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly int targetUnitId;
  public MireRequest(
      int gameId,
      int targetUnitId) {
    this.gameId = gameId;
    this.targetUnitId = targetUnitId;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + targetUnitId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(MireRequest a, MireRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(MireRequest a, MireRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is MireRequest)) {
      return false;
    }
    var that = obj as MireRequest;
    return true
               && gameId.Equals(that.gameId)
        && targetUnitId.Equals(that.targetUnitId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(MireRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (targetUnitId != that.targetUnitId) {
      return targetUnitId.CompareTo(that.targetUnitId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "MireRequest(" +
        gameId.DStr() + ", " +
        targetUnitId.DStr()
        + ")";

    }
    public static MireRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var targetUnitId = source.ParseInt();
      source.Expect(")");
      return new MireRequest(gameId, targetUnitId);
  }
}
       
}
