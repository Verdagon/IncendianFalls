using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CheatRequest : IComparable<CheatRequest> {
  public static readonly string NAME = "CheatRequest";
  public class EqualityComparer : IEqualityComparer<CheatRequest> {
    public bool Equals(CheatRequest a, CheatRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(CheatRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<CheatRequest> {
    public int Compare(CheatRequest a, CheatRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly string cheatName;
  public CheatRequest(
      int gameId,
      string cheatName) {
    this.gameId = gameId;
    this.cheatName = cheatName;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + cheatName.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(CheatRequest a, CheatRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(CheatRequest a, CheatRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is CheatRequest)) {
      return false;
    }
    var that = obj as CheatRequest;
    return true
               && gameId.Equals(that.gameId)
        && cheatName.Equals(that.cheatName)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(CheatRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (cheatName != that.cheatName) {
      return cheatName.CompareTo(that.cheatName);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "CheatRequest(" +
        gameId.DStr() + ", " +
        cheatName.DStr()
        + ")";

    }
    public static CheatRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var cheatName = source.ParseStr();
      source.Expect(")");
      return new CheatRequest(gameId, cheatName);
  }
}
       
}
