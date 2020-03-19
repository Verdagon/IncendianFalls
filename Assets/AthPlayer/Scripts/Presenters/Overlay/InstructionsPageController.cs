using Atharia.Model;
using Domino;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace AthPlayer {
  public class InstructionsPageController : IPageController {

    // /---------------\
    // | text here     |
    // \---------------/
    // can only be dismissed by code.

    private static readonly int panelWidth = 30;
    private static readonly int panelTopPadding = 1;
    private static readonly int panelBottomPadding = 1;
    private static readonly int panelLeftPadding = 1;
    private static readonly int panelRightPadding = 1;
    // If buttons leave less than this amount of space for text, throw an error.
    // Someday, make it so we just move the buttons down or something.
    private static readonly int minTextWidth = 15;

    OverlayPaneler overlayPaneler;
    IClock cinematicTimer;
    public InstructionsPageController(
        OverlayPaneler overlayPaneler,
        IClock cinematicTimer) {
      this.overlayPaneler = overlayPaneler;
      this.cinematicTimer = cinematicTimer;
    }

    public (int, int) GetPageTextMaxWidthAndHeight(bool isPortrait, List<OverlayPresenter.PageButton> buttons) {
      int textWidth = panelWidth - panelLeftPadding - panelRightPadding;
      if (textWidth < minTextWidth) {
        Asserts.Assert(false, "Too little area for text!");
      }
      return (textWidth, 13);
    }

    // Instructions overlay takes up a 2x1 at top of screen, wide as screen.
    public OverlayPanelView ShowPage(
        List<string> pageLines,
        UnityEngine.Color textColor,
        List<OverlayPresenter.PageButton> buttons,
        bool fadeInBackground,
        bool fadeOutBackground,
        bool isPortrait,
        bool callCallbackAfterFadeOut) {
      var font = new OverlayFont("prose", 2f);

      int panelHeight = pageLines.Count + panelTopPadding + panelBottomPadding;

      int widthPercent = isPortrait ? 94 : 44;
      int horizontalAlignmentPercent = isPortrait ? 50 : 97;
      OverlayPanelView panelView =
        overlayPaneler.MakePanel(cinematicTimer, horizontalAlignmentPercent, 97, widthPercent, 44, panelWidth, panelHeight, .6667f);
      int backgroundId =
        panelView.AddBackground(
          new UnityEngine.Color(0, 0, 0, .9f),
          new UnityEngine.Color(.1f, .1f, .1f, .9f));
      if (fadeInBackground) {
        panelView.SetFadeIn(backgroundId, new OverlayPanelView.FadeIn(0, 300));
      }
      if (fadeOutBackground) {
        panelView.SetFadeOut(backgroundId, new OverlayPanelView.FadeOut(-300, 0));
      }

      for (int i = 0; i < panelView.symbolsHigh && i < pageLines.Count; i++) {
        var textIds =
          panelView.AddString(
            0, 1f, panelView.symbolsHigh - 2 - i, panelView.symbolsWide,
          textColor, font,
            pageLines[i]);
        foreach (var textId in textIds) {
          if (fadeInBackground) {
            panelView.SetFadeIn(textId, new OverlayPanelView.FadeIn(300, 600));
          } else {
            panelView.SetFadeIn(textId, new OverlayPanelView.FadeIn(0, 300));
          }
          panelView.SetFadeOut(textId, new OverlayPanelView.FadeOut(-300, 0));
        }
      }

      return panelView;
    }
  }
}
