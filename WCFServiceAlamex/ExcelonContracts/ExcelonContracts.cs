using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;

[ServiceContract]
public interface IExcelonService
{
    [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetSessions/{userId}")]
    [OperationContract]
    List<string> GetSessions(string userId);

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "SavePDF")]
    [OperationContract]
    string SavePDF(string fileTable, int fileId, string sheetName, string isAdminStr);

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "SaveAsFile")]
    [OperationContract]
    int SaveAsFile(string fileTable, string userId, string fileName, string json);

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "UpdateFile")]
    [OperationContract]
    bool UpdateFile(string fileTable, int fileId, string json);

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "DeleteFile")]
    [OperationContract]
    bool DeleteFile(string fileTable, int fileId);

    [WebInvoke(Method = "POST", BodyStyle = WebMessageBodyStyle.Wrapped, RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "RenameFile")]
    [OperationContract]
    bool RenameFile(string fileTable, int fileId,string fileName);

    [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetAllCallBacks/{userId}")]
    [OperationContract]
    List<CallBack> GetAllCallBacks(string userId);

    [WebGet( RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetCallBacks/{idList}/{isAdminStr}")]
    [OperationContract]
    List<string> GetCallBacks(string idList, string isAdminStr);
    
    [WebGet( RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json, UriTemplate = "GetFileChanges/{fileTable}/{fileIdStr}")]
    [OperationContract]
    string GetFileChanges(string fileTable, string fileIdStr);

}

[DataContract]
public class CallBack
{
    [DataMember]
    public int fileId;
    [DataMember]
    public string callBackName;
    [DataMember]
    public string authorName;
    [DataMember]
    public string updatedDate;

    public CallBack(int fileId, string callBackName, string authorName, string updatedDate)
    {
        this.fileId = fileId;
        this.callBackName = callBackName;
        this.authorName = authorName;
        this.updatedDate = updatedDate;
    }

}
