using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OverlayIncarnation {
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
  public OverlayIncarnation(
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
  }
}

}
