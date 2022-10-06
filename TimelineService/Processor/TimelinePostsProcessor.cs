﻿using Amazon.Runtime;
using Amazon.SQS;
using Amazon;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using TimelineService.Model;

namespace TimelineService.Processor
{
    public class TimelinePostsProcessor : BackgroundService
    {

        private IConfiguration configuration;

        public TimelinePostsProcessor(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var appconfig = configuration.GetSection("AppConfig").Get<AppConfig>();
            Console.WriteLine("Starting background processor");
            var credentials = new BasicAWSCredentials(appconfig.AccessKeyId, appconfig.SecretAccessKey);
            var client = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Getting messages from the queue {DateTime.Now}");
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = appconfig.TimelinePostsQueueUrl,
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