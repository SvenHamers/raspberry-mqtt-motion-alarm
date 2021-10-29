using System;
using MQTTnet;
using MQTTnet.Client.Options;
using MQTTnet.Extensions.ManagedClient;

namespace raspberry_mqqt_motion_alarm.Factories
{
    public abstract class MqqtFactory
    {
        public IManagedMqttClient Client { get; }

        public MqqtFactory(string host, int port, string userName, string password)
        {
            var options = new ManagedMqttClientOptionsBuilder()
                .WithAutoReconnectDelay(TimeSpan.FromSeconds(5))
                .WithClientOptions(new MqttClientOptionsBuilder()
                    .WithCredentials(userName, password)
                .WithTcpServer(host, port).Build())
                .Build();

            this.Client = new MqttFactory().CreateManagedMqttClient();
            this.Client.StartAsync(options).Wait();
        }


        public static MqttApplicationMessage MessageBuilder(string topic, string payload)
        {
            return new MqttApplicationMessageBuilder()
                        .WithTopic(topic)
                        .WithPayload(payload)
                        .WithExactlyOnceQoS()
                        .WithRetainFlag()
                        .Build();
        }
    }
}
