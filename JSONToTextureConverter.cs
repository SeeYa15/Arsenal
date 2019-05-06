using Newtonsoft.Json.Linq;
using System;
using System.IO;

namespace Arsenal
{
    class JSONToTextureConverter
    {
        public static CustomTexture2D FetchJSONInformation(Uri path, string texturename)
        {
            //Fetches a JSON object that holds all info regarding what png to use and the cordinates on every single frame.
            JObject file = JObject.Parse(File.ReadAllText(path + texturename + ".JSON"));
            return file.ToObject<CustomTexture2D>();
        }
    }
}
