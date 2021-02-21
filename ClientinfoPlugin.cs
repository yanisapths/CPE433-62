using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;


namespace DNWS
{
  class ClientinfoPlugin : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public ClientinfoPlugin()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      String[] client = Regex.Split(request.getPropertyByKey("RemoteEndPoint"),":");
      sb.Append("<html><body>");
      sb.Append("Client IP: "+client[0].ToString() + "<br /><br />");
      sb.Append("Client Port: "+client[1].ToString() + "<br /><br />");
      sb.Append("Browser Information: "+request.getPropertyByKey("user-agent").ToString()+"<br /><br />");
      sb.Append("Acept Language: "+request.getPropertyByKey("accept-language").ToString()+"<br /><br />");
      sb.Append("Acept Encoding: "+request.getPropertyByKey("accept-encoding").ToString()+"<br /><br />");
      // sb.Append("<html><body><h1>Stat:</h1>");
      // foreach (KeyValuePair<String, int> entry in statDictionary)
      // {
      //   sb.Append(entry.Key + ": " + entry.Value.ToString() + "<br />");
      // } 
      //
      response = new HTTPResponse(200);
       sb.Append("</body></html>");
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}