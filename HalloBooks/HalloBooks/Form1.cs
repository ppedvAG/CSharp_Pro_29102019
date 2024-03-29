﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace HalloBooks
{
    public partial class Form1 : Form
    {

        public event Action<int, string, DateTime> TripleClick;

        int clickCount = 0;

        public Form1()
        {
            InitializeComponent();

            this.MouseClick += (s, m) =>
            {
                clickCount++;
                if (clickCount % 3 == 0)
                {
                    TripleClick(clickCount, "Toll", DateTime.Now.AddDays(-1));
                }
            };

            TripleClick += Form1_TripleClick;
        }

        private void Form1_TripleClick(int arg1, string arg2, DateTime arg3)
        {
            MessageBox.Show("TripleClick");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string url = "https://www.googleapis.com/books/v1/volumes?q=net";
            using (var web = new WebClient())
            {
                web.Encoding = Encoding.UTF8;
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
                        select new { Title = b.title, Pages = b.pageCount };

            dataGridView1.DataSource = query.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                var volInfos = (List<Volumeinfo>)dataGridView1.DataSource;

                //lambda linq query
                dataGridView1.DataSource = volInfos.Where(b => b.pageCount > 10)
                                                   .OrderBy(x => x.language)
                                                   .ThenBy(x => x.pageCount)
                                                   .ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Alles doof: {ex.Message}");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var volInfos = (List<Volumeinfo>)dataGridView1.DataSource;

            var result = volInfos.Sum(b => b.pageCount);

            MessageBox.Show($"Summe aller seiten: {result}");
            MessageBox.Show($"Durchschnitt: {volInfos.Average(x => x.pageCount)}");

            string txt1 = "Hallo" + 5.ToString() + "Welt";
            string txt2 = string.Format("Hallo {0} es ist {1:hh} Uhr", "Welt", DateTime.Now);
            //string interpoliation
            string txt3 = $"Hallo Welt {Environment.MachineName} es ist {DateTime.Now:HH} Uhr";
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var volInfos = (List<Volumeinfo>)dataGridView1.DataSource;

            listBox1.DataSource = volInfos.SelectMany(x => x.authors).Distinct().ToList();
            //listBox1.DataSource = volInfos.Select(x => x.publisher).ToList();

            MessageBox.Show(string.Join("\n", volInfos.Where(x => x.authors.Count() > 0 && x.description.Length > 1000)
                                                      .SelectMany(x => x.authors).OrderBy(x => x).Distinct()));


        }
    }

}
