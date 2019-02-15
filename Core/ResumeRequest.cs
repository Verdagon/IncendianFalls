using System;
using System.Collections.Generic;

namespace Atharia.Model {
public class ResumeRequest : IComparable<ResumeRequest> {
  public static readonly string NAME = "ResumeRequest";
  public class EqualityComparer : IEqualityComparer<ResumeRequest> {
    public bool Equals(ResumeRequest a, ResumeRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(ResumeRequest a) {
      return a.GetHashCode();
    }
  }
  public class Comparer : IComparer<ResumeRequest> {
    public int Compare(ResumeRequest a, ResumeRequest b) {
      return a.CompareTo(b);
    }
  }
       public readonly int gameId;
  public ResumeRequest(
      int gameId) {
    this.gameId = gameId;

  }
  public static bool operator==(ResumeRequest a, ResumeRequest b) {
    return a.Equals(b);
  }
  public static bool operator!=(ResumeRequest a, ResumeRequest b) {
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is ResumeRequest)) {
      return false;
    }
    var that = obj as ResumeRequest;
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
  public int CompareTo(ResumeRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    return 0;
  }
  public string DStr() {
    return "ResumeRequest(" +
        gameId
        + ")";

    }
    public static ResumeRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(")");
      return new ResumeRequest(gameId);
  }
}
     
}
