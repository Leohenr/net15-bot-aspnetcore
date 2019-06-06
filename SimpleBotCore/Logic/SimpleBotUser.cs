using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBotCore.Logic
{
    public class SimpleBotUser
    {
        public string Reply(SimpleMessage message)
        {
            SalvarHistorico(message);
            var contador = ContadorMensagens(message);
            return $"{message.User} disse '{message.Text}'. Falou '{contador}' mensagens";
        }
        public IMongoCollection<SimpleMessage> Connection(string database, string collection)
        {
            var cliente = new MongoClient();
            var db = cliente.GetDatabase(database);
            var col = db.GetCollection<SimpleMessage>(collection);
            
            return col;
        }
        public void SalvarHistorico(SimpleMessage message)
        {
            var col = Connection("15net", "historico");
            col.InsertOne(message);
        }

        public int ContadorMensagens(SimpleMessage message)
        {
            var col = Connection("15net", "historico");
            var resultado = col.Find(x => x.Id == message.Id).ToList();
            var contador = 0;

            foreach (var item in resultado)
            {
                contador++;
            }

            return contador;
        }
    }
}