using Amazon.Runtime;
using Amazon.SQS;
using Amazon;
using Amazon.SQS.Model;
using TimelineService.Model;

namespace TimelineService.Processor
{
    public class TimelineFollowersProcessor : BackgroundService
    {

        private IConfiguration configuration;
        public TimelineFollowersProcessor(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var sqsFollowerQueue = Environment.GetEnvironmentVariable("AWS_FOLLOWER_SQS_QUEUE");
            var secretKey = Environment.GetEnvironmentVariable("AWS_SECRET_ACCESS_KEY");
            var accessKey = Environment.GetEnvironmentVariable("AWS_ACCESS_KEY_ID");
            var credentials = new BasicAWSCredentials(accessKey, secretKey);
            Console.WriteLine("Starting background process");
            var client = new AmazonSQSClient(credentials, RegionEndpoint.EUCentral1);

            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Getting messages from the queue {DateTime.Now}");
                var request = new ReceiveMessageRequest()
                {
                    QueueUrl = sqsFollowerQueue,
                    WaitTimeSeconds = 15,
                    VisibilityTimeout = 20//for long polling

                };
                var response = await client.ReceiveMessageAsync(request);
                foreach (var message in response.Messages)
                {
                    Console.WriteLine(message.Body);
                    FollowerMessageProcessor processor = new FollowerMessageProcessor();
                    processor.addToDatabase(message);
                    if (message.Body.Contains("Exception")) continue; //send to dead letter queue if message contains exception
                                                                      //call createmethod and put message body inside 

                    await client.DeleteMessageAsync(sqsFollowerQueue, message.ReceiptHandle);
                }
            }
        }
    }
}