using ExcelonLibrary;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web;
using CM = System.Configuration.ConfigurationManager;
using DIR = System.IO.Directory;
using SqlCmd = System.Data.SqlClient.SqlCommand;
using SqlCon = System.Data.SqlClient.SqlConnection;

[ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
public class ExcelonService : IExcelonService
{
    static string ROOT = @"C:\Users\Alam\OneDrive\DO NOT TOUCH\";

    string USER_MAIN = ROOT + "User Main.xlsx";
    string ADMIN_MAIN = ROOT + "Admin Main.xlsx";

    string[] U_CALLBACKS = DIR.GetFiles(ROOT, "User CallBack ?.xlsx");
    string[] A_CALLBACKS = DIR.GetFiles(ROOT, "Admin CallBack ?.xlsx");
    string[] U_SESSIONS = DIR.GetFiles(ROOT, "User Session ?.xlsx");
    string[] A_SESSIONS = DIR.GetFiles(ROOT, "Admin Session ?.xlsx");

    static SqlCon connection = new SqlCon(CM.ConnectionStrings["ExcelonDbConn"].ConnectionString);
    static SqlCmd cmd = new SqlCmd("", connection);


    public List<string> GetSessions(string userId)
    {
        bool isAdmin = false;
        List<string> ewaPaths = null;
        if (connection.State != ConnectionState.Open)
            connection.Open();
        cmd.CommandText = "select [Role] from [User] where [id] = @UserId";
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@UserId", userId);
        var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            string role = reader[0].ToString();
            isAdmin = role == "Admin" || role == "GM";
            ewaPaths = new List<string> { @"\DO NOT TOUCH\" + (isAdmin ? "Admin.html" : "User.html") };
        }
        reader.Close();
        cmd.CommandText = string.Format("select top {0} Id,JsonText, [Name] from [Session] where UserId = @UserId", isAdmin ? A_SESSIONS.Length : U_SESSIONS.Length);
        reader = cmd.ExecuteReader();
        int i = 0;
        while (reader.Read())
        {
            string sessFile = isAdmin ? A_SESSIONS[i] : U_SESSIONS[i];
            string mainFile = isAdmin ? ADMIN_MAIN : USER_MAIN;
            string result = Excelon.saveChangesToExcel(reader[1].ToString(), mainFile, sessFile);
            if (result == "OK")
            {
                string ewaFile = @"\DO NOT TOUCH\" + string.Format(isAdmin ? "Admin Session {0}.html" : "User Session {0}.html", ++i);
                Excelon.updateEwaTitle(reader[0].ToString() + "@" + reader[2].ToString() + sessFile.Substring(ROOT.Length - 1), HttpRuntime.AppDomainAppPath + ewaFile);
                ewaPaths.Insert(0, ewaFile);
            }
        }
        reader.Close();
        return ewaPaths;
    }

    public string SavePDF(string fileTable, int fileId, string sheetName, string isAdminStr)
    {
        bool isAdmin = bool.Parse(isAdminStr);

        cmd.CommandText = string.Format("select JsonText from {0} where Id = @Id", fileTable);
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Id", fileId);
        var reader = cmd.ExecuteReader();

        try
        {
            if (reader.Read())
            {
                string json = reader[0].ToString();
                reader.Close();

                return Excelon.saveChangesToPDF(json, isAdmin ? ADMIN_MAIN:USER_MAIN, sheetName);
            }
            else
                return string.Format("{0} is not found", fileId);

        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            reader.Close();
        }

    }

    public int SaveAsFile(string fileTable, string userId, string fileName, string json)
    {
        try
        {
            while (json.Contains(",,"))
            {
                json = json.Replace(",,", ",");
            }

            cmd.CommandText = string.Format("insert into {0} values (@UpdatedTime,@JsonText,@UserId,@Name)", fileTable);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@UserId", userId);
            cmd.Parameters.AddWithValue("@Name", fileName);
            cmd.Parameters.AddWithValue("@UpdatedTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@JsonText", json);

            if (cmd.ExecuteNonQuery() > 0)
            {
                cmd.CommandText = string.Format("select top 1 Id from {0} order by Id desc", fileTable);
                cmd.Parameters.Clear();
                var r = cmd.ExecuteReader();
                int resId = int.MaxValue;
                if (r.Read())
                    resId = int.Parse(r[0].ToString());
                r.Close();
                return resId;
            }
            else return 0;
        }
        catch
        {
            return int.MaxValue;
        }
    }

    public bool UpdateFile(string fileTable, int fileId, string json)
    {
        if (json == "")
            return true;

        cmd.CommandText = string.Format("select JsonText from {0} where Id = @Id", fileTable);
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Id", fileId);
        var reader = cmd.ExecuteReader();

        try
        {
            string newJson;

            if (!reader.Read())
                return false;

            newJson = reader[0].ToString();
            newJson = newJson.Insert(newJson.Length - 2, ",");
            newJson = newJson.Insert(newJson.Length - 2, json.Substring(12, json.Length - 14));

            reader.Close();

            cmd.CommandText = string.Format("update {0} set UpdatedTime = @UpdatedTime, JsonText = @JsonText where Id = @Id", fileTable);
            cmd.Parameters.AddWithValue("@UpdatedTime", DateTime.Now);

            while (newJson.Contains(",,"))
            {
                newJson = newJson.Replace(",,", ",");
            }



            cmd.Parameters.AddWithValue("@JsonText", newJson);

            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;

        }
        catch
        {
            return false;
        }
        finally
        {
            reader.Close();
        }
    }

    public bool DeleteFile(string fileTable, int fileId)
    {
        try
        {
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", fileId);
            cmd.CommandText = string.Format("delete from {0} where Id = @Id", fileTable);

            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
        }
        catch
        {
            return false;
        }
    }

    public List<CallBack> GetAllCallBacks(string userId)
    {
        cmd.CommandText = "GetAllCallBacks";
        cmd.Parameters.Clear();
        cmd.Parameters.Add("@UserId", SqlDbType.VarChar, 100).Value = userId;
        cmd.CommandType = CommandType.StoredProcedure;
        var r = cmd.ExecuteReader();
        List<CallBack> cbList = null;
        try
        {
            cbList = new List<CallBack>();
            while (r.Read())
                cbList.Add(new CallBack(int.Parse(r[0].ToString()), r[1].ToString(), r[2].ToString(), r[3].ToString()));
        }
        catch
        {
            return null;
        }
        finally
        {
            r.Close();
            cmd.CommandType = CommandType.Text;
        }
        return cbList;
    }

    public List<string> GetCallBacks(string idList, string isAdminStr)
    {
        bool isAdmin = bool.Parse(isAdminStr);

        List<string> ewaPaths = new List<string>();

        cmd.Parameters.Clear();
        string[] Ides = idList.Split(',');
        int MaxCB = isAdmin ? A_CALLBACKS.Length : U_CALLBACKS.Length;
        string[] topIdes;
        if (Ides.Length > MaxCB)
        {
            topIdes = new string[MaxCB];
            Array.Copy(Ides, 0, topIdes, 0, MaxCB);
            Ides = topIdes;
        }
        var parameters = new string[Ides.Length];

        for (int i = 0; i < Ides.Length; i++)
        {
            parameters[i] = string.Format("@Id{0}", i);
            cmd.Parameters.AddWithValue(parameters[i], Ides[i]);
        }

        cmd.CommandText = string.Format("select Id,JsonText, [Name] from CallBack where Id in  ({0})", string.Join(", ", parameters));
        var reader = cmd.ExecuteReader();
        int j = 0;
        try
        {
            while (reader.Read())
            {
                string cbFile = isAdmin ? A_CALLBACKS[j] : U_CALLBACKS[j];
                string mainFile = isAdmin ? ADMIN_MAIN : USER_MAIN;
                string result = Excelon.saveChangesToExcel(reader[1].ToString(), mainFile, cbFile);
                if (result == "OK")
                {
                    string ewaFile = @"\DO NOT TOUCH\" + string.Format(isAdmin ? "Admin CallBack {0}.html" : "User CallBack {0}.html", ++j);
                    Excelon.updateEwaTitle(reader[0].ToString() + "@" + reader[2].ToString() + cbFile.Substring(ROOT.Length - 1), HttpRuntime.AppDomainAppPath + ewaFile);
                    ewaPaths.Add(ewaFile);
                }
            }
        }
        finally { reader.Close(); }

        return ewaPaths;

    }

    public string GetFileChanges(string fileTable, string fileIdStr)
    {
        int fileId = int.Parse(fileIdStr);
        cmd.Parameters.Clear();
        cmd.Parameters.AddWithValue("@Id", fileId);
        cmd.CommandText = string.Format("select JsonText from {0} where Id = @Id", fileTable);
        SqlDataReader reader = null;

        try
        {
            reader = cmd.ExecuteReader();
            if (reader.Read())
                return reader[0].ToString();
            else
                return "";

        }
        catch (Exception e)
        {
            return e.Message;
        }
        finally
        {
            reader.Close();
        }
    }

    public bool RenameFile(string fileTable, int fileId, string fileName)
    {
        try
        {
            cmd.CommandText = string.Format("update {0} set [Name] = @Name where Id = @Id", fileTable);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Id", fileId);
            cmd.Parameters.AddWithValue("@Name", fileName);

            if (cmd.ExecuteNonQuery() > 0)
                return true;
            else return false;
        }
        catch
        {
            return false;
        }
    }
}
