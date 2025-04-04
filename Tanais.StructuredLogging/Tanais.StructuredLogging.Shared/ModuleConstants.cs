using System;
using Sungero.Core;

namespace Tanais.StructuredLogging.Constants
{
  public static class Module
  {
    
    /// <summary>
    /// Логирование.
    /// </summary>
    public static class Logging
    {
      /// <summary>
      /// Значения для записи при отсутствии данных.
      /// </summary>
      public static class DefaultValuesConstLog
      {
        [Public]
        public const string Type = "Id";
        [Public]
        public const string Value = "Not defined";
        [Public]
        public const string MessageText = "Empty";
        [Public]
        public const string StackTrace = "Empty";
      }
      
    }
  }
}