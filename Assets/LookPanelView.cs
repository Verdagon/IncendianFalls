using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using Domino;
using UnityEngine;
using UnityEngine.UI;

namespace Domino {
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
      visibleOverlayPanelView.AddRectangle(0, 0, 3, visibleOverlayPanelView.symbolsWide, visibleOverlayPanelView.symbolsHigh - 3, 1, new UnityEngine.Color(0, 0, 0, .85f), new UnityEngine.Color(0, 0, 0, 0));

      int buttonsWidth = 2;

      visibleOverlayPanelView.AddString(0, 1, 5, 68, new UnityEngine.Color(1, 1, 1, 1), new OverlayFont("prose", 2f), message);
      visibleOverlayPanelView.AddString(0, 70 - buttonsWidth - 1 - 1 - status.Length, 5, 58, new UnityEngine.Color(1, 1, 1, 1), new OverlayFont("prose", 2f), status);
      visibleOverlayPanelView.SetFadeIn(0, new OverlayPanelView.FadeIn(0, 100));
      visibleOverlayPanelView.SetFadeOut(0, new OverlayPanelView.FadeOut(-200, 0));

      int x = 0;
      foreach (var symbolAndLabel in symbolsAndLabels) {
        x += 1; // Left margin

        var symbol = symbolAndLabel.Key;
        var label = symbolAndLabel.Value;
        visibleOverlayPanelView.AddSymbol(0, x, 3, 1f, 1, symbol.frontColor, new OverlayFont("symbols", 2.8f), symbol.symbolId, false);
        x += 2; // Symbol takes up a lot of space

        visibleOverlayPanelView.AddString(0, x, 3, 20, new UnityEngine.Color(1, 1, 1, 1), new OverlayFont("prose", 2f), label);
        x += label.Length;

        x += 1; // Right margin
      }
    }
  }
}
