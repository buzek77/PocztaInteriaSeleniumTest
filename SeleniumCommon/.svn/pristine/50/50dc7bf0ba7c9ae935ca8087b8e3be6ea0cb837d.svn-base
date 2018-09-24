using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Vulcan.Common2015.SeleniumLib.Helpers
{
    public class VXml
    {
        /// <summary>
        /// Wczytuje dokument XMLowy z pliku.
        /// </summary>
        /// <param name="fileName">Ścieżka do pliku.</param>
        /// <returns></returns>
        /// <exception cref="System.Xml.XmlException">Rzucany, gdy wczytywany dokument nie jest poprawnym dokumentem XMLowym.</exception>
        public static XmlDocument Load(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(fileName);
            }
            catch (XmlException ex)
            {
                throw new XmlException(string.Format("Plik {0} nie jest poprawnym dokumentem XML.", fileName), ex);
            }

            return doc;
        }

        /// <summary>
        /// Zwraca wartość atrybutu, gdy go nie ma to wartość domyślną.
        /// </summary>
        /// <param name="n">Węzeł.</param>
        /// <param name="attrName">Nazwa atrybutu.</param>
        /// <param name="def">Wartość domyślna.</param>
        /// <returns>Wartość atrybutu.</returns>
        public static string GetAttrVal(XmlNode n, string attrName, string def)
        {
            XmlAttribute a = n.Attributes[attrName];
            if (a == null)
                return def;

            return a.Value;
        }

        /// <summary>
        /// Zwraca wartość atrybutu, gdy go nie ma to wartość domyślną.
        /// </summary>
        /// <param name="n">Węzeł.</param>
        /// <param name="attrName">Nazwa atrybutu.</param>
        /// <param name="def">Wartość domyślna.</param>
        /// <returns>Wartość atrybutu.</returns>
        public static string GetAttrValEx(XmlNode n, string attrName, string def)
        {
            XmlAttribute a = n.Attributes[attrName];
            if (a == null)
                return def;

            return a.Value.Length > 0 ? a.Value : def;
        }

        /// <summary>
        /// Dołącza do węzła atrybut.
        /// </summary>
        /// <param name="n">Węzeł.</param>
        /// <param name="attrName">Nazwa atrybutu.</param>
        /// <param name="attrVal">Wartośc atrybutu.</param>
        /// <returns></returns>
        public static XmlAttribute AppendAttr(XmlNode n, string attrName, string attrVal)
        {
            XmlAttribute attr = n.OwnerDocument.CreateAttribute(attrName);
            attr.Value = attrVal;
            return n.Attributes.Append(attr);
        }

        /// todo: refactoring ta metoda jest mylna bo powinna dodawać prefix
        /// <summary>
        /// Dołącza do węzła atrybut o zadanym prefiksie.
        /// </summary>
        /// <param name="n">Węzeł.</param>
        /// <param name="attrName">Nazwa atrybutu.</param>
        /// <param name="attrVal">Wartośc atrybutu.</param>
        /// <returns></returns>
        public static XmlAttribute AppendAttr(XmlNode n, string attrName, string attrVal, string prefix, XmlNamespaceManager manager)
        {
            XmlAttribute attr = n.OwnerDocument.CreateAttribute(attrName, manager.LookupNamespace(prefix));
            attr.Value = attrVal;
            return n.Attributes.Append(attr);
        }

        /// <summary>
        /// Dołącza do węzła atrybut z wartością o zadanym prefiksie i przestrzeni nazw.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="attrName"></param>
        /// <param name="attrVal"></param>
        /// <param name="prefix"></param>
        /// <param name="nameSpace"></param>
        /// <returns></returns>
        public static XmlAttribute AppendAttr(XmlNode parent, string attrName, string attrVal, string prefix, string nameSpace)
        {
            XmlAttribute attr = parent.OwnerDocument.CreateAttribute(prefix, attrName, nameSpace);
            attr.Value = attrVal;
            return parent.Attributes.Append(attr);
        }

        public static XmlNode AppendNode(XmlNode parent, string nodeName, string prefix, XmlNamespaceManager manager)
        {
            XmlNode node = parent.OwnerDocument.CreateElement(nodeName, manager.LookupNamespace(prefix));
            return parent.AppendChild(node);
        }

        public static XmlNode InsertFirst(XmlNode parent, string nodeName, string prefix, XmlNamespaceManager manager)
        {
            XmlNode node = parent.OwnerDocument.CreateElement(nodeName, manager.LookupNamespace(prefix));
            XmlNode newNode = null;

            if (parent.HasChildNodes)
                newNode = parent.InsertBefore(node, parent.ChildNodes[0]);
            else
                newNode = parent.AppendChild(node);

            return newNode;
        }/*

		public static decimal GetDecimal(XmlNode node, string attrName, decimal def)
		{
			XmlAttribute attr = node.Attributes[attrName];
			if (attr == null)
				return def;

			return Conversion.Parse(attr.Value, def);
		}

		public static int GetInt(XmlNode node, string attrName, int def)
		{
			XmlAttribute attr = node.Attributes[attrName];
			if (attr == null)
				return def;

			return Conversion.Parse(attr.Value, def);
		}*/
    }
}
