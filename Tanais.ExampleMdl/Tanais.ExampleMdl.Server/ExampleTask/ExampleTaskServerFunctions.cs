using System;
using System.Collections.Generic;
using System.Linq;
using Sungero.Core;
using Sungero.CoreEntities;
using Tanais.ExampleMdl.ExampleTask;

namespace Tanais.ExampleMdl.Server
{
  partial class ExampleTaskFunctions
  {

    /// <summary>
    /// Выдать права на просмотр документа контролеру.
    /// </summary>
    /// <param name="document">Документ.</param>
    [Public, Remote]
    public virtual void GrantAccessRightsToDocument(Sungero.Docflow.IOfficialDocument document)
    {
      if (document == null)
      {
        // Добавляем отладочное сообщение в лог со следующими параметрами:
        // наименование ф-ции, наименование операции, постфикс логгера и параметры;
        // наименование ф-ции - GrantAttachmentRights; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - CheckDocument: проверка документа;
        // постфикс логгера - константа с текстом "ExampleMdlTask"; одинаков в рамках текущей задачи;
        // словарь, содержащий ИД задачи и информационное сообщение:
        //    1) задача:
        //       название идентификатора - константа с текстом "ExampleTask"; одинаков в рамках текущей задачи;
        //       идентификатор сущности - ИД задачи, одинаков в рамках текущего примера;
        //    2) сообщение:
        //       ключ - константа с текстом "Message";
        //       значение - констанста с текстом "Documents not found".
        StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.GrantAttachmentRights,
                                                             Constants.Module.OperationsConstLog.CheckDocument,
                                                             Constants.Module.LoggerPostfixesConstLog.ExampleMdlTask,
                                                             new Dictionary<string, string>()
                                                             {
                                                               { Constants.Module.IdentifierTypesConstLog.ExampleTask, _obj.Id.ToString() },
                                                               { Constants.Module.MessagesConstLogs.Message, Constants.Module.MessagesConstLogs.DocumentsNotFound }
                                                             });
        return;
      }
      
      var supervisor = _obj.Supervisor;
      if (supervisor == null)
      {
        // Добавляем отладочное сообщение в лог со следующими параметрами:
        // наименование ф-ции, наименование операции, постфикс логгера и параметры;
        // наименование ф-ции - GrantAttachmentRights; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - GetSupervisor: получение контролера;
        // постфикс логгера - константа с текстом "ExampleMdlTask"; одинаков в рамках текущей задачи;
        // словарь, содержащий ИД задачи и информационное сообщение:
        //    1) задача:
        //       название идентификатора - константа с текстом "ExampleTask"; одинаков в рамках текущей задачи;
        //       идентификатор сущности - ИД задачи, одинаков в рамках текущего примера;
        //    2) сообщение:
        //       ключ - константа с текстом "Message";
        //       значение - константа с текстом "Supervisor not found".
        StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.GrantAttachmentRights,
                                                             Constants.Module.OperationsConstLog.GetSupervisor,
                                                             Constants.Module.LoggerPostfixesConstLog.ExampleMdlTask,
                                                             new Dictionary<string, string>()
                                                             {
                                                               { Constants.Module.IdentifierTypesConstLog.ExampleTask, _obj.Id.ToString() },
                                                               { Constants.Module.MessagesConstLogs.Message, Constants.Module.MessagesConstLogs.SupervisorNotFound }
                                                             });
        return;
      }
      
      try
      {
        if (!document.AccessRights.CanRead(supervisor))
          document.AccessRights.Grant(supervisor, DefaultAccessRightsTypes.Read);
        document.AccessRights.Save();
        
        // Добавляем отладочное сообщение в лог со следующими параметрами:
        // наименование ф-ции, наименование операции, постфикс логгера и параметры;
        // наименование ф-ции - GrantAttachmentRights; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - Grant: выдача прав;
        // постфикс логгера - константа с текстом "ExampleMdlTask"; одинаков в рамках текущей задачи;
        // словарь, содержащий ИД задачи, документа, сотрудника и информационное сообщение:
        //    1) задача:
        //       название идентификатора - константа с текстом "ExampleTask"; одинаков в рамках текущей задачи;
        //       идентификатор сущности - ИД задачи, одинаков в рамках текущего примера;
        //    2) документ:
        //       название идентификатора - константа с текстом "Document"; одинаков в рамках текущей задачи;
        //       идентификатор сущности - ИД документа, одинаков в рамках текущего примера;
        //    3) сотрудник:
        //       название идентификатора - константа с текстом "Employee"; одинаков в рамках текущей задачи;
        //       идентификатор сущности - ИД сотрудника, одинаков в рамках текущего примера;
        StructuredLogging.PublicFunctions.Module.DebugLogger(Constants.Module.FunctionsConstLog.GrantAttachmentRights,
                                                             Constants.Module.OperationsConstLog.Grant,
                                                             Constants.Module.LoggerPostfixesConstLog.ExampleMdlTask,
                                                             new Dictionary<string, string>()
                                                             {
                                                               { Constants.Module.IdentifierTypesConstLog.ExampleTask, _obj.Id.ToString() },
                                                               { Constants.Module.IdentifierTypesConstLog.Document, document.Id.ToString() },
                                                               { Constants.Module.IdentifierTypesConstLog.Supervisor, supervisor.Id.ToString() }
                                                             });
      }
      catch (Exception ex)
      {
        // Добавляем в лог сообщение об ошибке со следующими параметрами:
        // наименование ф-ции, наименование операции, сообщение об ошибке, трассировка стека, постфикс логгера и словарь параметров;
        // наименование ф-ции - GrantAttachmentRights; параметр "функция" одинаков в рамках текущей функции;
        // наименование операции - Grant: выдача прав;
        // сообщение об ошибке - ex.Message;
        // трассировка стека - ex.StackTrace;
        // постфикс логгера - константа с текстом "ExampleMdlTask"; одинаков в рамках текущей задачи;
        // словарь, содержащий сведения о задаче:
        //    1) задача:
        //       название идентификатора - константа с текстом "SimpleTask"; одинаков в рамках текущей задачи;
        //       идентификатор сущности - ИД задачи, одинаков в рамках текущего примера;
        StructuredLogging.PublicFunctions.Module.ErrorLogger(Constants.Module.FunctionsConstLog.GrantAttachmentRights,
                                                             Constants.Module.OperationsConstLog.SendNotify,
                                                             ex.Message,
                                                             ex.StackTrace,
                                                             Constants.Module.LoggerPostfixesConstLog.ExampleMdlTask,
                                                             new Dictionary<string, string>()
                                                             {
                                                               { Constants.Module.IdentifierTypesConstLog.ExampleTask, _obj.Id.ToString() }
                                                             });
      }
    }

  }
}