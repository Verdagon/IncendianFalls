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
         public readonly int sizePercent;
  public readonly Color backgroundColor;
  public readonly int fadeInEndMs;
  public readonly int fadeOutStartMs;
  public readonly int fadeOutEndMs;
  public readonly string automaticActionTriggerName;
  public readonly string text;
  public readonly Color textColor;
  public readonly int textFadeInStartMs;
  public readonly int textFadeInEndMs;
  public readonly int textFadeOutStartMs;
  public readonly int textFadeOutEndMs;
  public readonly bool topAligned;
  public readonly bool leftAligned;
  public readonly ButtonImmList buttons;
  public ShowOverlayEvent(
      int sizePercent,
      Color backgroundColor,
      int fadeInEndMs,
      int fadeOutStartMs,
      int fadeOutEndMs,
      string automaticActionTriggerName,
      string text,
      Color textColor,
      int textFadeInStartMs,
      int textFadeInEndMs,
      int textFadeOutStartMs,
      int textFadeOutEndMs,
      bool topAligned,
      bool leftAligned,
      ButtonImmList buttons) {
    this.sizePercent = sizePercent;
    this.backgroundColor = backgroundColor;
    this.fadeInEndMs = fadeInEndMs;
    this.fadeOutStartMs = fadeOutStartMs;
    this.fadeOutEndMs = fadeOutEndMs;
    this.automaticActionTriggerName = automaticActionTriggerName;
    this.text = text;
    this.textColor = textColor;
    this.textFadeInStartMs = textFadeInStartMs;
    this.textFadeInEndMs = textFadeInEndMs;
    this.textFadeOutStartMs = textFadeOutStartMs;
    this.textFadeOutEndMs = textFadeOutEndMs;
    this.topAligned = topAligned;
    this.leftAligned = leftAligned;
    this.buttons = buttons;
    int hash = 0;
    hash = hash * 37 + sizePercent.GetDeterministicHashCode();
    hash = hash * 37 + backgroundColor.GetDeterministicHashCode();
    hash = hash * 37 + fadeInEndMs.GetDeterministicHashCode();
    hash = hash * 37 + fadeOutStartMs.GetDeterministicHashCode();
    hash = hash * 37 + fadeOutEndMs.GetDeterministicHashCode();
    hash = hash * 37 + automaticActionTriggerName.GetDeterministicHashCode();
    hash = hash * 37 + text.GetDeterministicHashCode();
    hash = hash * 37 + textColor.GetDeterministicHashCode();
    hash = hash * 37 + textFadeInStartMs.GetDeterministicHashCode();
    hash = hash * 37 + textFadeInEndMs.GetDeterministicHashCode();
    hash = hash * 37 + textFadeOutStartMs.GetDeterministicHashCode();
    hash = hash * 37 + textFadeOutEndMs.GetDeterministicHashCode();
    hash = hash * 37 + topAligned.GetDeterministicHashCode();
    hash = hash * 37 + leftAligned.GetDeterministicHashCode();
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
               && sizePercent.Equals(that.sizePercent)
        && backgroundColor.Equals(that.backgroundColor)
        && fadeInEndMs.Equals(that.fadeInEndMs)
        && fadeOutStartMs.Equals(that.fadeOutStartMs)
        && fadeOutEndMs.Equals(that.fadeOutEndMs)
        && automaticActionTriggerName.Equals(that.automaticActionTriggerName)
        && text.Equals(that.text)
        && textColor.Equals(that.textColor)
        && textFadeInStartMs.Equals(that.textFadeInStartMs)
        && textFadeInEndMs.Equals(that.textFadeInEndMs)
        && textFadeOutStartMs.Equals(that.textFadeOutStartMs)
        && textFadeOutEndMs.Equals(that.textFadeOutEndMs)
        && topAligned.Equals(that.topAligned)
        && leftAligned.Equals(that.leftAligned)
        && buttons.Equals(that.buttons)
        ;
  }
  public override int GetHashCode() {
    return GetDeterministicHashCode();
  }
  public int GetDeterministicHashCode() { return hashCode; }
  public int CompareTo(ShowOverlayEvent that) {
    if (sizePercent != that.sizePercent) {
      return sizePercent.CompareTo(that.sizePercent);
    }
    if (backgroundColor != that.backgroundColor) {
      return backgroundColor.CompareTo(that.backgroundColor);
    }
    if (fadeInEndMs != that.fadeInEndMs) {
      return fadeInEndMs.CompareTo(that.fadeInEndMs);
    }
    if (fadeOutStartMs != that.fadeOutStartMs) {
      return fadeOutStartMs.CompareTo(that.fadeOutStartMs);
    }
    if (fadeOutEndMs != that.fadeOutEndMs) {
      return fadeOutEndMs.CompareTo(that.fadeOutEndMs);
    }
    if (automaticActionTriggerName != that.automaticActionTriggerName) {
      return automaticActionTriggerName.CompareTo(that.automaticActionTriggerName);
    }
    if (text != that.text) {
      return text.CompareTo(that.text);
    }
    if (textColor != that.textColor) {
      return textColor.CompareTo(that.textColor);
    }
    if (textFadeInStartMs != that.textFadeInStartMs) {
      return textFadeInStartMs.CompareTo(that.textFadeInStartMs);
    }
    if (textFadeInEndMs != that.textFadeInEndMs) {
      return textFadeInEndMs.CompareTo(that.textFadeInEndMs);
    }
    if (textFadeOutStartMs != that.textFadeOutStartMs) {
      return textFadeOutStartMs.CompareTo(that.textFadeOutStartMs);
    }
    if (textFadeOutEndMs != that.textFadeOutEndMs) {
      return textFadeOutEndMs.CompareTo(that.textFadeOutEndMs);
    }
    if (topAligned != that.topAligned) {
      return topAligned.CompareTo(that.topAligned);
    }
    if (leftAligned != that.leftAligned) {
      return leftAligned.CompareTo(that.leftAligned);
    }
    if (buttons != that.buttons) {
      return buttons.CompareTo(that.buttons);
    }
    return 0;
  }
  public override string ToString() { return DStr(); }
  public string DStr() {
    return "ShowOverlayEvent(" +
        sizePercent.DStr() + ", " +
        backgroundColor.DStr() + ", " +
        fadeInEndMs.DStr() + ", " +
        fadeOutStartMs.DStr() + ", " +
        fadeOutEndMs.DStr() + ", " +
        automaticActionTriggerName.DStr() + ", " +
        text.DStr() + ", " +
        textColor.DStr() + ", " +
        textFadeInStartMs.DStr() + ", " +
        textFadeInEndMs.DStr() + ", " +
        textFadeOutStartMs.DStr() + ", " +
        textFadeOutEndMs.DStr() + ", " +
        topAligned.DStr() + ", " +
        leftAligned.DStr() + ", " +
        buttons.DStr()
        + ")";

    }
    public static ShowOverlayEvent Parse(ParseSource source) {
      source.Expect(NAME);
      source.Expect("(");
      var sizePercent = source.ParseInt();
      source.Expect(",");
      var backgroundColor = Color.Parse(source);
      source.Expect(",");
      var fadeInEndMs = source.ParseInt();
      source.Expect(",");
      var fadeOutStartMs = source.ParseInt();
      source.Expect(",");
      var fadeOutEndMs = source.ParseInt();
      source.Expect(",");
      var automaticActionTriggerName = source.ParseStr();
      source.Expect(",");
      var text = source.ParseStr();
      source.Expect(",");
      var textColor = Color.Parse(source);
      source.Expect(",");
      var textFadeInStartMs = source.ParseInt();
      source.Expect(",");
      var textFadeInEndMs = source.ParseInt();
      source.Expect(",");
      var textFadeOutStartMs = source.ParseInt();
      source.Expect(",");
      var textFadeOutEndMs = source.ParseInt();
      source.Expect(",");
      var topAligned = source.ParseBool();
      source.Expect(",");
      var leftAligned = source.ParseBool();
      source.Expect(",");
      var buttons = ButtonImmList.Parse(source);
      source.Expect(")");
      return new ShowOverlayEvent(sizePercent, backgroundColor, fadeInEndMs, fadeOutStartMs, fadeOutEndMs, automaticActionTriggerName, text, textColor, textFadeInStartMs, textFadeInEndMs, textFadeOutStartMs, textFadeOutEndMs, topAligned, leftAligned, buttons);
  }
}
       
}
