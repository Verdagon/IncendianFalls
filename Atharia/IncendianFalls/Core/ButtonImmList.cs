using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ButtonImmList : IEnumerable<Button> {
  List<Button> list;

  public ButtonImmList() {
    this.list = new List<Button>();
  }
  public ButtonImmList(Button[] list) {
    this.list = new List<Button>(list);
  }
  public ButtonImmList(IEnumerable<Button> list) {
    this.list = new List<Button>(list);
  }
  public int Count { get { return list.Count; } }

  public Button this[int index] { get { return list[index]; } }

  public IEnumerator<Button> GetEnumerator() {
    return list.GetEnumerator();
  }

  public int CompareTo(ButtonImmList that) {
    for (int i = 0; i < Count || i < that.Count; i++) {
      if (i >= Count) {
        return -1;
      }
      if (i >= that.Count) {
        return 1;
      }
      int diff = this[i].CompareTo(that[i]);
      if (diff != 0) {
        return diff;
      }
    }
    return 0;
  }

  public static ButtonImmList Parse(ParseSource source) {
    throw new Exception("Not implemented!");
  }

  public string DStr() {
    string result = "";
    foreach (var element in list) {
      result += element.DStr() + ", ";
    }
    return "(" + result + ")";
  }

  public int GetDeterministicHashCode() {
    int hash = 0;
    hash = hash * 37 + list.Count;
    foreach (var element in list) {
      hash = hash * 37 + element.GetDeterministicHashCode();
    }
    return hash;
  }
  IEnumerator<Button> IEnumerable<Button>.GetEnumerator() {
    return ((IEnumerable<Button>)list).GetEnumerator();
  }
  System.Collections.IEnumerator IEnumerable.GetEnumerator() {
    return this.GetEnumerator();
  }
}
         
}
