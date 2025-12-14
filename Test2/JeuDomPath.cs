using System;
using System.Xml;

public class DataCombat
{
    public int CAperso;
    public int CAmonstre;
    public int viePerso;
    public int vieMonstre;
    public int degatsPerso;
    public int degatsMonstre;

    public DataCombat(DOM2Xpath app) {
        string u = "https://www.univ-grenoble-alpes.fr/l3miage2";
        string p = "x";

        this.CAperso = int.Parse(app.getXPath(p, u, "//x:CAperso")[0].InnerText);
        this.CAmonstre = int.Parse(app.getXPath(p, u, "//x:CAmonstre")[0].InnerText);
        this.viePerso = int.Parse(app.getXPath(p, u, "//x:viePerso")[0].InnerText);
        this.vieMonstre = int.Parse(app.getXPath(p, u, "//x:vieMonstre")[0].InnerText);
        this.degatsPerso = int.Parse(app.getXPath(p, u, "//x:degatsPerso")[0].InnerText);
        this.degatsMonstre = int.Parse(app.getXPath(p, u, "//x:degatsMonstre")[0].InnerText);
    }
    public class DOM2Xpath
    {
        XmlDocument doc = new XmlDocument();

        public DOM2Xpath(string f)
        {
            doc.Load(f);
        }

        public XmlNodeList getXPath(string p, string u, string e)
        {
            XmlNamespaceManager m = new XmlNamespaceManager(doc.NameTable);
            m.AddNamespace(p, u);
            return doc.DocumentElement.SelectNodes(e, m);
        }

        public void save(string f)
        {
            doc.Save(f);
        }
    }
/*
    class Program
    {
        static void Main()
        {
            DataCombat data = new DataCombat();
            DOM2Xpath app = new DOM2Xpath("DataCombat.xml");

            VersCSharp(app, data);

            data.viePerso += 10;

            VersXml(app, data);

            app.save("DataCombat.xml");
        }

DOM */

    public void VersCSharpConstruct(DOM2Xpath app)
    {
        string u = "https://www.univ-grenoble-alpes.fr/l3miage2";
        string p = "x";

        this.CAperso = int.Parse(app.getXPath(p, u, "//x:CAperso")[0].InnerText);
        this.CAmonstre = int.Parse(app.getXPath(p, u, "//x:CAmonstre")[0].InnerText);
        this.viePerso = int.Parse(app.getXPath(p, u, "//x:viePerso")[0].InnerText);
        this.vieMonstre = int.Parse(app.getXPath(p, u, "//x:vieMonstre")[0].InnerText);
        this.degatsPerso = int.Parse(app.getXPath(p, u, "//x:degatsPerso")[0].InnerText);
        this.degatsMonstre = int.Parse(app.getXPath(p, u, "//x:degatsMonstre")[0].InnerText);
    }
        public static void VersCSharp(DOM2Xpath app, DataCombat d)
        {
            string u = "https://www.univ-grenoble-alpes.fr/l3miage2";
            string p = "x";

            d.CAperso = int.Parse(app.getXPath(p, u, "//x:CAperso")[0].InnerText);
            d.CAmonstre = int.Parse(app.getXPath(p, u, "//x:CAmonstre")[0].InnerText);
            d.viePerso = int.Parse(app.getXPath(p, u, "//x:viePerso")[0].InnerText);
            d.vieMonstre = int.Parse(app.getXPath(p, u, "//x:vieMonstre")[0].InnerText);
            d.degatsPerso = int.Parse(app.getXPath(p, u, "//x:degatsPerso")[0].InnerText);
            d.degatsMonstre = int.Parse(app.getXPath(p, u, "//x:degatsMonstre")[0].InnerText);
        }

        public static void VersXml(DOM2Xpath app, DataCombat d)
        {
            string u = "https://www.univ-grenoble-alpes.fr/l3miage2";
            string p = "x";

            app.getXPath(p, u, "//x:CAperso")[0].InnerText = d.CAperso.ToString();
            app.getXPath(p, u, "//x:CAmonstre")[0].InnerText = d.CAmonstre.ToString();
            app.getXPath(p, u, "//x:viePerso")[0].InnerText = d.viePerso.ToString();
            app.getXPath(p, u, "//x:vieMonstre")[0].InnerText = d.vieMonstre.ToString();
            app.getXPath(p, u, "//x:degatsPerso")[0].InnerText = d.degatsPerso.ToString();
            app.getXPath(p, u, "//x:degatsMonstre")[0].InnerText = d.degatsMonstre.ToString();
        }
}

