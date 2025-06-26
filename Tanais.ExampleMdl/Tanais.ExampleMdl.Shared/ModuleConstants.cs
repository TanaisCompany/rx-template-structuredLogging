using System;
using Sungero.Core;

namespace Tanais.ExampleMdl.Constants
{
  public static class Module
  {

    /// <summary>
    /// ФП "Делопроизводство. Рассылка уведомлений о завершении срока действия зарезервированных номеров для распорядительных документов."
    /// </summary>
    [Public]
    public static class JobSendNotifyAboutDeadlineOfReserveNumber
    {
      // Максимальное количество документов, обработанных за один раз.
      public const int MaxCountOfDocumentsToProcess = 50;
    }
    
    /// <summary>
    /// Настройки документооборота.
    /// </summary>
    /// <remarks>Свойства, которые должны помещаться в перекрытие справочника RecordManagementSetting.
    /// Для упрощения примера помещены в константы.</remarks>
    [Public]
    public static class Settings
    {
      // Дней для завершения.
      public const int DaysToComplete = 5;
      
      // Срок действия зарезервированного номера.
      public const int ReservedNumberValidityPeriod = 10;
    }
    
    /// <summary>
    /// HTTP запрос.
    /// </summary>
    [Public]
    public static class HTTPRequest
    {
      /// <summary>
      /// Результат.
      /// </summary>
      public static class SyncResult
      {
        // Успешно.
        public const string Success = "Success";
      }
    }
    
    /// <summary>
    /// Константы функций.
    /// </summary>
    [Public]
    public static class FunctionsConstLog
    {
      // Выдача прав на вложенный документ.
      public const string GrantAttachmentRights = "GrantAttachmentRights";
      
      // Наименование функции ReleasingReservedNumber.
      public const string ReleasingReservedNumber = "ReleasingReservedNumber";
      
      // Наименование функции SendNotifyAboutDeadlineReservedNumber.
      public const string SendNotifyAboutDeadlineReservedNumber = "SendNotifyAboutDeadlineReservedNumber";
      
      // Наименование функции JobSendNotifyAboutDeadlineOfReserveNumber.
      public const string JobSendNotifyAboutDeadlineOfReserveNumber = "JobSendNotifyAboutDeadlineOfReserveNumber";
      
      // Наименование функции CreateOrUpdateJobTitle.
      public const string CreateOrUpdateJobTitle = "CreateOrUpdateJobTitle";
      
      // Наименование функции SyncJobTitle.
      public const string SyncJobTitle = "SyncJobTitle";
    }
    
    /// <summary>
    /// Операции.
    /// </summary>
    [Public]
    public static class OperationsConstLog
    {
      // Получение контролера.
      public const string GetSupervisor = "GetSupervisor";
      
      // Проверка документа.
      public const string CheckDocument = "CheckDocument";
      
      // Выдать права.
      public const string Grant = "Grant";
      
      // Освобождение резервного номера.
      public const string CleanReservedNumber = "CleanReservedNumber";
      
      // Отправка уведомления.
      public const string SendNotify = "SendNotify";
      
      // Создание.
      public const string Create = "Create";
      
      // Старт.
      public const string Start = "Start";
      
      // Обработка превышения количества повторов.
      public const string MaxRetriesExceeded = "MaxRetriesExceeded";
      
      // Завершено.
      public const string Completion = "Completion";
      
      // Получение списка документов.
      public const string GetDocuments = "GetDocuments";
      
      // Получение должности.
      public const string GetJobTitle = "GetJobTitle";
      
      // Получение информации о блокировке сущности.
      public const string GetLockInfo = "GetLockInfo";
      
      // Сохранение сущности.
      public const string SaveEntity = "SaveEntity";
      
      // Обновление сущности.
      public const string UpdateEntity = "UpdateEntity";
      
      // Синхронизация сущности.
      public const string SyncEntity = "SyncEntity";
    }
    
    /// <summary>
    /// Название идентификатора.
    /// </summary>
    [Public]
    public static class IdentifierTypesConstLog
    {
      // Документ.
      public const string Document = "Document";
      
      // Контролер.
      public const string Supervisor = "Supervisor";
      
      // Задача.
      public const string ExampleTask = "ExampleTask";
      
      // Приказ/распоряжение.
      public const string OrderBase = "OrderBase";
      
      // Задача.
      public const string SimpleTask = "SimpleTask";
      
      // Должность.
      public const string JobTitle = "JobTitle";
    }
    
    /// <summary>
    /// Постфикс логгера.
    /// </summary>
    [Public]
    public static class LoggerPostfixesConstLog
    {
      // Задача.
      public const string ExampleMdlTask = "ExampleMdlTask";
      
      // ФП.
      public const string DocflowJob = "DocflowJob";
      
      // Интеграция.
      public const string Integration = "Integration";
    }
    
    /// <summary>
    /// Сообщение.
    /// </summary>
    [Public]
    public static class MessagesConstLogs
    {
      // Контролер не найден.
      public const string SupervisorNotFound = "Supervisor not found";
      
      // Сообщение.
      public const string Message = "Message";
      
      // Документы не найдены.
      public const string DocumentsNotFound = "Documents not found";
      
      // Сущность не найдена.
      public const string EntityNotFound = "Entity not found";
      
      // Не заполнено имя.
      public const string NoName = "No name";
    }
    
    /// <summary>
    /// Параметры.
    /// </summary>
    [Public]
    public static class Value
    {
      // Максимальное количество повторов.
      public const int MaxRetries = 10;
    }
    
  }
}