using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using DO;

namespace Dal
{
    static internal class XmlTools
    {
        #region load to file
        public static XElement LoadListFromXMLElement(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    return XElement.Load(filePath);
                }
                else
                {
                    XElement rootElem = new XElement(filePath);
                    if (filePath == @"config.xml")
                        rootElem.Add(new XElement("customerRunningNum", 1));
                    rootElem.Save(filePath);
                    return rootElem;
                }
            }
            catch (Exception ex)
            {
                throw new LoadingException(filePath, $"fail to load xml file: {filePath}", ex);
            }
        }
        #endregion
        #region save to file
        public static void SaveListToXMLElement(XElement rootElem, string filePath)
        {
            try
            {
                rootElem.Save(filePath);
            }
            catch (Exception ex)
            {
                throw new LoadingException(filePath, $"fail to create xml file: {filePath}", ex);
            }
        }

        #endregion
    }


}
