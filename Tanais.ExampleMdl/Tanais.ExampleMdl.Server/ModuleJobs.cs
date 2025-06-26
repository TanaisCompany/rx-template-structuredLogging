using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;

namespace Tanais.ExampleMdl.Server
{
  public class ModuleJobs
  {

    /// <summary>
    /// Рассылка уведомлений о завершении срока действия зарезервированных номеров для распорядительных документов.
    /// </summary>
    public virtual void JobSendNotifyAboutDeadlineOfReserveNumber()
    {
      // Добавляем отладочное сообщение в лог со следующими параметрами:
      // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
      // наименование ф-ции - JobSendNotifyAboutDeadlineOfReserveNumber; параметр "функция" одинаков в рамках текущей функции;
      // наименование операции - Start: операция начала выполнения фонового процесса;
      // название идентификатора - string.Empty;
      // идентификатор сущности - string.Empty;
      // постфикс логгера - константа с текстом "DocflowJob"; одинаков в рамках текущей задачи.
      StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.JobSendNotifyAboutDeadlineOfReserveNumber,
                                                           Constants.Module.OperationsConstLog.Start,
                                                           string.Empty,
                                                           string.Empty,
                                                           Constants.Module.LoggerPostfixesConstLog.DocflowJob);
      
      // Дней для завершения.
      var daysToComplete = Constants.Module.Settings.DaysToComplete;
      // Срок действия зарезервированного номера.
      var reservedNumberValidityPeriod = Constants.Module.Settings.ReservedNumberValidityPeriod;
      
      // Выборка документов с зарезервированными номерами.
      var documents = Sungero.RecordManagement.OrderBases
        .GetAll(g => g.RegistrationState == Sungero.RecordManagement.OrderBase.RegistrationState.Reserved)
        .OrderBy(o => o.Created.GetValueOrDefault());
      
      // Дата для выборки документов.
      var aboutDeadlineReservedNumberDate = Calendar.Now.AddWorkingDays(daysToComplete).AddWorkingDays(-reservedNumberValidityPeriod).Date;
      // Дней до завершения.
      var releasingReservedNumberDate = Calendar.Now.AddWorkingDays(-reservedNumberValidityPeriod).Date;
      
      // Отправка уведомлений о приближении срока действия зарезервированного номера.
      Functions.Module.SendNotifyAboutDeadlineReservedNumber(documents, aboutDeadlineReservedNumberDate, daysToComplete);
      // Освобождение резервного номера, отправка уведомлений об освобождении резервного номера.
      Functions.Module.ReleasingReservedNumber(documents, releasingReservedNumberDate);
      
      // Добавляем отладочное сообщение в лог со следующими параметрами:
      // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
      // наименование ф-ции - JobSendNotifyAboutDeadlineOfReserveNumber; параметр "функция" одинаков в рамках текущей функции;
      // наименование операции - Completion: операция завершения выполнения фонового процесса;
      // название идентификатора - string.Empty;
      // идентификатор сущности - string.Empty;
      // постфикс логгера - константа с текстом "DocflowJob"; одинаков в рамках текущей задачи.
      StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.JobSendNotifyAboutDeadlineOfReserveNumber,
                                                           Constants.Module.OperationsConstLog.Completion,
                                                           string.Empty,
                                                           string.Empty,
                                                           Constants.Module.LoggerPostfixesConstLog.DocflowJob);
    }

  }
}