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
            label3.Text = "trying to send message";
            var myprop = model.CreateBasicProperties();
            myprop.Persistent = true;
           model.QueueDeclare(queue: textBox1.Text ,
                                 durable: true, /* messages will not survive broker restart  */
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(textBox2.Text );

            model.BasicPublish(exchange: "",
                                 routingKey: textBox1.Text,
                                 basicProperties: myprop,
                                 body: body);
            label3.Text = "message sending succesful !!!";

        }
    }
}
