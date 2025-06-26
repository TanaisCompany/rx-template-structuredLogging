using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Newtonsoft.Json;

namespace Tanais.ExampleMdl.Server
{
  public class ModuleAsyncHandlers
  {

    public virtual void CreateOrUpdateJobTitle(Tanais.ExampleMdl.Server.AsyncHandlerInvokeArgs.CreateOrUpdateJobTitleInvokeArgs args)
    {
      // Пример вызова перегрузки метода DebugLogger:
      // var parameters = new Dictionary<string, string>() { { Constants.Module.IdentifierTypesConstLog.JobTitle, args.JobTitleName} };
      // StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
      //                                                      Constants.Module.OperationsConstLog.Start,
      //                                                      Constants.Module.LoggerPostfixesConstLog.Integration,
      //                                                      parameters);
      
      // Добавляем отладочное сообщение в лог со следующими параметрами:
      // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
      // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
      // наименование операции - Start: операция старта синхронизации сущности;
      // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
      // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
      // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
      StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                           Constants.Module.OperationsConstLog.Start,
                                                           Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                           args.JobTitleName,
                                                           Constants.Module.LoggerPostfixesConstLog.Integration);

      if (args.RetryIteration >= Constants.Module.Value.MaxRetries)
      {
        // Добавляем отладочное сообщение в лог со следующими параметрами:
        // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
        // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - MaxRetriesExceeded: операция обработки превышения количества повторов;
        // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
        // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
        // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
        StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                             Constants.Module.OperationsConstLog.MaxRetriesExceeded,
                                                             Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                             args.JobTitleName,
                                                             Constants.Module.LoggerPostfixesConstLog.Integration);
        
        args.Retry = false;
        return;
      }
      
      var jobTitleModel = Structures.Module.JobTitle.Create();
      var jobTitle = Sungero.Company.JobTitles.Null;
      
      try
      {
        // Десериализовать структуру.
        jobTitleModel = JsonConvert.DeserializeObject<Structures.Module.JobTitle>(args.JobTitleModel);
        
        // Получить должность.
        jobTitle = Functions.Module.GetJobTitle(jobTitleModel.GUID);
        if (jobTitle == null)
        {
          // Пример вызова перегрузки метода ErrorLogger:
          // var parameters = new Dictionary<string, string>() { {Constants.Module.IdentifierTypesConstLog.JobTitle, args.JobTitleName} };
          // StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
          //                                                      Constants.Module.OperationsConstLog.GetJobTitle,
          //                                                      Constants.Module.MessagesConstLogs.EntityNotFound,
          //                                                      string.Empty,
          //                                                      Constants.Module.LoggerPostfixesConstLog.Integration,
          //                                                      parameters);
          
          // Добавляем в лог сообщение об ошибке со следующими параметрами:
          // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, название индентификатора, идентификатор сущности и постфикс логгера;
          // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
          // наименование операции - GetJobTitle: операция получения должности;
          // сообщение об ошибке - константа с текстом "Entity not found";
          // трассировка стека - string.Empty;
          // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
          // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
          // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
          StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                               Constants.Module.OperationsConstLog.GetJobTitle,
                                                               Constants.Module.MessagesConstLogs.EntityNotFound,
                                                               string.Empty,
                                                               Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                               args.JobTitleName,
                                                               Constants.Module.LoggerPostfixesConstLog.Integration);

          args.Retry = true;
          return;
        }
        
        // Проверка блокировки.
        if (Locks.GetLockInfo(jobTitle).IsLocked)
        {
          // Добавляем в лог сообщение об ошибке со следующими параметрами:
          // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, название индентификатора, идентификатор сущности и постфикс логгера;
          // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
          // наименование операции - GetLockInfo: операция получения информации о блокировке сущности;
          // сообщение об ошибке - сообщение о блокировке: Locks.GetLockInfo(jobTitle).LockedMessage;
          // трассировка стека - string.Empty;
          // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
          // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
          // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
          StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                               Constants.Module.OperationsConstLog.GetLockInfo,
                                                               Locks.GetLockInfo(jobTitle).LockedMessage,
                                                               string.Empty,
                                                               Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                               args.JobTitleName,
                                                               Constants.Module.LoggerPostfixesConstLog.Integration);

          args.Retry = true;
          return;
        }
        
        Locks.Lock(jobTitle);
        
        // Заполнить свойства должности.
        var fillErrors = Tanais.ExampleMdl.Functions.Module.FillJobTitleFromIntegration(jobTitle, jobTitleModel);
        if (!string.IsNullOrEmpty(fillErrors))
        {
          // Добавляем в лог сообщение об ошибке со следующими параметрами:
          // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, название индентификатора, идентификатор сущности и постфикс логгера;
          // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
          // наименование операции - UpdateEntity: операция обновления сущности;
          // сообщение об ошибке - переменная fillErrors;
          // трассировка стека - string.Empty;
          // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
          // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
          // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
          StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                               Constants.Module.OperationsConstLog.UpdateEntity,
                                                               fillErrors,
                                                               string.Empty,
                                                               Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                               args.JobTitleName,
                                                               Constants.Module.LoggerPostfixesConstLog.Integration);
        }
        
      }
      catch (Sungero.Domain.Shared.Validation.ValidationException ex)
      {
        // Добавляем в лог сообщение об ошибке со следующими параметрами:
        // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, название индентификатора, идентификатор сущности и постфикс логгера;
        // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - SaveEntity: операция сохранения сущности;
        // сообщение об ошибке - ex.Message;
        // трассировка стека - ex.StackTrace;
        // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
        // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
        // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
        StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                             Constants.Module.OperationsConstLog.SaveEntity,
                                                             ex.Message,
                                                             ex.StackTrace,
                                                             Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                             args.JobTitleName,
                                                             Constants.Module.LoggerPostfixesConstLog.Integration);
        
        args.Retry = false;
      }
      catch (Sungero.Core.AppliedCodeException ex)
      {
        // Добавляем в лог сообщение об ошибке со следующими параметрами:
        // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, название индентификатора, идентификатор сущности и постфикс логгера;
        // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - SyncEntity: операция синхронизации сущности;
        // сообщение об ошибке - ex.Message;
        // трассировка стека - ex.StackTrace;
        // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
        // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
        // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
        StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                             Constants.Module.OperationsConstLog.SyncEntity,
                                                             ex.Message,
                                                             ex.StackTrace,
                                                             Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                             args.JobTitleName,
                                                             Constants.Module.LoggerPostfixesConstLog.Integration);
        
        args.Retry = false;
      }
      catch (Exception ex)
      {
        // Добавляем в лог сообщение об ошибке со следующими параметрами:
        // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, название индентификатора, идентификатор сущности и постфикс логгера;
        // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - SyncEntity: операция синхронизации сущности;
        // сообщение об ошибке - ex.Message;
        // трассировка стека - ex.StackTrace;
        // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
        // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
        // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
        StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                             Constants.Module.OperationsConstLog.SyncEntity,
                                                             ex.Message,
                                                             ex.StackTrace,
                                                             Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                             args.JobTitleName,
                                                             Constants.Module.LoggerPostfixesConstLog.Integration);

        args.Retry = true;
        throw ex;
      }
      finally
      {
        if (jobTitle != null && Locks.GetLockInfo(jobTitle).IsLocked)
          Locks.Unlock(jobTitle);
      }
      
      // Добавляем отладочное сообщение в лог со следующими параметрами:
      // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
      // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
      // наименование операции - Completion: операция завершения синхронизации сущности;
      // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
      // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
      // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
      StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                           Constants.Module.OperationsConstLog.Completion,
                                                           Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                           args.JobTitleName,
                                                           Constants.Module.LoggerPostfixesConstLog.Integration);
    }

  }
}