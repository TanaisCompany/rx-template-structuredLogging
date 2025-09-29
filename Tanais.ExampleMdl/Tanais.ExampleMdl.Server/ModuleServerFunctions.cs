using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Newtonsoft.Json;

namespace Tanais.ExampleMdl.Server
{
  public partial class ModuleFunctions
  {

    #region ФП "Рассылка уведомлений о завершении срока действия зарезервированных номеров для распорядительных документов"
    
    /// <summary>
    /// Отправка уведомлений о приближении срока действия зарезервированного номера.
    /// </summary>
    /// <param name="documents">Список документов с типом "Базовый приказ".</param>
    /// <param name="aboutDeadlineReservedNumberDate">Дата для выборки документов.</param>
    /// <param name="daysToCompleteTanais">Дней для завершения.</param>
    [Public]
    public virtual void SendNotifyAboutDeadlineReservedNumber(IQueryable<Sungero.RecordManagement.IOrderBase> documents, DateTime aboutDeadlineReservedNumberDate, int daysToComplete)
    {
      // Выборка документов, у которых приближается срок действия зарезервированного номера.
      documents = documents
        .Where(w => w.Created.GetValueOrDefault().Date == aboutDeadlineReservedNumberDate)
        .OrderBy(o => o.Id);
      
      if (!documents.Any())
      {
        // Добавляем отладочное сообщение в лог со следующими параметрами:
        // наименование ф-ции, наименование операции, постфикс логгера и параметры;
        // наименование ф-ции - SendNotifyAboutDeadlineReservedNumber; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - GetDocuments: операция получения выборки документов;
        // постфикс логгера - константа с текстом "DocflowJob"; одинаков в рамках текущей задачи;
        // словарь, содержащий информационное сообщение:
        //   - ключ - константа с текстом "Message";
        //   - значение - констанста с текстом "Documents not found".
        StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.SendNotifyAboutDeadlineReservedNumber,
                                                             Constants.Module.OperationsConstLog.GetDocuments,
                                                             Constants.Module.LoggerPostfixesConstLog.DocflowJob,
                                                             new Dictionary<string, string>()
                                                             {
                                                               { Constants.Module.MessagesConstLogs.Message, Constants.Module.MessagesConstLogs.DocumentsNotFound }
                                                             });
        return;
      }
      
      foreach (var doc in documents.Take(Constants.Module.JobSendNotifyAboutDeadlineOfReserveNumber.MaxCountOfDocumentsToProcess))
      {
        var task = Sungero.Workflow.SimpleTasks.Null;
        try
        {
          task = Sungero.Workflow.SimpleTasks
            .CreateWithNotices(Resources.SubjectTaskAboutDeadlineReservedNumberFormat(daysToComplete, doc.Name), doc.Author);
          task.Attachments.Add(doc);
          task.Start();
          
          // Добавляем отладочное сообщение в лог со следующими параметрами:
          // наименование ф-ции, наименование операции, постфикс логгера и параметры;
          // наименование ф-ции - SendNotifyAboutDeadlineReservedNumber; параметр "функция" одинаков в рамках текущей функции;
          // наименование операции - SendNotify: отправка уведомления;
          // постфикс логгера - константа с текстом "DocflowJob"; одинаков в рамках текущей задачи;
          // словарь, содержащий сведения о задаче и документе:
          //    1) задача:
          //       название идентификатора - константа с текстом "SimpleTask"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - ИД задачи, одинаков в рамках текущего примера;
          //    2) документ:
          //       название идентификатора - константа с текстом "OrderBase"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - ИД документа, одинаков в рамках текущего примера.
          StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.SendNotifyAboutDeadlineReservedNumber,
                                                               Constants.Module.OperationsConstLog.SendNotify,
                                                               Constants.Module.LoggerPostfixesConstLog.DocflowJob,
                                                               new Dictionary<string, string>()
                                                               {
                                                                 { Constants.Module.IdentifierTypesConstLog.SimpleTask, task.Id.ToString() },
                                                                 { Constants.Module.IdentifierTypesConstLog.OrderBase, doc.Id.ToString() }
                                                               });
        }
        catch (Exception ex)
        {
          // Добавляем в лог сообщение об ошибке со следующими параметрами:
          // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, постфикс логгера и словарь параметров;
          // наименование ф-ции - SendNotifyAboutDeadlineReservedNumber; параметр "функция" одинаков в рамках текущей функции;
          // наименование операции - SendNotify: отправка уведомления;
          // сообщение об ошибке - ex.Message;
          // трассировка стека - ex.StackTrace;
          // постфикс логгера - константа с текстом "DocflowJob"; одинаков в рамках текущей задачи;
          // словарь, содержащий сведения о задаче и документе:
          //    1) задача:
          //       название идентификатора - константа с текстом "SimpleTask"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - ИД задачи, одинаков в рамках текущего примера;
          //    2) документ:
          //       название идентификатора - константа с текстом "OrderBase"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - ИД документа, одинаков в рамках текущего примера;
          StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.SendNotifyAboutDeadlineReservedNumber,
                                                               Constants.Module.OperationsConstLog.SendNotify,
                                                               ex.Message,
                                                               ex.StackTrace,
                                                               Constants.Module.LoggerPostfixesConstLog.DocflowJob,
                                                               new Dictionary<string, string>()
                                                               {
                                                                 { Constants.Module.IdentifierTypesConstLog.SimpleTask, task?.Id.ToString() },
                                                                 { Constants.Module.IdentifierTypesConstLog.OrderBase, doc.Id.ToString() }
                                                               });
        }
      }
    }
    
    /// <summary>
    /// Освобождение резервного номера, отправка уведомлений об освобождении резервного номера.
    /// </summary>
    /// <param name="documents">Список документов с типом "Базовый приказ".</param>
    /// <param name="releasingReservedNumberDate">Дата для выборки документов.</param>
    [Public]
    public virtual void ReleasingReservedNumber(IQueryable<Sungero.RecordManagement.IOrderBase> documents, DateTime releasingReservedNumberDate)
    {
      // Выборка документов, резервные номера которых нужно освободить.
      documents = documents
        .Where(w => w.Created.GetValueOrDefault().Date == releasingReservedNumberDate)
        .OrderBy(o => o.Id);
      
      if (!documents.Any())
      {
        // Добавляем отладочное сообщение в лог со следующими параметрами:
        // наименование ф-ции, наименование операции, постфикс логгера и параметры;
        // наименование ф-ции - ReleasingReservedNumber; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - GetDocuments: операция получения выборки документов;
        // постфикс логгера - константа с текстом "DocflowJob"; одинаков в рамках текущей задачи;
        // словарь, содержащий информационное сообщение:
        //   - ключ - константа с текстом "Message";
        //   - значение - констанста с текстом "Documents not found".
        StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.ReleasingReservedNumber,
                                                             Constants.Module.OperationsConstLog.GetDocuments,
                                                             Constants.Module.LoggerPostfixesConstLog.DocflowJob,
                                                             new Dictionary<string, string>()
                                                             {
                                                               { Constants.Module.MessagesConstLogs.Message, Constants.Module.MessagesConstLogs.DocumentsNotFound }
                                                             });
        return;
      }
      
      foreach (var doc in documents.Take(Constants.Module.JobSendNotifyAboutDeadlineOfReserveNumber.MaxCountOfDocumentsToProcess))
      {
        Sungero.Docflow.PublicFunctions.OfficialDocument.RegisterDocument(doc, null, null, null, false, true);
        var task = Sungero.Workflow.SimpleTasks.Null;
        try
        {
          task = Sungero.Workflow.SimpleTasks
            .CreateWithNotices(Resources.CleanReservedNumberFormat(doc.Name), doc.Author);
          task.Attachments.Add(doc);
          task.Start();
          
          // Добавляем отладочное сообщение в лог со следующими параметрами:
          // наименование ф-ции, наименование операции, постфикс логгера и словарь параметров;
          // наименование ф-ции - ReleasingReservedNumber; параметр "функция" одинаков в рамках текущей функции;
          // наименование операции - CleanReservedNumber: освобождение резервного номера;
          // постфикс логгера - константа с текстом "DocflowJob"; одинаков в рамках текущей задачи;
          // словарь, содержащий сведения о задаче и документе:
          //    1) задача:
          //       название идентификатора - константа с текстом "SimpleTask"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - ИД задачи, одинаков в рамках текущего примера;
          //    2) документ:
          //       название идентификатора - константа с текстом "OrderBase"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - ИД документа, одинаков в рамках текущего примера.
          StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.ReleasingReservedNumber,
                                                               Constants.Module.OperationsConstLog.CleanReservedNumber,
                                                               Constants.Module.LoggerPostfixesConstLog.DocflowJob,
                                                               new Dictionary<string, string>()
                                                               {
                                                                 { Constants.Module.IdentifierTypesConstLog.SimpleTask, task.Id.ToString() },
                                                                 { Constants.Module.IdentifierTypesConstLog.OrderBase, doc.Id.ToString() }
                                                               });
        }
        catch (Exception ex)
        {
          // Добавляем в лог сообщение об ошибке со следующими параметрами:
          // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, постфикс логгера и словарь параметров;
          // наименование ф-ции - ReleasingReservedNumber; параметр "функция" одинаков в рамках текущей функции;
          // наименование операции - CleanReservedNumber: освобождение резервного номера;
          // сообщение об ошибке - ex.Message;
          // трассировка стека - ex.StackTrace;
          // постфикс логгера - константа с текстом "DocflowJob"; одинаков в рамках текущей задачи;
          // словарь, содержащий сведения о задаче и документе:
          //    1) задача:
          //       название идентификатора - константа с текстом "SimpleTask"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - ИД задачи, одинаков в рамках текущего примера;
          //    2) документ:
          //       название идентификатора - константа с текстом "OrderBase"; одинаков в рамках текущей задачи;
          //       идентификатор сущности - ИД документа, одинаков в рамках текущего примера.
          StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.ReleasingReservedNumber,
                                                               Constants.Module.OperationsConstLog.CleanReservedNumber,
                                                               ex.Message,
                                                               ex.StackTrace,
                                                               Constants.Module.LoggerPostfixesConstLog.DocflowJob,
                                                               new Dictionary<string, string>()
                                                               {
                                                                 { Constants.Module.IdentifierTypesConstLog.SimpleTask, task?.Id.ToString() },
                                                                 { Constants.Module.IdentifierTypesConstLog.OrderBase, doc.Id.ToString() }
                                                               });
        }
      }
    }
    
    #endregion
    
    #region АО "Синхронизация должности из внешней системы"

    /// <summary>
    /// Заполнить свойства должности.
    /// </summary>
    /// <param name="jobTitle">Должность.</param>
    /// <param name="jobTitleStructure">Структура должности.</param>
    /// <returns>Сообщение.</returns>
    public virtual string FillJobTitleFromIntegration(Sungero.Company.IJobTitle jobTitle, Structures.Module.IJobTitle jobTitleStructure)
    {
      var result = string.Empty;
      
      try
      {
        if (jobTitle.Name != jobTitleStructure.Name)
          jobTitle.Name = jobTitleStructure.Name;
        
        var department = Sungero.Company.Departments.Null;
        if (jobTitleStructure.DepartmentId.HasValue)
        {
          department = Sungero.Company.Departments.GetAll(d => d.Id == jobTitleStructure.DepartmentId.Value).FirstOrDefault();
          if (department == null)
            result = Resources.DepartmentNotFoundFormat(jobTitleStructure.DepartmentId.Value);
        }
        if (jobTitle.Department != department)
          jobTitle.Department = department;
        
        if (jobTitle.ExternalId != jobTitleStructure.GUID)
          jobTitle.ExternalId = jobTitleStructure.GUID;
        
        var status = new Enumeration(jobTitleStructure.Status);
        if (jobTitle.Status != status)
          jobTitle.Status = status;
        
        if (jobTitle.State.IsChanged)
          jobTitle.Save();
      }
      catch (Sungero.Domain.Shared.Validation.ValidationException ex)
      {
        throw ex;
      }
      catch (Exception ex)
      {
        throw ex;
      }
      
      return result;
    }

    /// <summary>
    /// Получить должность и если ее нет, создать.
    /// </summary>
    /// <param name="jobTitleExternalId">Ид во внешней системе.</param>
    /// <returns>Должность.</returns>
    [Public, Remote(IsPure = true)]
    public static Sungero.Company.IJobTitle GetJobTitle(string jobTitleExternalId)
    {
      if (string.IsNullOrEmpty(jobTitleExternalId))
      {
        throw Sungero.Core.AppliedCodeException.Create(Tanais.ExampleMdl.Resources.JobTitle_GuidIsEmptyErrorLog,
                                                       Tanais.ExampleMdl.Resources.GuidIsEmpty);
      }
      
      var jobTitle = Sungero.Company.JobTitles.GetAll(j => j.ExternalId == jobTitleExternalId).FirstOrDefault();

      if (jobTitle == null)
        jobTitle = Sungero.Company.JobTitles.Create();
      
      return jobTitle;
    }

    /// <summary>
    /// Сериализовать объект.
    /// </summary>
    /// <param name="string">Объект.</param>
    /// <returns>Строка в формате json.</returns>
    [Public]
    public virtual string GetJsonByObject(object objectValue)
    {
      return JsonConvert.SerializeObject(objectValue, Formatting.Indented, new JsonSerializerSettings
                                         {
                                           TypeNameHandling = TypeNameHandling.All
                                         });
    }
    
    /// <summary>
    /// Запустить асинхронный обработчик синхронизации должности из внешней системы.
    /// </summary>
    /// <param name="JobTitleModel">Модель данных, описывающая должность.</param>
    /// <param name="JobTitleName">Наименование должности.</param>
    [Remote]
    public void CreateOrUpdateJobTitle(string JobTitleModel, string JobTitleName)
    {
      var createOrUpdateJobTiltle = Tanais.ExampleMdl.AsyncHandlers.CreateOrUpdateJobTitle.Create();
      createOrUpdateJobTiltle.JobTitleModel = JobTitleModel;
      createOrUpdateJobTiltle.JobTitleName = JobTitleName;
      createOrUpdateJobTiltle.ExecuteAsync();
    }
    
    /// <summary>
    /// Синхронизирует должность из внешней системы.
    /// </summary>
    /// <param name="GUID">GUID сущности.</param>
    /// <param name="Name">Наименование.</param>
    /// <param name="DepartmentId">ИД подразделения.</param>
    /// <param name="Status">Состояние.</param>
    /// <returns>Результат обработки.</returns>
    [Public(WebApiRequestType = RequestType.Post)]
    public string SyncJobTitle(string GUID,
                               string Name,
                               long? DepartmentId,
                               string Status)
    {
      // Добавляем отладочное сообщение в лог со следующими параметрами:
      // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
      // наименование ф-ции - SyncJobTitle; параметр "функция" одинаков в рамках текущей функции;
      // наименование операции - Start: операция старта синхронизации должности из внешней системы;
      // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
      // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
      // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
      StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.SyncJobTitle,
                                                           Constants.Module.OperationsConstLog.Start,
                                                           Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                           Name,
                                                           Constants.Module.LoggerPostfixesConstLog.Integration);
      
      var jobTitle = Structures.Module.JobTitle.Create(GUID,
                                                       Name,
                                                       DepartmentId,
                                                       Status);
      
      CreateOrUpdateJobTitle(GetJsonByObject(jobTitle), Name);
      
      // Добавляем отладочное сообщение в лог со следующими параметрами:
      // наименование ф-ции, наименование операции, название идентификатора, идентификатор сущности и постфикс логгера;
      // наименование ф-ции - SyncJobTitle; параметр "функция" одинаков в рамках текущей функции;
      // наименование операции - Completion: операция завершения синхронизации должности из внешней системы;
      // название идентификатора - константа с текстом "JobTitle"; одинаков в рамках текущей задачи;
      // идентификатор сущности - наименование сущности в системе; одинаков в рамках текущего примера;
      // постфикс логгера - константа с текстом "Integration"; одинаков в рамках текущей задачи.
      StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.SyncJobTitle,
                                                           Constants.Module.OperationsConstLog.Completion,
                                                           Constants.Module.IdentifierTypesConstLog.JobTitle,
                                                           Name,
                                                           Constants.Module.LoggerPostfixesConstLog.Integration);
      
      return Constants.Module.HTTPRequest.SyncResult.Success;
    }
    
    #endregion
    
  }
}