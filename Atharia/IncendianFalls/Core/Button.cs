using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class Button : IComparable<Button> {
  public static readonly string NAME = "Button";
  public class EqualityComparer : IEqualityComparer<Button> {
    public bool Equals(Button a, Button b) {
      return a.Equals(b);
    }
    public int GetHashCode(Button a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<Button> {
    public int Compare(Button a, Button b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly string label;
  public readonly Color backgroundColor;
  public readonly string triggerName;
  public Button(
      string label,
      Color backgroundColor,
      string triggerName) {
    this.label = label;
    this.backgroundColor = backgroundColor;
    this.triggerName = triggerName;
    int hash = 0;
    hash = hash * 37 + label.GetDeterministicHashCode();
    hash = hash * 37 + backgroundColor.GetDeterministicHashCode();
    hash = hash * 37 + triggerName.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(Button a, Button b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(Button a, Button b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is Button)) {
      return false;
    }
    var that = obj as Button;
    return true
               && label.Equals(that.label)
        && backgroundColor.Equals(that.backgroundColor)
        && triggerName.Equals(that.triggerName)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(Button that) {
    if (label != that.label) {
      return label.CompareTo(that.label);
    }
    if (backgroundColor != that.backgroundColor) {
      return backgroundColor.CompareTo(that.backgroundColor);
    }
    if (triggerName != that.triggerName) {
      return triggerName.CompareTo(that.triggerName);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "Button(" +
        label.DStr() + ", " +
        backgroundColor.DStr() + ", " +
        triggerName.DStr()
        + ")";

    }
    public static Button Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var label = source.ParseStr();
      source.Expect(",");
      var backgroundColor = Color.Parse(source);
      source.Expect(",");
      var triggerName = source.ParseStr();
      source.Expect(")");
      return new Button(label, backgroundColor, triggerName);
  }
}
       
}
