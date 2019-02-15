using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class InteractRequest : IComparable<InteractRequest> {
  public static readonly string NAME = "InteractRequest";
  public class EqualityComparer : IEqualityComparer<InteractRequest> {
    public bool Equals(InteractRequest a, InteractRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(InteractRequest a) {
      return a.GetHashCode();
    }
  }
  public class Comparer : IComparer<InteractRequest> {
    public int Compare(InteractRequest a, InteractRequest b) {
      return a.CompareTo(b);
    }
  }
       public readonly int gameId;
  public InteractRequest(
      int gameId) {
    this.gameId = gameId;

  }
  public static bool operator==(InteractRequest a, InteractRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(InteractRequest a, InteractRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is InteractRequest)) {
      return false;
    }
    var that = obj as InteractRequest;
    return true
             && gameId.Equals(that.gameId)
        ;
  }
  public override int GetHashCode() {
    int hash = 0;
    hash = hash * 1337;
    hash = hash + gameId.GetHashCode();
    return hash;
  }
  public int CompareTo(InteractRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public string DStr() {
    return "InteractRequest(" +
        gameId
        + ")";

    }
    public static InteractRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new InteractRequest(gameId);
  }
}
     
}
