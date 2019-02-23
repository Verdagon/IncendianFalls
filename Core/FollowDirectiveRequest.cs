using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class FollowDirectiveRequest : IComparable<FollowDirectiveRequest> {
  public static readonly string NAME = "FollowDirectiveRequest";
  public class EqualityComparer : IEqualityComparer<FollowDirectiveRequest> {
    public bool Equals(FollowDirectiveRequest a, FollowDirectiveRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(FollowDirectiveRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<FollowDirectiveRequest> {
    public int Compare(FollowDirectiveRequest a, FollowDirectiveRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public FollowDirectiveRequest(
      int gameId) {
    this.gameId = gameId;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(FollowDirectiveRequest a, FollowDirectiveRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(FollowDirectiveRequest a, FollowDirectiveRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is FollowDirectiveRequest)) {
      return false;
    }
    var that = obj as FollowDirectiveRequest;
    return true
               && gameId.Equals(that.gameId)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(FollowDirectiveRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public string DStr() {
    return "FollowDirectiveRequest(" +
        gameId.DStr()
        + ")";

    }
    public static FollowDirectiveRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new FollowDirectiveRequest(gameId);
  }
}
       
}
