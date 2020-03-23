﻿using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using AthPlayer;
using Domino;
using UnityEngine;
using UnityEngine.UI;

namespace AthPlayer {
  public class LookPanelView {
    IClock cinematicTimer;
    OverlayPaneler overlayPaneler;
    OverlayPanelView visibleOverlayPanelView;

    public LookPanelView(IClock cinematicTimer, OverlayPaneler overlayPaneler) {
      this.cinematicTimer = cinematicTimer;
      this.overlayPaneler = overlayPaneler;
    }

    public void ShowMessage(string message) {
      SetStuff(true, message, "", new List<KeyValuePair<SymbolDescription, string>>());
    }
    public void ClearMessage() {
      if (visibleOverlayPanelView != null) {
        visibleOverlayPanelView.ScheduleClose(0);
        visibleOverlayPanelView = null;
      }
    }
    public void SetStuff(
        bool visible,
        string message,
        string status,
        List<KeyValuePair<SymbolDescription, string>> symbolsAndLabels) {
      ClearMessage();
      if (!visible) {
        return;
      }
      visibleOverlayPanelView =
        overlayPaneler.MakePanel(cinematicTimer, 0, 0, 100, 30, 70, 7, .6667f);
      visibleOverlayPanelView.AddRectangle(
        0, 0, 3, visibleOverlayPanelView.symbolsWide - 3.25f, visibleOverlayPanelView.symbolsHigh - 3, 1, new UnityEngine.Color(0, 0, 0, .85f), new UnityEngine.Color(0, 0, 0, 0));

      int buttonsWidth = 3;

      if (status.Length == 0 && symbolsAndLabels.Count == 0) {
        var lines = LineWrapper.Wrap(message, 68 - buttonsWidth);
        for (int i = 0; i < lines.Length; i++) {
          visibleOverlayPanelView.AddString(0, 1, 5 - i, 68, new UnityEngine.Color(1, 1, 1, 1), Fonts.PROSE_OVERLAY_FONT, lines[i]);
        }
      } else {
        visibleOverlayPanelView.AddString(0, 1, 5, 68, new UnityEngine.Color(1, 1, 1, 1), Fonts.PROSE_OVERLAY_FONT, message);
        visibleOverlayPanelView.AddString(0, 70 - buttonsWidth - 1 - status.Length, 5, 58, new UnityEngine.Color(1, 1, 1, 1), Fonts.PROSE_OVERLAY_FONT, status);
        visibleOverlayPanelView.SetFadeIn(0, new OverlayPanelView.FadeIn(0, 100));
        visibleOverlayPanelView.SetFadeOut(0, new OverlayPanelView.FadeOut(-200, 0));

        int x = 0;
        foreach (var symbolAndLabel in symbolsAndLabels) {
          x += 1; // Left margin

          var symbol = symbolAndLabel.Key;
          var label = symbolAndLabel.Value;
          visibleOverlayPanelView.AddSymbol(0, x, 3, 1f, 1, symbol.frontColor, Fonts.SYMBOLS_OVERLAY_FONT, symbol.symbolId, false);
          x += 2; // Symbol takes up a lot of space

          visibleOverlayPanelView.AddString(0, x, 3, 20, new UnityEngine.Color(1, 1, 1, 1), Fonts.PROSE_OVERLAY_FONT, label);
          x += label.Length;

          x += 1; // Right margin
        }
      }
    }
  }
}
