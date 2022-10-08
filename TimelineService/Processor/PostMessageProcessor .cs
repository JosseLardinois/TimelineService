using Amazon.SQS.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PostService.Models;
using PostService.Repositories;
using System.Reflection;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using TimelineService.Interfaces;
using TimelineService.Model;
using TimelineService.Repositories;

namespace TimelineService.Processor
{
    public class PostMessageProcessor
    {
        public async void addToDatabase(Message message)
        {
            JObject request = JObject.Parse(message.Body);
            var userid = request["UserId"];
            var creationTime = request["CreationTime"];
            var text = request["Text"];


            string _text = text.ToString();
            var _user = new Guid((string)userid);
            DateTime _creationTime = DateTime.Parse(creationTime.ToString());

            PostInputModel model = new PostInputModel()
            {
                UserId = _user,
                CreationTime = _creationTime,
                Text = _text

            };
            PostsRepository _repository = new PostsRepository();
            await _repository.Add(model);
        }
    }
}
