using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace Tanais.StructuredLogging.Server
{
  public class ModuleFunctions
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
    public void SyncEntityErrorLogger(string function,
                                      string operation,
                                      string message,
                                      string stackTrace,
                                      string identifierType,
                                      string identifier,
                                      string loggerPostfix)
    {
      if (string.IsNullOrEmpty(function))
        return;
      if (string.IsNullOrEmpty(message))
        message = Constants.Module.Logging.DefaultValuesConstLog.MessageText;
      if (string.IsNullOrEmpty(stackTrace))
        stackTrace = Constants.Module.Logging.DefaultValuesConstLog.StackTrace;
      if (string.IsNullOrEmpty(identifierType))
        identifierType = Constants.Module.Logging.DefaultValuesConstLog.Type;
      if (string.IsNullOrEmpty(identifier))
        identifier = Constants.Module.Logging.DefaultValuesConstLog.Value;
      
      var identityDictionary = new Dictionary<string, string>();
      identityDictionary.Add(identifierType, identifier);
      
      var log = Structures.Module.SyncEntityErrorLog.Create(function,
                                                            operation,
                                                            identityDictionary,
                                                            message,
                                                            stackTrace);
      
      Logger.WithLogger(loggerPostfix).WithObject(log).Error(string.Empty);
    }
    
    /// <summary>
    /// Добавить в лог информацию о появлении ошибки при выполнении операции.
    /// </summary>
    /// <param name="function">Наименование функции.</param>
    /// <param name="messageGuid">GUID обмена.</param>
    /// <param name="source">Источник вызова.</param>
    /// <param name="resultDescription">Информация для отправки результата.</param>
    /// <param name="resultDrxId">ИД сущности для отправки результата.</param>
    /// <param name="result">Результат обработки сущности.</param>
    /// <param name="message">Текст ошибки.</param>
    /// <param name="stackTrace">Трассировка.</param>
    /// <param name="identifierType">Название идентификатора.</param>
    /// <param name="identifier">Идентификтор сущности.</param>
    /// <param name="loggerPostfix">Постфикс логгера.</param>
    [Public]
    public void SyncEntityErrorLogger(string function,
                                      string messageGuid,
                                      string source,
                                      string resultDescription,
                                      string resultDrxId,
                                      string result,
                                      string message,
                                      string stackTrace,
                                      string identifierType,
                                      string identifier,
                                      string loggerPostfix)
    {
      if (string.IsNullOrEmpty(function))
        return;
      if (string.IsNullOrEmpty(messageGuid))
        messageGuid = Constants.Module.Logging.DefaultValuesConstLog.Value;
      if (string.IsNullOrEmpty(message))
        message = Constants.Module.Logging.DefaultValuesConstLog.MessageText;
      if (string.IsNullOrEmpty(stackTrace))
        stackTrace = Constants.Module.Logging.DefaultValuesConstLog.StackTrace;
      if (string.IsNullOrEmpty(identifierType))
        identifierType = Constants.Module.Logging.DefaultValuesConstLog.Type;
      if (string.IsNullOrEmpty(identifier))
        identifier = Constants.Module.Logging.DefaultValuesConstLog.Value;
      
      var identityDictionary = new Dictionary<string, string>();
      identityDictionary.Add(identifierType, identifier);
      
      var syncResult = Structures.Module.SyncResult.Create(messageGuid,
                                                           resultDrxId,
                                                           result,
                                                           resultDescription);
      
      var log = Structures.Module.SendResultErrorLog.Create(function,
                                                            messageGuid,
                                                            source,
                                                            syncResult,
                                                            identityDictionary,
                                                            message,
                                                            stackTrace);
      
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
    public void SyncEntityDebugLogger(string function,
                                      string operation,
                                      string identifierType,
                                      string identifier,
                                      string loggerPostfix)
    {
      if (string.IsNullOrEmpty(function))
        return;
      if (string.IsNullOrEmpty(identifierType))
        identifierType = Constants.Module.Logging.DefaultValuesConstLog.Type;
      if (string.IsNullOrEmpty(identifier))
        identifier = Constants.Module.Logging.DefaultValuesConstLog.Value;
      
      var identityDictionary = new Dictionary<string, string>();
      identityDictionary.Add(identifierType, identifier);
      
      var log = Structures.Module.SyncEntityDebugLog.Create(function,
                                                            operation,
                                                            identityDictionary);
      
      Logger.WithLogger(loggerPostfix).WithObject(log).Debug(string.Empty);
    }
    
    /// <summary>
    /// Добавить отладочное сообщение в лог при выполнении операции.
    /// </summary>
    /// <param name="function">Наименование функции.</param>
    /// <param name="messageGuid">GUID обмена.</param>
    /// <param name="source">Источник вызова.</param>
    /// <param name="resultDescription">Информация для отправки результата.</param>
    /// <param name="resultDrxId">ИД сущности для отправки результата.</param>
    /// <param name="result">Результат операции.</param>
    /// <param name="identifierType">Название идентификатора.</param>
    /// <param name="identifier">Идентификтор сущности.</param>
    /// <param name="loggerPostfix">Постфикс логгера.</param>
    [Public]
    public void SyncEntityDebugLogger(string function,
                                      string messageGuid,
                                      string source,
                                      string resultDescription,
                                      string resultDrxId,
                                      string result,
                                      string identifierType,
                                      string identifier,
                                      string loggerPostfix)
    {
      if (string.IsNullOrEmpty(function))
        return;
      if (string.IsNullOrEmpty(messageGuid))
        messageGuid = Constants.Module.Logging.DefaultValuesConstLog.Value;
      if (string.IsNullOrEmpty(identifierType))
        identifierType = Constants.Module.Logging.DefaultValuesConstLog.Type;
      if (string.IsNullOrEmpty(identifier))
        identifier = Constants.Module.Logging.DefaultValuesConstLog.Value;
      
      var identityDictionary = new Dictionary<string, string>();
      identityDictionary.Add(identifierType, identifier);
      
      var syncResult = Structures.Module.SyncResult.Create(messageGuid,
                                                           resultDrxId,
                                                           result,
                                                           resultDescription);
      
      var log = Structures.Module.SendResultDebugLog.Create(function,
                                                            messageGuid,
                                                            source,
                                                            syncResult,
                                                            identityDictionary);
      
      Logger.WithLogger(loggerPostfix).WithObject(log).Debug(string.Empty);
    }
  }
}