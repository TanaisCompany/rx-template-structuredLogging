using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace Tanais.StructuredLogging.Structures.Module
{
  
  /// <summary>
  /// Структура лога уровня Debug.
  /// </summary>
  partial class DebugLog
  {
    public string Function { get; set; }
    public string Operation { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
  }
  
  /// <summary>
  /// Структура лога уровня Error.
  /// </summary>
  partial class ErrorLog
  {
    public string Function { get; set; }
    public string Operation { get; set; }
    public string MessageText { get; set; }
    public string StackTrace { get; set; }
    public Dictionary<string, string> Parameters { get; set; }
  }
  
}