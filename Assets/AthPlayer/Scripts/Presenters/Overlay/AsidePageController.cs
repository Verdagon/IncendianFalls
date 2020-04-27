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
  public class AsidePageController : IPageController {

    // /----------------X----\
    // | text here      | OK |
    // \----------------X----/

    // or, if theres more text,

    // /---------------------\
    // | i am thakuum,       |
    // | elder dragon   /----X
    // | of the east.   | OK |
    // \----------------X----/

    private static readonly int panelWidth = 30;
    private static readonly int panelTopPadding = 1;
    private static readonly int panelBottomPadding = 1;
    private static readonly int panelLeftPadding = 1;
    private static readonly int buttonLeftMargin = 1;
    private static readonly int buttonPadding = 1;
    private static readonly int buttonBetweenMargin = 1;
    // If buttons leave less than this amount of space for text, throw an error.
    // Someday, make it so we just move the buttons down or something.
    private static readonly int minTextWidth = 15;

    OverlayPaneler overlayPaneler;
    IClock cinematicTimer;
    bool isError;

    public AsidePageController(
        OverlayPaneler overlayPaneler,
        IClock cinematicTimer,
        bool isError) {
      this.overlayPaneler = overlayPaneler;
      this.cinematicTimer = cinematicTimer;
      this.isError = isError;
    }

    // G = in grid units
    public (int, int) GetPageTextMaxGWAndGH(
        int maxGW,
        int maxGH,
        List<OverlayPresenter.PageButton> buttons) {
      int textWidth = panelWidth - panelLeftPadding;
      foreach (var button in buttons) {
        textWidth -= buttonLeftMargin + buttonPadding + button.label.Length + buttonPadding;
      }
      if (textWidth < minTextWidth) {
        Asserts.Assert(false, "Too little area for text!");
      }
      return (textWidth, 3);
    }

    // Aside overlay takes up a 2x1 at top of screen, wide as screen.
    public OverlayPanelView ShowPage(
        List<string> pageLines,
        UnityEngine.Color textColor,
        List<OverlayPresenter.PageButton> buttons,
        bool fadeInBackground,
        bool fadeOutBackground,
        bool isPortrait,
        bool callCallbackAfterFadeOut) {
      var (textMaxWidth, textMaxHeight) = GetPageTextMaxGWAndGH(overlayPaneler.screenGW, overlayPaneler.screenGH, buttons);
      if (pageLines.Count > textMaxHeight) {
        Debug.LogError("Too many lines for this kind of overlay!");
      }

      int panelHeight = pageLines.Count + panelTopPadding + panelBottomPadding;

      OverlayPanelView panelView =
        overlayPaneler.MakePanel(1, overlayPaneler.screenGH - 1 - panelHeight, overlayPaneler.screenGW - 2, panelHeight);
      int backgroundId;
      if (isError) {
        backgroundId =
          panelView.AddBackground(
            new UnityEngine.Color(.3f, 0, 0, .9f),
            new UnityEngine.Color(.4f, .1f, .1f, .9f));
      } else {
        backgroundId =
        panelView.AddBackground(
          new UnityEngine.Color(0, 0, 0, .9f),
          new UnityEngine.Color(.1f, .1f, .1f, .9f));
      }
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
          textColor, Fonts.PROSE_OVERLAY_FONT,
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

      int nextButtonDistanceFromRight = 0;
      foreach (var button in buttons) {
        var buttonCallback = button.clicked;
        var buttonText = button.label;
        int buttonTextWidth = buttonText.Length;
        int buttonTextHeight = 1;
        int buttonWidth = buttonPadding + buttonTextWidth + buttonPadding;
        int buttonHeight = buttonPadding + buttonTextHeight + buttonPadding;

        int buttonId =
          panelView.AddButton(
            0,
            panelWidth - nextButtonDistanceFromRight - buttonWidth,
            0,
            buttonWidth,
            buttonHeight,
            1,
            new UnityEngine.Color(.4f, .4f, .4f, 1),
            new UnityEngine.Color(.5f, .5f, .5f, 1),
            new UnityEngine.Color(.3f, .3f, .3f, 1),
            () => {
              if (!callCallbackAfterFadeOut) {
                panelView.SetOnStartHideCallback(buttonCallback);
              } else {
                panelView.SetOnFinishHideCallback(buttonCallback);
              }
            },
            () => { }, () => { });
        panelView.SetFadeIn(buttonId, new OverlayPanelView.FadeIn(300, 600));
        panelView.SetFadeOut(buttonId, new OverlayPanelView.FadeOut(-300, 0));
        panelView.AddString(
          buttonId,
          panelWidth - buttonWidth,
          2f,
          buttonTextWidth,
          new UnityEngine.Color(1f, 1f, 1f, 1),
            Fonts.PROSE_OVERLAY_FONT, buttonText);

        nextButtonDistanceFromRight += buttonWidth + buttonBetweenMargin;
      }

      int numChars = 0;
      foreach (var line in pageLines) {
        numChars += line.Length;
      }
      // 600 for animations, 1000 to orient oneself to the dialog, and then time per word.
      int startClosingAfterMs = 600 + 1000 + numChars * 1000 / 20;
      panelView.ScheduleClose(startClosingAfterMs);

      return panelView;
    }
  }
}
