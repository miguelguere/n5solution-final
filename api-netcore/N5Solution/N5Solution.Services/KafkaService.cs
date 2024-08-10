using Confluent.Kafka;
using Confluent.SchemaRegistry;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Text.Json;
using System.Threading.Tasks;

namespace N5Solution.Services
{
    public class KafkaService
    {
        private readonly string _bootstrapServers;
        private readonly ProducerConfig _producerConfig;
        
        private string _topicDefault;
        public string TopicDefault { 
            
            get { return _topicDefault; } 
            
            set { _topicDefault = value;} 
        }

        public KafkaService(ProducerConfig producerConfig)
        {           
            _producerConfig = producerConfig;            
        }

        public async Task<bool> SendMessageAsync(string message)
        {

            //var schemaRegistry = new CachedSchemaRegistryClient(new SchemaRegistryConfig { Url = "http://localhost:8081" });            

            var producer = new ProducerBuilder<string, string>(_producerConfig)               
                .Build();
            var topic = new TopicPartition(_topicDefault, (int)Partitioner.Random);

            var result = await producer.ProduceAsync(topic, new Message<string, string> { Key = Guid.NewGuid().ToString(), Value= message });

            if (result.Status != PersistenceStatus.NotPersisted)
            {
                return true;
            }

            return false ;
        }
    }
}
