using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CancelRequest : IComparable<CancelRequest> {
  public static readonly string NAME = "CancelRequest";
  public class EqualityComparer : IEqualityComparer<CancelRequest> {
    public bool Equals(CancelRequest a, CancelRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(CancelRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<CancelRequest> {
    public int Compare(CancelRequest a, CancelRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public CancelRequest(
      int gameId) {
    this.gameId = gameId;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(CancelRequest a, CancelRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(CancelRequest a, CancelRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is CancelRequest)) {
      return false;
    }
    var that = obj as CancelRequest;
    return true
               && gameId.Equals(that.gameId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(CancelRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "CancelRequest(" +
        gameId.DStr()
        + ")";

    }
    public static CancelRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new CancelRequest(gameId);
  }
}
       
}
