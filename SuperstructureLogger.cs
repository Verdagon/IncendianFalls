using System;
using System.IO;
using Atharia.Model;

namespace IncendianFalls {
  public class SuperstructureRequestLogger : IRequestObserver {
    private readonly string logFilename;
    public SuperstructureRequestLogger(string logFilename) {
      this.logFilename = logFilename;
    }

    public void AfterRequest(IRequest request) { }

    public void BeforeRequest(IRequest request) {
      StreamWriter writer = new StreamWriter(logFilename, true);
      writer.Write(request.DStr() + "\n");
      writer.Flush();
    }
  }

  public class SuperstructureRequestReader {
    StreamReader reader;
    public SuperstructureRequestReader(string logFilename) {
      reader = new StreamReader(logFilename);
    }
    public IRequest Read() {
      string line = reader.ReadLine();
      if (line == null) {
        Console.WriteLine("Reached end!");
        return null;
      }
      Console.WriteLine("Read: " + line);
      ParseSource source = new ParseSource(line);
      var request = IRequestParser.Parse(source);
      Console.WriteLine("Parsed: " + request.ToString());
      Asserts.Assert(line.Trim() == request.DStr().Trim());
      return request;
    }
  }
}

