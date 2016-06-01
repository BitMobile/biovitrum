using BitMobile.ClientModel3;

namespace Test
{
    public static partial class DBHelper
    {
        /// <summary>
        ///     Удаляет строку в таблице услуг и материалов. Использовать осторожно!!
        /// </summary>
        /// <param name="serviceMaterialId">Идентификатор строки в таблице соответствий услуг и материалов</param>
        public static void DeleteServiceOrMaterialById(string serviceMaterialId)
        {
            // TODO: Возможно, тут должно быть DeletionMark, но для теста приложения пока так
            var query = new Query("delete from _Document_Event_ServicesMaterials " +
                                  "where Id=@serviceMAterialId");
            query.AddParameter("serviceMAterialId", serviceMaterialId);
            query.Execute();
        }
    }
}