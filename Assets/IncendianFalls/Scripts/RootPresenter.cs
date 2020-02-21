using System;
using System.Collections.Generic;
using IncendianFalls;
using Atharia.Model;
using UnityEngine;
using UnityEngine.UI;
using Domino;
namespace IncendianFalls {
  public class TimerEntry {
    public readonly float time;
    public readonly int id;
    public readonly ITimerCallback callback;
    public TimerEntry(float time, int id, ITimerCallback callback) {
      this.time = time;
      this.id = id;
      this.callback = callback;
    }
  }

  public class TimerEntryComparer : IComparer<TimerEntry> {
    public int Compare(TimerEntry x, TimerEntry y) {
      var timeDiff = x.time - y.time;
      if (timeDiff != 0) {
        return Math.Sign(timeDiff);
      }
      return Math.Sign(x.id - y.id);
    }
  }

  // We need this later to call the code we're generating.
  public abstract class Sample {
    // Sometimes you can use C# dynamic instead of building an abstract class like this.
    public abstract int Demo(int value);
  }

  public class RootPresenter : MonoBehaviour, ITimer {
    ExecutionStaller resumeStaller;
    ExecutionStaller turnStaller;

    ISuperstructure ss;
    public Instantiator instantiator;
    public GameObject cameraObject;

    GamePresenter gamePresenter;
    PlayerController playerController;
    FollowingCameraController cameraController;

    private DateTime startTime;
    private SortedDictionary<TimerEntry, object> timers;
    private int nextTimerId = 1;
    
    public LookPanelView lookPanelView;

    public PlayerPanelView playerPanelView;

    public NarrationPanelView messageView;

    public SoundPlayer soundPlayer;

    public void Start() {
      //Debug.LogError("starting!");
      //DoWasmThing();
      //Debug.LogError("done!");

      startTime = System.DateTime.UtcNow;
      timers = new SortedDictionary<TimerEntry, object>(new TimerEntryComparer());
      resumeStaller = new ExecutionStaller(this);
      turnStaller = new ExecutionStaller(this);

      var timestamp = (int)DateTimeOffset.Now.ToUnixTimeMilliseconds();

      var modelSS = new Superstructure(new LoggerImpl());
      new ReplayLogger(modelSS, new string[] { "Latest.sslog", timestamp + ".sslog" });
      ss = new SuperstructureWrapper(modelSS);

      var randomSeed = timestamp;
      //var randomSeed = 1525224206;
      var game = ss.RequestSetupGame(randomSeed, false, false);
      gamePresenter =
          new GamePresenter(
              this, soundPlayer, resumeStaller, turnStaller, ss, game, instantiator, messageView);

      playerController =
          new PlayerController(
              this,
              resumeStaller,
              turnStaller,
              ss,
              ss.GetSuperstate(game.id),
              game,
              gamePresenter,
              playerPanelView,
              messageView,
              lookPanelView.GetComponent<LookPanelView>());
      playerController.Start();

      cameraController = new FollowingCameraController(cameraObject, game);
    }

    //public void DoWasmThing() {
    //  // Module can be used to create, read, modify, and write WebAssembly files.
    //  var module = new Module(); // In this case, we're creating a new one.

    //  // Types are function signatures: the list of parameters and returns.
    //  module.Types.Add(new WebAssemblyType // The first added type gets index 0.
    //  {
    //    Parameters = new[]
    //      {
    //            WebAssemblyValueType.Int32, // This sample takes a single Int32 as input.
    //            // Complex types can be passed by sending them in pieces.
    //        },
    //    Returns = new[]
    //      {
    //            // Multiple returns are supported by the binary format.
    //            // Standard currently allows a count of 0 or 1, though.
    //            WebAssemblyValueType.Int32,
    //        },
    //  });
    //  // Types can be re-used for multiple functions to reduce WASM size.

    //  // The function list associates a function index to a type index.
    //  module.Functions.Add(new Function // The first added function gets index 0.
    //  {
    //    Type = 0, // The index for the "type" value added above.
    //  });

    //  // Code must be passed in the exact same order as the Functions above.
    //  module.Codes.Add(new FunctionBody {
    //    Code = new Instruction[]
    //      {
    //            new LocalGet(0), // The parameters are the first locals, in order.
    //            // We defined the first parameter as Int32, so now an Int32 is at the top of the stack.
    //            new Int32CountOneBits(), // Returns the count of binary bits set to 1.
    //            // It takes the Int32 from the top of the stack, and pushes the return value.
    //            // So, in the end, there is still a single Int32 on the stack.
    //            new End(), // All functions must end with "End".
    //                       // The final "End" also delivers the returned value.
    //      },
    //  });

    //  // Exports enable features to be accessed by external code.
    //  // Typically this means JavaScript, but this library adds .NET execution capability, too.
    //  module.Exports.Add(new Export {
    //    Kind = ExternalKind.Function,
    //    Index = 0, // This should match the function index from above.
    //    Name = "Demo", // Anything legal in Unicode is legal in an export name.
    //  });

    //  Debug.LogError("llama a!");

    //  // We now have enough for a usable WASM file, which we could save with module.WriteToBinary().
    //  // Below, we show how the Compile feature can be used for .NET-based execution.
    //  // For stream-based compilation, WebAssembly.Compile should be used.
    //  var instanceCreator = module.Compile<Sample>();

    //  Debug.LogError("llama b!");

    //  // Instances should be wrapped in a "using" block for automatic disposal.
    //  // This sample doesn't import anything, so we pass an empty import dictionary.
    //  using (var instance = instanceCreator(new ImportDictionary())) {
    //    Debug.LogError("llama c!");

    //    // FYI, instanceCreator can be used multiple times to create independent instances.
    //    Debug.LogError(instance.Exports.Demo(0)); // Binary 0, result 0
    //    Debug.LogError(instance.Exports.Demo(1)); // Binary 1, result 1,
    //    Debug.LogError(instance.Exports.Demo(42));  // Binary 101010, result 3

    //    Debug.LogError("llama d!");
    //  } // Automatically release the WebAssembly instance here.

    //  Debug.LogError("llama e!");
    //}

    public float GetTime() {
      DateTime now = System.DateTime.UtcNow;
      float difference = (float)now.Subtract(startTime).TotalMilliseconds / 1000.0f;
      return difference;
    }

    public void ScheduleTimer(float secondsFromNow, ITimerCallback callback) {
      int timerId = nextTimerId++;
      float now = GetTime();
      timers.Add(new TimerEntry(now + secondsFromNow, timerId, callback), new object());
    }

    public void Update() {
      if (timers == null) {
        Debug.LogError("timers is null!?");
        return;
      }
      var timersCopy =
          new SortedDictionary<TimerEntry, object>(timers, new TimerEntryComparer());
      while (timersCopy.Count > 0) {
        float now = GetTime();
        var first = DictionaryUtils.GetFirstKey(timersCopy);
        //Logger.Warning("Frame at " + now + ", late by: " + (now - first.time) + ", executing?: " + (first.time < now));
        if (first.time < now) {
          timersCopy.Remove(first);
          timers.Remove(first);
          first.callback();
        } else {
          break;
        }
      }

      if (Input.GetKeyUp(KeyCode.A)) {
        TimeAnchorMoveClicked();
      }
      if (Input.GetKeyUp(KeyCode.R)) {
        TimeShiftClicked();
      }
      if (Input.GetKeyUp(KeyCode.I)) {
        InteractClicked();
      }
      if (Input.GetKeyUp(KeyCode.D)) {
        DefendClicked();
      }
      if (Input.GetKeyUp(KeyCode.C)) {
        CounterClicked();
      }
      if (Input.GetKeyUp(KeyCode.F)) {
        FireClicked();
      }
      if (Input.GetKeyUp(KeyCode.Slash)) {
        playerController.ActivateCheat("warptoend");
      }

      if (Input.GetKey(KeyCode.RightArrow)) {
        cameraController.MoveRight(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.LeftArrow)) {
        cameraController.MoveLeft(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.UpArrow)) {
        cameraController.MoveUp(Time.deltaTime);
      }
      if (Input.GetKey(KeyCode.DownArrow)) {
        cameraController.MoveDown(Time.deltaTime);
      }

      UnityEngine.Ray ray = cameraObject.GetComponentInChildren<Camera>().ScreenPointToRay(Input.mousePosition);

      Location hoveredLocation = null;
      RaycastHit hit;
      if (Physics.Raycast(ray, out hit)) {
        if (hit.collider != null) {
          hoveredLocation = gamePresenter.LocationFor(hit.collider.gameObject);
        }
      }
      gamePresenter.SetHighlightedLocation(hoveredLocation);

      Unit unit = null;
      if (hoveredLocation != null) {
        unit = gamePresenter.UnitAtLocation(hoveredLocation);
      }
      playerController.LookAt(unit);

      if (hoveredLocation != null && Input.GetMouseButtonDown(0)) {
        playerController.OnTileMouseClick(hoveredLocation);
      }
    }

    public void TimeAnchorMoveClicked() {
      playerController.TimeAnchorMoveClicked();
    }

    public void TimeShiftClicked() {
      playerController.TimeShiftClicked();
    }

    public void InteractClicked() {
      playerController.InteractClicked();
    }

    public void DefendClicked() {
      playerController.DefendClicked();
    }

    public void CounterClicked() {
      playerController.CounterClicked();
    }

    public void FireClicked() {
      playerController.FireClicked();
    }

  }
}
