using Amazon.SQS.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using TimelineService.Interfaces;
using TimelineService.Model;
using TimelineService.Repositories;

namespace TimelineService.Processor
{
    public class FollowerMessageProcessor
    {

        public void ProcessMessage(Message message)
        {
            if (message.Body.Contains("add"))
            {
                var originalMessage = message.Body.Substring(0, message.Body.Length - 3);
                message.Body = originalMessage;
                addToDatabase(message);
            }
            else if (message.Body.Contains("remove"))
            {
                var originalMessage = message.Body.Substring(0, message.Body.Length - 6);
                message.Body = originalMessage;
                removeFromDatabase(message);
            }
            else
            {
                Console.WriteLine("Message type not recognized");
            }
        }


        public async void addToDatabase(Message message)
        {
            JObject request = JObject.Parse(message.Body);
            var userid = request["UserId"];
            var followerid = request["FollowerId"];
            var _user = new Guid((string)userid);
            var _follower = new Guid((string)followerid);

            FollowerInputModel model = new FollowerInputModel()
            {
                UserId = _user,
                FollowerId = _follower
            };
            FollowersRepository _repository = new FollowersRepository();
            await _repository.Add(model);
        }
        public async void removeFromDatabase(Message message)
        {
            JObject request = JObject.Parse(message.Body);
            var userid = request["UserId"];
            var followerid = request["FollowerId"];
            var _user = new Guid((string)userid);
            var _follower = new Guid((string)followerid);
            FollowerInputModel model = new FollowerInputModel()
            {
                UserId = _user,
                FollowerId = _follower
            };
            FollowersRepository _repository = new FollowersRepository();
            await _repository.Remove(model);
        }
    }
}
