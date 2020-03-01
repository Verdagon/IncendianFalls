using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {
public class OverlayIncarnation {
  public readonly int sizePercent;
  public readonly Color backgroundColor;
  public readonly string overlayText;
  public readonly Color overlayTextColor;
  public readonly bool topAligned;
  public readonly bool leftAligned;
  public readonly int fadeInMs;
  public readonly int fadeOutMs;
  public readonly ButtonImmList buttons;
  public readonly int automaticDismissDelayMs;
  public readonly string automaticDismissTriggerName;
  public OverlayIncarnation(
      int sizePercent,
      Color backgroundColor,
      string overlayText,
      Color overlayTextColor,
      bool topAligned,
      bool leftAligned,
      int fadeInMs,
      int fadeOutMs,
      ButtonImmList buttons,
      int automaticDismissDelayMs,
      string automaticDismissTriggerName) {
    this.sizePercent = sizePercent;
    this.backgroundColor = backgroundColor;
    this.overlayText = overlayText;
    this.overlayTextColor = overlayTextColor;
    this.topAligned = topAligned;
    this.leftAligned = leftAligned;
    this.fadeInMs = fadeInMs;
    this.fadeOutMs = fadeOutMs;
    this.buttons = buttons;
    this.automaticDismissDelayMs = automaticDismissDelayMs;
    this.automaticDismissTriggerName = automaticDismissTriggerName;
  }
}

}
