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
    }
}
