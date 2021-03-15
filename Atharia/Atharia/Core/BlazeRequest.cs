using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class BlazeRequest : IComparable<BlazeRequest> {
  public static readonly string NAME = "BlazeRequest";
  public class EqualityComparer : IEqualityComparer<BlazeRequest> {
    public bool Equals(BlazeRequest a, BlazeRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(BlazeRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<BlazeRequest> {
    public int Compare(BlazeRequest a, BlazeRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly Location targetLoc;
  public BlazeRequest(
      int gameId,
      Location targetLoc) {
    this.gameId = gameId;
    this.targetLoc = targetLoc;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + targetLoc.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(BlazeRequest a, BlazeRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(BlazeRequest a, BlazeRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is BlazeRequest)) {
      return false;
    }
    var that = obj as BlazeRequest;
    return true
               && gameId.Equals(that.gameId)
        && targetLoc.Equals(that.targetLoc)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(BlazeRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (targetLoc != that.targetLoc) {
      return targetLoc.CompareTo(that.targetLoc);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "BlazeRequest(" +
        gameId.DStr() + ", " +
        targetLoc.DStr()
        + ")";

    }
    public static BlazeRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var targetLoc = Location.Parse(source);
      source.Expect(")");
      return new BlazeRequest(gameId, targetLoc);
  }
}
       
}
