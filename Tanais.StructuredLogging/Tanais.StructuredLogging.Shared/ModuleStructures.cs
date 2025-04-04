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
  partial class SyncEntityDebugLog
  {
    public string Function { get; set; }
    public string Operation { get; set; }
    public Dictionary<string, string> Identifier { get; set; }
  }
  
  /// <summary>
  /// Структура лога уровня Error.
  /// </summary>
  partial class SyncEntityErrorLog
  {
    public string Function { get; set; }
    public string Operation { get; set; }
    public Dictionary<string, string> Identifier { get; set; }
    public string MessageText { get; set; }
    public string StackTrace { get; set; }
  }
  
  /// <summary>
  /// Структура лога уровня Debug для интеграции.
  /// </summary>
  partial class SendResultDebugLog
  {
    public string Function { get; set; }
    public string MessageGuid { get; set; }
    public string Source { get; set; }
    public Tanais.StructuredLogging.Structures.Module.ISyncResult SyncResult { get; set; }
    public Dictionary<string, string> Identifier { get; set; }
  }
  
  /// <summary>
  /// Структура лога уровня Error для интеграции.
  /// </summary>
  partial class SendResultErrorLog
  {
    public string Function { get; set; }
    public string MessageGuid { get; set; }
    public string Source { get; set; }
    public Tanais.StructuredLogging.Structures.Module.ISyncResult SyncResult { get; set; }
    public Dictionary<string, string> Identifier { get; set; }
    public string MessageText { get; set; }
    public string StackTrace { get; set; }
  }
  
  /// <summary>
  /// Результат синхронизации сущности.
  /// </summary>
  [Public]
  partial class SyncResult
  {
    public string MessageGuid { get; set; }
    public string DrxId { get; set; }
    public string Result { get; set; }
    public string Description { get; set; }
  }
}