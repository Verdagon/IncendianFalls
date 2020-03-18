using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class ShowOverlayEvent : IComparable<ShowOverlayEvent> {
  public static readonly string NAME = "ShowOverlayEvent";
  public class EqualityComparer : IEqualityComparer<ShowOverlayEvent> {
    public bool Equals(ShowOverlayEvent a, ShowOverlayEvent b) {
      return a.Equals(b);
    }
    public int GetHashCode(ShowOverlayEvent a) {
      return a.GetDeterministicHashCode();
    }
  }
  public class Comparer : IComparer<ShowOverlayEvent> {
    public int Compare(ShowOverlayEvent a, ShowOverlayEvent b) {
      return a.CompareTo(b);
    }
  }
  private readonly int hashCode;
         public readonly string text;
  public readonly string template;
  public readonly string speakerRole;
  public readonly bool isFirstInSequence;
  public readonly bool isLastInSequence;
  public readonly bool isObscuring;
  public readonly ButtonImmList buttons;
  public ShowOverlayEvent(
      string text,
      string template,
      string speakerRole,
      bool isFirstInSequence,
      bool isLastInSequence,
      bool isObscuring,
      ButtonImmList buttons) {
    this.text = text;
    this.template = template;
    this.speakerRole = speakerRole;
    this.isFirstInSequence = isFirstInSequence;
    this.isLastInSequence = isLastInSequence;
    this.isObscuring = isObscuring;
    this.buttons = buttons;
    int hash = 0;
    hash = hash * 37 + text.GetDeterministicHashCode();
    hash = hash * 37 + template.GetDeterministicHashCode();
    hash = hash * 37 + speakerRole.GetDeterministicHashCode();
    hash = hash * 37 + isFirstInSequence.GetDeterministicHashCode();
    hash = hash * 37 + isLastInSequence.GetDeterministicHashCode();
    hash = hash * 37 + isObscuring.GetDeterministicHashCode();
    hash = hash * 37 + buttons.GetDeterministicHashCode();
    this.hashCode = hash;

  }
  public static bool operator==(ShowOverlayEvent a, ShowOverlayEvent b) {
    if (object.ReferenceEquals(a, null))
      return object.ReferenceEquals(b, null);
    return a.Equals(b);
  }
  public static bool operator!=(ShowOverlayEvent a, ShowOverlayEvent b) {
    if (object.ReferenceEquals(a, null))
      return !object.ReferenceEquals(b, null);
    return !a.Equals(b);
  }
  public override bool Equals(object obj) {
    if (obj == null) {
      return false;
    }
    if (!(obj is ShowOverlayEvent)) {
      return false;
    }
    var that = obj as ShowOverlayEvent;
    return true
               && text.Equals(that.text)
        && template.Equals(that.template)
        && speakerRole.Equals(that.speakerRole)
        && isFirstInSequence.Equals(that.isFirstInSequence)
        && isLastInSequence.Equals(that.isLastInSequence)
        && isObscuring.Equals(that.isObscuring)
        && buttons.Equals(that.buttons)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(ShowOverlayEvent that) {
    if (text != that.text) {
      return text.CompareTo(that.text);
    }
    if (template != that.template) {
      return template.CompareTo(that.template);
    }
    if (speakerRole != that.speakerRole) {
      return speakerRole.CompareTo(that.speakerRole);
    }
    if (isFirstInSequence != that.isFirstInSequence) {
      return isFirstInSequence.CompareTo(that.isFirstInSequence);
    }
    if (isLastInSequence != that.isLastInSequence) {
      return isLastInSequence.CompareTo(that.isLastInSequence);
    }
    if (isObscuring != that.isObscuring) {
      return isObscuring.CompareTo(that.isObscuring);
    }
    if (buttons != that.buttons) {
      return buttons.CompareTo(that.buttons);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "ShowOverlayEvent(" +
        text.DStr() + ", " +
        template.DStr() + ", " +
        speakerRole.DStr() + ", " +
        isFirstInSequence.DStr() + ", " +
        isLastInSequence.DStr() + ", " +
        isObscuring.DStr() + ", " +
        buttons.DStr()
        + ")";

    }
    public static ShowOverlayEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var text = source.ParseStr();
      source.Expect(",");
      var template = source.ParseStr();
      source.Expect(",");
      var speakerRole = source.ParseStr();
      source.Expect(",");
      var isFirstInSequence = source.ParseBool();
      source.Expect(",");
      var isLastInSequence = source.ParseBool();
      source.Expect(",");
      var isObscuring = source.ParseBool();
      source.Expect(",");
      var buttons = ButtonImmList.Parse(source);
      source.Expect(")");
      return new ShowOverlayEvent(text, template, speakerRole, isFirstInSequence, isLastInSequence, isObscuring, buttons);
  }
}
       
}
