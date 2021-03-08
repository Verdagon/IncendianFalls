using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class CommActionRequest : IComparable<CommActionRequest> {
  public static readonly string NAME = "CommActionRequest";
  public class EqualityComparer : IEqualityComparer<CommActionRequest> {
    public bool Equals(CommActionRequest a, CommActionRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(CommActionRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<CommActionRequest> {
    public int Compare(CommActionRequest a, CommActionRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly int commId;
  public readonly int actionIndex;
  public CommActionRequest(
      int gameId,
      int commId,
      int actionIndex) {
    this.gameId = gameId;
    this.commId = commId;
    this.actionIndex = actionIndex;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + commId.GetDeterministicHashCode();
    hash = hash * 37 + actionIndex.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(CommActionRequest a, CommActionRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(CommActionRequest a, CommActionRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is CommActionRequest)) {
      return false;
    }
    var that = obj as CommActionRequest;
    return true
               && gameId.Equals(that.gameId)
        && commId.Equals(that.commId)
        && actionIndex.Equals(that.actionIndex)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(CommActionRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (commId != that.commId) {
      return commId.CompareTo(that.commId);
    }
    if (actionIndex != that.actionIndex) {
      return actionIndex.CompareTo(that.actionIndex);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "CommActionRequest(" +
        gameId.DStr() + ", " +
        commId.DStr() + ", " +
        actionIndex.DStr()
        + ")";

    }
    public static CommActionRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var commId = source.ParseInt();
      source.Expect(",");
      var actionIndex = source.ParseInt();
      source.Expect(")");
      return new CommActionRequest(gameId, commId, actionIndex);
  }
}
       
}
