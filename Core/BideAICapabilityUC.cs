using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class BideAICapabilityUC {
  public readonly Root root;
  public readonly int id;
  public BideAICapabilityUC(Root root, int id) {
    this.root = root;
    this.id = id;
  }
  public BideAICapabilityUCIncarnation incarnation { get { return root.GetBideAICapabilityUCIncarnation(id); } }
  public void AddObserver(IBideAICapabilityUCEffectObserver observer) {
    root.AddBideAICapabilityUCObserver(id, observer);
  }
  public void RemoveObserver(IBideAICapabilityUCEffectObserver observer) {
    root.RemoveBideAICapabilityUCObserver(id, observer);
  }
  public void Delete() {
    root.EffectBideAICapabilityUCDelete(id);
  }
  public static BideAICapabilityUC Null = new BideAICapabilityUC(null, 0);
  public bool Exists() { return root != null && root.BideAICapabilityUCExists(id); }
  public bool NullableIs(BideAICapabilityUC that) {
    if (!this.Exists() && !that.Exists()) {
      return true;
    }
  if (!this.Exists() || !that.Exists()) {
    return false;
  }
    return this.Is(that);
  }
  public bool Is(BideAICapabilityUC that) {
    if (!this.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    if (!that.Exists()) {
      throw new Exception("Called Is on a null!");
    }
    return this.root == that.root && id == that.id;
  }
       }
}
