using Amazon.Runtime;
using Amazon.SQS;
using Amazon;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using TimelineService.Model;

namespace TimelineService.SQSProcessor
{
    public class TimelineProcessor : BackgroundService
    {

        private IConfiguration configuration;

        public TimelineProcessor(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
          //  var appconfig = new AppConfig();
            //configuration.GetSection("QueueUrl").Bind(appconfig);

            var url = configuration["AppConfig:QueueUrl"];
            var accessKeyId = configuration["AppConfig:AccessKeyId"];
            var secretAccessKey = configuration["AppConfig:AccessSecreyKey"];
            Console.WriteLine("Starting background processor");
            var credentials = new BasicAWSCredentials(accessKeyId, secretAccessKey);
            var client = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Getting messages from the queue {DateTime.Now}");
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = url,
                    WaitTimeSeconds = 15,
                    VisibilityTimeout = 20//for long polling

                };
                var response = await client.ReceiveMessageAsync(request);
                foreach (var message in response.Messages)
                {
                    Console.WriteLine(message.Body);
                    if (message.Body.Contains("Exception")) continue; //send to dead letter queue if message contains exception
                    //call createmethod and put message body inside 

                    await client.DeleteMessageAsync("https://sqs.eu-central-1.amazonaws.com/075206908135/PostTimelineQueue", message.ReceiptHandle);
                }
            }
        }
    }
}