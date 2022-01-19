using Scooter.Hubs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Scooter.Models
{
    public class NotificationService
    {
        static readonly string connString = @"data source=DESKTOP-C8SPAQN;initial catalog=InventoryManagement;integrated security=true;";

        internal static SqlCommand command = null;
        internal static SqlDependency dependency = null;

        public static string GetNotification()
        {
            try
            {
                var messages = new List<tbl_Notifications>();
                using (var connection = new SqlConnection(connString))
                {
                    connection.Open();
                    using (command = new SqlCommand(@"SELECT [NotificaionId],[Status],[Message],[ExtraColumn] FROM [dbo].[tbl_Notifications]", connection))
                    {
                        command.Notification = null;

                        if (dependency == null)
                        {
                            dependency = new SqlDependency(command);
                            dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);
                        }

                        if (connection.State == ConnectionState.Closed)
                            connection.Open();

                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            messages.Add(item: new tbl_Notifications
                            {
                                NotificaionId = (int)reader["NotificaionId"],
                                Status = reader["Status"] != DBNull.Value ? (string)reader["Status"] : "",
                                Message = reader["Message"] != DBNull.Value ? (string)reader["Message"] : "",
                                ExtraColumn = reader["ExtraColumn"] != DBNull.Value ? (string)reader["ExtraColumn"] : ""

                            });
                        }
                    }
                }
                var jsonSerialiser = new JavaScriptSerializer();
                var json = jsonSerialiser.Serialize(messages);
                return json;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (dependency != null)
            {
                dependency.OnChange -= dependency_OnChange;
                dependency = null;
            }
            if (e.Type == SqlNotificationType.Change)
            {
                MyNewHub.Send();
            }
        }
    }
}