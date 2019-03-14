using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class TimeCloneAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public TimeCloneAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public TimeCloneAICapabilityUCIncarnation incarnation { get { return root.GetTimeCloneAICapabilityUCIncarnation(id); } }
  public void AddObserver(ITimeCloneAICapabilityUCEffectObserver observer) {
    root.AddTimeCloneAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(ITimeCloneAICapabilityUCEffectObserver observer) {
    root.RemoveTimeCloneAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectTimeCloneAICapabilityUCDelete(id);
  }
  public static TimeCloneAICapabilityUC Null = new TimeCloneAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.TimeCloneAICapabilityUCExists(id); }
  public bool NullableIs(TimeCloneAICapabilityUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
    if (!this.Exists() || !that.Exists()) {
      return false;
    }
    return this.Is(that);
  }
  public void CheckForNullViolations(List<string> violations) {
  }
  public void FindReachableObjects(SortedSet<int> foundIds) {
    if (foundIds.Contains(id)) {
      return;
    }
    foundIds.Add(id);
    if (root.IRequestMutListExists(script.id)) {
      script.FindReachableObjects(foundIds);
    }
  }
  public bool Is(TimeCloneAICapabilityUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
         public IRequestMutList script {

    get {
      if (root == null) {
        throw new Exception("Tried to get member script of null!");
      }
      return new IRequestMutList(root, incarnation.script);
    }
                       }
}
}
