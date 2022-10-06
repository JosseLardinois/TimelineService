using Amazon.SQS.Model;

namespace TimelineService.Processor
{
    public class MessageProcessor
    {
        public Message addToMessageAndDisplay(Message message)
        {
            string addtostring = "added to body";//store to redis

            message.Body += addtostring;
            Console.WriteLine(message.Body);
            return message;
        }
    }
}
