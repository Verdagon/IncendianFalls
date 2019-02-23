using System;
using System.Collections;

using System.Collections.Generic;

namespace Atharia.Model {

public class ParseSource {
  string source;
  int position;
  public ParseSource(string source) {
    this.source = source;
    this.position = 0;
  }
  public void Expect(string expectation) {
    SkipWhitespace();
    for (int i = 0; i < expectation.Length; i++) {
      if (position + i >= source.Length) {
        throw new Exception("Expected " + expectation + " but hit end of string! " + position);
      }
      if (source[position + i] != expectation[i]) {
        throw new Exception("Unexpected: " + expectation[i]);
      }
    }
    position += expectation.Length;
  }
  public void Expect(char expectation) {
    Expect(expectation.ToString());
  }
  public string PeekNextWord() {
    SkipWhitespace();
    int initialPosition = position;
    string nextThingPeek = "";
    while (source[position] != '(') {
      nextThingPeek += source[position];
      position++;
    }
    position = initialPosition;
    return nextThingPeek;
  }
  public string PeekNextChar() {
    SkipWhitespace();
    return source[position].ToString();
  }

  string NextWord() {
    string result = "";
    while (true) {
      if (position >= source.Length) {
        break;
      }
      char c = source[position];
      if (c == '(' || c == ')' || c == '"' || c == ' ' || c == '\t' || c == '\r' || c == '\n') {
        break;
      }
      result += source[position];
      position++;
    }
    return result;
  }
  public int ParseInt() {
    SkipWhitespace();
    string result = "";
    while (source[position] >= '0' && source[position] <= '9' || source[position] == '-') {
      result += source[position];
      position++;
    }
    return int.Parse(result);
  }
  public float ParseFloat() {
    SkipWhitespace();
    string result = "";
    while ((source[position] >= '0' && source[position] <= '9')| source[position] == '.' || source[position] == '-') {
      result += source[position];
      position++;
    }
    return float.Parse(result);
  }
  public bool ParseBool() {
    SkipWhitespace();
    var word = NextWord();
    switch (word) {
      case "true": return true;
      case "false": return false;
      default: throw new Exception("Unexpected: " + word);
    }
  }
  public string ParseStr() {
    SkipWhitespace();
    Expect('"');
    string result = "";
    while (source[position] != '"') {
      result += source[position];
      position++;
    }
    Expect('"');
    return result;
  }
  private void SkipWhitespace() {
    while (source[position] == '\t' || source[position] == '\n' || source[position] == '\r' || source[position] == ' ') {
      position++;
    }
  }
}
       
}
