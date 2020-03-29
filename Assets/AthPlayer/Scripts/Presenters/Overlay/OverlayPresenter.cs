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
  // Handles the pagination and stuff for showing an overlay, and with a certain theme.
  // We could theoretically make multiple of these.
  public class OverlayPresenter {
    public class PageButton {
      public string label;
      public OverlayPanelView.OnClicked clicked;

      public PageButton(
          string label,
          OverlayPanelView.OnClicked clicked) {
        this.label = label;
        this.clicked = clicked;
      }
    }

    public class PageText {
      public string text;
      public UnityEngine.Color color;

      public PageText(
          string text,
          UnityEngine.Color color) {
        this.text = text;
        this.color = color;
      }
    }

    private class SubPageText {
      public List<string> wrappedLines;
      public UnityEngine.Color color;

      public SubPageText(
          List<string> wrappedLines,
          UnityEngine.Color color) {
        this.wrappedLines = wrappedLines;
        this.color = color;
      }
    }

    public delegate void OnExit();

    bool isPortrait;

    List<PageButton> finalButtons;
    bool isObscuring;

    List<SubPageText> subPageTexts;

    IPageController pageController;
    int currentSubPageIndex;

    OverlayPanelView currentPageOverlayPanelView;

    public OverlayPresenter(
        SlowableTimerClock cinematicTimer,
        OverlayPaneler overlayPaneler,
        InputSemaphore inputSemaphore,
        ICommTemplate template,
        List<PageText> pageTexts,
        List<PageButton> buttons) {
      //this.cinematicTimer = cinematicTimer;
      //this.overlayPaneler = overlayPaneler;
      //this.inputSemaphore = inputSemaphore;

      isPortrait = Screen.height > Screen.width;


      this.finalButtons = buttons;
      this.isObscuring = false;

      if (template is DramaticCommTemplateAsICommTemplate dc) {
        isObscuring = dc.obj.isObscuring;
        pageController = new NormalPageController(overlayPaneler, cinematicTimer, inputSemaphore, true, true);
      } else if (template is AsideCommTemplateAsICommTemplate a) {
        pageController = new AsidePageController(overlayPaneler, cinematicTimer, false);
      } else if (template is DialogueCommTemplateAsICommTemplate d) {
        pageController = new NormalPageController(overlayPaneler, cinematicTimer, inputSemaphore, false, false);
      } else if (template is ErrorCommTemplateAsICommTemplate e) {
        pageController = new AsidePageController(overlayPaneler, cinematicTimer, true);
      } else if (template is NormalCommTemplateAsICommTemplate) {
        pageController = new NormalPageController(overlayPaneler, cinematicTimer, inputSemaphore, false, false);
      } else if (template is InstructionsCommTemplateAsICommTemplate) {
        pageController = new InstructionsPageController(overlayPaneler, cinematicTimer);
      } else {
        Asserts.Assert(false);
      }

      subPageTexts = new List<SubPageText>();
      foreach (var pageText in pageTexts) {
        var (textMaxWidth, textMaxHeight) = pageController.GetPageTextMaxWidthAndHeight(isPortrait, finalButtons);
        var wrappedPageLines = LineWrapper.Wrap(pageText.text, textMaxWidth);

        List<string> subPageLines = new List<string>();
        foreach (var line in wrappedPageLines) {
          subPageLines.Add(line);
          if (subPageLines.Count == textMaxHeight) {
            subPageTexts.Add(new SubPageText(subPageLines, pageText.color));
            subPageLines = new List<string>();
          }
        }
        if (subPageLines.Count > 0) {
          subPageTexts.Add(new SubPageText(subPageLines, pageText.color));
          subPageLines = new List<string>();
        }
      }

      ShowPage(0);
    }

    private void ShowPage(int subPageIndex) {
      currentSubPageIndex = subPageIndex;

      var (textMaxWidth, textMaxHeight) = pageController.GetPageTextMaxWidthAndHeight(isPortrait, finalButtons);

      var isFirstSubPage = currentSubPageIndex == 0;
      var isLastSubPage = currentSubPageIndex == subPageTexts.Count - 1;

      //int numPages = (currentPageWrappedLines.Count + textMaxHeight - 1) / textMaxHeight; // Rounds up

      List<PageButton> buttons = new List<PageButton>();
      if (isLastSubPage) {
        //foreach (var finalButton in finalButtons) {
        //  buttons.Add(new PageButton(finalButton.label, () => {
        //    finalButton.clicked();
        //  }));
        //}
        buttons = finalButtons;
      } else {
        buttons.Add(new PageButton("...", () => ShowPage(subPageIndex + 1)));
      }

      var fadeInBackground = isFirstSubPage;
      var fadeOutBackground = isLastSubPage;
      var callCallbackAfterFadeOut = !isObscuring;

      Debug.LogError("lines: " + subPageTexts[currentSubPageIndex].wrappedLines.Count);

      currentPageOverlayPanelView =
        pageController.ShowPage(
          subPageTexts[currentSubPageIndex].wrappedLines,
          subPageTexts[currentSubPageIndex].color,
          buttons,
          fadeInBackground,
          fadeOutBackground,
          isPortrait,
          callCallbackAfterFadeOut);
    }

    public void Close() {
      currentPageOverlayPanelView.ScheduleClose(0);
    }
  }
}
