using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace Tanais.StructuredLogging.Server
{
  public partial class ModuleFunctions
  {
    
    /// <summary>
    /// Добавить в лог информацию о появлении ошибки при выполнении операции.
    /// </summary>
    /// <param name="function">Наименование функции.</param>
    /// <param name="operation">Наименование операции.</param>
    /// <param name="message">Текст ошибки.</param>
    /// <param name="stackTrace">Трассировка.</param>
    /// <param name="identifierType">Название идентификатора.</param>
    /// <param name="identifier">Идентификтор сущности.</param>
    /// <param name="loggerPostfix">Постфикс логгера.</param>
    [Public]
    public void ErrorLogger(string function,
                            string operation,
                            string message,
                            string stackTrace,
                            string identifierType,
                            string identifier,
                            string loggerPostfix)
    {
      if (string.IsNullOrEmpty(function))
        function = Constants.Module.Logging.DefaultValuesConstLog.Value;
      if (string.IsNullOrEmpty(message))
        message = Constants.Module.Logging.DefaultValuesConstLog.MessageText;
      if (string.IsNullOrEmpty(stackTrace))
        stackTrace = Constants.Module.Logging.DefaultValuesConstLog.StackTrace;
      if (string.IsNullOrEmpty(identifierType))
        identifierType = Constants.Module.Logging.DefaultValuesConstLog.Type;
      if (string.IsNullOrEmpty(identifier))
        identifier = Constants.Module.Logging.DefaultValuesConstLog.Value;
      
      var parameters = new System.Collections.Generic.Dictionary<string, string>();
      parameters.Add(identifierType, identifier);
      
      ErrorLogger(function, operation, message, stackTrace, loggerPostfix, parameters);
    }
    
    /// <summary>
    /// Добавить в лог информацию о появлении ошибки при выполнении операции.
    /// </summary>
    /// <param name="function">Наименование функции.</param>
    /// <param name="operation">Наименование операции.</param>
    /// <param name="message">Текст ошибки.</param>
    /// <param name="stackTrace">Трассировка.</param>
    /// <param name="loggerPostfix">Постфикс логгера.</param>
    /// <param name="parameters">Список дополнительных параметров.</param>
    [Public]
    public void ErrorLogger(string function,
                            string operation,
                            string message,
                            string stackTrace,
                            string loggerPostfix,
                            System.Collections.Generic.Dictionary<string, string> parameters)
    {
      if (string.IsNullOrEmpty(function))
        function = Constants.Module.Logging.DefaultValuesConstLog.Value;
      if (string.IsNullOrEmpty(message))
        message = Constants.Module.Logging.DefaultValuesConstLog.MessageText;
      if (string.IsNullOrEmpty(stackTrace))
        stackTrace = Constants.Module.Logging.DefaultValuesConstLog.StackTrace;
      if (parameters.Count == 0)
        parameters.Add(Constants.Module.Logging.DefaultValuesConstLog.Type, Constants.Module.Logging.DefaultValuesConstLog.Value);
      
      var log = Structures.Module.ErrorLog.Create(function,
                                                  operation,
                                                  message,
                                                  stackTrace,
                                                  parameters);
      
      Logger.WithLogger(loggerPostfix).WithObject(log).Error(string.Empty);
    }
    
    /// <summary>
    /// Добавить отладочное сообщение в лог при выполнении операции.
    /// </summary>
    /// <param name="function">Наименование функции.</param>
    /// <param name="operation">Наименование операции.</param>
    /// <param name="identifierType">Название идентификатора.</param>
    /// <param name="identifier">Идентификтор сущности.</param>
    /// <param name="loggerPostfix">Постфикс логгера.</param>
    [Public]
    public void DebugLogger(string function,
                            string operation,
                            string identifierType,
                            string identifier,
                            string loggerPostfix)
    {
      if (string.IsNullOrEmpty(function))
        function = Constants.Module.Logging.DefaultValuesConstLog.Value;
      if (string.IsNullOrEmpty(identifierType))
        identifierType = Constants.Module.Logging.DefaultValuesConstLog.Type;
      if (string.IsNullOrEmpty(identifier))
        identifier = Constants.Module.Logging.DefaultValuesConstLog.Value;
      
      var parameters = new System.Collections.Generic.Dictionary<string, string>();
      parameters.Add(identifierType, identifier);
      
      DebugLogger(function, operation, loggerPostfix, parameters);
    }
    
    /// <summary>
    /// Добавить отладочное сообщение в лог при выполнении операции.
    /// </summary>
    /// <param name="function">Наименование функции.</param>
    /// <param name="operation">Наименование операции.</param>
    /// <param name="loggerPostfix">Постфикс логгера.</param>
    /// <param name="parameters">Список дополнительных параметров.</param>
    [Public]
    public void DebugLogger(string function,
                            string operation,
                            string loggerPostfix,
                            System.Collections.Generic.Dictionary<string, string> parameters)
    {
      if (string.IsNullOrEmpty(function))
        function = Constants.Module.Logging.DefaultValuesConstLog.Value;
      if (parameters.Count == 0)
        parameters.Add(Constants.Module.Logging.DefaultValuesConstLog.Type, Constants.Module.Logging.DefaultValuesConstLog.Value);
      
      var log = Structures.Module.DebugLog.Create(function,
                                                  operation,
                                                  parameters);
      
      Logger.WithLogger(loggerPostfix).WithObject(log).Debug(string.Empty);
    }
    
  }
}