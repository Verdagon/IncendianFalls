using System.Collections;
using System.Collections.Generic;
using Atharia.Model;
using AthPlayer;
using Domino;
using UnityEngine;
using UnityEngine.UI;

namespace AthPlayer {
  public class ListView {
    public class Entry {
      public string symbol;
      public string text;

      public Entry(string symbol, string text) {
        this.symbol = symbol;
        this.text = text;
      }
    }

    //IClock cinematicTimer;
    //OverlayPaneler overlayPaneler;
    OverlayPanelView view;

    public ListView(OverlayPanelView view) {
      //this.cinematicTimer = cinematicTimer;
      //this.overlayPaneler = overlayPaneler;

      this.view = view;
    }

    public void ShowEntries(List<Entry> entries) {
      view.Remove(0);
      if (entries.Count > 0) {
        view.AddBackground(new UnityEngine.Color(0, 0, 0, .9f), new UnityEngine.Color(0, 0, 0, 0));

        for (int i = 0; i < entries.Count; i++) {
          view.AddSymbol(0, 1, view.symbolsHigh - (i * 2 + 2), 2.0f, 0, new UnityEngine.Color(1, 1, 1), Fonts.SYMBOLS_OVERLAY_FONT, entries[i].symbol);
          view.AddString(0, 5, view.symbolsHigh - (i * 2 + 2 - 0.5f), view.symbolsWide - 3, new UnityEngine.Color(1, 1, 1), Fonts.PROSE_OVERLAY_FONT, entries[i].text);
        }
      }
    }
  }
}
