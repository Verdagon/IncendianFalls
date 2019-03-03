using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefendRequest : IComparable<DefendRequest> {
  public static readonly string NAME = "DefendRequest";
  public class EqualityComparer : IEqualityComparer<DefendRequest> {
    public bool Equals(DefendRequest a, DefendRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(DefendRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<DefendRequest> {
    public int Compare(DefendRequest a, DefendRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public DefendRequest(
      int gameId) {
    this.gameId = gameId;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(DefendRequest a, DefendRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(DefendRequest a, DefendRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is DefendRequest)) {
      return false;
    }
    var that = obj as DefendRequest;
    return true
               && gameId.Equals(that.gameId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(DefendRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "DefendRequest(" +
        gameId.DStr()
        + ")";

    }
    public static DefendRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new DefendRequest(gameId);
  }
}
       
}
