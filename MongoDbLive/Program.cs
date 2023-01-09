using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;

var hostname = "localhost";
var port = 27017;
var databaseName = "liveDemo";
var connectionString = $"mongodb://{hostname}:{port}";

var client = new MongoClient(connectionString);
var database = client.GetDatabase(databaseName);

var collection = database.GetCollection<Person>(
    "people",
        new MongoCollectionSettings()
        {
            AssignIdOnInsert = true
        }
    );

var person = collection
    .Find(_=>true)
    .ToList()
    .FirstOrDefault(p=>p.FirstName == "Niklas");

Console.WriteLine(person);

var filter = Builders<Person>.Filter.Eq("FirstName", "Niklas");
var update = Builders<Person>.Update.Set("Age", 36);

collection.FindOneAndUpdate(filter, update);

person = collection
    .Find(_ => true)
    .ToList()
    .FirstOrDefault(p => p.FirstName == "Niklas");

Console.WriteLine(person);

record Person
{
    [BsonId]
    public ObjectId Id { get; set; }

    [BsonElement]
    public string FirstName { get; set; } = string.Empty;

    [BsonElement]
    public string LastName { get; set; } = string.Empty;

    [BsonElement]
    public int Age { get; set; }
}