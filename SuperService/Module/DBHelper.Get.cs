using BitMobile.ClientModel3;
using BitMobile.DbEngine;
using System;
using System.Collections;
using Test.Entities.Document;
using DbRecordset = BitMobile.ClientModel3.DbRecordset;

//using Database = BitMobile.ClientModel3.Database;

namespace Test
{
    public static partial class DBHelper
    {
        /// <summary>
        ///     Method returns list of all events
        ///     Возвращает список всех событий
        /// </summary>
        public static ArrayList GetEvents()
        {
            return GetEvents(new DateTime());
        }

        /// <summary>
        ///     Method returns list of events which plan start date biger then param
        ///     Получает список событий плановая дата начала которых больше передаваемого параметра
        /// </summary>
        /// <param name="eventSinceDate"> Дата начания с которой необходимо получить события</param>
        public static ArrayList GetEvents(DateTime eventSinceDate)
        {
            var events = new ArrayList();

            var query = new Query("select " +
                                  "  event.Id, " +
                                  "  event.StartDatePlan, " + //full date
                                  "  date(event.StartDatePlan) as startDatePlanDate, " + //date only
                                  "  ifnull(TypeDeparturesTable.description, '') as TypeDeparture, " +
                                  "  event.ActualStartDate as ActualStartDate, " + //4
                                  "  ifnull(Enum_StatusImportance.Description, '') as Importance, " +
                                  "  ifnull(Enum_StatusImportance.Name, '') as ImportanceName, " +
                                  "  ifnull(client.Description, '') as Description, " +
                                  "  ifnull(client.Address, '') as Address, " +
                                  "  ifnull(Enum_StatusyEvents.Name, '') as statusName, " +
                                  //имя значения статуса (служебное имя)
                                  "  ifnull(Enum_StatusyEvents.Description, '') as statusDescription " +
                                  //представление статуса
                                  "from " +
                                  "  Document_Event as event " +
                                  "    left join Catalog_Client as client " +
                                  "    on event.client = client.Id " +
                                  "      left join " +
                                  "         (select " +
                                  "             Document_Event_TypeDepartures.Ref, " +
                                  "             Catalog_TypesDepartures.description " +
                                  "          from " +
                                  "             (select " +
                                  "                 ref, " +
                                  "                 min(lineNumber) as lineNumber " +
                                  "              from " +
                                  "                 Document_Event_TypeDepartures " +
                                  "              where " +
                                  "                 active = 1 " +
                                  "              group by " +
                                  "                 ref) as t1 " +
                                  "                       left join Document_Event_TypeDepartures " +
                                  "                             on t1.ref= Document_Event_TypeDepartures.ref " +
                                  "                                     and t1.lineNumber = Document_Event_TypeDepartures.lineNumber " +
                                  "                       left join Catalog_TypesDepartures " +
                                  "                             on Document_Event_TypeDepartures.typeDeparture = Catalog_TypesDepartures.id) as TypeDeparturesTable " +
                                  "     on event.id = TypeDeparturesTable.Ref " +
                                  "          left join Enum_StatusImportance " +
                                  "               on event.Importance = Enum_StatusImportance.Id " +
                                  "  " +
                                  "  left join Enum_StatusyEvents " +
                                  "      on event.status = Enum_StatusyEvents.Id " +
                                  "  where " +
                                  "      event.DeletionMark = 0 " +
                                  "      AND (event.StartDatePlan >= @eventDate OR (event.ActualEndDate > date('now','start of day') and Enum_StatusyEvents.Name IN (@statusDone, @statusCancel))) " +
                                  " order by " +
                                  "  event.StartDatePlan");

            query.AddParameter("eventDate", eventSinceDate);
            query.AddParameter("statusDone", EventStatusDoneName);
            query.AddParameter("statusCancel", EventStatusCancelName);
            var querryResult = query.Execute();

            while (querryResult.Next())
            {
                var @event = EventListElement.CreateFromRecordSet(querryResult);
                events.Add(@event);
            }

            return events;
        }

        /// <summary>
        ///     Полуает статистику по нарядам (событиям). Возвращает объект содержащий: количество нарядов на день, количество
        ///     закрытых
        ///     нарядов за день, количество нарядов с начала месяца, количество закрытых нарядов с начала месяца
        /// </summary>
        public static EventsStatistic GetEventsStatistic()
        {
            var statistic = new EventsStatistic();
            var query = new Query("select " +
                                  "  TOTAL(CASE " +
                                  "        when StartDatePlan > date('now','start of day') then 1 " +
                                  "        else 0 " +
                                  "   End) as DayTotalAmount, " +
                                  "    TOTAL(CASE " +
                                  "        when Enum_StatusyEvents.name like 'Done' and StartDatePlan > date('now','start of day') then 1 " +
                                  "        else 0 " +
                                  "   End) as DayCompleteAmout, " +
                                  "   TOTAL(CASE " +
                                  "        when StartDatePlan > date('now', 'start of month') and StartDatePlan < date('now', 'start of month', '+1 month') then 1 " +
                                  "        else 0 " +
                                  "   End) as MonthCompleteAmout, " +
                                  "   TOTAL(CASE " +
                                  "        when Enum_StatusyEvents.name like 'Done' and StartDatePlan > date('now', 'start of month') and StartDatePlan < date('now', 'start of month', '+1 month') then 1 " +
                                  "        else 0 " +
                                  "   End) as MonthTotalAmount " +
                                  "  from " +
                                  "      Document_Event as event " +
                                  "       left join Enum_StatusyEvents " +
                                  "         on event.Status = Enum_StatusyEvents.Id " +
                                  " " +
                                  "where " +
                                  "     event.DeletionMark = 0");
            var result = query.Execute();

            if (result.Next())
            {
                statistic.DayTotalAmount = (int)result["DayTotalAmount"];
                statistic.DayCompleteAmout = (int)result["DayCompleteAmout"];
                statistic.MonthTotalAmount = (int)result["MonthCompleteAmout"];
                statistic.MonthCompleteAmout = (int)result["MonthTotalAmount"];
            }

            return statistic;
        }

        /// <summary>
        ///     Получает полную информацию по событию
        /// </summary>
        /// <param name="eventID"> Идентификатор события</param>
        public static DbRecordset GetEventByID(string eventID)
        {
            var queryText = "select " +
                            "    event.Id,  " + //гуид события
                            "    event.StartDatePlan,  " + //плановая дата начала
                            "    Date(event.StartDatePlan) as StartDatePlanDate,  " +
                            "    Time(event.StartDatePlan) as StartDatePlanTime,  " +
                            "    TypeDeparturesTable.description as TypeDeparture,  " +
                            //вид работ - выбирается одна из табличной части
                            "    event.ActualStartDate,  " + //фактическая дата начала
                            "    event.ActualEndDate,  " + // фактическая дата конца
                            "    _Enum_StatusImportance.Description as Importance,  " + //важность
                            "    event.Comment,  " +
                            //"    100500 as sumFact, " +
                            "    docSUm.sumFact,  " +
                            "    docCheckList.Total as checkListTotal,  " + //общее количество вопросов в чеклисте
                            "    docCheckList.Answered as checkListAnswered,  " +
                            //количество отвеченных вопросов в чеклисте
                            "    docEquipment.Total as equipmentTotal,  " + //количество оборудования (задач)
                            "    docEquipment.Answered as equipmentAnswered,  " +
                            //количество оборудования (задач) с заполненным результатом
                            "    client.id as clientId,  " +
                            "    client.Description as clientDescription,  " + //имя клиента
                            "    client.Address as clientAddress,  " + //адрес клиента
                            "    docCheckList.Required as checkListRequired, " +
                            // количество обязательных вопросов в чеклистах
                            "    docCheckList.RequiredAnswered as checkListRequiredAnswered, " +
                            //количество отвеченных обязательных вопросов в чеклистах
                            "    case  " +
                            "        when ifnull(docCheckList.Required, 0) = ifnull(docCheckList.RequiredAnswered, 0) then 1 " +
                            "        else 0 " +
                            "    end as checkListAllRequiredIsAnswered, " +
                            //признак, что все обязательные вопросы в чеклистах отвечены
                            "    Enum_StatusyEvents.Name as statusName, " + //наименование статуса (служебное имя)
                            "    Enum_StatusyEvents.Description as statusDescription " + //представление статуса +
                            "from  " +
                            "    _Document_Event as event  " +
                            "        left join _Catalog_Client as client  " +
                            "        on  event.id = @id and event.client = client.Id  " +
                            "      " +
                            "        left join  " +
                            "            (select  " +
                            "                  _Document_Event_TypeDepartures.Ref,   " +
                            "                  _Catalog_TypesDepartures.description  " +
                            "             from  " +
                            "                 (select   " +
                            "                      ref,  " +
                            "                      min(lineNumber) as lineNumber  " +
                            "                  from  " +
                            "                      _Document_Event_TypeDepartures  " +
                            "                  where   " +
                            "                      ref = @id   " +
                            "                      and active = 1   " +
                            "                  group by " +
                            "                      ref) as t1  " +
                            "    " +
                            "               left join _Document_Event_TypeDepartures " +
                            "                    on t1.ref= _Document_Event_TypeDepartures.ref " +
                            "                       and t1.lineNumber = _Document_Event_TypeDepartures.lineNumber  " +
                            "               left join _Catalog_TypesDepartures  " +
                            "                    on _Document_Event_TypeDepartures.typeDeparture =  _Catalog_TypesDepartures.id) as TypeDeparturesTable  " +
                            "        on event.id = TypeDeparturesTable.Ref  " +
                            "    " +
                            "        left join _Enum_StatusImportance  " +
                            "           on event.Importance = _Enum_StatusImportance.Id  " +
                            "    " +
                            "        left join (select _Document_Event_ServicesMaterials.Ref, TOTAL(SumFact) as sumFact from _Document_Event_ServicesMaterials where _Document_Event_ServicesMaterials.Ref = @id group by _Document_Event_ServicesMaterials.Ref ) as docSum  " +
                            "           on event.id = docSUm.ref " +
                            "    " +
                            "        left join (select " +
                            "                       Document_Event_CheckList.Ref,  " +
                            "                       count(Document_Event_CheckList.Ref) as Total,  " +
                            "                       TOTAL(case when result = '' then 0 else 1 end) as Answered, " +
                            "                       TOTAL(case when Required = 1 then 1 else 0 end) as Required, " +
                            "                       TOTAL(case when Required = 1 and result <> ''  then 1 else 0 end) as RequiredAnswered " +
                            "                   from  " +
                            "                       Document_Event_CheckList  " +
                            "                   where  " +
                            "                       Document_Event_CheckList.Ref = @id group by Document_Event_CheckList.Ref ) as docCheckList " +
                            "           on event.id = docCheckList.ref " +
                            "    " +
                            "        left join (select Document_Event_Equipments.Ref, count(Document_Event_Equipments.Ref) as Total, TOTAL(case when result is null or result = '' then 0 else 1 end) as Answered from Document_Event_Equipments where Document_Event_Equipments.Ref = @id group by Document_Event_Equipments.Ref ) as docEquipment " +
                            "           on event.id = docEquipment.ref " +
                            "    " +
                            "        left join Enum_StatusyEvents " +
                            "           on event.status = Enum_StatusyEvents.Id     " +
                            "    " +
                            "where  " +
                            "   event.id = @id  ";

            var query = new Query(queryText);
            query.AddParameter("id", eventID);
            var result = query.Execute();

            return result;
        }

        /// <summary>
        ///     Получает список задач события
        /// </summary>
        /// <param name="eventID"> Идентификатор события</param>
        public static DbRecordset GetTasksByEventID(string eventID)
        {
            var query = new Query("select " +
                                  "    tasks.Id,  " + //ид задачи
                                  "    tasks.Ref, " + //ид документа События
                                  "    tasks.Terget, " + //цель
                                  "    equipment.Description as equipmentDescription, " +
                                  "    ResultEvent.Description as ResultEventDescription, " +
                                  "    ResultEvent.Name as ResultEventName, " +
                                  "    case  " +
                                  "        when ResultEvent.Name like 'Done' then 1 " +
                                  "        else 0 " +
                                  "    end as isDone " +
                                  "  from " +
                                  "     Document_Event_Equipments as tasks " +
                                  "      left join Catalog_Equipment as equipment " +
                                  "       on tasks.Equipment = equipment.Id " +
                                  " " +
                                  "      left join _Enum_ResultEvent as ResultEvent " +
                                  "       on tasks.Result = ResultEvent.Id " +
                                  "       " +
                                  " where " +
                                  "   tasks.Ref = @id  ");
            query.AddParameter("id", eventID);
            var result = query.Execute();

            return result;
        }

        /// <summary>
        ///     Возвращает перечень контактных лиц клиента
        /// </summary>
        public static DbRecordset GetContactsByClientID(string clientID)
        {
            var query = new Query("select " +
                                  "     Contacts.Id, " + //гиуд контакноголица
                                  "     Contacts.DeletionMark, " + // признак пометки удаления
                                  "     Contacts.Description, " + //имя
                                  "     Contacts.Position, " + // должность
                                  "     Contacts.Tel " + //телефон
                                  "from " +
                                  "  Catalog_Client_Contacts as ClientContacts " +
                                  "    left join Catalog_Contacts as Contacts " +
                                  "      on ClientContacts.Ref = @clientID " +
                                  "        and  ClientContacts.Contact = Contacts.Id " +
                                  " " +
                                  "where " +
                                  "    Contacts.DeletionMark = 0" +
                                  "    and ClientContacts.Ref = @clientID " +
                                  "    and ClientContacts.Actual = 0 ");
            //выбираем только неактуальных сотрудников, потому что актуальные являются уволенными

            query.AddParameter("clientID", clientID);

            return query.Execute();
        }

        /// <summary>
        ///     Возвращает перечень оборудования клиента. Возвращается все оборудование во всех статусах
        /// </summary>
        /// <param name="clientID"> Идентификатор клиента</param>
        public static DbRecordset GetEquipmentByClientID(string clientID)
        {
            var query = new Query("select " +
                                  "    equipmentLastChangeDate.Equiement as equipmentID, " +
                                  "    equipmentLastChangeDate.period as lastChange, " +
                                  "    Catalog_Equipment.Description " +
                                  "" +
                                  "from " +
                                  "       (select " +
                                  "            clients, " +
                                  "            Equiement, " +
                                  "            MAX(period) as period " +
                                  "        from " +
                                  "            Catalog_Equipment_Equiements " +
                                  "        where " +
                                  "            clients = @clientID " +
                                  "        group by " +
                                  "            clients, Equiement) as equipmentLastChangeDate " +
                                  "" +
                                  "        left join Catalog_Equipment_Equiements " +
                                  "        on equipmentLastChangeDate.clients = Catalog_Equipment_Equiements.clients " +
                                  "        and equipmentLastChangeDate.Equiement = Catalog_Equipment_Equiements.Equiement " +
                                  "        and equipmentLastChangeDate.Period = Catalog_Equipment_Equiements.Period " +
                                  "" +
                                  "        left join Catalog_Equipment " +
                                  "        on equipmentLastChangeDate.Equiement = Catalog_Equipment.id");

            query.AddParameter("clientID", clientID);

            return query.Execute();
        }

        /// <summary>
        ///     Возвращает список всех клиентов
        /// </summary>
        public static DbRecordset GetClients()
        {
            var query = new Query("select " +
                                  "      Catalog_Client.Id, " +
                                  "      Catalog_Client.Description, " +
                                  "      Catalog_Client.Address, " +
                                  "      Catalog_Client.Latitude, " +
                                  "      Catalog_Client.Longitude " +
                                  "  from " +
                                  "      Catalog_Client " +
                                  " " +
                                  "where " +
                                  "    Catalog_Client.DeletionMark = 0");

            return query.Execute();
        }

        /// <summary>
        ///     Возвращает информацию по клиенту
        /// </summary>
        /// <param name="clientID">
        ///     Идентификатор клиента
        /// </param>
        public static DbRecordset GetClientByID(string clientID)
        {
            var query = new Query("select " +
                                  "    Catalog_Client.Id, " +
                                  "    Catalog_Client.Description, " +
                                  "    Catalog_Client.Address, " +
                                  "    Catalog_Client.Latitude, " +
                                  "    Catalog_Client.Longitude " +
                                  "from " +
                                  "    Catalog_Client " +
                                  "where " +
                                  "    Catalog_Client.Id = @clientID");

            query.AddParameter("clientID", clientID);
            return query.Execute();
        }

        /// <summary>
        ///     Получает список вопросов чек-листов по идентификаторы события
        /// </summary>
        /// <param name="eventID"> Идентификатор события</param>
        public static DbRecordset GetCheckListByEventID(string eventID)
        {
            var query = new Query("select " +
                                  "   checkList.Id as CheckListId, " +
                                  "   checkList.Ref as EventId, " +
                                  "   checkList.Required as Required, " + //признак обязательности
                                  "   checkList.Result as Result, " + //значение результата
                                  "   checkList.Action as ActionId, " +
                                  "   actions.Description as Description, " + //название пункта чек-листа
                                  "   typesDataParameters.Name as TypeName " +
                                  //Тип значения чек-листа: ValList - выбор из списка значений; Snapshot - фото; остальное понятно из названий
                                  "from " +
                                  "   Document_Event_CheckList as checkList " +
                                  "   left join Catalog_Actions as actions " +
                                  "     ON checkList.Ref = @eventId " +
                                  "       AND checkList.Action = actions.Id " +
                                  "    " +
                                  "   left join Enum_TypesDataParameters as typesDataParameters " +
                                  "     ON checkList.ActionType = TypesDataParameters.Id " +
                                  "    " +
                                  "where " +
                                  "    checkList.Ref = @eventId");

            query.AddParameter("eventId", eventID);
            return query.Execute();
        }

        /// <summary>
        ///     Получает список вариантов ответов для действий (вопросов)  с типом результата "выбор из списка"
        /// </summary>
        /// <param name="actionID"> Идентификатор действия</param>
        public static DbRecordset GetActionValuesList(string actionID)
        {
            var query = new Query("select " +
                                  "     Catalog_Actions_ValueList.Id, " + //идентификатор ответа
                                  "     Catalog_Actions_ValueList.Val " + //представление ответа
                                  "from " +
                                  "     Catalog_Actions_ValueList " +
                                  "where " +
                                  /*"     Catalog_Actions_ValueList.DeletionMark = 0 " +
                                  "     and "*/ "Catalog_Actions_ValueList.Ref = @actionID");
            query.AddParameter("actionID", actionID);
            return query.Execute();
        }

        /// <summary>
        ///     Возвращает задачу по ее идентификатору
        /// </summary>
        /// <param name="taskID">
        ///     Идентификатор задачи
        /// </param>
        public static DbRecordset GetTaskById(string taskID)
        {
            var query = new Query("select  " +
                                  "      tasks.id as taskID, " + //гуид задачи
                                  "      tasks.Ref as EventID, " + //гуид наряда (события) к которому относится задача
                                  "      tasks.terget as Target, " + //Цель
                                  "      tasks.Comment as Comment, " + // комментарий
                                  "      equipment.Description as EquipmentDescription, " + //наименование оборудование
                                  "      Enum_ResultEvent.Name as resultName, " + //результат имя
                                  "      Enum_ResultEvent.Description as resultDescription, " +
                                  //результат представление
                                  "      TypeDeparturesTable.TypeDepartures " +
                                  //вид работ - выбирается первая активная из списка наряда
                                  " " +
                                  "from " +
                                  "    Document_Event_Equipments as tasks " +
                                  "       left join _Catalog_Equipment as equipment " +
                                  "         on tasks.Id = @taskID " +
                                  "         and tasks.Equipment = equipment.Id " +
                                  " " +
                                  "       left join Enum_ResultEvent " +
                                  "          on tasks.Result = Enum_ResultEvent.Id " +
                                  " " +
                                  "        left join " +
                                  "                (select " +
                                  "                     Document_Event_TypeDepartures.Ref, " +
                                  "                     Catalog_TypesDepartures.description as TypeDepartures " +
                                  "                from " +
                                  "                    (select " +
                                  "                          ref, " +
                                  "                          min(lineNumber) as lineNumber " +
                                  "                     from " +
                                  "                          Document_Event_TypeDepartures " +
                                  "                     where " +
                                  "                          ref = (select Ref from Document_Event_Equipments where id = @taskID limit 1) " +
                                  "                          and active = 1 " +
                                  "                     group by " +
                                  "                          ref) as trueTypeDepartures " +
                                  " " +
                                  "                        left join Document_Event_TypeDepartures " +
                                  "                             on trueTypeDepartures.ref = Document_Event_TypeDepartures.ref " +
                                  "                                and trueTypeDepartures.lineNumber = Document_Event_TypeDepartures.lineNumber " +
                                  " " +
                                  "                        left join Catalog_TypesDepartures " +
                                  "                             on Document_Event_TypeDepartures.typeDeparture = Catalog_TypesDepartures.id) as TypeDeparturesTable " +
                                  " " +
                                  "            on tasks.Ref = TypeDeparturesTable.Ref " +
                                  " " +
                                  "where " +
                                  "      tasks.Id = @taskID");
            query.AddParameter("taskID", taskID);

            return query.Execute();
        }

        /// <summary>
        ///     Возвращает суммы по АВР
        /// </summary>
        /// <param name="eventId">Идентификатор наряда</param>
        /// <returns>
        ///     DbRecordset со следующими полями:
        ///     Sum - сумма по наряду
        ///     SumMaterials - сумма только по материалам
        ///     SumServices - сумма только по услугам
        /// </returns>
        public static DbRecordset GetCocSumsByEventId(string eventId)
        {
            var query = new Query("select " +
                                  "    TOTAL(SumFact) as Sum, " +
                                  "    TOTAL(case when Service = 0 then SumFact else 0 end) as SumMaterials, " +
                                  "    TOTAL(case when Service = 1 then SumFact else 0 end) as SumServices " +
                                  "from " +
                                  "    _Document_Event_ServicesMaterials " +
                                  "    join Catalog_RIM " +
                                  "        on _Document_Event_ServicesMaterials.SKU = Catalog_RIM.Id " +
                                  "where Ref = @eventId");
            query.AddParameter("eventId", eventId);
            return query.Execute();
        }

        /// <summary>
        ///     Возвращает информацию по материалам
        /// </summary>
        /// <param name="eventId">Идентификатор наряда</param>
        /// <returns></returns>
        public static DbRecordset GetMaterialsByEventId(string eventId)
        {
            // TODO: Написать запрос

            var query = new Query("select " +
                                  "    _Document_Event_ServicesMaterials.Id," +
                                  "    _Document_Event_ServicesMaterials.SKU," +
                                  "    Catalog_RIM.Price," +
                                  "    AmountPlan," +
                                  "    SumPlan," +
                                  "    AmountFact," +
                                  "    SumFact," +
                                  "    Description," +
                                  "    Code," +
                                  "    Unit " +
                                  "from" +
                                  "    _Document_Event_ServicesMaterials " +
                                  "    join Catalog_RIM " +
                                  "        on _Document_Event_ServicesMaterials.SKU = Catalog_RIM.Id " +
                                  " where Catalog_RIM.Service = 0 and " +
                                  " _Document_Event_ServicesMaterials.AmountFact != 0 and" +
                                  "    _Document_Event_ServicesMaterials.Ref = @eventId");
            query.AddParameter("eventId", eventId);
            return query.Execute();
        }

        /// <summary>
        ///     Возвращает информацию по услугам
        /// </summary>
        /// <param name="eventId">Идентификатор наряда</param>
        /// <returns></returns>
        public static DbRecordset GetServicesByEventId(string eventId)
        {
            var query = new Query("select " +
                                  "    _Document_Event_ServicesMaterials.Id," +
                                  "    _Document_Event_ServicesMaterials.SKU," +
                                  "    Catalog_RIM.Price," +
                                  "    AmountPlan," +
                                  "    SumPlan," +
                                  "    AmountFact," +
                                  "    SumFact," +
                                  "    Description," +
                                  "    Code," +
                                  "    Unit " +
                                  "from" +
                                  "    _Document_Event_ServicesMaterials " +
                                  "       join Catalog_RIM" +
                                  "        on _Document_Event_ServicesMaterials.SKU = Catalog_RIM.Id " +
                                  " where Catalog_RIM.Service = 1 and " +
                                  " _Document_Event_ServicesMaterials.AmountFact != 0 and" +
                                  "    _Document_Event_ServicesMaterials.Ref = @eventId");
            query.AddParameter("eventId", eventId);
            return query.Execute();
        }

        /// <summary>
        ///     Возвращает список материалов и услуг по указанному типу
        /// </summary>
        /// <param name="rimType">необходимый тип элементов работы и услуги</param>
        /// <returns></returns>
        public static DbRecordset GetRIMByType(RIMType rimType)
        {
            var query = new Query("select " +
                                  "    id, " +
                                  "    Description, " +
                                  "    Price, " +
                                  "    Unit " +
                                  "from " +
                                  "    Catalog_RIM " +
                                  "where " +
                                  "    deletionMark = 0 " +
                                  "    and isFolder = 0 " +
                                  "    and service = @rim_type");

            DConsole.WriteLine("rimType = " + rimType);
            if (rimType == RIMType.Material)
                query.AddParameter("rim_type", 0);
            else
                query.AddParameter("rim_type", 1);

            return query.Execute();
        }

        /// <summary>
        ///     Возвращает строку табличной части "услуги и материалы" документа Событие с указанным идентификатором номенклатуры.
        ///     Используется для определения наличия в ТЧ документа номенклатуры с заданным ИД (проверка есть уже такая или нет)
        /// </summary>
        /// <param name="docEventID">Идентификатор документа событие</param>
        /// <param name="rimID">Идентификатор искомого элемента справочинка Товары и услуги</param>
        /// <returns>
        ///     null - если в указанном документе нету номенклатуры с указанным идентификатором;
        ///     Заполнненую структуру EventServicesMaterialsLine в случае если строка есть
        /// </returns>
        public static EventServicesMaterialsLine GetEventServicesMaterialsLineByRIMID(string docEventID, string rimID)
        {
            EventServicesMaterialsLine result = null;
            var queryText = "select " +
                            "    id, " +
                            "    LineNumber, " +
                            "    Ref, " +
                            "    SKU, " +
                            "    Price, " +
                            "    AmountPlan, " +
                            "    SumPlan, " +
                            "    AmountFact, " +
                            "    SumFact, " +
                            "    isDirty " +
                            "from " +
                            "    _Document_Event_ServicesMaterials " +
                            "where " +
                            "    _Document_Event_ServicesMaterials.Ref = @EventDocRef " +
                            "    and _Document_Event_ServicesMaterials.SKU = @SKUID";

            var query = new Query(queryText);
            query.AddParameter("EventDocRef", docEventID);
            query.AddParameter("SKUID", rimID);

            var queryResult = query.Execute();

            if (queryResult.Next())
            {
                DConsole.WriteLine("зашли в обработку результата запроса");
                result = new EventServicesMaterialsLine
                {
                    ID = queryResult.GetString(0),
                    LineNumber = queryResult.GetInt32(1),
                    Ref = queryResult.GetString(2),
                    SKU = queryResult.GetString(3),
                    Price = queryResult.GetDecimal(4),
                    AmountPlan = queryResult.GetDecimal(5),
                    SumPlan = queryResult.GetDecimal(6),
                    AmountFact = queryResult.GetDecimal(7),
                    SumFact = queryResult.GetDecimal(8)
                };
            }

            return result;
        }

        /// <summary>
        ///     Возвращает строку табличной части "услуги и материалы" документа Событие по ее идентификатору
        /// </summary>
        /// <param name="lineID">Идентификатор строки ТЧ услуги и материалы</param>
        /// <returns>
        ///     строка табличной части
        /// </returns>
        public static DbRecordset GetEventServicesMaterialsLineById(string lineID)
        {
            var queryString = "select " +
                              "    id, " +
                              "    LineNumber, " +
                              "    Ref, " +
                              "    SKU, " +
                              "    Price, " +
                              "    AmountPlan, " +
                              "    AmountFact, " +
                              "    SumPlan, " +
                              "    SumFact " +
                              "from " +
                              "  _Document_Event_ServicesMaterials " +
                              "where " +
                              "   _Document_Event_ServicesMaterials.id = @lineId";

            var query = new Query(queryString);
            query.AddParameter("lineId", lineID);

            return query.Execute();
        }

        /// <summary>
        ///     Получает текущие остатки рюкзака монтажника
        /// </summary>
        /// <param name="userID">Идентификатор пользоателя для получения остатков рюкзака</param>
        /// <returns>
        ///     текущие остатки рюкзака
        /// </returns>
        public static DbRecordset GetUserBagByUserId(string userID)
        {
            var queryString = "select " +
                              "    _Catalog_RIM.id,  " +
                              "    _Catalog_RIM.Description, " +
                              "    _Catalog_RIM.Unit,   " +
                              "    _Catalog_User_Bag.Count " +
                              "  from " +
                              "    _Catalog_User_Bag " +
                              "        left join _Catalog_RIM " +
                              "            on _Catalog_User_Bag.Materials = _Catalog_RIM.id " +
                              "  where " +
                              "    _Catalog_User_Bag.Ref = @userId";
            var query = new Query(queryString);
            query.AddParameter("userId", userID);

            return query.Execute();
        }

        /// <summary>
        ///     Получает список документов заявка на материалы
        /// </summary>
        /// <returns>
        ///     список документов заявка на материалы
        /// </returns>
        public static DbRecordset GetNeedMats()
        {
            var queryText = "select " +
                            "   _Document_NeedMat.id, " +
                            "   _Document_NeedMat.Date, " +
                            "   Time(_Document_NeedMat.Date) as docTime, " +
                            "   _Document_NeedMat.Number, " +
                            "   _Enum_StatsNeedNum.name as statusName, " +
                            "   _Enum_StatsNeedNum.Description as statusDescription " +
                            " " +
                            "from " +
                            "   _Document_NeedMat " +
                            "       left join _Enum_StatsNeedNum " +
                            "           on _Document_NeedMat.StatsNeed = _Enum_StatsNeedNum.id " +
                            "order by " +
                            "   _Document_NeedMat.Date desc";

            var query = new Query(queryText);

            return query.Execute();
        }

        /// <summary>
        ///     Получает информацию по строке материалов и услуг документа Наряд
        /// </summary>
        /// ///
        /// <param name="lineId">Идентификатор строки</param>
        public static DbRecordset GetServiceMaterialPriceByLineID(string lineId)
        {
            var query = new Query("select " +
                                  "      _Document_Event_ServicesMaterials.Id, " +
                                  "      _Document_Event_ServicesMaterials.SKU as RIMID, " +
                                  "      _Catalog_RIM.Description, " +
                                  "      _Document_Event_ServicesMaterials.Price, " +
                                  "      _Document_Event_ServicesMaterials.AmountFact, " +
                                  "      _Document_Event_ServicesMaterials.SumFact " +
                                  "from " +
                                  "    _Document_Event_ServicesMaterials " +
                                  "         left join _Catalog_RIM " +
                                  "            on _Document_Event_ServicesMaterials.SKU = _Catalog_RIM.Id " +
                                  " " +
                                  "where " +
                                  "    _Document_Event_ServicesMaterials.id = @lineId");
            query.AddParameter("lineId", lineId);
            return query.Execute();
        }

        /// <summary>
        ///     Получает информацию по строке материалов и услуг документа Наряд
        /// </summary>
        /// ///
        /// <param name="rimId">Идентификатор строки</param>
        public static DbRecordset GetServiceMaterialPriceByRIMID(string rimId)
        {
            var query = new Query("select " +
                                  "      _Catalog_RIM.id, " +
                                  "      _Catalog_RIM.Description, " +
                                  "      _Catalog_RIM.Price, " +
                                  "      _Catalog_RIM.Unit,  " +
                                  "      1 as AmountFact, " +
                                  "      _Catalog_RIM.Price as SumFact " +
                                  "from " +
                                  "    _Catalog_RIM " +
                                  " " +
                                  "where " +
                                  "    _Catalog_RIM.id = @RIMId");
            query.AddParameter("RIMId", rimId);
            return query.Execute();
        }

        /// <summary>
        ///     возвращает перечень невыполненных нарядов с их координатами
        /// </summary>
        public static DbRecordset GetEventsLocation()
        {
            var queryText = "";
            var query = new Query(queryText);

            return query.Execute();
        }

        public static Event_Equipments GetEventEquipmentsById(string id)
        {
            var query = new Query("select * from Document_Event_Equipments where id = @id");
            query.AddParameter("id", id);
            var dbRecordset = query.Execute();
            return new Event_Equipments((DbRef)dbRecordset["Id"])
            {
                Comment = (string)dbRecordset["Comment"],
                Equipment = (DbRef)dbRecordset["Equipment"],
                LineNumber = (int)dbRecordset["LineNumber"],
                Ref = (DbRef)dbRecordset["Ref"],
                Result = (DbRef)dbRecordset["Result"],
                SID = (DbRef)dbRecordset["SID"],
                Terget = (string)dbRecordset["Terget"]
            };
        }

        public static DbRecordset GetClientLocationByClientId(string clientId)
        {
            var query = new Query(@"select
                                    client.Description as Description,
                                    client.Latitude as Latitude,
                                    client.Longitude as Longitude
                                from
                                    _Catalog_Client as client
                                where
                                    client.Latitude != 0
                                    and client.Longitude != 0
                                    and client.Id = @clientId");

            query.AddParameter("clientId", clientId);

            return query.Execute();
        }

        public static DbRecordset GetEventsLocationToday()
        {
            var query = new Query(@"select
                                        client.Description as Description,
                                        client.Latitude as Latitude,
                                        client.Longitude as Longitude
                                    from
                                        _Document_Event as event
                                    left join _Catalog_Client as client
                                        on event.client = client.id
                                    where
                                        event.DeletionMark = 0
                                        and event.ResultInteractions != '@ref[Enum_ResultEvent]:81270b2c-190a-faf2-440f-4a593042495e'
                                        and date(event.StartDatePlan) = date('now','start of day')
                                        and client.Latitude != 0
                                        and client.Longitude != 0");

            return query.Execute();
        }
        /// <summary>
        /// Получаем значение связанное с тем,
        /// что используется ли рюкзак или нет.
        /// возращяет булевское значение упакованное в object
        /// </summary>
        /// <returns>true используется рюкзак монтажника,
        /// false если не используется. null если таблица пустая или не найдено значение</returns>
        public static bool GetIsBag()
        {
            var query = new Query(@"SELECT LogicValue
                                    FROM _Catalog_SettingMobileApplication
                                    WHERE Description = 'UsedServiceBag' ");

            var dbResult = query.Execute();


            return dbResult.Next() ? (bool) dbResult["LogicValue"] : Convert.ToBoolean("False");
            
        }

        public static DbRecordset GetRIMFromBag(RIMType type = RIMType.Material)
        {
            var query = new Query(@"SELECT _Catalog_RIM.Id as id, 
                                           _Catalog_RIM.Description as Description,
                                           _Catalog_RIM.Price as Price,
                                           _Catalog_RIM.Unit as Unit
                                    FROM
                                           _Catalog_User_Bag
                                    LEFT JOIN
                                           _Catalog_Rim
                                    ON _Catalog_User_Bag.Materials =  _Catalog_RIM.Id
                                    WHERE _Catalog_RIM.IsFolder = 0 and
                                          _Catalog_RIM.DeletionMark = 0 and
                                           service = @isService ");

            query.AddParameter("isService",(int)type);

            return query.Execute();
        }
    }
}