using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HalloBooks
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=csharp";
            using (var web = new WebClient())
            {
                var json = web.DownloadString(url);

                var books = JsonConvert.DeserializeObject<Rootobject>(json);

                var volInfos = books.items.Select(x => x.volumeInfo).ToList();

                dataGridView1.DataSource = volInfos;

            } //web.Dispose();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (var sw = new StreamWriter("books.xml"))
            {
                var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                serial.Serialize(sw, dataGridView1.DataSource);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (var sr = new StreamReader("books.xml"))
            {
                var serial = new XmlSerializer(typeof(List<Volumeinfo>));
                dataGridView1.DataSource = serial.Deserialize(sr);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var volInfos = (List<Volumeinfo>)dataGridView1.DataSource;

            //linq query expression
            var query = from b in volInfos
                        where b.pageCount > 10
                        orderby b.language, b.pageCount
                        select b;

            dataGridView1.DataSource = query.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var volInfos = (List<Volumeinfo>)dataGridView1.DataSource;

            //lambda linq query
            dataGridView1.DataSource = volInfos.Where(b => b.pageCount > 10)
                                               .OrderBy(x => x.language)
                                               .ThenBy(x => x.pageCount)
                                               .ToList();
        }
    }

}
