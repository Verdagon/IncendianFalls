using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class DialogueCommTemplate : IComparable<DialogueCommTemplate> {
  public static readonly string NAME = "DialogueCommTemplate";
  public class EqualityComparer : IEqualityComparer<DialogueCommTemplate> {
    public bool Equals(DialogueCommTemplate a, DialogueCommTemplate b) {
      return a.Equals(b);
    }
    public int GetHashCode(DialogueCommTemplate a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<DialogueCommTemplate> {
    public int Compare(DialogueCommTemplate a, DialogueCommTemplate b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public DialogueCommTemplate(
) {
    int hash = 0;
    this.hashCode = hash;

  }
  public static bool operator==(DialogueCommTemplate a, DialogueCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(DialogueCommTemplate a, DialogueCommTemplate b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is DialogueCommTemplate)) {
      return false;
    }
    var that = obj as DialogueCommTemplate;
    return true
               ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(DialogueCommTemplate that) {
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "DialogueCommTemplate()";
    }
    public static DialogueCommTemplate Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      source.Expect(")");
      return new DialogueCommTemplate();
  }
}
       
}
