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
  public class NormalPageController : IPageController {
    OverlayPaneler overlayPaneler;
    IClock cinematicTimer;
    InputSemaphore inputSemaphore;
    bool isFullscreen;
    bool longBackgroundFade;

    const int PANEL_PADDING = 1;
    const int PANEL_MARGIN = 1;

    public NormalPageController(
        OverlayPaneler overlayPaneler,
        IClock cinematicTimer,
        InputSemaphore inputSemaphore,
        bool isFullscreen,
        bool longBackgroundFade) {
      this.overlayPaneler = overlayPaneler;
      this.cinematicTimer = cinematicTimer;
      this.inputSemaphore = inputSemaphore;
      this.isFullscreen = isFullscreen;
      this.longBackgroundFade = longBackgroundFade;
    }

    // G = in grid units
    public (int, int) GetPageTextMaxGWAndGH(
        int maxGW,
        int maxGH,
        List<OverlayPresenter.PageButton> buttons) {
      if (isFullscreen) {
        return (maxGW, maxGH);
      } else {
        // We want 50 characters wide for text, or if the screens to small, then whatever the width is minus some side margin.
        int gw = Math.Min(50, maxGW - PANEL_MARGIN * 2 - PANEL_PADDING * 2);
        // Margin on top and bottom
        int gh = maxGH - 2;

        return (gw, gh);
      }
    }

    // Normal overlay takes up a 2x1 at top of screen, wide as screen.
    public OverlayPanelView ShowPage(
        List<string> pageLines,
        UnityEngine.Color textColor,
        List<OverlayPresenter.PageButton> buttons,
        bool fadeInBackground,
        bool fadeOutBackground,
        bool isPortrait,
        bool callCallbackAfterFadeOut) {

      int fadeTimeMultiplier = longBackgroundFade ? 3 : 1;

      if (buttons.Count == 0) {
        // Do nothing. It'll close itself and nothing else.
        buttons.Add(new OverlayPresenter.PageButton(" ", () => { }));
      }

      // Will be unlocked by the buttons being clicked.
      inputSemaphore.Lock();

      var (textMaxWidth, textMaxHeight) = GetPageTextMaxGWAndGH(overlayPaneler.screenGW, overlayPaneler.screenGH, buttons);
      if (pageLines.Count > textMaxHeight) {
        Debug.LogError("Too many lines for this kind of overlay!");
      }

      // - 1 line for borders
      // - as many lines for text as we can fit
      // - 1 line for space between text and buttons
      // - 2 lines for buttons
      int numLinesBetweenTextAndButtons = 1;
      int numLinesForButtons = 3; // 1 for top border, 1 for the text, 1 for bottom border.

      int panelWidth = overlayPaneler.screenGW - PANEL_MARGIN * 2;

      // Can be smaller than maxHeight.
      int panelHeight = pageLines.Count + PANEL_PADDING + numLinesBetweenTextAndButtons + numLinesForButtons + PANEL_PADDING;

      OverlayPanelView panelView;
      if (isFullscreen) {
        panelView = overlayPaneler.MakePanel(-1, -1, overlayPaneler.screenGW + 2, overlayPaneler.screenGH + 2);
      } else {
        panelView = overlayPaneler.MakePanel(PANEL_MARGIN, overlayPaneler.screenGH - panelHeight - PANEL_MARGIN, panelWidth, panelHeight);
      }

      int backgroundId;
      if (isFullscreen) {
        backgroundId =
        panelView.AddFullscreenRect(new UnityEngine.Color(0, 0, 0, 1));
      } else {
        backgroundId =
          panelView.AddBackground(
            new UnityEngine.Color(0, 0, 0, .85f),
            new UnityEngine.Color(.2f, .2f, .2f, .85f));
      }
      if (fadeInBackground) {
        panelView.SetFadeIn(backgroundId, new OverlayPanelView.FadeIn(0, 300 * fadeTimeMultiplier));
      }
      if (fadeOutBackground) {
        panelView.SetFadeOut(backgroundId, new OverlayPanelView.FadeOut(-300 * fadeTimeMultiplier, 0));
      }

      for (int i = 0; i < panelView.symbolsHigh && i < pageLines.Count; i++) {
        var textIds =
          panelView.AddString(
            0, 1f, panelView.symbolsHigh - 2 - i, panelView.symbolsWide,
          textColor, Fonts.PROSE_OVERLAY_FONT,
            pageLines[i]);
        foreach (var textId in textIds) {
          if (fadeInBackground) {
            panelView.SetFadeIn(textId, new OverlayPanelView.FadeIn(300 * fadeTimeMultiplier, 600 * fadeTimeMultiplier));
          } else {
            panelView.SetFadeIn(textId, new OverlayPanelView.FadeIn(0, 300 * fadeTimeMultiplier));
          }
          panelView.SetFadeOut(textId, new OverlayPanelView.FadeOut(-300 * fadeTimeMultiplier, 0));
        }
      }

      foreach (var button in buttons) {
        var buttonCallback = button.clicked;
        var buttonText = button.label;
        int buttonTextWidth = buttonText.Length;
        int buttonTextHeight = 1;
        int buttonBorderWidth = 1;
        int buttonWidth = buttonBorderWidth + buttonTextWidth + buttonBorderWidth;
        int buttonHeight = buttonBorderWidth + buttonTextHeight + buttonBorderWidth;

        int buttonId =
          panelView.AddButton(
            0,
            panelWidth - PANEL_PADDING - buttonWidth,
            1,
            buttonWidth,
            buttonHeight,
            1,
            new UnityEngine.Color(.3f, .3f, .3f, 1),
            new UnityEngine.Color(.1f, .1f, .1f, 1),
            new UnityEngine.Color(1f, 1f, 1f, 1),
            () => {
              if (!callCallbackAfterFadeOut) {
                panelView.SetOnStartHideCallback(buttonCallback);
              }
              panelView.SetOnFinishHideCallback(() => {
                inputSemaphore.Unlock();
                if (callCallbackAfterFadeOut) {
                  buttonCallback();
                }
              });
              panelView.ScheduleClose(0);
            },
            () => { }, () => { });
        panelView.AddString(
          buttonId,
          panelWidth - PANEL_PADDING - buttonWidth + buttonBorderWidth,
          2f,
          buttonTextWidth,
          new UnityEngine.Color(1, 1, 1, 1),
            Fonts.PROSE_OVERLAY_FONT, buttonText);
        if (fadeInBackground) {
          panelView.SetFadeIn(buttonId, new OverlayPanelView.FadeIn(300 * fadeTimeMultiplier, 600 * fadeTimeMultiplier));
        } else {
          panelView.SetFadeIn(buttonId, new OverlayPanelView.FadeIn(0, 300 * fadeTimeMultiplier));
        }
        panelView.SetFadeOut(buttonId, new OverlayPanelView.FadeOut(-300 * fadeTimeMultiplier, 0));
      }

      return panelView;

      //if (wrapped) {
      //  panelView.AddString(
      //    0,
      //    panelWidth - ellipsisWidth - rightBorderWidth,
      //    numLinesForBottomBorder + numLinesForButtons + 0.4f,
      //    3,
      //    new UnityEngine.Color(1, 1, 1, 1), font,
      //    "...");
      //}
    }
  }
}
