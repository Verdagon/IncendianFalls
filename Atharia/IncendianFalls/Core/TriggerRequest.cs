using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class TriggerRequest : IComparable<TriggerRequest> {
  public static readonly string NAME = "TriggerRequest";
  public class EqualityComparer : IEqualityComparer<TriggerRequest> {
    public bool Equals(TriggerRequest a, TriggerRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(TriggerRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<TriggerRequest> {
    public int Compare(TriggerRequest a, TriggerRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly string triggerName;
  public TriggerRequest(
      int gameId,
      string triggerName) {
    this.gameId = gameId;
    this.triggerName = triggerName;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + triggerName.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(TriggerRequest a, TriggerRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(TriggerRequest a, TriggerRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is TriggerRequest)) {
      return false;
    }
    var that = obj as TriggerRequest;
    return true
               && gameId.Equals(that.gameId)
        && triggerName.Equals(that.triggerName)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(TriggerRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (triggerName != that.triggerName) {
      return triggerName.CompareTo(that.triggerName);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "TriggerRequest(" +
        gameId.DStr() + ", " +
        triggerName.DStr()
        + ")";

    }
    public static TriggerRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var triggerName = source.ParseStr();
      source.Expect(")");
      return new TriggerRequest(gameId, triggerName);
  }
}
       
}
