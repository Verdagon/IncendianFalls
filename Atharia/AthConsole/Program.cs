using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AthConsole {
  class Program {
    static void Main(string[] args) {
      BearLib.Terminal.Open();
      Console.ReadKey();
      BearLib.Terminal.Close();
    }
  }
}
