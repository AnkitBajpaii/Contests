using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;


using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

[DataContract]
public class Company{
    [DataMember]
    public  string name {get;set;}
    
    [DataMember]
    public  string basename {get;set;}
}

[DataContract]
public class Geo{
    [DataMember]
    public  string lat {get;set;}
    
    [DataMember]
    public  string lng {get;set;}
}

[DataContract]
public class Address{
    [DataMember]
    public  string street  {get;set;}
    
    [DataMember]
    public  string suite {get;set;}
    
    [DataMember]
    public  string city {get;set;}
    
    [DataMember]
    public  string zipcode {get;set;}
    
    [DataMember]
    public  Geo geo {get;set;}
}

[DataContract]
public class User{
    [DataMember]
    public string id {get;set;}
    
    [DataMember]
    public  string name {get;set;}
    
    [DataMember]
    public  string username {get;set;}
    
    [DataMember]
    public  string email {get;set;}
    
    [DataMember]
    public  Address address {get;set;}
    
    [DataMember]
    public  string website {get;set;}
    
    [DataMember]
    public  Company company {get;set;}
    
}

class Result
{
    static HttpClient client = new HttpClient();

    /*
     * Complete the 'apiResponseParser' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. STRING_ARRAY inputList ex: ["username", "EQUALS", "Ankit"] ex: ["address", "IN", "Mumbai,Kolkata"]
     *  2. INTEGER size
     */

    public static List<int> apiResponseParser(List<string> inputList, int size)
    {
        string path = "https://raw.githubusercontent.com/arcjsonapi/ApiSampleData/master/api/users";
        
        Task<HttpResponseMessage> usersTask = client.GetAsync(path);
        usersTask.Wait();
        
        var response = usersTask.Result;
        
        List<User> users = new List<User>();
        
        if(response.IsSuccessStatusCode){
            
                var bodyTask = response.Content.ReadAsStringAsync();
                bodyTask.Wait();
                var body = bodyTask.Result;               
                
                using(var ms = new MemoryStream(Encoding.UTF8.GetBytes(body)))
                {
                    var ser = new DataContractJsonSerializer(typeof(List<User>));
                    users = (List<User>)ser.ReadObject(ms);
                }
        }
        
        var res = new List<int>();
        
        if(users != null){
                        
            string opType = inputList[1];
            string val = inputList[2];
            string[] props = inputList[0].Split('.');
            
            res = users.Where(x => {
                Type type = x.GetType();
                
                string propValue = string.Empty;
                
                if(props.Length == 1){
                    propValue = type.GetProperty(props[0]).GetValue(x).ToString();
                                        
                } else{                    
                    object anObject = type.GetProperty(props[0]).GetValue(x);
                    
                    Type typeOfObject = anObject.GetType();
                    
                    propValue = typeOfObject.GetProperty(props[1]).GetValue(anObject).ToString();
                }
                
                if(opType == "EQUALS"){
                    if(propValue.Equals(val, StringComparison.Ordinal)){
                        return true;
                        }
                    }
                    else if(opType == "IN"){                            
                        string[] vals = val.Split(',');
                        
                        return vals.Any(v => propValue.Equals(v, StringComparison.Ordinal));
                    }
                
                return false;
                
            }).Select(y => Convert.ToInt32(y.id)).ToList();
        }
        
        if(res.Count == 0){
            res.Add(-1);
        }
        return res;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int inputListCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<string> inputList = new List<string>();

        for (int i = 0; i < inputListCount; i++)
        {
            string inputListItem = Console.ReadLine();
            inputList.Add(inputListItem);
        }

        int size = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> result = Result.apiResponseParser(inputList, size);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
