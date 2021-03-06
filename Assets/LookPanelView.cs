﻿using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using AthPlayer;
using Domino;
using UnityEngine;
using UnityEngine.UI;

namespace AthPlayer {
  public class LookPanelView {
    OverlayPaneler overlayPaneler;
    OverlayPanelView visibleOverlayPanelView;
    int panelGYInScreen;
    int bottomPadding;

    public LookPanelView(OverlayPaneler overlayPaneler, int panelGYInScreen, int bottomPadding) {
      this.overlayPaneler = overlayPaneler;
      this.panelGYInScreen = panelGYInScreen;
      this.bottomPadding = bottomPadding;
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

      int topPadding = 1;
      int contentYStart = bottomPadding;
      // 1 line of text, 1 padding between, 1 line of text
      int panelGH = bottomPadding + 1 + 1 + 1 + topPadding;


      int panelGXInScreen = 0;
      int panelGYInScreen = PlayerPanelView.PANEL_GH;
      visibleOverlayPanelView =
        overlayPaneler.MakePanel(panelGXInScreen, panelGYInScreen, overlayPaneler.screenGW, panelGH);
      visibleOverlayPanelView.AddRectangle(
        0,
        -1,
        0,
        1 + overlayPaneler.screenGW + 1,
        panelGH,
        1,
        new UnityEngine.Color(0, 0, 0, .85f), new UnityEngine.Color(0, 0, 0, 0));

      int buttonsWidth = 3;

      if (status.Length == 0 && symbolsAndLabels.Count == 0) {
        var lines = LineWrapper.Wrap(message, 68 - buttonsWidth);
        for (int i = 0; i < lines.Length; i++) {
          visibleOverlayPanelView.AddString(0, 1, contentYStart + 2 - i, 68, new UnityEngine.Color(1, 1, 1, 1), Fonts.PROSE_OVERLAY_FONT, lines[i]);
        }
      } else {
        visibleOverlayPanelView.AddString(0, 1, contentYStart + 2, 68, new UnityEngine.Color(1, 1, 1, 1), Fonts.PROSE_OVERLAY_FONT, message);
        visibleOverlayPanelView.AddString(0, 70 - buttonsWidth - 1 - status.Length, contentYStart + 2, 58, new UnityEngine.Color(1, 1, 1, 1), Fonts.PROSE_OVERLAY_FONT, status);
        visibleOverlayPanelView.SetFadeIn(0, new OverlayPanelView.FadeIn(0, 100));
        visibleOverlayPanelView.SetFadeOut(0, new OverlayPanelView.FadeOut(-200, 0));

        int x = 0;
        foreach (var symbolAndLabel in symbolsAndLabels) {
          x += 1; // Left margin

          var symbol = symbolAndLabel.Key;
          var label = symbolAndLabel.Value;
          visibleOverlayPanelView.AddSymbol(0, x, contentYStart, 1f, 1, symbol.frontColor.Get(long.MaxValue), Fonts.SYMBOLS_OVERLAY_FONT, symbol.symbolId.name, false);
          x += 2; // Symbol takes up a lot of space

          visibleOverlayPanelView.AddString(0, x, contentYStart, 20, new UnityEngine.Color(1, 1, 1, 1), Fonts.PROSE_OVERLAY_FONT, label);
          x += label.Length;

          x += 1; // Right margin
        }
      }
    }
  }
}
