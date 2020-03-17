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
    public NormalPageController(
        OverlayPaneler overlayPaneler,
        IClock cinematicTimer,
        InputSemaphore inputSemaphore) {
      this.overlayPaneler = overlayPaneler;
      this.cinematicTimer = cinematicTimer;
      this.inputSemaphore = inputSemaphore;
    }

    public (int, int) GetPageTextMaxWidthAndHeight(bool isPortrait, List<OverlayPresenter.PageButton> buttons) {
      if (isPortrait) {
        return (28, 9);
      } else {
        return (43, 14);
      }
    }

    // Normal overlay takes up a 2x1 at top of screen, wide as screen.
    public void ShowPage(
        List<string> pageLines,
        List<OverlayPresenter.PageButton> buttons,
        bool isFirstPage,
        bool isLastPage,
        bool isPortrait) {

      if (buttons.Count == 0) {
        // Do nothing. It'll close itself and nothing else.
        buttons.Add(new OverlayPresenter.PageButton(" ", () => { }));
      }

      // Will be unlocked by the buttons being clicked.
      inputSemaphore.Lock();

      var font = new OverlayFont("cascadia", 2f);

      var (textMaxWidth, textMaxHeight) = GetPageTextMaxWidthAndHeight(isPortrait, buttons);
      if (pageLines.Count > textMaxHeight) {
        Debug.LogError("Too many lines for this kind of overlay!");
      }

      // - 1 line for borders
      // - as many lines for text as we can fit
      // - 1 line for space between text and buttons
      // - 2 lines for buttons
      int numLinesForTopBorder = 1;
      int numLinesForBottomBorder = 1;
      int numLinesBetweenTextAndButtons = 1;
      int leftBorderWidth = 1;
      int rightBorderWidth = 1;
      int numLinesForButtons = 3; // 1 for top border, 1 for the text, 1 for bottom border.

      int panelWidth = textMaxWidth + leftBorderWidth + rightBorderWidth;

      // Can be smaller than maxHeight.
      int panelHeight = pageLines.Count + numLinesForTopBorder + numLinesBetweenTextAndButtons + numLinesForButtons + numLinesForBottomBorder;

      OverlayPanelView panelView;
      if (isPortrait) {
        panelView = overlayPaneler.MakePanel(cinematicTimer, 50, 100, 100, 50, panelWidth, panelHeight, .6667f);
      } else {
        panelView = overlayPaneler.MakePanel(cinematicTimer, 50, 80, 80, 50, panelWidth, panelHeight, .6667f);
      }
      int backgroundId =
        panelView.AddBackground(
          new UnityEngine.Color(0, 0, 0, .85f),
          new UnityEngine.Color(.2f, .2f, .2f, .85f));
      if (isFirstPage) {
        panelView.SetFadeIn(backgroundId, new OverlayPanelView.FadeIn(0, 300));
      }
      if (isLastPage) {
        panelView.SetFadeOut(backgroundId, new OverlayPanelView.FadeOut(-300, 0));
      }

      for (int i = 0; i < panelView.symbolsHigh && i < pageLines.Count; i++) {
        var textIds =
          panelView.AddString(
            0, 1f, panelView.symbolsHigh - 2 - i, panelView.symbolsWide,
            new UnityEngine.Color(.3f, 1, 1, 1), font,
            pageLines[i]);
        foreach (var textId in textIds) {
          if (isFirstPage) {
            panelView.SetFadeIn(textId, new OverlayPanelView.FadeIn(300, 600));
          } else {
            panelView.SetFadeIn(textId, new OverlayPanelView.FadeIn(0, 300));
          }
          panelView.SetFadeOut(textId, new OverlayPanelView.FadeOut(-300, 0));
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
            panelWidth - rightBorderWidth - buttonWidth,
            1,
            buttonWidth,
            buttonHeight,
            1,
            new UnityEngine.Color(.4f, .4f, .4f, 1),
            new UnityEngine.Color(.6f, .6f, .6f, 1),
            new UnityEngine.Color(.3f, .3f, .3f, 1),
            () => {
              panelView.ScheduleClose(
                0,
                () => {
                  buttonCallback();
                  inputSemaphore.Unlock();
                });
            });
        panelView.SetFadeIn(buttonId, new OverlayPanelView.FadeIn(0, 300));
        panelView.SetFadeOut(buttonId, new OverlayPanelView.FadeOut(-300, 0));
        panelView.AddString(
          buttonId,
          panelWidth - rightBorderWidth - buttonWidth + buttonBorderWidth,
          2f,
          buttonTextWidth,
          new UnityEngine.Color(1f, 1f, 1f, 1),
            font, buttonText);
      }


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
