using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RabbitMQ.Client;

namespace demo1Sender
{
    public partial class Form1 : Form
    {
        ConnectionFactory factory;
        IConnection connection;
        IModel model;
        

        public Form1()
        {
            InitializeComponent();
            factory = new ConnectionFactory() { HostName = "localhost" };
            connection = factory.CreateConnection();
            model = connection.CreateModel();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            model.ExchangeDeclare(exchange: "myexc", type: "fanout");
            label3.Text = "trying to send message";
           
           
            var body = Encoding.UTF8.GetBytes(textBox2.Text );

            model.BasicPublish(exchange: "myexc",
                                 routingKey: "",
                                 basicProperties: null,
                                 body: body);
            label3.Text = "message sending succesful !!!";

        }
    }
}
