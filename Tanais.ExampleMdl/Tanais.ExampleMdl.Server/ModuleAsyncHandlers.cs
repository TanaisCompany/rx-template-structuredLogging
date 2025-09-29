using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Newtonsoft.Json;

namespace Tanais.ExampleMdl.Server
{
  public partial class ModuleAsyncHandlers
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
      // наименование операции - Start: операция старта АО "Синхронизация должности из внешней системы";
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
        
        // Получить должность по GUID, если такой не найдено - создать новую.
        jobTitle = Functions.Module.GetJobTitle(jobTitleModel.GUID);
        
        // Проверка блокировки.
        var lockInfo = Locks.GetLockInfo(jobTitle);
        if (lockInfo.IsLocked)
        {
          // Добавляем отладочное сообщение в лог со следующими параметрами:
          // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
          // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
          // наименование операции - GetLockInfo: получение информациии о блокировке сущности;
          // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи;
          // словарь, содержащий информацию об идентификаторе сущности и информационное сообщение:
          //    1) идентификатор:
          //       название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
          //    2) сообщение:
          //       ключ - константа с текстом "Message";
          //       значение - сообщение о блокировке.
          StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.CreateOrUpdateJobTitle,
                                                               Constants.Module.OperationsConstLog.GetLockInfo,
                                                               Constants.Module.LoggerPostfixesConstLog.Integration,
                                                               new Dictionary<string, string>()
                                                               {
                                                                 { Constants.Module.IdentifierTypesConstLog.JobTitle, args.JobTitleName },
                                                                 { Constants.Module.MessagesConstLogs.Message, lockInfo.LockedMessage }
                                                               });

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
      // Обработка исключений, которые происходят во время валидации данных (переповтор не требуется).
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
      // Обработка исключений из прикладного кода.
      // Пример: если не удалось получить сущность по ExternalId (переповтор не требуется).
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
      // Обработка непредвиденных ошибок, возникающих во время выполнения приложения (требуется переповтор).
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
        if (jobTitle != null)
          Locks.Unlock(jobTitle);
      }
      
      // Добавляем отладочное сообщение в лог со следующими параметрами:
      // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
      // наименование ф-ции - CreateOrUpdateJobTitle; параметр "функция" одинаков в рамках текущей функции;
      // наименование операции - Completion: операция завершения АО "Синхронизация должности из внешней системы";
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