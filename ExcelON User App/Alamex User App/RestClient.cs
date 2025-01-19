using System;
using System.Collections.Generic;
using System.Data;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace ExcelON_User_App
{
    internal class RestClient : ClientBase<IExcelonService>, IExcelonService
    {
        public RestClient(string address)
            : base(new WebHttpBinding(), new EndpointAddress(address))
        {
            Endpoint.Behaviors.Add(new WebHttpBehavior());
        }

        public bool DeleteFile(string fileTable,int fileId)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.DeleteFile(fileTable, fileId);
            }
        }

        public List<CallBack> GetAllCallBacks(string userId)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.GetAllCallBacks(userId);
            }
        }

        public List<string> GetCallBacks(string idList, string isAdminStr)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.GetCallBacks(idList,isAdminStr);
            }
        }

        public List<string> GetSessions(string userId)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.GetSessions(userId);
            }
        }

        public string GetFileChanges(string fileTable,string fileId)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.GetFileChanges(fileTable, fileId);
            }
        }

        public int SaveAsFile(string fileTable, string userId, string fileName, string json)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.SaveAsFile(fileTable, userId, fileName, json);
            }
        }

        public string SavePDF(string fileTable,int fileId, string sheetName, string isAdminStr)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.SavePDF(fileTable, fileId, sheetName, isAdminStr);
            }
        }

        public bool UpdateFile(string fileTable,int fileId, string json)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.UpdateFile(fileTable, fileId, json);
            }
        }

        public bool RenameFile(string fileTable,int fileId, string fileName)
        {
            using (new OperationContextScope(this.InnerChannel))
            {
                return Channel.RenameFile(fileTable, fileId, fileName);
            }
        }
    }

}
