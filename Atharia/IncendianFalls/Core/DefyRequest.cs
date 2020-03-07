using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DefyRequest : IComparable<DefyRequest> {
  public static readonly string NAME = "DefyRequest";
  public class EqualityComparer : IEqualityComparer<DefyRequest> {
    public bool Equals(DefyRequest a, DefyRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(DefyRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<DefyRequest> {
    public int Compare(DefyRequest a, DefyRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public DefyRequest(
      int gameId) {
    this.gameId = gameId;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(DefyRequest a, DefyRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(DefyRequest a, DefyRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is DefyRequest)) {
      return false;
    }
    var that = obj as DefyRequest;
    return true
               && gameId.Equals(that.gameId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(DefyRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "DefyRequest(" +
        gameId.DStr()
        + ")";

    }
    public static DefyRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new DefyRequest(gameId);
  }
}
       
}
