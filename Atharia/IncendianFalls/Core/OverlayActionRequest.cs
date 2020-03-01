using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OverlayActionRequest : IComparable<OverlayActionRequest> {
  public static readonly string NAME = "OverlayActionRequest";
  public class EqualityComparer : IEqualityComparer<OverlayActionRequest> {
    public bool Equals(OverlayActionRequest a, OverlayActionRequest b) {
      return a.Equals(b);
    }
    public int GetHashCode(OverlayActionRequest a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<OverlayActionRequest> {
    public int Compare(OverlayActionRequest a, OverlayActionRequest b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly int gameId;
  public readonly int buttonIndex;
  public OverlayActionRequest(
      int gameId,
      int buttonIndex) {
    this.gameId = gameId;
    this.buttonIndex = buttonIndex;
    int hash = 0;
    hash = hash * 37 + gameId.GetDeterministicHashCode();
    hash = hash * 37 + buttonIndex.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(OverlayActionRequest a, OverlayActionRequest b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(OverlayActionRequest a, OverlayActionRequest b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is OverlayActionRequest)) {
      return false;
    }
    var that = obj as OverlayActionRequest;
    return true
               && gameId.Equals(that.gameId)
        && buttonIndex.Equals(that.buttonIndex)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(OverlayActionRequest that) {
    if (gameId != that.gameId) {
      return gameId.CompareTo(that.gameId);
    }
    if (buttonIndex != that.buttonIndex) {
      return buttonIndex.CompareTo(that.buttonIndex);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "OverlayActionRequest(" +
        gameId.DStr() + ", " +
        buttonIndex.DStr()
        + ")";

    }
    public static OverlayActionRequest Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var gameId = source.ParseInt();
      source.Expect(",");
      var buttonIndex = source.ParseInt();
      source.Expect(")");
      return new OverlayActionRequest(gameId, buttonIndex);
  }
}
       
}
